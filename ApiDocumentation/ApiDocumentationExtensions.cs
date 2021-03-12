using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace Ajupov.Infrastructure.All.ApiDocumentation
{
    public static class ApiDocumentationExtensions
    {
        public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
        {
            var info = new OpenApiInfo
            {
                Title = GetAssemblyName(),
                Version = GetAssemblyVersion()
            };

            return services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(info.Version, info);
                x.AddEnumsWithValuesFixFilters();
            });
        }


        public static IApplicationBuilder UseApiDocumentationsMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder
                .UseSwagger()
                .UseSwaggerUI(x =>
                    x.SwaggerEndpoint($"/swagger/{GetAssemblyVersion()}/swagger.json", GetAssemblyName()));
        }

        private static string GetAssemblyName()
        {
            return Assembly.GetCallingAssembly().GetName().Name;
        }

        private static string GetAssemblyVersion()
        {
            return Assembly.GetCallingAssembly().GetName().Version?.ToString(3) ?? "1.0.0";
        }
    }
}
