using Logger.Abstractions;
using Logger.Console;
using System;
using System.Threading.Tasks;

namespace Logger.Console
{
    class Logger : ILogger
    {
        public void Log(LogLevel logLevel, string message)
        {
            if (message.Length > 1000)
            { 
                throw new ConsoleMessageArgumentOutOfRangeException("The message must be under 1000 character.");
            }

            ConsoleColor originalColor = System.Console.ForegroundColor;

            switch (logLevel)
            {
                // gray
                case LogLevel.Debug:
                    System.Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                // green
                case LogLevel.Information:
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    break;
                // red
                case LogLevel.Error:
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    System.Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }

            System.Console.WriteLine(LogFormatter(logLevel, message));
            System.Console.ForegroundColor = originalColor;
        }

        public async Task LogAsync(LogLevel logLevel, string message)
        {
            if (message.Length > 1000)
            {
                throw new ConsoleMessageArgumentOutOfRangeException("The message must be under 1000 character.");
            }

            ConsoleColor originalColor = System.Console.ForegroundColor;

            switch (logLevel)
            {
                // gray
                case LogLevel.Debug:
                    System.Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                // green
                case LogLevel.Information:
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    break;
                // red
                case LogLevel.Error:
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    System.Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }

            await System.Console.Out.WriteLineAsync(LogFormatter(logLevel, message));
            System.Console.ForegroundColor = originalColor;
        }

        private string LogFormatter(LogLevel logLevel, string message)
        {
            return $"{DateTime.UtcNow} {logLevel} {message}";
        }
    }
}
