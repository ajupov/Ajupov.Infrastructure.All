using System.Text;
using System.Threading.Tasks;
using Ajupov.Infrastructure.All.SmsSending.Settings;
using MainSMS;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ajupov.Infrastructure.All.SmsSending.SmsSender
{
    public class SmsSender : ISmsSender
    {
        private readonly SmsSendingSettings _settings;
        private readonly ILogger<SmsSender> _logger;

        public SmsSender(IOptions<SmsSendingSettings> options, ILogger<SmsSender> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public Task SendAsync(string phoneNumber, string fromName, string message, bool isTestSms)
        {
            _logger.LogDebug("Send sms. To number: {0}. Message: {1}. From name: {2}. Is test: {3}",
                phoneNumber, message, fromName, isTestSms);

            if (_settings.IsTestMode)
            {
                return Task.CompletedTask;
            }

            var utf8Message = Encoding.UTF8.GetString(Encoding.Default.GetBytes(message));
            
            var client = new MainSmsClient(_settings.AccountName, _settings.ApiKey);

            return client.SendAsync(phoneNumber, utf8Message, fromName, testMode: isTestSms);
        }
    }
}