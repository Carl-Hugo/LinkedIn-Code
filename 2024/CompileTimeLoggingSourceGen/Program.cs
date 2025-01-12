var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<OrderProcessingService>();

var app = builder.Build();
app.MapPost("/process-order", (OrderProcessingService service, Order order) =>
{
    try
    {
        service.ProcessOrder(order);
        return Results.Ok("Order processed successfully!");
    }
    catch (ArgumentException ex)
    {
        return Results.Problem(ex.Message, statusCode: 400);
    }
});
app.Run();

public record Order(int OrderId, string ProductName, int Quantity);

public partial class OrderProcessingService(ILogger<OrderProcessingService> logger)
{
    public void ProcessOrder(Order order)
    {
        if (order.Quantity <= 0)
        {
            logger.InvalidOrder(order.OrderId);
            throw new ArgumentException("Quantity must be greater than zero.");
        }

        logger.OrderProcessingStarted(order.OrderId, order.ProductName);
        //
        // Imagine that we are processing the order here...
        //
        logger.OrderProcessingCompleted(order.OrderId);
    }
}

internal static partial class OrderProcessingLogMessages
{
    [LoggerMessage(EventId = 1, Level = LogLevel.Warning, Message = "Invalid order received. OrderId: {OrderId}")]
    public static partial void InvalidOrder(this ILogger logger, int orderId);

    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Order processing started. OrderId: {OrderId}, Product: {ProductName}")]
    public static partial void OrderProcessingStarted(this ILogger logger, int orderId, string productName);

    [LoggerMessage(EventId = 3, Level = LogLevel.Information, Message = "Order processing completed. OrderId: {OrderId}")]
    public static partial void OrderProcessingCompleted(this ILogger logger, int orderId);
}