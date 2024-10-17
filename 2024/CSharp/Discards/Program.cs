// Pattern Matching: As a default case in switch expressions.
var input = 2;
var output = input switch
{
    0 => "Order received",
    1 => "Order processing",
    2 => "Order shipped",
    3 => "Order delivered",
    4 => "Order canceled",
    _ => "Unknown status"
};

// Deconstructing Tuples: When only some members are needed.
var tuple = (name: "Foo", age: 23);
var (name, _) = tuple;
Console.WriteLine(name);

// Out Parameters: When the value of the out parameter is not needed.
if (bool.TryParse("true", out _))
{
    Console.WriteLine("true was parsable!");
}

// Error prone and hard to read code
var address = new Address(
    Country: "USA",
    StateCode: "CA",
    PostalCode: "12345",
    AdditionalInfo: "Apt. 101",
    Street: "123 Main St",
    City: "Anytown",
    State: "California",
    Zip: "12345"
);

var (_, _, _, _, street, city, state, zip) = address;
Console.WriteLine($"Address: {street}, {city}, {state}, {zip}");

public record Address(
    string Country,
    string StateCode,
    string PostalCode,
    string AdditionalInfo,
    string Street,
    string City,
    string State,
    string Zip
);