using System.Threading.Tasks;
using Infrastructure.All.MessageBroking.Models;

namespace Infrastructure.All.MessageBroking.Producing
{
    public interface IProducer
    {
        Task ProduceAsync(string topic, Message message);
    }
}