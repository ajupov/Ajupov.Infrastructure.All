using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ajupov.Infrastructure.All.Mvc
{
    public static class MvcExtensions
    {
        public static IServiceCollection AddControllers(this IServiceCollection services, params Type[] filters)
        {
            services
                .AddControllers(x =>
                {
                    x.EnableEndpointRouting = false;
                    filters.ToList().ForEach(f => x.Filters.Add(f));
                });

            return services;
        }

        public static IApplicationBuilder UseMvcMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
