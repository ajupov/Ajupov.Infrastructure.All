using System.Threading;
using System.Threading.Tasks;
using Ajupov.Infrastructure.All.Metrics.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Prometheus;

namespace Ajupov.Infrastructure.All.Metrics.Collector
{
    public class MetricsCollector : IHostedService
    {
        private readonly KestrelMetricServer _metricsServer;

        public MetricsCollector(IOptions<MetricsSettings> options)
        {
            _metricsServer = new KestrelMetricServer(options.Value.Host, options.Value.Port);
        }

        public Task StartAsync(CancellationToken ct)
        {
            _metricsServer.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken ct)
        {
            return _metricsServer.StopAsync();
        }
    }
}