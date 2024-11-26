using Microsoft.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<GitHubService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.github.com");
});
builder.Services.AddTransient<IHttpMessageHandlerBuilderFilter, GlobalHttpMessageHandlerBuilderFilter>();

var app = builder.Build();
app.MapGet("api/users/{username}", async (string username, GitHubService gitHubService)
    => await gitHubService.GetByUsernameAsync(username));
app.Run();

public class GlobalLoggingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Request: {request.Method} {request.RequestUri}");
        var response = await base.SendAsync(request, cancellationToken);
        Console.WriteLine($"Response: {response.StatusCode}");
        return response;
    }
}

public class GlobalHttpMessageHandlerBuilderFilter : IHttpMessageHandlerBuilderFilter
{
    public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
    {
        return builder =>
        {
            builder.AdditionalHandlers.Add(new GlobalLoggingHandler());
            next(builder);
        };
    }
}

public class GitHubService(HttpClient client)
{
    public async Task<Dictionary<string, object>?> GetByUsernameAsync(string username)
    {
        var url = $"users/{username}";
        return await client.GetFromJsonAsync<Dictionary<string, object>>(url);
    }
}