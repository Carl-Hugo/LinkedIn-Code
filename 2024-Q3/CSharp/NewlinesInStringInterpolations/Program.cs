Product[] products = [
    new Product(1, "SKU12345", "Laptop", 100, false),
    new Product(2, "SKU67890", "Smartphone", 50, false),
    new Product(3, "SKU54321", "Wireless Headphones", 0, false),
    new Product(4, "SKU98765", "Smartwatch", 10, true),
    new Product(5, "SKU11223", "Tablet", 250, false)
];

var product = Random.Shared.GetItems(products, 1).Single();

var productStatus = $"Product status: {product switch
{
    { Stock: > 50, IsDiscontinued: false } => "In stock and available",
    { Stock: > 0, IsDiscontinued: false } => "Limited stock, order soon",
    { Stock: 0, IsDiscontinued: false } => "Out of stock, restocking soon",
    { IsDiscontinued: true } => "Discontinued, no longer available",
    _ => "Status unknown"
}}";

Console.WriteLine("---[ PRODUCT ]---");
Console.WriteLine(product);

Console.WriteLine("---[ PRODUCT STATUS ]---");
Console.WriteLine(productStatus);

public record Product(int Id, string Sku, string Name, int Stock, bool IsDiscontinued);