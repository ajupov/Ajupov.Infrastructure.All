﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Ajupov.Infrastructure.All.MessageBroking.Models;

namespace Ajupov.Infrastructure.All.MessageBroking.Consuming
{
    public interface IConsumer
    {
        void Consume(string topic, Action<Message, CancellationToken> func);

        void Consume(string topic, Func<Message, CancellationToken, Task> action);

        void UnConsume();
    }
}