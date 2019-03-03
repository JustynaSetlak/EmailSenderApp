using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace EmailReceiverApp.Interfaces
{
    public interface IMessageHandler
    {
        Task HandleReceivedMessage(Message message, CancellationToken token);

        Task HandleExceptions(ExceptionReceivedEventArgs exceptionReceivedEventArgs);
    }
}
