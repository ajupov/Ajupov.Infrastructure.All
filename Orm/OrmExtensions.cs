using System;
using Ajupov.Infrastructure.All.Orm.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ajupov.Infrastructure.All.Orm
{
    public static class OrmExtensions
    {
        public static IServiceCollection AddOrm<TStorage>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TStorage : Storage
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.Configure<OrmSettings>(configuration.GetSection(nameof(OrmSettings)));
            services.AddEntityFrameworkNpgsql();

            return services.AddTransient<TStorage>();
        }
    }
}
