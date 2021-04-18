using Logger.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.File
{
	public static class ServicesConfiguration
	{
		public static void AddLoggerServiceFile(this IServiceCollection services, string fileDestination, string fileName)
		{
			// The task said should be 5000
			services.AddSingleton<ILogger>(s=> new Logger(fileDestination, fileName, 5000));
		}
	}
}
