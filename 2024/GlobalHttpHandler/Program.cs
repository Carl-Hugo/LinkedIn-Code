using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient<JsonPlaceholderService>(httpClient =>
    {
        httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    })
;
builder.Services
    .AddHttpClient<DogService>(httpClient =>
    {
        httpClient.BaseAddress = new Uri("https://api.thedogapi.com/v1/");
    })
    .AddHttpMessageHandler<ApiKeyHandler>()
;
builder.Services
    .AddTransient<IHttpMessageHandlerBuilderFilter, GlobalHttpMessageHandlerBuilderFilter>()
    .AddTransient<GlobalLoggingHandler>()
    .AddTransient<ApiKeyHandler>()
;
builder.Services
    .AddSingleton(sp => sp.GetRequiredService<IOptions<DogApiSettings>>().Value)
    .AddOptionsWithValidateOnStart<DogApiSettings>()
    .Bind(builder.Configuration.GetSection("dogApi"))
;

var app = builder.Build();
app.MapGet("api/users/{userId}", async (int userId, JsonPlaceholderService jsonPlaceholderService, CancellationToken cancellationToken)
    => await jsonPlaceholderService.FetchUserAsync(userId, cancellationToken));
app.MapGet("api/dogs", async (DogService dogService, CancellationToken cancellationToken)
    => await dogService.FetchRandomDogsAsync(cancellationToken));
app.MapGet("api/dogs/{dogId}", async (string dogId, DogService dogService, CancellationToken cancellationToken)
    => await dogService.FetchDogAsync(dogId, cancellationToken));
app.Run();

public class GlobalLoggingHandler(ILogger<GlobalLoggingHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        logger.LogTrace($"Request Starting: {request.Method} {request.RequestUri}");
        var sw = Stopwatch.StartNew();
        var response = await base.SendAsync(request, cancellationToken);
        sw.Stop();
        logger.LogTrace("Response received in {responseTime} ms with StatusCode {StatusCode}.", sw.ElapsedMilliseconds, response.StatusCode);
        return response;
    }
}

public class ApiKeyHandler(ILogger<ApiKeyHandler> logger, DogApiSettings dogApiSettings) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        logger.LogTrace("Adding the 'x-api-key' HTTP header.");
        request.Headers.Add("x-api-key", dogApiSettings.ApiKey);
        return base.SendAsync(request, cancellationToken);
    }
}

public class GlobalHttpMessageHandlerBuilderFilter(IServiceProvider serviceProvider) : IHttpMessageHandlerBuilderFilter
{
    public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
    {
        return builder =>
        {
            builder.AdditionalHandlers.Add(serviceProvider.GetRequiredService<GlobalLoggingHandler>());
            next(builder);
        };
    }
}

public class JsonPlaceholderService(HttpClient client)
{
    public async Task<Dictionary<string, object>?> FetchUserAsync(int userId, CancellationToken cancellationToken)
    {
        var url = $"users/{userId}";
        return await client.GetFromJsonAsync<Dictionary<string, object>>(url, cancellationToken);
    }
}

public class DogService(HttpClient client)
{
    public async Task<Dictionary<string, object>?[]> FetchRandomDogsAsync(CancellationToken cancellationToken)
    {
        var url = $"images/search?page=0&limit=15&has_breeds=true&include_breeds=true&include_categories=true";
        return await client.GetFromJsonAsync<Dictionary<string, object>[]>(url, cancellationToken) ?? [];
    }

    public async Task<Dictionary<string, object>> FetchDogAsync(string dogId, CancellationToken cancellationToken)
    {
        var url = $"images/{dogId}";
        return await client.GetFromJsonAsync<Dictionary<string, object>?>(url, cancellationToken) ?? [];
    }
}

public class DogApiSettings
{
    public required string ApiKey { get; set; }
}