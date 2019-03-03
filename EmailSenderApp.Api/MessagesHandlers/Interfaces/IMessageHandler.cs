using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace EmailSenderApp.Api.MessagesHandlers.Interfaces
{
    public interface IMessageHandler
    {
        Task HandleReceivedMessage(Message message, CancellationToken token);

        Task HandleExceptions(ExceptionReceivedEventArgs exceptionReceivedEventArgs);
    }
}
