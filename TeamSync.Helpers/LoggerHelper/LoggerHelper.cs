using Microsoft.Extensions.Logging;

namespace TeamSync.Helpers.LoggerHelper;

public class LoggerHelper :  ILoggerHelper
{
    private readonly ILogger<LoggerHelper> _logger;

    public LoggerHelper(ILogger<LoggerHelper> logger)
    {
        _logger = logger;
    }
    public void LogProprieties<T>(T data)
    {
        var type = typeof(T);
        foreach (var prop in type.GetProperties())
        {
            var value = prop.GetValue(data);
            _logger.LogInformation("{Property}: {Value}", prop.Name, value);
        }
    }
}