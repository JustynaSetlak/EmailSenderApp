using EmailReceiverApp.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;

namespace EmailReceiverApp.Config
{
    public static class MessageReceiverConfig
    {
        public static void RegisterReceivingMessages(IMessageReceiver messageReceiver, IMessageHandler messageHandler)
        {
            messageReceiver.RegisterMessageHandler(messageHandler.HandleReceivedMessage, new MessageHandlerOptions(messageHandler.HandleExceptions));
        }
    }
}
