using Ajupov.Infrastructure.All.SmsSending.Settings;
using Ajupov.Infrastructure.All.SmsSending.SmsSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ajupov.Infrastructure.All.SmsSending
{
    public static class SmsSendingExtensions
    {
        public static IServiceCollection AddSmsSending(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddHttpClient()
                .Configure<SmsSendingSettings>(configuration.GetSection(nameof(SmsSendingSettings)))
                .AddSingleton<ISmsSender, SmsSender.SmsSender>();
        }
    }
}