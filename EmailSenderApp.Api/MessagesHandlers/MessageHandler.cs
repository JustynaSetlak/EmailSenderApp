using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmailSender.Dtos.Requests;
using EmailSenderApp.Api.Hubs;
using EmailSenderApp.Api.Hubs.Interfaces;
using EmailSenderApp.Api.MessagesHandlers.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace EmailSenderApp.Api.MessagesHandlers
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IHubContext<MessagesHub, IMessagesHubClientActions> _messagesHubContext;

        public MessageHandler(IHubContext<MessagesHub, IMessagesHubClientActions> messagesHubContext)
        {
            _messagesHubContext = messagesHubContext;
        }

        public async Task HandleReceivedMessage(Message message, CancellationToken token)
        {
            var textMessage = Encoding.UTF8.GetString(message.Body);

            var sentEmail = JsonConvert.DeserializeObject<EmailMessageDto>(textMessage);

            await _messagesHubContext.Clients.All.BroadcastMessageSentAction(sentEmail.To, sentEmail.Identifier.ToString());

        }

        public Task HandleExceptions(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
