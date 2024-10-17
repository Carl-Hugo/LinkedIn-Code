var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions());
builder.WebHost.UseKestrelCore();

var app = builder.Build();
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Middleware that always runs...");
    await next(context);
});

Console.WriteLine("Running...");
app.Run();
