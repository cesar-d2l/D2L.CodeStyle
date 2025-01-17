using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;

namespace D2L.CodeStyle.Analyzers {

	[SuppressMessage(
		category: "Naming",
		checkId: "CA1707",
		Justification = "Happy to have underscores here for ease of understanding."
	)]
	public static class Diagnostics {
		// Retired:
		// D2L0002 (UnsafeStatic): "Ensure that static field is safe in undifferentiated servers."
		// D2L0003 (ImmutableClassIsnt): "Classes marked as immutable should be immutable.",

		public static readonly DiagnosticDescriptor RpcContextFirstArgument = new DiagnosticDescriptor(
			id: "D2L0004",
			title: "RPCs must take an IRpcContext, IRpcPostContext or IRpcPostContextBase as their first argument",
			messageFormat: "RPCs must take an IRpcContext, IRpcPostContext or IRpcPostContextBase as their first argument",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "RPCs must take an IRpcContext, IRpcPostContext or IRpcPostContextBase as their first argument."
		);

		public static readonly DiagnosticDescriptor RpcArgumentSortOrder = new DiagnosticDescriptor(
			id: "D2L0005",
			title: "Dependency-injected arguments in RPC methods must preceed other parameters (other than the first context argument)",
			messageFormat: "Dependency-injected arguments in RPC methods must preceed other parameters (other than the first context argument)",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "Dependency-injected arguments in RPC methods must preceed other parameters (other than the first context argument)."
		);

		// Retired:
		// D2L0006 (UnsafeSingletonRegistration): "Ensure that a singleton is safe in undifferentiated servers"
		// D2L0007 (UnnecessaryStaticAnnotation): "Unnecessary static annotations should be removed to keep the code base clean",
		// D2L0008 (ConflictingStaticAnnotation): "Statics.Audited and Statics.Unaudited are mutually exclusive",

		public static readonly DiagnosticDescriptor OldAndBrokenLocatorIsObsolete = new DiagnosticDescriptor(
			id: "D2L0009",
			title: "OldAndBrokenServiceLocator should be avoided (use dependency injection instead)",
			messageFormat: "OldAndBrokenServiceLocator should be avoided.  Use dependency injection instead.",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "OldAndBrokenServiceLocator should be avoided.  Use dependency injection instead."
		);

		// Retired:
		// D2L0010 (NullPassedToNotNullParameter): "Parameter cannot be passed with a null value",
		// D2L0011 (SingletonRegistrationTypeUnknown) "Unable to resolve the concrete or plugin type for this registration"
		// D2L0012 (RegistrationKindUnknown): "Unable to determine the kind of dependency registration attempted"

		public static readonly DiagnosticDescriptor ClassShouldBeSealed = new DiagnosticDescriptor(
			id: "D2L0013",
			title: "Non-public class should be sealed because it doesn't have any subtypes",
			messageFormat: "Non-public class should be sealed because it doesn't have any subtypes",
			category: "Style",
			defaultSeverity: DiagnosticSeverity.Info,
			isEnabledByDefault: true,
			description: "Non-public class should be sealed because it doesn't have any subtypes."
		);

		// Retired:
		// D2L0014 (SingletonIsntImmutable): "Classes marked as a singleton should be immutable.",

		public static readonly DiagnosticDescriptor SingletonLocatorMisuse = new DiagnosticDescriptor(
			id: "D2L0017",
			title: "Can only use OldAndBrokenSingletonLocator to inject interfaces with the [Singleton] attribute",
			messageFormat: "Cannot use OldAndBrokenSingletonLocator to inject {0} because it lacks the [Singleton] attribute",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "Can only use OldAndBrokenSingletonLocator to inject interfaces with the [Singleton] attribute."
		);

		public static readonly DiagnosticDescriptor DangerousMethodsShouldBeAvoided = new DiagnosticDescriptor(
			id: "D2L0018",
			title: "Avoid using dangerous methods",
			messageFormat: "Should not use {0} because it's considered dangerous",
			category: "Safety",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "Avoid using of dangerous methods."
		);

		// Retired:
		// D2L0019 (AttributeRegistrationMismatch): "Singleton attribute cannot appear on non-singleton object scopes"

