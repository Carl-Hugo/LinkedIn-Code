using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/{key}", async (string key) =>
{
    var stopWatch = Stopwatch.StartNew();
    var delay = 2_000;
    await Task.Delay(delay);
    stopWatch.Stop();
    return Results.Ok(new {
        key,
        delay,
        stopWatch.Elapsed
    });
});

app.MapDefaultEndpoints();

app.Run();
