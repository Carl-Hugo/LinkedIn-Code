namespace GlobalHttpHandler.Features.Users;

public class JsonPlaceholderApiClient(HttpClient client)
{
    public async Task<User?> FetchUserAsync(int userId, CancellationToken cancellationToken)
    {
        var url = $"users/{userId}";
        return await client.GetFromJsonAsync<User>(url, cancellationToken);
    }
}

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddUsersServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddHttpClient<JsonPlaceholderApiClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            })
        ;
        return builder;
    }
}

public static class WebApplicationExtensions
{
    public static WebApplication MapUsersEndpoints(this WebApplication app)
    {
        app.MapGet("api/users/{userId}", async (int userId, JsonPlaceholderApiClient jsonPlaceholderService, CancellationToken cancellationToken)
            => await jsonPlaceholderService.FetchUserAsync(userId, cancellationToken));

        return app;
    }
}

public record Geo(string Lat, string Lng);

public record Address(string Street, string Suite, string City, string Zipcode, Geo Geo);

public record Company(string Name, string CatchPhrase, string Bs);

public record User(
    int Id,
    string Name,
    string Username,
    string Email,
    Address Address,
    string Phone,
    string Website,
    Company Company
);
