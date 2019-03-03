using System;
using System.Text;
using System.Threading.Tasks;
using EmailSender.Dtos.Requests;
using EmailSenderApp.Api.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmailSenderApp.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMessageSender _messageSender;
        private readonly ILogger<MessageSender> _logger;

        public EmailService(IMessageSender messageSender, ILogger<MessageSender> logger)
        {
            _messageSender = messageSender;
            _logger = logger;
        }

        public async Task SendMessage(EmailMessageDto messageDto)
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
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
