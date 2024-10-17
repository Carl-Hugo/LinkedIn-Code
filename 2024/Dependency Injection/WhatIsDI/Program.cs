var builder = WebApplication.CreateBuilder(args);

// Register dependencies with IoC Container
// (Composition Root)
builder.Services
    .AddSingleton<Client>()
    .AddSingleton<Service>()
;

var app = builder.Build();

// Inject dependency in the handler
// The container creates and manages the dependency tree
app.MapGet("/", (Client client) =>
{
    client.DoWork();
});

app.Run();

public class Client
{
    private readonly Service _service;
    // Service is injected into the Client
    public Client(Service service)
    {
        _service = service;
    }

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