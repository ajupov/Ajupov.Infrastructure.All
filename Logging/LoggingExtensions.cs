using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Ajupov.Infrastructure.All.Logging
{
    public static class LoggingExtensions
    {
        private const string Template = "[{Timestamp:o} - {Level:u3}]: {Message:lj}{NewLine}{Exception}";

        public static IWebHostBuilder ConfigureLogging(this IWebHostBuilder hostBuilder, IConfiguration configuration)
        {
            var applicationName = Assembly.GetCallingAssembly().GetName().Name;
            var applicationVersion = Assembly.GetCallingAssembly().GetName().Version;
            var host = configuration.GetValue<string>("LoggingHost");

            return hostBuilder.ConfigureLogging(x =>
            {
                x.ClearProviders();

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .Enrich.FromLogContext()
                    .Enrich.With()
                    .WriteTo.Elasticsearch(host, autoRegisterTemplate: true)
                    .WriteTo.Console(outputTemplate: Template)
                    .CreateLogger();

                x.AddSerilog(Log.Logger);
            });
        }
    }
}