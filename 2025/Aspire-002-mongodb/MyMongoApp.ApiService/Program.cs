using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add MongoDB client
builder.AddMongoDBClient(connectionName: "openfoodfacts");

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/openfoodfacts/raw/products/{productId}", async Task<Results<Ok<Result>, NotFound>> (
    string productId, IMongoClient mongo, CancellationToken cancellationToken) =>
{
    var database = mongo.GetDatabase("openfoodfacts");
    var documents = database.GetCollection<Result>("default");
    var filter = new BsonDocument("_id", productId);
    var result = await documents
        .Find(filter)
        .FirstOrDefaultAsync(cancellationToken)
    ;
    if (result == null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(result);
});

app.MapDefaultEndpoints();

app.Run();

public sealed class Result() : Dictionary<string, object>;
