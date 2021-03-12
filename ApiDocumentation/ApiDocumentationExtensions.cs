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
            var assembly = Assembly.GetCallingAssembly();

            var info = new OpenApiInfo
            {
                Title = GetAssemblyName(assembly),
                Version = GetAssemblyVersion(assembly)
            };

            return services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(info.Version, info);
                x.AddEnumsWithValuesFixFilters();
            });
        }

        public static IApplicationBuilder UseApiDocumentationsMiddleware(this IApplicationBuilder applicationBuilder)
        {
            var assembly = Assembly.GetCallingAssembly();

            return applicationBuilder
                .UseSwagger()
                .UseSwaggerUI(x =>
                    x.SwaggerEndpoint($"/swagger/{GetAssemblyVersion(assembly)}/swagger.json",
                        GetAssemblyName(assembly)));
        }

        private static string GetAssemblyName(Assembly assembly)
        {
            return assembly.GetName().Name;
        }

        private static string GetAssemblyVersion(Assembly assembly)
        {
            return assembly.GetName().Version?.ToString(3) ?? "1.0.0";
        }
    }
}
