using System.Threading.Tasks;
using EmailSender.Dtos.Requests;
using EmailSenderApp.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderApp.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/message")]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [ProducesResponseType((int)StatusCodes.Status202Accepted)]
        public async Task<IActionResult> SendMessage([FromBody] EmailMessageDto message)
        {
            await _emailService.SendMessage(message);

            return Accepted(message.Identifier);
        }
    }
}