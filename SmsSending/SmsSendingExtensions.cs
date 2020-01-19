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
            return services
                .Configure<SmsSendingSettings>(configuration.GetSection(nameof(SmsSendingSettings)))
                .AddSingleton<ISmsSender, SmsSender.SmsSender>()
                .AddHttpClient();
        }
    }
}