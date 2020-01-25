using Ajupov.Infrastructure.All.MessageBroking.Consuming;
using Ajupov.Infrastructure.All.MessageBroking.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ajupov.Infrastructure.All.MessageBroking
{
    public static class MessageBrokingExtensions
    {
        public static IServiceCollection AddConsumer<TConsumer>(
            this IServiceCollection services,
            IConfiguration configuration) where TConsumer : class, IHostedService
        {
            return services
                .Configure<MessageBrokingConsumerSettings>(
                    configuration.GetSection(nameof(MessageBrokingConsumerSettings)))
                .Configure<MessageBrokingProducerSettings>(
                    configuration.GetSection(nameof(MessageBrokingProducerSettings)))
                .AddSingleton<IConsumer, Consumer>()
                .AddSingleton<IHostedService, TConsumer>();
        }
    }
}