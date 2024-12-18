using Microsoft.Extensions.Http;
using System.Diagnostics;

namespace GlobalHttpHandler.Features.GlobalHttpMessageHandler;
public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddGlobalHttpMessageHandling(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddSingleton<IHttpMessageHandlerBuilderFilter, GlobalHttpMessageHandlerBuilderFilter>()
            .AddTransient<RequestLoggingHandler>()
            .AddTransient<ResponseTimeLoggingHandler>()
        ;
        return builder;
    }
}

public class GlobalHttpMessageHandlerBuilderFilter(IServiceProvider serviceProvider) : IHttpMessageHandlerBuilderFilter
{
    public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
    {
        return builder =>
        {
            builder.AdditionalHandlers.Add(serviceProvider.GetRequiredService<RequestLoggingHandler>());
            builder.AdditionalHandlers.Add(serviceProvider.GetRequiredService<ResponseTimeLoggingHandler>());
            next(builder);
        };
    }
}

public class RequestLoggingHandler(ILogger<RequestLoggingHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
    CancellationToken cancellationToken)
    {
        logger.LogTrace(
            "Sending request '{Method} {RequestUri}'",
            request.Method,
            request.RequestUri
        );
        var response = await base.SendAsync(request, cancellationToken);
        return response;
    }
}

public class ResponseTimeLoggingHandler(ILogger<ResponseTimeLoggingHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
    CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();
        var response = await base.SendAsync(request, cancellationToken);
        sw.Stop();
        logger.LogTrace(
            "The request '{Method} {RequestUri}' took in {responseTime} milliseconds to respond.",
            request.Method,
            request.RequestUri,
            sw.ElapsedMilliseconds
        );
        return response;
    }
}