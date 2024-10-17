// Readable
List<string> names = new() { "Alice", "Bob", "Charlie" };

// Readable
List<int> numbers = new(capacity: 5) { 1, 2, 3, 4, 5 };

// Hard to read
Customer customer = new(new("John Doe")) { Orders = new() { new("Order1"), new("Order2") } };

// Readable
Product product = new() { Name = "Laptop", Price = 999.99M };

PrintCustomer(customer);
PrintProduct(product);

static void PrintCustomer(Customer customer)
{
    Console.WriteLine($"Customer: {customer.Info.Name}");
    Console.WriteLine("Orders:");
    foreach (var order in customer.Orders)
    {
        Console.WriteLine($" - {order.OrderId}");
    }
}

static void PrintProduct(Product product)
{
    Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");
}

public record Customer(CustomerInfo Info)
{
    public List<Order> Orders { get; init; } = new();
}
public record CustomerInfo(string Name);
public record Order(string OrderId);
public record Product
{
    public string? Name { get; init; }
    public decimal Price { get; init; }
}