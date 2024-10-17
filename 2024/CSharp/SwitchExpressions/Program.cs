var statusCode = 2;
var obj = new Placeholder();
var before = obj.GetOrderStatusMessageBefore(statusCode);
var after = obj.GetOrderStatusMessageAfter(statusCode);

Console.WriteLine($"statusCode: {statusCode} | before: {before} | after: {after}");

public class Placeholder
{
    public string GetOrderStatusMessageBefore(int statusCode)
    {
        string statusMessage = string.Empty;
        switch (statusCode)
        {
            case 0:
                statusMessage = "Order received";
                break;
            case 1:
                statusMessage = "Order processing";
                break;
            case 2:
                statusMessage = "Order shipped";
                break;
            case 3:
                statusMessage = "Order delivered";
                break;
            case 4:
                statusMessage = "Order canceled";
                break;
            default:
                statusMessage = "Unknown status";
                break;
        }
        return statusMessage;
    }


    public string GetOrderStatusMessageAfter(int statusCode)
    {
        return statusCode switch
        {
            0 => "Order received",
            1 => "Order processing",
            2 => "Order shipped",
            3 => "Order delivered",
            4 => "Order canceled",
            _ => "Unknown status"
        };
    }

}