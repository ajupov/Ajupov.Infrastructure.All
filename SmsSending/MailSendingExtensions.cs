using Infrastructure.All.SmsSending.Settings;
using Infrastructure.All.SmsSending.SmsSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.All.SmsSending
{
    public static class MailSendingExtensions
    {
        public static IServiceCollection ConfigureMailSending(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<SmsSendingSettings>(configuration.GetSection("SmsSendingSettings"))
                .AddSingleton<ISmsSender, SmsSender.SmsSender>()
                .BuildServiceProvider();

            return services;
        }
    }
}