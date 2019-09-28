using Ajupov.Infrastructure.All.MailSending.MailSender;
using Ajupov.Infrastructure.All.MailSending.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ajupov.Infrastructure.All.MailSending
{
    public static class MailSendingExtensions
    {
        public static IServiceCollection ConfigureMailSending(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MailSendingSettings>(configuration.GetSection("MailSendingSettings"))
                .AddSingleton<IMailSender, global::Ajupov.Infrastructure.All.MailSending.MailSender.MailSender>()
                .BuildServiceProvider();

            return services;
        }
    }
}