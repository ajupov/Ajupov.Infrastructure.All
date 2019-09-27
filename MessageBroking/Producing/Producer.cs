using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Infrastructure.All.MessageBroking.Models;
using Infrastructure.All.MessageBroking.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.All.MessageBroking.Producing
{
    public class Producer : IProducer
    {
        private readonly KeyValuePair<string, string>[] _config;

        public Producer(IOptions<MessageBrokingProducerSettings> options)
        {
            _config = new[]
            {
                new KeyValuePair<string, string>("bootstrap.servers", options.Value.Host)
            };
        }

        public Task ProduceAsync(string topic, Message message)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();

            return producer.ProduceAsync(topic,
                new Message<Null, string> {Value = JsonConvert.SerializeObject(message)});
        }
    }
}