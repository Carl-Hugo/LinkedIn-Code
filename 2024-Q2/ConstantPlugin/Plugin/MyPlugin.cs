using Microsoft.Extensions.Logging;
using Shared;

namespace Plugin;

public class MyPlugin : IPlugin
{
    public void Execute(ILogger logger)
    {
        logger.LogInformation($"Plugin using const: {Constants.MY_CONST}");
    }
}