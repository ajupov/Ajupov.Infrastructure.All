using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ajupov.Infrastructure.All.Cors
{
    public static class CorsExtensions
    {
        private const string SingleOriginCorsPolicy = "SingleOriginCorsPolicy";

        public static IServiceCollection AddSingleOriginCorsPolicy(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var allowedCorsOrigin = configuration["AllowedCorsOrigin"];
            if (string.IsNullOrWhiteSpace(allowedCorsOrigin))
            {
                throw new Exception("AllowedCorsOrigin is empty");
            }

            return services
                .AddCors(x => x.AddPolicy(SingleOriginCorsPolicy, builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins(allowedCorsOrigin);
                }));
        }

        public static IApplicationBuilder UseSingleOriginCors(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder
                .UseCors(SingleOriginCorsPolicy);
        }
    }
}