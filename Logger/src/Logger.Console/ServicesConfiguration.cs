using Logger.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Console
{
	public static class ServicesConfiguration
	{
		public static void AddLoggerServiceConsole(this IServiceCollection services)
		{
			services.AddSingleton<ILogger, Logger>();
		}
	}
}
