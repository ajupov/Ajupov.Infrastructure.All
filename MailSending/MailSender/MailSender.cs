using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ajupov.Infrastructure.All.MailSending.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Ajupov.Infrastructure.All.MailSending.MailSender
{
    public class MailSender : IMailSender
    {
        private readonly MailSendingSettings _settings;
        private readonly ILogger<MailSender> _logger;

        public MailSender(IOptions<MailSendingSettings> options, ILogger<MailSender> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public Task SendAsync(
            string fromName,
            string fromAddress,
            string subject,
            IEnumerable<string> toAddresses,
            bool isBodyHtml,
            string body)
        {
            var toAddressesArray = toAddresses as string[] ?? toAddresses.ToArray();
            var toAddressesJoined = string.Join(",", toAddressesArray);

            _logger.LogDebug("Send email. From name: {0}. From address: {1}. Subject: {2}. To addresses: {3}. " +
                             "Message: {4}.", fromName, fromAddress, subject, toAddressesJoined, body);

            if (_settings.IsTestMode)
            {
                return Task.CompletedTask;
            }

            var from = new[]
                {new MailboxAddress(!string.IsNullOrWhiteSpace(fromName) ? fromName : string.Empty, fromAddress)};
            var to = toAddressesArray.Select(x => new MailboxAddress(x, x));
            var textPart = new TextPart(isBodyHtml ? TextFormat.Html : TextFormat.Text) {Text = body};

            var mimeMessage = new MimeMessage(from, to, subject, textPart);

            return SendAsync(mimeMessage);
        }

        private async Task SendAsync(params MimeMessage[] messages)
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, true);
            await client.AuthenticateAsync(_settings.AccountName, _settings.Password);
            await Task.WhenAll(messages.Select(x => client.SendAsync(x)));
            await client.DisconnectAsync(true);
        }
    }
}