using System.Collections.Immutable;
using D2L.CodeStyle.Analyzers.Extensions;
using D2L.CodeStyle.Analyzers.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace D2L.CodeStyle.Analyzers.ApiUsage.ServiceLocator {

	[DiagnosticAnalyzer( LanguageNames.CSharp )]
	public sealed class OldAndBrokenServiceLocatorAnalyzer : DiagnosticAnalyzer {

		private static readonly ImmutableArray<string> DisallowedTypeMetadataNames = ImmutableArray.Create(
				"D2L.LP.Extensibility.Activation.Domain.OldAndBrokenServiceLocator",
				"D2L.LP.Extensibility.Activation.Domain.OldAndBrokenServiceLocatorFactory",
				"D2L.LP.Extensibility.Activation.Domain.IObjectActivator",
				"D2L.LP.Extensibility.Activation.Domain.ICustomObjectActivator",
				"D2L.LP.Extensibility.Activation.Domain.ObjectActivatorExtensions",
				"D2L.LP.Extensibility.Activation.Domain.Default.StaticDI.StaticDILocator"
			);

		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(
			Diagnostics.OldAndBrokenLocatorIsObsolete,
			Diagnostics.UnnecessaryAllowedListEntry
		);

		private readonly bool m_excludeKnownProblems;

		public OldAndBrokenServiceLocatorAnalyzer() : this( excludeKnownProblemDlls: true ) { }

		public OldAndBrokenServiceLocatorAnalyzer( bool excludeKnownProblemDlls ) {
			m_excludeKnownProblems = excludeKnownProblemDlls;
		}

		public override void Initialize( AnalysisContext context ) {
			context.EnableConcurrentExecution();
			context.ConfigureGeneratedCodeAnalysis( GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics );
			context.RegisterCompilationStartAction( RegisterServiceLocatorAnalyzer );
		}

		public void RegisterServiceLocatorAnalyzer( CompilationStartAnalysisContext context ) {

			ImmutableHashSet<ITypeSymbol> disallowedTypes = GetDisallowedTypes( context.Compilation );
			if( disallowedTypes.IsEmpty ) {
				return;
			}

			AllowedTypeList allowedTypeList = AllowedTypeList.CreateFromAnalyzerOptions(
				allowedListFileName: "OldAndBrokenServiceLocatorAllowedList.txt",
				analyzerOptions: context.Options
			);

			TypeRuleSets typeRules = new( allowedTypeList, disallowedTypes );

			context.RegisterOperationAction(
				context => {
					IInvocationOperation invocation = (IInvocationOperation)context.Operation;
					AnalyzeMemberUsage( context, invocation.TargetMethod, typeRules );
				},
				OperationKind.Invocation
			);

			context.RegisterOperationAction(
				context => {
					IMethodReferenceOperation reference = (IMethodReferenceOperation)context.Operation;
					AnalyzeMemberUsage( context, reference.Method, typeRules );
				},
				OperationKind.MethodReference
			);

			context.RegisterOperationAction(
				context => {
					IPropertyReferenceOperation reference = (IPropertyReferenceOperation)context.Operation;
					AnalyzeMemberUsage( context, reference.Property, typeRules );
				},
				OperationKind.PropertyReference
			);

			context.RegisterSymbolAction(
				context => {
					IFieldSymbol field = (IFieldSymbol)context.Symbol;
					AnalyzeTypeUsage( context, field.Type, typeRules );
				},
				SymbolKind.Field
			);

			context.RegisterSymbolAction(
				context => {
					IParameterSymbol parameter = (IParameterSymbol)context.Symbol;
					AnalyzeTypeUsage( context, parameter.Type, typeRules );
				},
				SymbolKind.Parameter
			);

			context.RegisterSymbolAction(
				context => {
					IPropertySymbol property = (IPropertySymbol)context.Symbol;
					AnalyzeTypeUsage( context, property.Type, typeRules );
				},
				SymbolKind.Property
			);

			context.RegisterSymbolAction(
				ctx => PreventUnnecessaryAllowedListing( ctx, typeRules ),
				SymbolKind.NamedType
			);
		}

		private void AnalyzeMemberUsage(
			OperationAnalysisContext context,
			ISymbol member,
			TypeRuleSets typeRules
		) {

			if( !typeRules.Disallowed.Contains( member.ContainingType ) ) {
				return;
			}

			if( HasExemption( context.ContainingSymbol, typeRules ) ) {
				return;
			}

			Diagnostic diagnostic = Diagnostic.Create(
				Diagnostics.OldAndBrokenLocatorIsObsolete,
				context.Operation.Syntax.GetLocation()
			);

			context.ReportDiagnostic( diagnostic );
		}

		private void AnalyzeTypeUsage(
			SymbolAnalysisContext context,
			ITypeSymbol type,
			TypeRuleSets typeRules
		) {

			if( !typeRules.Disallowed.Contains( type ) ) {
				return;
			}

			if( HasExemption( context.Symbol, typeRules ) ) {
				return;
			}

			Diagnostic diagnostic = Diagnostic.Create(
				Diagnostics.OldAndBrokenLocatorIsObsolete,
				context.Symbol.Locations[ 0 ]
			);

			context.ReportDiagnostic( diagnostic );
		}

		private bool HasExemption(
				ISymbol symbol,
				TypeRuleSets typeRules
			) {

			ImmutableArray<INamedTypeSymbol> containingTypes = symbol.GetAllContainingTypes();

			// Allow the DI framework to call the disallowed types
			if( containingTypes.Any( Attributes.DIFramework.IsDefined ) ) {
				return true;
			}

			if( m_excludeKnownProblems ) {

				// Allow the types listed in OldAndBrokenServiceLocatorAllowedList.txt
				if( containingTypes.Any( typeRules.Allowed.Contains ) ) {
					return true;
				}
			}

			return false;
		}

		private void PreventUnnecessaryAllowedListing(
			SymbolAnalysisContext context,
			TypeRuleSets typeRules
		) {
			if( !( context.Symbol is INamedTypeSymbol namedType ) ) {
				return;
			}

			if( !typeRules.Allowed.Contains( namedType ) ) {
				return;
			}

			Location? diagnosticLocation = null;
			foreach( var syntaxRef in namedType.DeclaringSyntaxReferences ) {

				TypeDeclarationSyntax? typeSyntax = syntaxRef.GetSyntax( context.CancellationToken ) as TypeDeclarationSyntax;
				if( typeSyntax == null ) {
					throw new InvalidOperationException( "Existing implementation assumed not null" );
				}

				diagnosticLocation = diagnosticLocation ?? typeSyntax.Identifier.GetLocation();

				SemanticModel model = context.Compilation.GetSemanticModel( typeSyntax.SyntaxTree );

				bool usesDisallowedTypes = typeSyntax
					.DescendantNodes()
					.OfType<IdentifierNameSyntax>()
					.Any( syntax => IdentifierIsOfDisallowedType( model, typeRules.Disallowed, syntax, context.CancellationToken ) );

				if( usesDisallowedTypes ) {
					return;
				}
			}

			if( diagnosticLocation != null ) {
				typeRules.Allowed.ReportEntryAsUnnecesary(
					entry: namedType,
					location: diagnosticLocation,
					report: context.ReportDiagnostic
				);
			}
		}

		private static bool IdentifierIsOfDisallowedType(
			SemanticModel model,
			ImmutableHashSet<ITypeSymbol> disallowedTypes,
			IdentifierNameSyntax syntax,
			CancellationToken cancellationToken
		) {

			ITypeSymbol? actualType = model.GetTypeInfo( syntax, cancellationToken ).Type;
			if( actualType.IsNullOrErrorType() ) {
				return false;
			}

			if( !disallowedTypes.Contains( actualType ) ) {
				return false;
			}

			return true;
		}

		private ImmutableHashSet<ITypeSymbol> GetDisallowedTypes( Compilation compilation ) {

			var builder = ImmutableHashSet.CreateBuilder<ITypeSymbol>( SymbolEqualityComparer.Default );

			foreach( string metadataName in DisallowedTypeMetadataNames ) {

				INamedTypeSymbol? factoryType = compilation.GetTypeByMetadataName( metadataName );
				if( !factoryType.IsNullOrErrorType() ) {
					builder.Add( factoryType );
				}
			}

			return builder.ToImmutable();
		}

		private readonly record struct TypeRuleSets(
			AllowedTypeList Allowed,
			ImmutableHashSet<ITypeSymbol> Disallowed
		);
	}
}
