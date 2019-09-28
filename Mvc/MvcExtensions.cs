using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Ajupov.Infrastructure.All.Mvc
{
    public static class MvcExtensions
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services, params Type[] filters)
        {
            services
                .AddMvc(x =>
                {
                    x.EnableEndpointRouting = false;
                    x.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    filters.ToList().ForEach(f => x.Filters.Add(f));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddDataProtection();

            return services;
        }

        public static IApplicationBuilder UseMvcMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMvc();
        }
    }
}