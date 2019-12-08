using System.Net.Http;
using System.Threading.Tasks;
using Ajupov.Infrastructure.All.SmsSending.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ajupov.Infrastructure.All.SmsSending.SmsSender
{
    public class SmsSender : ISmsSender
    {
        private readonly SmsSendingSettings _settings;
        private readonly ILogger<SmsSender> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public SmsSender(
            IOptions<SmsSendingSettings> options,
            ILogger<SmsSender> logger,
            IHttpClientFactory httpClientFactory)
        {
            _settings = options.Value;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> SendAsync(string phoneNumber, string message)
        {
            _logger.LogDebug("Send sms. To number: {0}. Message: {1}", phoneNumber, message);

            if (_settings.IsTestMode)
            {
                return "{\"status\": \"OK\"}";
            }

            using var client = _httpClientFactory.CreateClient();

            var uri = $"https://sms.ru/sms/send?api_id={_settings.ApiKey}&to={phoneNumber}&msg={message}&json=1";

            return await client.GetStringAsync(uri);
        }
    }
}