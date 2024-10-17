namespace CreateVariable;

public class SpecializedInitialization
{
    public void ConstantVariable()
    {
        // Constant: The value is set at the time of declaration and cannot be changed later. (C# 1.0)
        const int constantValue = 10;
        Console.WriteLine($"Constant value: {constantValue}");
        // Output: Constant value: 10
    }

    public void ReadonlyFieldInitialization()
    {
        // Readonly field initialization using constructor. (C# 1.0)
        Example example = new Example(42);
        example.Write();
        // Output: Readonly field value: 42
    }

    public class Example
    {
        private readonly int _myReadonlyValue;
        public Example(int value)
        {
            _myReadonlyValue = value;
        }

        public void Write()
        {
            Console.WriteLine($"Readonly field value: {_myReadonlyValue}");
        }
    }

    public void LazyInitialization()
    {
        // Lazy initialization: Object is created only when accessed for the first time. (C# 4.0)
        Lazy<Person> lazyPerson = new(() => new Person { Name = "John", Age = 30 });
        Console.WriteLine($"Lazy initialized person: Name={lazyPerson.Value.Name}, Age={lazyPerson.Value.Age}");
        // Output: Lazy initialized person: Name=John, Age=30
    }

    public class Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }

    public void StaticVariableInitialization()
    {
        // Static variable initialization: Shared across all instances of the class. (C# 1.0)
        Console.WriteLine($"Static value: {StaticExample.StaticValue}");
        // Output: Static value: 100

        Console.WriteLine($"Static property: {StaticExample.StaticProperty}");
        // Output: Static property: 200
    }

    public class StaticExample
    {
        public static int StaticValue = 100;
        public static int StaticProperty { get; } = 200;
    }
}
