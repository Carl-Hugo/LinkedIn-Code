namespace CreateVariable;

public class ObjectInitialization
{
    public void ObjectInitializationMethod()
    {
        // Object Initializers with a defined class instead of an anonymous type. (C# 3.0)
        Person person = new Person { Name = "John", Age = 30 };
        Console.WriteLine($"Person: Name={person.Name}, Age={person.Age}");
        // Person: Name=John, Age=30
    }

    public void ObjectInitializationWithConstructor()
    {
        // Creating an instance of Car using the constructor. (C# 1.0)
        Car car = new Car("Tesla Model S", 2022);
        Console.WriteLine($"Car: Model={car.Model}, Year={car.Year}");
        // Car: Model=Tesla Model S, Year=2022
    }

    public void VarPersonInitialization()
    {
        // Using 'var' with explicit constructor. (C# 3.0)
        var person = new Person { Name = "John", Age = 30 };
        Console.WriteLine($"Person (var): Name={person.Name}, Age={person.Age}");
        // Person (var): Name=John, Age=30
    }

    public void ExplicitPersonInitialization()
    {
        // Using explicit type with target-typed new(). (C# 9.0)
        Person person = new() { Name = "Jane", Age = 25 };
        Console.WriteLine($"Person (explicit): Name={person.Name}, Age={person.Age}");
        // Person (explicit): Name=Jane, Age=25
    }

    public void RecordClassExample()
    {
        // Records automatically provide value-based equality and immutability by default. (C# 9.0)
        PersonRecord person = new("John", 30);
        Console.WriteLine($"PersonRecord: Name={person.Name}, Age={person.Age}");
        // PersonRecord: Name=John, Age=30

        // Deconstructing the record into individual variables.
        var (name, age) = person;
        Console.WriteLine($"Deconstructed: Name={name}, Age={age}");
        // Deconstructed: Name=John, Age=30
    }

    public class Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }

    public record PersonRecord(string Name, int Age);

    public class Car
    {
        public string Model { get; set; }
        public int Year { get; set; }

        // Constructor that takes parameters.
        public Car(string model, int year)
        {
            Model = model;
            Year = year;
        }
    }
}
