using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmailReceiverApp.ApiRequests;
using EmailReceiverApp.Interfaces;
using EmailSender.Dtos.Requests;
using EmailSenderApp.Options;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;

namespace EmailReceiverApp.Handlers
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IEmailSenderApi _emailSender;
        private readonly IMessageSender _messageSender;
        private readonly ILogger<MessageSender> _logger;
        private readonly EmailSenderOptions _emailSenderOptions;

        public MessageHandler(IMessageSender messageSender, ILogger<MessageSender> logger, IOptions<EmailSenderOptions> emailSenderOptions)
        {
            _messageSender = messageSender;
            _logger = logger;
            _emailSenderOptions = emailSenderOptions.Value;
            _emailSender = RestService.For<IEmailSenderApi>(_emailSenderOptions.BaseUrl);
        }

        public async Task HandleReceivedMessage(Message message, CancellationToken token)
        {
            var textMessage = Encoding.UTF8.GetString(message.Body);

            var messageReceived = JsonConvert.DeserializeObject<EmailMessageDto>(textMessage);

            await _emailSender.SendEmail(messageReceived);

            await NotifyThatRequestedMessageSent(messageReceived);
        }

        public async Task HandleExceptions(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            await Task.CompletedTask;
        }

        private async Task NotifyThatRequestedMessageSent(EmailMessageDto messageDto)
        {
            try
            {
                var serializedMessage = JsonConvert.SerializeObject(messageDto);

                var message = new Message(Encoding.UTF8.GetBytes(serializedMessage))
                {
                    CorrelationId = messageDto.Identifier.ToString()
                };
                await _messageSender.SendAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
