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
            var isInMemoryHotStorage = configuration.GetValue<bool>("IsInMemoryHotStorage");

            if (isInMemoryHotStorage)
            {
                services
                    .AddSingleton<IHotStorage, InMemoryHotStorage>();
            }
            else
            {
                services
                    .AddSingleton<IHotStorage, RedisHotStorage>()
                    .AddSingleton<IRedisClientsManager>(_ =>
                        new RedisManagerPool(configuration.GetConnectionString("HotStorageConnectionString")));
            }

            return services;
        }
    }
}
