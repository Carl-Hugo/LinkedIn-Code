#pragma warning disable CA1050 // Declare types in namespaces
#define DRY3
using DRY;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddSingleton<InvoiceService>()
    .AddSingleton<OrderService>()
#if DRY1
    .AddSingleton<CalculationService>()
#endif
    .BuildServiceProvider()
;

HashSet<Product> products = [
    new("Habanero pepper", 0.25, 10),
    new("Jalapeno pepper", 0.20, 15),
    new("Bell pepper", 0.50, 20)
];

var invoiceService = services.GetRequiredService<InvoiceService>();
var invoiceTotal = invoiceService.CalculateTotal(products);

var orderService = services.GetRequiredService<OrderService>();
var orderTotal = orderService.CalculateTotal(products);

Console.WriteLine($"invoiceTotal: {invoiceTotal} | orderTotal: {orderTotal}");

#if DRY1
public class CalculationService
{
    public double CalculateTotal(IEnumerable<Product> products)
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.Price * product.Quantity;
        }
        total += total * 0.1; // Apply 10% sales tax
        return total;
    }
}

public class InvoiceService(CalculationService calculationService)
{
    public double CalculateTotal(IEnumerable<Product> products)
    {
        return calculationService.CalculateTotal(products);
    }
}

public class OrderService(CalculationService calculationService)
{
    public double CalculateTotal(IEnumerable<Product> products)
    {
        return calculationService.CalculateTotal(products);
    }
}
#elif DRY2
public class InvoiceService
{
    public double CalculateTotal(IEnumerable<Product> products)
    {
        return products.CalculateTotal();
    }
}

public class OrderService
{
    public double CalculateTotal(IEnumerable<Product> products)
    {
        return products.CalculateTotal();
    }
}

public static class ProductCollectionExtensions
{
    public static double CalculateTotal(this IEnumerable<Product> products)
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.Price * product.Quantity;
        }
        total += total * 0.1; // Apply 10% sales tax
        return total;
    }
}
#else
public class InvoiceService
{
    public double CalculateTotal(IEnumerable<Product> products)
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.Price * product.Quantity;
        }
        total += total * 0.1; // Apply 10% sales tax
        return total;
    }
}

public class OrderService
{
    public double CalculateTotal(IEnumerable<Product> products)
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.Price * product.Quantity;
        }
        total += total * 0.1; // Apply 10% sales tax
        return total;
    }
}
#endif
#pragma warning restore CA1050 // Declare types in namespaces