		public static readonly DiagnosticDescriptor InvalidLaunchDarklyFeatureDefinition = new DiagnosticDescriptor(
			id: "D2L0021",
			title: "Launch Darkly feature definitions are limited to support types",
			messageFormat: "Invalid feature flag value type: {0}",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "Must be one of [ bool, int, float, string ]."
		);

		public static readonly DiagnosticDescriptor ObsoleteJsonParamBinder = new DiagnosticDescriptor(
			id: "D2L0025",
			title: "Use the new JsonConvertParameterBinder instead",
			messageFormat: "Should not use JsonParamBinder because it is obsolete",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "JsonParamBinder uses the custom D2L JSON framework, so use JsonConvertParameterBinder (which uses Newtonsoft.Json) instead."
		);

		// Retired:
		// D2L0026 (ImmutableGenericAttributeInWrongAssembly): "Cannot apply ImmutableGeneric for the given type.",
		// D2L0027 (ImmutableGenericAttributeAppliedToNonGenericType): "Cannot apply ImmutableGeneric for a non-generic type.",
		// D2L0028 (ImmutableGenericAttributeAppliedToOpenGenericType): "Cannot apply ImmutableGeneric for an open generic type.",

		public static readonly DiagnosticDescriptor DontUseImmutableArrayConstructor = new DiagnosticDescriptor(
			id: "D2L0029",
			title: "Don't use the default constructor for ImmutableArray<T>",
			messageFormat: "The default constructor for ImmutableArray<T> doesn't correctly initialize the object and leads to runtime errors. Use ImmutableArray<T>.Empty for empty arrays, ImmutableArray.Create() for simple cases and ImmutableArray.Builder<T> for more complicated cases.",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "The default constructor for ImmutableArray<T> doesn't correctly initialize the object and leads to runtime errors. Use ImmutableArray<T>.Empty for empty arrays, ImmutableArray.Create() for simple cases and ImmutableArray.Builder<T> for more complicated cases."
		);

		public static readonly DiagnosticDescriptor UnnecessaryMutabilityAnnotation = new DiagnosticDescriptor(
			id: "D2L0030",
			title: "Unnecessary Mutability.(Un)Audited Attribute",
			messageFormat: "There is a Mutability.Audited or Mutability.Unaudited attribute on an immutable member. Remove the unnecessary attribute.",
			category: "Cleanliness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor TooManyUnnamedArgs = new DiagnosticDescriptor(
			id: "D2L0032",
			title: "Name arguments for readability",
			messageFormat: "There are more than {0} unnamed arguments. Use named arguments for readability.",
			category: "Readability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "When a lot of arguments are used, they should be named for readability."
		);

		public static readonly DiagnosticDescriptor LiteralArgShouldBeNamed = new DiagnosticDescriptor(
			id: "D2L0033",
			title: "Literal arguments should be named for readability",
			messageFormat: "The argument for the {0} parameter is a literal expression. It's often hard to tell what the parameter for the argument is at the call-site in this case. Please use a named argument for readability.",
			category: "Readability",
			defaultSeverity: DiagnosticSeverity.Info, // TODO: Change to Error once fixed in LMS
			isEnabledByDefault: true,
			description: "Literal arguments should be named for readability."
		);

		public static readonly DiagnosticDescriptor ArgumentsWithInterchangableTypesShouldBeNamed = new DiagnosticDescriptor(
			id: "D2L0034",
			title: "Arguments that map to parameters with interchangable types should be named",
			messageFormat: "The parameters {0} and {1} have interchangable types. There is a risk of not passing arguments to the right parameters. Please use named arguments for readability.",
			category: "Readability",
			defaultSeverity: DiagnosticSeverity.Info,
			isEnabledByDefault: true,
			description: "Arguments that map to parameters with interchangable types should be named."
		);

		// Retired:
		// D2L0035 (SingletonDependencyHasCustomerState): "Singleton holding a dependency containing customer state.",
		// D2L0036 (PublicClassHasHiddenCustomerState): "Missing CustomerState attribute."",
		// D2L0037 (GenericArgumentImmutableMustBeApplied): "Missing immutability attribute."

		// Retired:
		// D2L0038 (GenericArgumentTypeMustBeImmutable): "Declared type is not immutable.",

