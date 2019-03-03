using EmailSenderApp.Api.MessagesHandlers.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;

namespace EmailSenderApp.Api.Config
{
    public static class MessageReceiverConfig
    {
        public static void RegisterReceivingMessages(IMessageReceiver messageReceiver, IMessageHandler messageHandler)
        {
            messageReceiver.RegisterMessageHandler(messageHandler.HandleReceivedMessage, new MessageHandlerOptions(messageHandler.HandleExceptions));
        }
    }
}
