using System.Reflection;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Ajupov.Infrastructure.All.Logging
{
    public static class LoggingExtensions
    {
        public static IConfiguration ConfigureLogging(this IConfiguration configuration)
        {
            var applicationName = Assembly.GetCallingAssembly().GetName().Name.ToLower();
            var applicationVersion = Assembly.GetCallingAssembly().GetName().Version;
            var host = configuration.GetValue<string>("LoggingHost");

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("applicationName", applicationName)
                .Enrich.WithProperty("applicationVersion", applicationVersion)
                .WriteTo.Console();

            if (!string.IsNullOrWhiteSpace(host))
            {
                loggerConfiguration.WriteTo.Elasticsearch(host, applicationName, autoRegisterTemplate: true);
            }

            Log.Logger = loggerConfiguration.CreateLogger();

            return configuration;
        }
    }
}
