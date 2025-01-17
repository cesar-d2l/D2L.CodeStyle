﻿using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace D2L.CodeStyle.Analyzers.Async.Generator;

[Generator]
internal sealed partial class SyncGenerator : IIncrementalGenerator {
	public void Initialize( IncrementalGeneratorInitializationContext context ) {
		// Collect all the methods we want to generate sync versions for
		// individually (for better incremental builds)
		IncrementalValuesProvider<(MethodDeclarationSyntax, Compilation)> methodsToGenerate =
			context.SyntaxProvider
			   .CreateSyntaxProvider( predicate: IsInterestingLookingMethod, transform: ExtractSyntax )
			   .Where( static m => m != null )
			   .Combine( context.CompilationProvider )
			   .WithComparerThatIgnoresCompilation()!;

		// Do the generation per method
		IncrementalValuesProvider<MethodGenerationResult> generatedMethods =
			methodsToGenerate
				.Select( GenerateSyncMethod )
				// Filter out things we simply couldn't generate anything for.
				.Where( static mr => mr.HasValue )
				.Select( static ( mr, _ ) => mr!.Value );

		// Collect and generate the output files
		//
		// Opportunity for better caching here -- changing one source file
		// should only impact one output file. Currently we only generate the
		// methods when their source file changed, but we re-collect all of
		// that when any file with [GenerateSync] has changed.
		IncrementalValuesProvider<FileGenerationResult> generatedFiles =
			generatedMethods.Collect()
				.SelectMany( GenerateFiles );

		context.RegisterSourceOutput( generatedFiles, WriteFiles );
	}

	private static bool IsInterestingLookingMethod(
		SyntaxNode node,
		CancellationToken _
	) {
		if( node is not MethodDeclarationSyntax method ) {
			return false;
		}

		// Must have Async suffix.
		// TODO: add an analyzer to alert about uses of [GenerateSync] that this would filter out
		if( !method.Identifier.ValueText.EndsWith( "Async", StringComparison.Ordinal ) ) {
			return false;
		}

		// Require the async keyword if applicable.
		// Interface and abstract methods won't have bodies and thus won't have the
		// keyword but we still want to translate their signatures.
		// TODO: we could probably relax this, or we should have an error.
		if( method.Body is not null && !method.Modifiers.Any( t => t.IsKind( SyntaxKind.AsyncKeyword ) ) ) {
			return false;
		}

		// The ones we care about have to have a particular attribute -- so
		// perk up when we see _any_ attributes (we don't have a SemanticModel
		// in this method.)
		return method.AttributeLists.Any();
	}

	private static MethodDeclarationSyntax? ExtractSyntax(
		GeneratorSyntaxContext context,
		CancellationToken cancellationToken
	) {
		var methodDeclaration = (MethodDeclarationSyntax)context.Node;

		var attributes = methodDeclaration.AttributeLists
			.SelectMany( al => al.Attributes );

		foreach( var attribute in attributes ) {
			// We have the semantic model now, so make sure this method has
			// the right attribute.
			if( IsGenerateSyncAttribute( context.SemanticModel, attribute, cancellationToken ) ) {
				return methodDeclaration;
			}
		}

		return null;
	}

	internal static bool IsGenerateSyncAttribute(
		SemanticModel model,
		AttributeSyntax attribute,
		CancellationToken cancellationToken
	) {
		var attributeConstructorSymbol = model.GetSymbolInfo( attribute, cancellationToken ).Symbol as IMethodSymbol;

		if( attributeConstructorSymbol == null ) {
			return false;
		}

		return attributeConstructorSymbol.ContainingType.ToDisplayString()
			== "D2L.CodeStyle.Annotations.GenerateSyncAttribute";
	}

	private static MethodGenerationResult? GenerateSyncMethod(
		(MethodDeclarationSyntax MethodDeclaration, Compilation Compilation) data,
		CancellationToken cancellationToken
	) {
		if( !data.Compilation.ContainsSyntaxTree( data.MethodDeclaration.SyntaxTree ) ) {
			return null;
		}

		var model = data.Compilation.GetSemanticModel( data.MethodDeclaration.SyntaxTree );
		var methodSymbol = model.GetDeclaredSymbol( data.MethodDeclaration, cancellationToken );

		if( methodSymbol == null || methodSymbol.Kind == SymbolKind.ErrorType ) {
			return null;
		}

		var transformer = new AsyncToSyncMethodTransformer( model, cancellationToken );

		var transformResult = transformer.Transform( data.MethodDeclaration );

		return new(
			Original: data.MethodDeclaration,
			GeneratedSyntax: transformResult.Success ? transformResult.Value!.ToFullString() : "",
			Diagnostics: transformResult.Diagnostics
		);
	}

	private static IEnumerable<FileGenerationResult> GenerateFiles(
		ImmutableArray<MethodGenerationResult> methodResults,
		CancellationToken cancellationToken
	) {
		var methodsByFile = methodResults
			.GroupBy( static mr => mr.Original.SyntaxTree.GetCompilationUnitRoot() )
			.OrderBy( static mr => mr.First().Original.SyntaxTree.FilePath )
			.ToImmutableArray();

		var hintNames = SourceFilePathHintNameGenerator.Create(
			// We're assuming every root has a tree here -- when is that false?
			// Generated code?
			methodsByFile.Select( static grp => grp.Key.SyntaxTree.FilePath ).ToImmutableArray()
		).GetHintNames();

		var files = methodsByFile.Zip(
			hintNames,
			static ( fileData, name ) => ( HintName: name, Data: fileData )
		);

		foreach( var file in files ) {
			cancellationToken.ThrowIfCancellationRequested();

			var generatedMethods = ImmutableArray.CreateBuilder<(TypeDeclarationSyntax, string)>();
			var diagnostics = ImmutableArray.CreateBuilder<Diagnostic>();

			foreach( var method in file.Data ) {
				diagnostics.AddRange( method.Diagnostics );
				generatedMethods.Add( ((method.Original.Parent as TypeDeclarationSyntax)!, method.GeneratedSyntax ) );
			}

			var collector = FileCollector.Create(
				file.Data.Key,
				generatedMethods.ToImmutable()
			);

			var generatedFile = collector.CollectSource();

			yield return new(
				HintName: file.HintName,
				GeneratedSource: generatedFile,
				Diagnostics: diagnostics.ToImmutable()
			);
		}
	}

	private static void WriteFiles(
		SourceProductionContext context,
		FileGenerationResult result
	) {
		foreach( var diagnostic in result.Diagnostics ) {
			context.ReportDiagnostic( diagnostic );
		}

		context.AddSource(
			hintName: result.HintName,
			source: result.GeneratedSource
		);
	}
}
