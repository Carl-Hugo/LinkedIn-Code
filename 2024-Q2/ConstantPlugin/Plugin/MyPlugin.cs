using Microsoft.Extensions.Logging;
using Shared;

namespace Plugin;

public class MyPlugin : IPlugin
{
    public void Execute(ILogger logger)
    {
        logger.LogInformation("Plugin using const: {const}", Constants.MY_CONST);
        logger.LogInformation("Plugin using readonly: {readonly}", Constants.MY_READONLY);
    }
}