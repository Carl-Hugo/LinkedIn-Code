namespace CreateVariable;

public class AdvancedTypes
{
    public void TupleInitialization()
    {
        // Tuple Initialization: Allows grouping of multiple values into one object. (C# 7.0)
        var person = ("John", 30);
        Console.WriteLine($"Tuple: Name={person.Item1}, Age={person.Item2}");

        // Deconstructing a tuple.
        var (name, age) = person;
        Console.WriteLine($"Deconstructed: Name={name}, Age={age}");
    }

    public void NullableTypeInitialization()
    {
        // Nullable Types: Allows value types to represent null values. (C# 2.0)
        int? nullableInt = null;
        Console.WriteLine($"Nullable int: {nullableInt}");

        // Long syntax; achieves the same result.
        var nullableInt2 = new Nullable<int>();
        Console.WriteLine($"Nullable int 2: {nullableInt2}");
    }

    public void ReferenceVariable()
    {
        // Reference Variables: Using ref keyword to create reference to another variable. (C# 1.0)
        int value = 10;
        ref int refValue = ref value;
        refValue = 20;  // updates the original variable
        Console.WriteLine($"Original value: {value}");
        // Output: Original value: 20
    }

    public void RefReturn()
    {
        ref int FindMax(ref int x, ref int y)
        {
            return ref (x > y ? ref x : ref y);
        }

        int a = 10, b = 20;
        ref int maxRef = ref FindMax(ref a, ref b);
        maxRef = 30;  // Updates the original value (b)
        Console.WriteLine($"Original values: a = {a}, b = {b}");
        // Output: Original values: a = 10, b = 30
    }
}
