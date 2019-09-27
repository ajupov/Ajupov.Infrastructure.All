using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Infrastructure.All.MessageBroking.Models;
using Infrastructure.All.MessageBroking.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.All.MessageBroking.Consuming
{
    public class Consumer : IConsumer
    {
        private readonly IConsumer<Null, string> _consumer;
        private bool _isWorking;

        public Consumer(IOptions<MessageBrokingConsumerSettings> options)
        {
            var consumerConfig = new ConsumerConfig
            {
                GroupId = "1",
                EnableAutoCommit = false,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                BootstrapServers = options.Value.Host
            };

            _consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
        }

        public void Consume(string topic, Action<Message, CancellationToken> func)
        {
            _consumer.Subscribe(topic);

            _isWorking = true;
            Consume(topic, (x, y) => Task.FromResult(func));
        }

        public void Consume(string topic, Func<Message, CancellationToken, Task> action)
        {
            while (_isWorking)
            {
                var result = JsonConvert.DeserializeObject<Message>(_consumer.Consume().Value);
                action(result, CancellationToken.None);
                _consumer.Commit();
            }
        }

        public void UnConsume()
        {
            _isWorking = false;
            _consumer.Unsubscribe();
            _consumer.Close();
        }
    }
}