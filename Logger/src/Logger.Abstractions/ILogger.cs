using System.Threading.Tasks;

namespace Logger.Abstractions
{
    public interface ILogger
    {
        void Log(LogLevel logLevel, string message);
        Task LogAsync(LogLevel logLevel, string message);
    }
}
