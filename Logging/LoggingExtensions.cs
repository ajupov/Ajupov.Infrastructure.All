using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Ajupov.Infrastructure.All.Logging
{
    public static class LoggingExtensions
    {
        public static IWebHostBuilder ConfigureLogging(this IWebHostBuilder hostBuilder, IConfiguration configuration)
        {
            var applicationName = Assembly.GetCallingAssembly().GetName().Name.ToLower();
            var applicationVersion = Assembly.GetCallingAssembly().GetName().Version;
            var host = configuration.GetValue<string>("LoggingHost");

            return hostBuilder.ConfigureLogging(x =>
            {
                x.ClearProviders();

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("applicationName", applicationName)
                    .Enrich.WithProperty("applicationVersion", applicationVersion)
                    .WriteTo.Elasticsearch(host, autoRegisterTemplate: true, indexFormat: applicationName)
                    .WriteTo.Console()
                    .CreateLogger();

                x.AddSerilog(Log.Logger);
            });
        }
    }
}