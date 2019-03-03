using System.Threading.Tasks;

namespace EmailSenderApp.Api.Hubs.Interfaces
{
    public interface IMessagesHubClientActions
    {
        Task BroadcastMessageSentAction(string recipientEmail, string messageIdentifier);
    }
}