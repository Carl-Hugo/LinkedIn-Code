var person = new Person { Name = "Alice", Age = 30 };
Console.WriteLine($"Hello, {person.Name}, you are {person.Age} years old!");

public class Person
{
    public string Name { get; init; }
    public int Age { get; init; }
}