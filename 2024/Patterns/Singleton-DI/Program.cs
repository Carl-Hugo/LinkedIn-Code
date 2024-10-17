var builder = WebApplication.CreateBuilder(args);

// 2. Register the service with a singleton lifetime
builder.Services.AddSingleton<MyService>();

var app = builder.Build();

// 3. Inject the Singleton service into a consumer
app.MapGet("/", (MyService myService) => myService.Execute());

app.Run();

// 1. Define the service
public class MyService
{
    public string Execute() => "MyService ExecuteTask result.";
}