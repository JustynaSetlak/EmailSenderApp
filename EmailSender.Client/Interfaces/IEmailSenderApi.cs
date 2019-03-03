using System.Threading.Tasks;
using EmailSender.Dtos.Requests;
using RestEase;

namespace EmailSender.Client.Interfaces
{
    public interface IEmailSenderApi
    {
        [Post("api/message")]
        Task<string> SendMessageAsync([Body] EmailMessageDto emailMessage);
    }
}