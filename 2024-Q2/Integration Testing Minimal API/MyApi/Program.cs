var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.Run();

// Workaround to test Minimal API projects
public partial class Program { }