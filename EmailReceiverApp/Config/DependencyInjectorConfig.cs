using EmailReceiverApp.Handlers;
using EmailReceiverApp.Interfaces;
using EmailSenderApp.Options;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailReceiverApp.Config
{
    public static class DependencyInjectorConfig
    {
        public static void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
        {
            var queueOptions = new MessagesQueueOptions();
            configuration.GetSection(nameof(MessagesQueueOptions)).Bind(queueOptions);
            services.AddSingleton<IMessageReceiver>(new MessageReceiver(queueOptions.QueueConnectionString, queueOptions.QueueName));

            var sentEmailMessagesQueueOptions = new SentEmailMessagesQueueOptions();
            configuration.GetSection(nameof(SentEmailMessagesQueueOptions)).Bind(sentEmailMessagesQueueOptions);
            services.AddSingleton<IMessageSender>(new MessageSender(sentEmailMessagesQueueOptions.QueueConnectionString, sentEmailMessagesQueueOptions.QueueName));

            services.AddScoped<IMessageHandler, MessageHandler>();
        }
    }
}
