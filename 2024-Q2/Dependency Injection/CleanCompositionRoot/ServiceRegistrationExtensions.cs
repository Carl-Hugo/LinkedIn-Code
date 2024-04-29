using Microsoft.Extensions.DependencyInjection;

namespace MyApplication.Extensions;
public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
    {
        services.AddScoped<IDataRepository, SqlDataRepository>();
        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<ILoggingService, FileLoggingService>();
        services.AddTransient<IEmailService, SmtpEmailService>();
        return services;
    }
}