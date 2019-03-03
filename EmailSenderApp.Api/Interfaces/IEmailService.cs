using System.Threading.Tasks;
using EmailSender.Dtos.Requests;

namespace EmailSenderApp.Api.Interfaces
{
    public interface IEmailService
    {
        Task SendMessage(EmailMessageDto messageToAdd);
    }
}
