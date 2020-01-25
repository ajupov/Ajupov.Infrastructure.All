using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ajupov.Infrastructure.All.Migrations
{
    public static class MigrationsExtensions
    {
        public static IServiceCollection AddMigrator(this IServiceCollection services, IConfiguration configuration)
        {
            var isSkipMigrations = bool.Parse(configuration["IsSkipMigrations"]);
            if (isSkipMigrations)
            {
                return services;
            }

            var assembly = Assembly.GetCallingAssembly();

            services.AddFluentMigratorCore()
                .ConfigureRunner(x =>
                    x.AddPostgres()
                        .WithGlobalConnectionString(configuration.GetConnectionString("MigrationsConnectionString"))
                        .ScanIn(assembly)
                        .For.Migrations())
                .AddLogging(x => x.AddFluentMigratorConsole());

            return services;
        }

        public static IApplicationBuilder UseMigrationsMiddleware(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            scope.ServiceProvider
                .GetService<IMigrationRunner>()?
                .MigrateUp();

            return applicationBuilder;
        }
    }
}