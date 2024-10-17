using HybridCacheApp;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductsDb"));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "ProductCache";
});

builder.Services.AddHybridCache();

var app = builder.Build();
app.MapGet("products/{id}", async ValueTask<Results<NotFound, Ok<Product>>> (int id, HybridCache cache, AppDbContext context, CancellationToken token) =>
{
    // Get the cached item or retrieve it from the database and cache it if it is not in the cache already
    var product = await cache.GetOrCreateAsync(
        key: $"product-{id}",
        factory: async token => await context.Products.FindAsync([id], cancellationToken: token),
        token: token
    );

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
