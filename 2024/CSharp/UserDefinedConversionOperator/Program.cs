// Example of implicit conversion from string to SomeGenericClass<string>
SomeGenericClass<string> stringInstance = "Hello, World!";
Console.WriteLine($"String Value: {stringInstance.Value}");

// Example of implicit conversion from int to SomeGenericClass<int>
SomeGenericClass<int> intInstance = 42;
Console.WriteLine($"Integer Value: {intInstance.Value}");

public class SomeGenericClass<T>
{
    public T? Value { get; set; }

    // Implicit conversion from T to SomeGenericClass<T>
    public static implicit operator SomeGenericClass<T>(T value)
    {
        return new SomeGenericClass<T> { Value = value };
    }
}
