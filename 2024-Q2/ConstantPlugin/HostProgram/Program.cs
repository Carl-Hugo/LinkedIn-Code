using Shared;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.MapGet("/load-plugins", (IWebHostEnvironment hostingEnvironment, ILoggerFactory loggerFactory) =>
{
    var pluginsDirectory = Path.Combine(hostingEnvironment.ContentRootPath, "Plugins");
    var pluginAssemblies = Directory.GetFiles(pluginsDirectory, "*.dll");
    var pluginType = typeof(IPlugin);
    foreach (var pluginPath in pluginAssemblies)
    {
        var pluginTypes = Assembly.LoadFrom(pluginPath)
            .GetTypes()
            .Where(type => pluginType.IsAssignableFrom(type) && !type.IsInterface)
        ;
        foreach (var type in pluginTypes)
        {
            var logger = loggerFactory.CreateLogger(type);
            var plugin = Activator.CreateInstance(type) as IPlugin;
            plugin?.Execute(logger);
        }
    }
    var programLogger = loggerFactory.CreateLogger("Program");
    programLogger.LogInformation("Program using const: {const}", Constants.MY_CONST);
    return Results.Ok($"Plugins loaded and executed. Current constant value: {Constants.MY_CONST}");
});

app.Run();
