using MyApplication.Extensions;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Leverage extension methods to better organize
// the composition root.
builder.Services.AddDatabaseServices();
builder.Services.AddApplicationServices();

var app = builder.Build();
// Map endpoints
app.Run();