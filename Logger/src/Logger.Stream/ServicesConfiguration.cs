using Logger.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Logger.Stream
{
    public static class ServicesConfiguration
    {
        public static void AddLoggerServiceStream(this IServiceCollection services, System.IO.Stream stream)
        {
            services.AddSingleton<ILogger>(s => new Logger(stream));
        }
    }
}
