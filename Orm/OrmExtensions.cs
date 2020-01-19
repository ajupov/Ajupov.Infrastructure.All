using Ajupov.Infrastructure.All.Orm.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ajupov.Infrastructure.All.Orm
{
    public static class OrmExtensions
    {
        public static IServiceCollection ConfigureOrm<TStorage>(
            this IServiceCollection services,
            IConfiguration configuration) where TStorage : Storage
        {
            services.Configure<OrmSettings>(configuration.GetSection(nameof(OrmSettings)));

            var isTestMode = configuration.GetSection(nameof(OrmSettings)).GetValue<bool>("IsTestMode");
            if (isTestMode)
            {
                services.AddEntityFrameworkInMemoryDatabase();
            }
            else
            {
                services.AddEntityFrameworkNpgsql();
            }

            return services.AddTransient<TStorage>();
        }
    }
}