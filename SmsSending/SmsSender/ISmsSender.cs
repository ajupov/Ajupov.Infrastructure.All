using System.Threading.Tasks;

namespace Infrastructure.All.SmsSending.SmsSender
{
    public interface ISmsSender
    {
        Task SendAsync(string phoneNumber, string message);
    }
}