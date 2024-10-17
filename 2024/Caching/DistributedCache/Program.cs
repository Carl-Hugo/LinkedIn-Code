using DistributedCache;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductsDb"));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "ProductCache";
});

var app = builder.Build();
app.MapGet("products/{id}", async ValueTask<Results<NotFound, Ok<Product>>> (int id, IDistributedCache cache, AppDbContext context) =>
{
    // Start reading the cache
    var cachedProduct = await cache.GetStringAsync($"product-{id}");
    Product? product = null;

    if (cachedProduct is not null)
    {
        product = JsonSerializer.Deserialize<Product>(cachedProduct);
    }
    else
    {
        // Retrieve from the database
        product = await context.Products.FindAsync(id);

        // Serialize and cache the data
        if (product is not null)
        {
            var serializedProduct = JsonSerializer.Serialize(product);
            await cache.SetStringAsync($"product-{id}", serializedProduct);
        }
    }

    // Tell the client the product does not exists
    if (product is null)
    {
        return TypedResults.NotFound();
    }

    // Return the product
    return TypedResults.Ok(product);
});

// Ensure the database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