		public static readonly DiagnosticDescriptor DangerousPropertiesShouldBeAvoided = new DiagnosticDescriptor(
			id: "D2L0039",
			title: "Avoid using dangerous properties",
			messageFormat: "Should not use {0} because it's considered dangerous",
			category: "Safety",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "Avoid using of dangerous properties."
		);

		public static readonly DiagnosticDescriptor MissingTransitiveImmutableAttribute = new DiagnosticDescriptor(
			id: "D2L0040",
			title: "Missing an explicit transitive [Immutable] attribute",
			messageFormat: "{0} should be [Immutable]{1} because the {2} {3} is",
			category: "",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "The implications of [Immutable] apply transitively to derived classes and interface implementations. We require that [Immutable] is explicity applied transitively for clarity and simplicity."
		);

		public static readonly DiagnosticDescriptor EventHandlerDisallowed = new DiagnosticDescriptor(
			id: "D2L0041",
			title: "Disallowed Event Handler",
			messageFormat: "Event handlers of type {0} have been disallowed",
			category: "Safety",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "This event type no longer supports event handlers."
		);

		public static readonly DiagnosticDescriptor MustReferenceAnnotations = new DiagnosticDescriptor(
			id: "D2L0042",
			title: "To use D2L.CodeStyle.Analyzers you must also reference the assembly D2L.CodeStyle.Annotations",
			messageFormat: "To use D2L.CodeStyle.Analyzers you must also reference the assembly D2L.CodeStyle.Annotations",
			category: "Build",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "To use D2L.CodeStyle.Analyzers you must also reference the assembly D2L.CodeStyle.Annotations."
		);

