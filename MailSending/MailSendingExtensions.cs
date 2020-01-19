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
            return services
                .Configure<MailSendingSettings>(configuration.GetSection(nameof(MailSendingSettings)))
                .AddSingleton<IMailSender, MailSender.MailSender>();
        }
    }
}