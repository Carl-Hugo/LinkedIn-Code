using CreateVariable;

// Get the post number as input from the user
Console.WriteLine("Enter the post number (1-7) to run the example:");

if (int.TryParse(Console.ReadLine(), out int postNumber))
{
    // Execute the specified post code sample
    switch (postNumber)
    {
        case 1:
            RunBasicTypeInitialization();
            break;
        case 2:
            RunCollectionsInitialization();
            break;
        case 3:
            RunObjectInitialization();
            break;
        case 4:
            RunAnonymousAndDynamicTyping();
            break;
        case 5:
            RunAdvancedTypes();
            break;
        case 6:
            RunSpecializedInitialization();
            break;
        case 7:
            RunPerformanceOptimization();
            break;
        default:
            Console.WriteLine("Invalid post number. Please enter a number between 1 and 7.");
            break;
    }
}
else
{
    Console.WriteLine("Invalid input. Please enter a valid number.");
}

// Post 1: Basic Type Initialization
void RunBasicTypeInitialization()
{
    var basicInit = new BasicTypeInitialization();
    basicInit.ExplicitTypeDeclaration();
    basicInit.ImplicitTypingWithVar();
    basicInit.DefaultInitialization();
}

// Post 2: Collections Initialization
void RunCollectionsInitialization()
{
    var collectionsInit = new CollectionsInitialization();
    collectionsInit.TargetTypedNew();
    collectionsInit.ArrayInitialization();
    collectionsInit.CollectionInitialization();
    collectionsInit.ListInitializationWithSquareBrackets();
}

// Post 3: Object Initialization
void RunObjectInitialization()
{
    var objectInit = new ObjectInitialization();
    objectInit.ObjectInitializationMethod();
    objectInit.ObjectInitializationWithConstructor();
    objectInit.VarPersonInitialization();
    objectInit.ExplicitPersonInitialization();
    objectInit.RecordClassExample();
}

// Post 4: Anonymous and Dynamic Typing
void RunAnonymousAndDynamicTyping()
{
    var anonymousDynamicTyping = new AnonymousAndDynamicTyping();
    anonymousDynamicTyping.AnonymousTypeCreation();
    anonymousDynamicTyping.DynamicVariable();
    anonymousDynamicTyping.DynamicVariableWithProperties();
}

// Post 5: Advanced Types
void RunAdvancedTypes()
{
    var advancedTypes = new AdvancedTypes();
    advancedTypes.TupleInitialization();
    advancedTypes.NullableTypeInitialization();
    advancedTypes.ReferenceVariable();
    advancedTypes.RefReturn();
}

// Post 6: Specialized Initialization
void RunSpecializedInitialization()
{
    var specializedInit = new SpecializedInitialization();
    specializedInit.ConstantVariable();
    specializedInit.ReadonlyFieldInitialization();
    specializedInit.LazyInitialization();
    specializedInit.StaticVariableInitialization();
}

// Post 7: Performance Optimization
void RunPerformanceOptimization()
{
    var performanceOpt = new PerformanceOptimization();
    performanceOpt.StackAllocation();
    performanceOpt.PointerVariable();
    performanceOpt.FixedBufferUsage();
    performanceOpt.ArrayPooling();
    performanceOpt.ArrayPooling2();
    performanceOpt.SpanMemoryOptimization();
    performanceOpt.MemoryOptimization();
    performanceOpt.RefStructUsage();
    performanceOpt.InParameterExample();
}
