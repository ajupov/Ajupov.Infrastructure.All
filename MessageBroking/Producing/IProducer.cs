using System.Threading.Tasks;
using Ajupov.Infrastructure.All.MessageBroking.Models;

namespace Ajupov.Infrastructure.All.MessageBroking.Producing
{
    public interface IProducer
    {
        Task ProduceAsync(string topic, Message message);
    }
}