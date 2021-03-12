using System;
using System.Linq;
using Ajupov.Infrastructure.All.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Ajupov.Infrastructure.All.Api
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApiControllers(this IServiceCollection services, params Type[] filters)
        {
            services
                .AddControllers(x =>
                {
                    x.EnableEndpointRouting = false;
                    x.Filters.Add(typeof(ValidationFilter));
                    filters.ToList().ForEach(f => x.Filters.Add(f));
                });

            return services;
        }

        public static IServiceCollection AddApiControllersWithViews(
            this IServiceCollection services,
            params Type[] filters)
        {
            services
                .AddControllersWithViews(x =>
                {
                    x.EnableEndpointRouting = false;
                    x.Filters.Add(typeof(ValidationFilter));
                    x.Filters.Add(typeof(AutoValidateAntiforgeryTokenAttribute));
                    filters.ToList().ForEach(f => x.Filters.Add(f));
                });

            return services;
        }

        public static IApplicationBuilder UseControllers(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
