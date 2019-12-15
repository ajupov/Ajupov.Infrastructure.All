using System.Reflection;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;

namespace Ajupov.Infrastructure.All.Tracing
{
    public static class TracingExtensions
    {
        public static IServiceCollection ConfigureTracing(this IServiceCollection services)
        {
            var applicationName = Assembly.GetCallingAssembly().GetName().Name.ToLower();

            services
                .AddOpenTracing()
                .AddSingleton<ITracer>(x =>
                {
                    var tracer = new Tracer.Builder(applicationName)
                        .WithLoggerFactory(x.GetRequiredService<ILoggerFactory>())
                        .WithSampler(new ConstSampler(true))
                        .Build();

                    GlobalTracer.Register(tracer);

                    return tracer;
                });

            return services;
        }
    }
}