using Microsoft.Extensions.Http;
using System.Diagnostics;

namespace GlobalHttpHandler.Features.GlobalHttpMessageHandler;
public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddGlobalHttpMessageHandling(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddTransient<IHttpMessageHandlerBuilderFilter, GlobalHttpMessageHandlerBuilderFilter>()
            .AddTransient<GlobalLoggingHandler>()
        ;
        return builder;
    }
}

public class GlobalLoggingHandler(ILogger<GlobalLoggingHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        logger.LogTrace("Request Starting: {Method} {RequestUri}", request.Method, request.RequestUri);
        var sw = Stopwatch.StartNew();
        var response = await base.SendAsync(request, cancellationToken);
        sw.Stop();
        logger.LogTrace("Response received in {responseTime} milliseconds with StatusCode {StatusCode}.", sw.ElapsedMilliseconds, response.StatusCode);
        return response;
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