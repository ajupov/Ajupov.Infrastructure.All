using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.All.MailSending.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.All.MailSending.MailSender
{
    public class MailSender : IMailSender
    {
        private readonly MailSendingSettings _settings;

        public MailSender(IOptions<MailSendingSettings> options)
        {
            _settings = options.Value;
        }

        public Task SendAsync(
            string fromName,
            string fromAddress,
            string subject,
            IEnumerable<string> toAddresses,
            bool isBodyHtml,
            string body)
        {
            var from = new[]
                {new MailboxAddress(!string.IsNullOrWhiteSpace(fromName) ? fromName : string.Empty, fromAddress)};
            var to = toAddresses.Select(x => new MailboxAddress(x, x));
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