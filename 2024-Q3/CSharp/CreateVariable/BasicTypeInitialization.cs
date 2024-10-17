namespace CreateVariable;
public class BasicTypeInitialization
{
    public void ExplicitTypeDeclaration()
    {
        // Explicit Type Declaration: The traditional approach involves specifying the type of the variable explicitly. (C# 1.0)
        int number = 5;
        string text = "Hello";
        Console.WriteLine($"number: {number} | text: {text}");
        // Output: number: 5 | text: Hello
    }

    public void ImplicitTypingWithVar()
    {
        // Implicit Typing with var: Introduced in C# 3.0, allows for implicitly typed local variables. (C# 3.0)
        var number = 5;  // inferred as int
        var text = "Hello";  // inferred as string
        Console.WriteLine($"number: {number} | text: {text}");
        // Output: number: 5 | text: Hello
    }

    public void DefaultInitialization()
    {
        // Default Initialization: Using the default keyword to initialize variables. (C# 1.0)
        int defaultInt = default;
        Person? defaultPerson = default;
        Console.WriteLine($"Default int: {defaultInt}, Default person: {defaultPerson}");
        // Output: Default int: 0, Default person:
    }

    public class Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}
