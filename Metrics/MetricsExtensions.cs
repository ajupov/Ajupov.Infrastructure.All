using Ajupov.Infrastructure.All.Metrics.Collector;
using Ajupov.Infrastructure.All.Metrics.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;

namespace Ajupov.Infrastructure.All.Metrics
{
    public static class MetricsExtensions
    {
        public static IServiceCollection ConfigureMetrics(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .Configure<MetricsSettings>(configuration.GetSection("MetricsSettings"))
                .AddSingleton<IHostedService, MetricsCollector>();
        }

        public static IApplicationBuilder UseMetricsMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseHttpMetrics();
        }
    }
}