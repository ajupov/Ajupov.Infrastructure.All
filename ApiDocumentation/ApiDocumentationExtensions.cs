using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Infrastructure.All.ApiDocumentation
{
    public static class ApiDocumentationExtensions
    {
        private const string DefaultApiVersion = "v1";

        public static IServiceCollection ConfigureApiDocumentation(
            this IServiceCollection services,
            string apiVersion = DefaultApiVersion)
        {
            var info = new Info
            {
                Title = Assembly.GetCallingAssembly().GetName().Name,
                Version = apiVersion
            };

            return services.AddSwaggerGen(x => x.SwaggerDoc(apiVersion, info));
        }

        public static IApplicationBuilder UseApiDocumentationsMiddleware(
            this IApplicationBuilder applicationBuilder,
            string apiVersion = DefaultApiVersion)
        {
            var applicationName = Assembly.GetCallingAssembly().GetName().Name;

            return applicationBuilder
                .UseSwagger()
                .UseSwaggerUI(x => x.SwaggerEndpoint($"/swagger/{apiVersion}/swagger.json", applicationName));
        }
    }
}