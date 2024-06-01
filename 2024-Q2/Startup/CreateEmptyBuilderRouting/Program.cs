var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions());
builder.WebHost.UseKestrelCore();
builder.Services.AddRouting();

var app = builder.Build();
app.MapGet("/", () => "Hello, World!");

Console.WriteLine("Running...");
app.Run();