		public static readonly DiagnosticDescriptor EventTypeMissingEventAttribute = new DiagnosticDescriptor(
			id: "D2L0043",
			title: "Event Type Missing [Event] Attribute",
			description: "All event types must be marked with [Event] attribute.",
			messageFormat: "Event type {0} must be marked with [Event] attribute",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor EventHandlerTypeMissingEventAttribute = new DiagnosticDescriptor(
			id: "D2L0044",
			title: "Event Handler Type Missing [EventHandler] Attribute",
			description: "All event handler types must be marked with [EventHandler] attribute.",
			messageFormat: "Event handler type {0} must be marked with [EventHandler] attribute",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor EventTypeMissingImmutableAttribute = new DiagnosticDescriptor(
			id: "D2L0045",
			title: "Event Type Missing [Immutable] Attribute",
			messageFormat: "{0} must be marked [Immutable] because all event types must be immutable",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "We require that [Immutable] be explicity applied to all event types."
		);

		// Retired:
		// D2L0046 (DependencyRegistraionMissingPublicConstructor): "Dependency Registration Missing Public Constructor"

		public static readonly DiagnosticDescriptor IncludeDefaultValueInOverrideForReadability = new DiagnosticDescriptor(
			id: "D2L0047",
			title: "The parameter {0} has a default value in {1}, but not here",
			messageFormat: "The parameter {0} has a default value in {1}, but not here. This causes inconsistent behaviour and reduces readability. Please repeat the default value here explicitly.",
			category: "Language",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "The parameter {0} has a default value in {1}, but not here. This causes inconsistent behaviour and reduces readability. Please repeat the default value here explicitly."
		);

		public static readonly DiagnosticDescriptor DontIntroduceNewDefaultValuesInOverrides = new DiagnosticDescriptor(
			id: "D2L0048",
			title: "The parameter {0} does not have a default value in the original version of this method in {1}, but does here",
			messageFormat: "The parameter {0} does not have a default value in the original version of this method in {1}, but does here. This causes inconsistent behaviour. Please remove the default (or add it everywhere).",
			category: "Language",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "The parameter {0} does not have a default value in the original version of this method in {1}, but does here. This causes inconsistent behaviour. Please remove the default (or add it everywhere)."
		);

		public static readonly DiagnosticDescriptor DefaultValuesInOverridesShouldBeConsistent = new DiagnosticDescriptor(
			id: "D2L0049",
			title: "The parameter {0} has a default value of {1} here, but {2} in its original definition in {3}",
			messageFormat: "The parameter {0} has a default value of {1} here, but {2} in its original definition in {3}. This causes inconsistent behaviour. Please use the same defualt value everywhere.",
			category: "Language",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "The parameter {0} has a default value of {1} here, but {2} in its original definition in {3}. This causes inconsistent behaviour. Please use the same defualt value everywhere."
		);

		public static readonly DiagnosticDescriptor LoggingContextRunAwaitable = new DiagnosticDescriptor(
			id: "D2L0050",
			title: "Use RunAsync for awaitable actions",
			messageFormat: "Use RunAsync for awaitable actions",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "Use RunAsync for awaitable actions."
		);

		public static readonly DiagnosticDescriptor BannedConfig = new DiagnosticDescriptor(
			id: "D2L0051",
			title: "Fetching the config variable \"{0}\" has been deprecated",
			messageFormat: "Fetching the config variable \"{0}\" has been deprecated. {1}.",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "Fetching the config variable \"{0}\" has been deprecated. {1}."
		);

		public static readonly DiagnosticDescriptor ContentPhysicalPathUsages = new DiagnosticDescriptor(
			id: "D2L0052",
			title: "Use the ContentPath property",
			messageFormat: "Use the ContentPath property instead",
			category: "Storageable",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "Use the ContentPath property instead."
		);

		public static readonly DiagnosticDescriptor StatelessFuncIsnt = new DiagnosticDescriptor(
			id: "D2L0053",
			title: "StatelessFunc cannot hold state",
			messageFormat: "StatelessFunc cannot hold state: {0}",
			category: "Safety",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "StatelessFunc is to be used to hold Func private members, and need to be undiff safe."
		);

		public static readonly DiagnosticDescriptor ReadOnlyParameterIsnt = new DiagnosticDescriptor(
			id: "D2L0054",
			title: "Parameter is not readonly",
			messageFormat: "Parameter is not readonly: {0}",
			category: "Safety",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "[ReadOnly] paramaters must not be assigned to or passed by non-readonly reference."
		);

		public static readonly DiagnosticDescriptor AwaitedTaskNotConfigured = new DiagnosticDescriptor(
			id: "D2L0055",
			title: "Awaited task is not configured",
			messageFormat: "Awaited task is not configured",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "Awaited task should have 'continueOnCapturedContext' configured (preferably with 'false')."
		);

		public static readonly DiagnosticDescriptor StructShouldBeReadonly = new DiagnosticDescriptor(
			id: "D2L0056",
			title: "Struct should be readonly",
			messageFormat: "Struct '{0}' should be marked as readonly",
			description: "A struct without mutable fields or properties should be marked readonly. A non-readonly struct other weird mutability behaviours and performance pitfalls.",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor UnnecessaryAllowedListEntry = new DiagnosticDescriptor(
			id: "D2L0057",
			title: "Unnecessarily listed in an analyzer allowed list",
			messageFormat: "The entry for '{0}' in '{1}' is unnecessary",
			category: "Cleanliness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "Unnecessarily listed in an analyzer allowed list."
		 );

		public static readonly DiagnosticDescriptor NamedArgumentsRequired = new DiagnosticDescriptor(
			id: "D2L0058",
			title: "Named arguments are required for readability",
			messageFormat: "Named arguments are required for this call. Use named arguments for readability.",
			category: "Readability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "This call requires arguments be named for readability."
		);

		public static readonly DiagnosticDescriptor InvalidSerializerType = new DiagnosticDescriptor(
			id: "D2L0059",
			title: "Serializers must implement D2L.LP.Serialization.ITrySerializer",
			messageFormat: "'{0}' does not implement D2L.LP.Serialization.ITrySerializer",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "This [Serializer] attribute requires the provided type to implement D2L.LP.Serialization.ITrySerializer."
		);

		public static readonly DiagnosticDescriptor EventTypeNotSealed = new DiagnosticDescriptor(
			id: "D2L0060",
			title: "Event Type Not Sealed",
			messageFormat: "{0} must be sealed because you can register event handlers based on inheritance",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "We require that event types be sealed."
		);

		public static readonly DiagnosticDescriptor MutableBaseType = new DiagnosticDescriptor(
			id: "D2L0061",
			title: "Base class for immutable type must be [Immutable] (or [ImmutableBaseClass])",
			messageFormat: "{0}'s base class is {1} which is missing [Immutable] (or, more weakly, [ImmutableBaseClass])",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor ArraysAreMutable = new DiagnosticDescriptor(
			id: "D2L0062",
			title: "Arrays are mutable and thus can't be held by an immutable type",
			messageFormat: "{0}[]'s are mutable even if {0}'s are immutable because array items can be substituted. Prefer an immutable container type such as ImmutableArray<{0}>.",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor DynamicObjectsAreMutable = new DiagnosticDescriptor(
			id: "D2L0063",
			title: "Dynamic objects are mutable and thus can't be held by an immutable type",
			messageFormat: "Dynamic objects are mutable and thus can't be held by an immutable type",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor TypeParameterIsNotKnownToBeImmutable = new DiagnosticDescriptor(
			id: "D2L0064",
			title: "Type parameter is not known to be immutable",
			messageFormat: "The type parameter {0} is not known to be immutable. Add [Immutable] to it.",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor UnexpectedTypeKind = new DiagnosticDescriptor(
			id: "D2L0065",
			title: "Unsupported type kind inside an immutable type declaration",
			messageFormat: "The type kind {0} is unexpected/unhandled by the immutability analyzer, and for safety we are emitting an error. This is a bug in the analyzer.",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			helpLinkUri: "https://github.com/Brightspace/D2L.CodeStyle/issues/new?title=Unexpected%20type%20kind%20in%20immutability%20analysis&labels=bug"
		);

		public static readonly DiagnosticDescriptor NonImmutableTypeHeldByImmutable = new DiagnosticDescriptor(
			id: "D2L0066",
			title: "Type may be mutable",
			messageFormat: "The {0} \"{1}\" is missing the [Immutable]{2} attribute, so it isn't safe to be held by an immutable type",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor MemberIsNotReadOnly = new DiagnosticDescriptor(
			id: "D2L0067",
			title: "Member must be readonly inside immutable type",
			messageFormat: "The {0} {1} must be readonly because {2} is immutable",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor UnexpectedMemberKind = new DiagnosticDescriptor(
			id: "D2L0068",
			title: "Unsupported member type in immutable type declaration",
			messageFormat: "The member {0} is of type {1} which is not supported by the immutability analyzer. This is a bug.",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			helpLinkUri: "https://github.com/Brightspace/D2L.CodeStyle/issues/new?title=Unexpected%20member%20kind%20in%20immutability%20analysis&labels=bug"
		);

		public static readonly DiagnosticDescriptor DelegateTypesPossiblyMutable = new DiagnosticDescriptor(
			id: "D2L0069",
			title: "Delegates members are not allowed in immutable types",
			messageFormat: "Delegate fields and properties can close over mutable state. For safety, we don't allow these in immutable types.",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor EventMemberMutable = new DiagnosticDescriptor(
			id: "D2L0070",
			title: "Event members are not allowed in immtuable types",
			messageFormat: "Event members hold registered event handlers. We consider this to be a source of mutability.",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor UnexpectedConditionalImmutability = new DiagnosticDescriptor(
			id: "D2L0071",
			title: "The [ConditionallyImmutable.OnlyIf] attribute cannot be used with methods",
			messageFormat: "The [ConditionallyImmutable.OnlyIf] attribute is only valid on parameters of named types",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor ConflictingImmutability = new DiagnosticDescriptor(
			id: "D2L0072",
			title: "Conflicting immutability attributes",
			messageFormat: "The [{0}] and [{1}] attributes cannot be used on the same {2}",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor InvalidAuditType = new DiagnosticDescriptor(
			id: "D2L0073",
			title: "Wrong type of auditing was used",
			messageFormat: "A {0} {1} should be audited using the [{2}] attributes",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor NonConstantPassedToConstantParameter = new DiagnosticDescriptor(
			id: "D2L0074",
			title: "Constant parameter cannot be passed a non-constant value",
			messageFormat: "The \"{0}\" parameter is marked with the [Constant] attribute, and so it must be passed a compile-time constant value",
			category: "Safety",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "The method being called has declared that this parameter must receive a constant, but a non-constant value is being passed."
		);

		public static readonly DiagnosticDescriptor InvalidConstantType = new DiagnosticDescriptor(
			id: "D2L0075",
			title: "Invalid data type marked as [Constant]",
			messageFormat: "The [Constant] attribute cannot be used on \"{0}\" types, because they cannot be compile-time constants",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor InconsistentMethodAttributeApplication = new DiagnosticDescriptor(
			id: "D2L0076",
			title: "Attribute is applied inconsistently to method definitions",
			messageFormat: "The [{0}] attribute is applied inconsistently between \"{1}\" and its source definition \"{2}\". This wiil cause inconsistent behaviour.",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor AnonymousFunctionsMayCaptureMutability = new DiagnosticDescriptor(
			id: "D2L0077",
			title: "Anonymous functions may capture mutable state and thus can't be assigned to members of an immutable type",
			messageFormat: "Anonymous functions may capture mutable state and thus can't be assigned to members of an immutable type. Try making the function static, or moving the assignment to an initializer.",
			category: "Immutability",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor RpcContextMarkedDependency = new DiagnosticDescriptor(
			id: "D2L0078",
			title: "RPC context argument should not be marked [Dependency]",
			messageFormat: "RPC context argument should not be marked [Dependency]",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor UnknownImmutabilityAssignmentKind = new DiagnosticDescriptor(
			id: "D2L0079",
			title: "ImmutabilityAnalyzer does not understand this assignment expression",
			messageFormat: "ImmutabilityAnalyzer does not understand this assignment expression due to: {0}",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "The analyzer does not understand the result of this assignment, and as such cannot determine if it is safe."
		);

		public static readonly DiagnosticDescriptor RpcInvalidParameterType = new DiagnosticDescriptor(
			id: "D2L0080",
			title: "Invalid RPC parameter type",
			messageFormat: "RPC parameters must be of a valid type",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: "RPC parameters must be of a valid type."
		);

		public static readonly DiagnosticDescriptor ReflectionSerializer_ConstructorParameter_CannotBeDeserialized = new DiagnosticDescriptor(
			id: "D2L0081",
			title: "[ReflectionSerializer] attribute requires that constructor parameters have a corresponding property of the same name",
			messageFormat: "The '{0}' parameter is missing a corresponding property of the same name. [ReflectionSerializer] requires that constructor parameters have a corresponding property of the same name in order to deserialize.",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor ReflectionSerializer_NoPublicConstructor = new DiagnosticDescriptor(
			id: "D2L0082",
			title: "[ReflectionSerializer] attribute requires classes and records to have a public constructor",
			messageFormat: "The type has no public constructors. [ReflectionSerializer] attribute requires classes and records to have a public constructor in order to deserialize. Records are encouraged to use an inline primary constructor.",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor ReflectionSerializer_MultiplePublicConstructors = new DiagnosticDescriptor(
			id: "D2L0083",
			title: "[ReflectionSerializer] attribute requires classes and records to have a single public constructor",
			messageFormat: "The type has too many public constructors. [ReflectionSerializer] attribute requires classes and records to have a single public constructor in order to deterministically deserialize.",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor ReflectionSerializer_StaticClass = new DiagnosticDescriptor(
			id: "D2L0084",
			title: "[ReflectionSerializer] attribute cannot be applied to static classes",
			messageFormat: "The [ReflectionSerializer] attribute cannot be applied to static classes",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor ReflectionSerializer_InitOnlySetter = new DiagnosticDescriptor(
			id: "D2L0085",
			title: "[ReflectionSerializer] attribute does not support init-only property setters",
			messageFormat: "Init-only property setters are not supported. The [ReflectionSerializer] attribute does not support deserialization into init-only property setters.",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor ReflectionSerializer_ConstructorParameter_InvalidRefKind = new DiagnosticDescriptor(
			id: "D2L0086",
			title: "[ReflectionSerializer] attribute only supports input constructor parameters",
			messageFormat: "The {0} keyword is not support on the '{1}' parameter. The [ReflectionSerializer] attribute only supports input constructor parameters.",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor InterfaceBinder_InterfacesOnly = new DiagnosticDescriptor(
			id: "D2L0087",
			title: "InterfaceBinder<T> can only be used with interfaces",
			messageFormat: "The generic type argument '{0}' is not an interface. The InterfaceBinder<T> can only be used with interfaces.",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor AsyncMethodCannotBeBlocking = new DiagnosticDescriptor(
			id: "D2L0088",
			title: "Async methods cannot have the [Blocking] attribute",
			messageFormat: "Methods are either async, [Blocking], or neither. Remove the [Blocking] attribute from {0}.",
			category: "Async",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor AsyncMethodCannotCallBlockingMethod = new DiagnosticDescriptor(
			id: "D2L0089",
			title: "Async methods cannot call blocking methods",
			messageFormat: "Calling blocking methods from async methods can lead to a variety of deadlocks and performance problems. Replace the call to {0} with an async version.",
			category: "Async",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor OnlyCallBlockingMethodsFromMethods = new DiagnosticDescriptor(
			id: "D2L0090",
			title: "Only methods can call blocking methods",
			messageFormat: "Blocking methods like {0} are only allowed inside other blocking methods, not {1}",
			category: "Async",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor BlockingCallersMustBeBlocking = new DiagnosticDescriptor(
			id: "D2L0091",
			title: "Methods that call blocking methods must have the [Blocking] attribute",
			messageFormat: "The call to {0} is blocking, so {1} must have the [Blocking] attribute",
			category: "Async",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor UnnecessaryBlocking = new DiagnosticDescriptor(
			id: "D2L0092",
			title: "[Blocking] may be unnecessary",
			messageFormat: "{0} doesn't call any blocking methods, so the [Blocking] attribute may be unnecessary. If this diagnostic is a surprise it may mean that something is not properly annotated, or you are doing something fancy.",
			category: "Async",
			defaultSeverity: DiagnosticSeverity.Warning, // Not error! For special cases.
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor DontIntroduceBlockingInImplementation = new DiagnosticDescriptor(
			id: "D2L0093",
			title: "Don't add [Blocking] to an overridden method that isn't itself blocking",
			messageFormat: "{0} implements {1}, which is not blocking. Callers of {1} will not expect blocking.",
			category: "Async",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor NonBlockingImplementationOfBlockingThing = new DiagnosticDescriptor(
			id: "D2L0094",
			title: "Method implements a blocking method but isn't itself blocking",
			messageFormat: "{0} does not have the [Blocking] attribute but implements the blocking method {1}. Add [Blocking] if you're going to call other blocking methods within {0}.",
			category: "Async",
			defaultSeverity: DiagnosticSeverity.Info, // Only used for the suggested edit
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor GenericGeneratorError = new DiagnosticDescriptor(
			id: "D2L0095",
			title: "A source generator encountered an unexpected situation",
			messageFormat: "The source generator {0} encountered an unexpected situation. This is a bug in D2L.CodeStyle.Analyzers: {1}.",
			category: "Generators",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor AliasingAttributeNamesNotSupported = new DiagnosticDescriptor(
			id: "D2L0096",
			title: "Aliasing attribute class names not supported",
			messageFormat: "Aliasing attribute class names makes code less readable and forces analyzers to use the symbolic model for type equality (which is expensive)",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor ExpectedAsyncSuffix = new DiagnosticDescriptor(
			id: "D2L0097",
			title: "Expected method name to have an -Async suffix",
			messageFormat: "The method {0} does not have an -Async suffix, so we can't generate an idiomatic sync alternative method",
			category: "Async",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor NonTaskReturnType = new DiagnosticDescriptor(
			id: "D2L0098",
			title: "Expected return type to be Task or Task<T>",
			messageFormat: "We only know how to generate sync alternatives for methods that return Task or Task<T>",
			category: "Async",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor MemberNotVisibleToCaller = new DiagnosticDescriptor(
			id: "D2L0099",
			title: "Member is not visible to caller",
			messageFormat: "The member '{0}' has restricted its visibility to an explicit set of callers",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);

		public static readonly DiagnosticDescriptor ExplicitCancellationTokenArgumentRequired = new DiagnosticDescriptor(
			id: "D2L0100",
			title: "CancellationToken arguments must be explicitly passed",
			messageFormat: "CancellationToken arguments must be explicitly passed. CancellationToken.None is acceptable in some situations, but in most case should be passed through an entire async chain to ensure our applications are responsive.",
			category: "Correctness",
			defaultSeverity: DiagnosticSeverity.Error,
			isEnabledByDefault: true
		);
	}
}
