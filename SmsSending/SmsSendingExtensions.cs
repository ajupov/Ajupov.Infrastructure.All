using Ajupov.Infrastructure.All.SmsSending.Settings;
using Ajupov.Infrastructure.All.SmsSending.SmsSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ajupov.Infrastructure.All.SmsSending
{
    public static class SmsSendingExtensions
    {
        public static IServiceCollection ConfigureSmsSending(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<SmsSendingSettings>(configuration.GetSection("SmsSendingSettings"))
                .AddSingleton<ISmsSender, SmsSender.SmsSender>()
                .AddHttpClient()
                .BuildServiceProvider();

            return services;
        }
    }
}