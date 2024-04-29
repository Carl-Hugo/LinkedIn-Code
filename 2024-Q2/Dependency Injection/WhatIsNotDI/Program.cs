var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    // Manual instantiation of the Client class
    var client = new Client();
    client.DoWork();
});

app.Run();

public class Client
{
    // Client directly creates an instance of Service
    private readonly Service _service = new();

    public void DoWork()
    {
        _service.Execute();
    }
}

public class Service
{
    public void Execute()
    {
        Console.WriteLine("Perform some action");
    }
}
