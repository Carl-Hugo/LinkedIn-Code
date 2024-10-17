// Enable nullable reference types using a directives
#nullable enable

var person = new Person(null);

// This will cause a "warning CS8602: Dereference of a possibly null reference."
// at compile time.
// And result in a "Unhandled exception. System.NullReferenceException: Object
// reference not set to an instance of an object." at runtime.
Console.WriteLine($"The person name contains {person.Name.Length} characters!");

// Fix the issue with a null check
if (person.Name != null)
{
    Console.WriteLine($"The person name contains {person.Name.Length} characters!");
}
else
{
    Console.WriteLine("The person name contains 0 characters!");
}

// Or use the null-conditional operator
Console.WriteLine($"The person name contains {person.Name?.Length ?? 0} characters!");

public class Person
{
    public string? Name { get; set; }

    public Person(string? name)
    {
        Name = name;
    }
}