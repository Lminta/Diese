namespace Diese.Utils;

public interface ILogger
{
    void Log(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogException(Exception exception);
}