using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Ajupov.Infrastructure.All.Logging
{
    public static class LoggingExtensions
    {
        private const string Template = "[{Timestamp:o} - {Level:u3}]: {Message:lj}{NewLine}{Exception}";

        public static IWebHostBuilder ConfigureLogging(this IWebHostBuilder hostBuilder)
        {
            var applicationName = Assembly.GetCallingAssembly().GetName().Name;
            var applicationVersion = Assembly.GetCallingAssembly().GetName().Name;

            return hostBuilder.ConfigureLogging(x =>
            {
                x.ClearProviders();

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .Enrich.FromLogContext()
                    .WriteTo.Console(outputTemplate: Template)
                    .WriteTo.File($"{applicationName}_{applicationVersion}_.txt", rollingInterval: RollingInterval.Day,
                        outputTemplate: Template, shared: true)
                    .CreateLogger();

                x.AddSerilog(Log.Logger);
            });
        }
    }
}