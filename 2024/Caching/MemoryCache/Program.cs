using MemoryCache;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductsDb"));
builder.Services.AddMemoryCache();

var app = builder.Build();
app.MapGet("products/{id}", Results<NotFound, Ok<Product>> (int id, IMemoryCache cache, AppDbContext context) =>
{
    // Start reading the cache
    if (!cache.TryGetValue(id, out Product? product))
    {
        // Retrieve from the database
        product = context.Products.Find(id);

        // Cache the data
        cache.Set(id, product);
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
