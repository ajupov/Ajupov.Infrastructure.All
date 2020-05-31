using Ajupov.Infrastructure.All.HotStorage.HotStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;

namespace Ajupov.Infrastructure.All.HotStorage
{
    public static class HotStorageExtensions
    {
        public static IServiceCollection AddHotStorage(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddSingleton<IHotStorage, HotStorage.HotStorage>()
                .AddSingleton<IRedisClientsManager>(x =>
                    new RedisManagerPool(configuration.GetConnectionString("HotStorageConnectionString")));
        }
    }
}
