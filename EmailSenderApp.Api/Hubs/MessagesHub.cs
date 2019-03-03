using System.Threading.Tasks;
using EmailSenderApp.Api.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace EmailSenderApp.Api.Hubs
{
    public class MessagesHub : Hub<IMessagesHubClientActions>
    {

    }
}
