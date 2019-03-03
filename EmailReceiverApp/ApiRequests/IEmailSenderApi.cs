using System.Threading.Tasks;
using EmailSender.Dtos.Requests;
using Refit;

namespace EmailReceiverApp.ApiRequests
{
    public interface IEmailSenderApi
    {
        [Post("/api/EmailSender")]
        Task SendEmail(EmailMessageDto message);
    }
}
