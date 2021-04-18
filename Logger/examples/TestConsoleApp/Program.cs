using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool consoleLoggerTest = false;
            bool fileLoggerTest = false;
            bool streamLoggerTest = true;

            Console.WriteLine("Hello World!");

            if (consoleLoggerTest == true)
            {
                // Test Console Logger
                Logger.Console.Logger loggerConsole = new Logger.Console.Logger();

                loggerConsole.Log(Logger.Abstractions.LogLevel.Information, "Information");
                loggerConsole.Log(Logger.Abstractions.LogLevel.Debug, "Debug");
                loggerConsole.Log(Logger.Abstractions.LogLevel.Error, "Error");

                await loggerConsole.LogAsync(Logger.Abstractions.LogLevel.Information, "Information LogAsync");
                await loggerConsole.LogAsync(Logger.Abstractions.LogLevel.Debug, "Debug LogAsync");
                await loggerConsole.LogAsync(Logger.Abstractions.LogLevel.Error, "Error LogAsync");
            }

            if (fileLoggerTest == true)
            {
                // Test File Logger
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Logger.File.Logger loggerFile = new Logger.File.Logger(path, "log.txt", 100);

                for (int i = 0; i < 50; i++)
                {
                    await Task.Delay(100);
                    //loggerFile.Log(Logger.Abstractions.LogLevel.Information, $"Information {i}");
                    //await loggerFile.LogAsync(Logger.Abstractions.LogLevel.Information, $"Information {i}");
                }
            }

            if (streamLoggerTest == true)
            {
                string pathStream = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                FileStream memoryStream = new FileStream(Path.Combine(pathStream,"mylog.txt"), FileMode.Append);
                // Test Console Logger
                Logger.Stream.Logger loggerStream = new Logger.Stream.Logger(memoryStream);

                for (int i = 0; i < 50; i++)
                {
                    await Task.Delay(100);
                    //loggerStream.Log(Logger.Abstractions.LogLevel.Information, "Information");
                    await loggerStream.LogAsync(Logger.Abstractions.LogLevel.Error, "Error LogAsync");
                }
            }

            Console.ReadLine();
        }
    }
}
