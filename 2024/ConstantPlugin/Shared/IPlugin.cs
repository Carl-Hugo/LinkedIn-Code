using Microsoft.Extensions.Logging;

namespace Shared;

public interface IPlugin
{
    void Execute(ILogger logger);
}