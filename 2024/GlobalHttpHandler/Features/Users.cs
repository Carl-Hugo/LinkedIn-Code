namespace GlobalHttpHandler.Features.Users;

public class JsonPlaceholderApiClient(HttpClient client)
{
    public async Task<Dictionary<string, object>?> FetchUserAsync(int userId, CancellationToken cancellationToken)
    {
        var url = $"users/{userId}";
        return await client.GetFromJsonAsync<Dictionary<string, object>>(url, cancellationToken);
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