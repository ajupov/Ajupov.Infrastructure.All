using Infrastructure.All.HotStorage.HotStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;

namespace Infrastructure.All.HotStorage
{
    public static class MailSendingExtensions
    {
        public static IServiceCollection ConfigureHotStorage(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddSingleton<IHotStorage, global::Infrastructure.All.HotStorage.HotStorage.HotStorage>()
                .AddSingleton<IRedisClientsManager>(x =>
                    new RedisManagerPool(configuration.GetConnectionString("HotStorageConnectionString")));
        }
    }
}