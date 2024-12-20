﻿using Microsoft.Extensions.Options;

namespace GlobalHttpHandler.Features.Dogs;

public class DogApiClient(HttpClient client)
{
    public async Task<Image[]> FetchRandomDogsAsync(CancellationToken cancellationToken)
    {
        var url = $"images/search?page=0&limit=15&has_breeds=true&include_breeds=true&include_categories=true";
        return await client.GetFromJsonAsync<Image[]>(url, cancellationToken) ?? [];
    }

    public async Task<Image?> FetchDogAsync(string dogId, CancellationToken cancellationToken)
    {
        var url = $"images/{dogId}";
        return await client.GetFromJsonAsync<Image>(url, cancellationToken);
    }
}

public class DogApiSettings
{
    public required string ApiKey { get; set; }
}

public class DogApiKeyHandler(ILogger<DogApiKeyHandler> logger, DogApiSettings dogApiSettings) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        logger.LogTrace("Adding the 'x-api-key' HTTP header.");
        request.Headers.Add("x-api-key", dogApiSettings.ApiKey);
        return base.SendAsync(request, cancellationToken);
    }
}

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddDogsServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddHttpClient<DogApiClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.thedogapi.com/v1/");
            })
            .AddHttpMessageHandler<DogApiKeyHandler>()
        ;
        builder.Services
            .AddTransient<DogApiKeyHandler>()
            .AddSingleton(sp => sp.GetRequiredService<IOptions<DogApiSettings>>().Value)
            .AddOptionsWithValidateOnStart<DogApiSettings>()
            .Bind(builder.Configuration.GetSection("dogApi"))
        ;
        return builder;
    }
}

public static class WebApplicationExtensions
{
    public static WebApplication MapDogsEndpoints(this WebApplication app)
    {
        app.MapGet("api/dogs", async (DogApiClient dogService, CancellationToken cancellationToken)
            => await dogService.FetchRandomDogsAsync(cancellationToken))
            .WithDisplayName("Fetch random dogs");

        app.MapGet("api/dogs/{dogId}", async (string dogId, DogApiClient dogService, CancellationToken cancellationToken)
            => await dogService.FetchDogAsync(dogId, cancellationToken))
            .WithDisplayName("Fetch the details of the specified dog");

        return app;
    }
}

public record Weight(string Imperial, string Metric);

public record Height(string Imperial, string Metric);

public record Breed(
    Weight Weight,
    Height Height,
    int Id,
    string Name,
    string? BredFor,
    string? BreedGroup,
    string LifeSpan,
    string Temperament,
    string? Origin,
    string ReferenceImageId
);

public record Image(
    string Id,
    string Url,
    int Width,
    int Height,
    List<Breed> Breeds
);
