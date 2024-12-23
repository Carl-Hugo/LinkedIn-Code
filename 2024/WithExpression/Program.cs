var originalOrder = new Order(1, "Laptop", 2);
var updatedOrder = originalOrder with { Quantity = 3 };

Console.WriteLine($"Original: {originalOrder}");
Console.WriteLine($"Updated: {updatedOrder}");
Console.WriteLine($"Same Object: {ReferenceEquals(originalOrder, updatedOrder)}");

public record class Order(int Id, string Product, int Quantity);

/*
Output:
Original: Order { Id = 1, Product = Laptop, Quantity = 2 }
Updated: Order { Id = 1, Product = Laptop, Quantity = 3 }
Same Object: False
*/