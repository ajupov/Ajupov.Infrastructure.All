using System.Reflection;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.Extensions.DependencyInjection;
using OpenTracing;
using OpenTracing.Util;

namespace Infrastructure.All.Tracing
{
    public static class TracingExtensions
    {
        public static IServiceCollection ConfigureTracing(this IServiceCollection services)
        {
            var applicationName = Assembly.GetCallingAssembly().GetName().Name;

            services.AddOpenTracing();
            services.AddSingleton<ITracer>(x =>
            {
                var tracer = new Tracer.Builder(applicationName)
                    .WithSampler(new ConstSampler(true))
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            return services;
        }
    }
}