namespace CreateVariable;

public class AnonymousAndDynamicTyping
{
    public void AnonymousTypeCreation()
    {
        // Anonymous Types: Useful for creating objects without explicitly defining a class. (C# 3.0)
        var item = new { Name = "Apple", Price = 1.2 };
        Console.WriteLine($"Item: Name={item.Name}, Price={item.Price}");

        // error CS0200: Property or indexer '<anonymous type: string Name, double Price>.Name' cannot be assigned to -- it is read only
        // item.Name = "Foo";
    }

    public void DynamicVariable()
    {
        // Dynamic Variables: Using the dynamic keyword for late-bound, runtime-checked variables. (C# 4.0)
        dynamic dynVar = 1;
        Console.WriteLine($"Dynamic variable (int): {dynVar}");
        dynVar = "Hello";
        Console.WriteLine($"Dynamic variable (string): {dynVar}");
    }

    public void DynamicVariableWithProperties()
    {
        // Creating a dynamic object and setting properties dynamically. (C# 4.0)
        dynamic person = new System.Dynamic.ExpandoObject();
        person.Name = "John";
        person.Age = 30;
        person.Occupation = "Software Developer";

        Console.WriteLine($"Name: {person.Name}, Age: {person.Age}, Occupation: {person.Occupation}");
    }
}
