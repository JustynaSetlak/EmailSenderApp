using EmailSenderApp.Api.Interfaces;
using EmailSenderApp.Api.MessagesHandlers;
using EmailSenderApp.Api.MessagesHandlers.Interfaces;
using EmailSenderApp.Api.Services;
using EmailSenderApp.Options;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSenderApp.Api.Config
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMessageHandler, MessageHandler>();

            var queueOptions = new MessagesQueueOptions();
            configuration.GetSection(nameof(MessagesQueueOptions)).Bind(queueOptions);

            var sentMessagesQueueOptions = new SentEmailMessagesQueueOptions();
            configuration.GetSection(nameof(SentEmailMessagesQueueOptions)).Bind(sentMessagesQueueOptions);

            services.AddSingleton<IMessageSender>(new MessageSender(queueOptions.QueueConnectionString, queueOptions.QueueName));
            services.AddSingleton<IMessageReceiver>(new MessageReceiver(sentMessagesQueueOptions.QueueConnectionString, sentMessagesQueueOptions.QueueName));

        }
    }
}
