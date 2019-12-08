using System.Threading.Tasks;

namespace Ajupov.Infrastructure.All.SmsSending.SmsSender
{
    public interface ISmsSender
    {
        Task<string> SendAsync(string phoneNumber, string message);
    }
}