#undef LAX
#define DO_NOT_COMPILE
#if LAX
// Oops! LastName is not initialized...
var person = new Person { FirstName = "John" };
Console.WriteLine($"FirstName: {person.FirstName}");
Console.WriteLine($"LastName: {person.LastName}");

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
#elif DO_NOT_COMPILE
// ERROR: Required member 'Person.LastName' must be set in the
// object initializer or attribute constructor.
var person2 = new Person { FirstName = "John" };

public class Person
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
#else
// Enforced initialization of required properties
var person = new Person { FirstName = "John", LastName = "Doe" };

Console.WriteLine($"FirstName: {person.FirstName}");
Console.WriteLine($"LastName: {person.LastName}");

public class Person
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
#endif

