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

        public Task SendAsync(string phoneNumber, string message)
        {
            _logger.LogDebug("Send sms. To number: {0}. Message: {1}.", phoneNumber, message);

            if (_settings.IsTestMode)
            {
                return Task.CompletedTask;
            }

            var client = new MainSmsClient(_settings.AccountName, _settings.ApiKey);

            return client.SendAsync(phoneNumber, message);
        }
    }
}