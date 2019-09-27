using Infrastructure.All.MailSending.MailSender;
using Infrastructure.All.MailSending.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.All.MailSending
{
    public static class MailSendingExtensions
    {
        public static IServiceCollection ConfigureMailSending(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MailSendingSettings>(configuration.GetSection("MailSendingSettings"))
                .AddSingleton<IMailSender, global::Infrastructure.All.MailSending.MailSender.MailSender>()
                .BuildServiceProvider();

            return services;
        }
    }
}