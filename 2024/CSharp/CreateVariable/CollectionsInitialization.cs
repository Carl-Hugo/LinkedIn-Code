namespace CreateVariable;

public class CollectionsInitialization
{
    public void TargetTypedNew()
    {
        // Target-typed new: Introduced in C# 9.0, you can omit the type on the right-hand side when it can be inferred from the left. (C# 9.0)
        List<int> numbers = new() { 1, 2, 3 };  // inferred as List<int>
        Console.WriteLine($"numbers count: {numbers.Count}");
        // Output: numbers count: 3
    }

    public void ArrayInitialization()
    {
        // Array Initialization: Arrays can be initialized using either explicit or implicit typing. (C# 1.0)
        var numbers = new[] { 1, 2, 3 };  // inferred as int[]
        Console.WriteLine($"Array elements: {string.Join(", ", numbers)}");
        // Output: Array elements: 1, 2, 3
    }

    public void CollectionInitialization()
    {
        // Collection Initializers: Collections such as List<T> can be initialized with values using collection initializers. (C# 3.0)
        List<int> numbers = new List<int> { 1, 2, 3 };
        Console.WriteLine($"Collection elements: {string.Join(", ", numbers)}");
        // Output: Collection elements: 1, 2, 3
    }

    public void ListInitializationWithSquareBrackets()
    {
        // List Initialization with Square Brackets—Collection expressions: A shorthand for initializing lists. (C# 12.0)
        List<int> numbers = [1, 2, 3];
        Console.WriteLine($"List initialized with {numbers.Count} elements");
        // Output: List initialized with 3 elements
    }
}
