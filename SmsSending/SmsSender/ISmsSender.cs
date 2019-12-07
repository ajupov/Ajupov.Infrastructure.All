using System.Threading.Tasks;

namespace Ajupov.Infrastructure.All.SmsSending.SmsSender
{
    public interface ISmsSender
    {
        Task SendAsync(string phoneNumber, string fromName, string message, bool isTestSms);
    }
}