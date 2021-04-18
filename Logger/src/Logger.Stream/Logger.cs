using Logger.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Logger.Stream
{
    class Logger : ILogger
    {
        public System.IO.Stream LoggingStream { get; private set; }

        public Logger(System.IO.Stream stream)
        {
            this.LoggingStream = stream;
        }

        public void Log(LogLevel logLevel, string message)
        {
            using (var writer = new StreamWriter(this.LoggingStream, System.Text.Encoding.UTF8, 1024, true ))
            {
                writer.WriteLine(this.LogMessageFormatter(logLevel, message));
            }
        }

        public async Task LogAsync(LogLevel logLevel, string message)
        {
            using (var writer = new StreamWriter(this.LoggingStream, System.Text.Encoding.UTF8, 1024, true))
            {
                await writer.WriteLineAsync(this.LogMessageFormatter(logLevel, message));
            }
        }

        private string LogMessageFormatter(LogLevel logLevel, string message)
        {
            return $"{DateTime.UtcNow} {logLevel} {message}";
        }
    }
}
