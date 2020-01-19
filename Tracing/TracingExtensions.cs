using System.Reflection;
using Ajupov.Infrastructure.All.Tracing.Settings;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTracing;
using OpenTracing.Util;

namespace Ajupov.Infrastructure.All.Tracing
{
    public static class TracingExtensions
    {
        public static IServiceCollection ConfigureTracing(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var applicationName = Assembly.GetCallingAssembly().GetName().Name.ToLower();

            services
                .AddOpenTracing()
                .Configure<TracingSettings>(configuration.GetSection(nameof(TracingSettings)))
                .AddSingleton<ITracer>(x =>
                {
                    var loggerFactory = x.GetRequiredService<ILoggerFactory>();
                    var options = x.GetService<IOptions<TracingSettings>>().Value;

                    var senderConfig = new Jaeger.Configuration.SenderConfiguration(loggerFactory)
                        .WithAgentHost(options.AgentHost)
                        .WithAgentPort(options.AgentPort);

                    var reporter = new RemoteReporter.Builder()
                        .WithLoggerFactory(loggerFactory)
                        .WithSender(senderConfig.GetSender())
                        .Build();

                    var sampler = new GuaranteedThroughputSampler(options.SamplingRate, options.LowerBound);

                    var tracer = new Tracer.Builder(applicationName)
                        .WithLoggerFactory(loggerFactory)
                        .WithSampler(new ConstSampler(true))
                        .WithReporter(reporter)
                        .WithSampler(sampler)
                        .Build();

                    GlobalTracer.Register(tracer);

                    return tracer;
                });

            return services;
        }
    }
}