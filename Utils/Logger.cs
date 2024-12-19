namespace Diese.Utils;

internal static class Logger 
{
    static ILogger? _externalLogger;

    public static void SetupLogger(ILogger externalLogger)
    {
        _externalLogger = externalLogger;
    }

    public static void Log(string message)
    {
        _externalLogger?.Log(message);
    }

    public static void LogWarning(string message)
    {
        _externalLogger?.LogWarning(message);
    }
}