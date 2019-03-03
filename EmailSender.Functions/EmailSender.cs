using System.IO;
using System.Threading.Tasks;
using EmailSender.Dtos.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailSender.Functions
{
    public static class EmailSender
    {
        [FunctionName("EmailSender")]
        public static async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ExecutionContext context)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var requestedMessageToSend = JsonConvert.DeserializeObject<EmailMessageDto>(requestBody);

            var key = GetKeyFromSettings(context);
            await SendEmail(key, requestedMessageToSend);
        }

        private static async Task SendEmail(string key, EmailMessageDto requestedMessageToSend)
        {
            var client = new SendGridClient(key);

            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("emailSender@mail.com", "EmailSender app"));
            msg.AddTo(requestedMessageToSend.To);
            msg.SetSubject(requestedMessageToSend.Title);
            msg.PlainTextContent = requestedMessageToSend.Body;

            await client.SendEmailAsync(msg);
        }

        private static string GetKeyFromSettings(ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var key = config["SendGridOptions:Key"];
            return key;
        }
    }
}
