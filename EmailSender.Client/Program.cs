using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmailSender.Client.Interfaces;
using EmailSender.Dtos.Requests;
using Microsoft.AspNetCore.SignalR.Client;
using RestEase;

namespace EmailSender.Client
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var baseUrl = "http://localhost:54987";
                var emailToSend = new EmailMessageDto
                {
                    Body = "Sending test message",
                    Title = "EmailSenderApp test",
                    To = "justa96.s1@gmail.com"
                };
                await HandleSignalRConfiguration(baseUrl);

                var emailSenderApi = RestClient.For<IEmailSenderApi>(baseUrl);
                await emailSenderApi.SendMessageAsync(emailToSend);

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error. Something wrong happend");
            }
        }

        private static async Task HandleSignalRConfiguration(string baseUrl)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl($"{baseUrl}/messagesHub")
                .Build();

            connection.On<string, string>("BroadcastMessageSentAction", (to, sentMessageIdentifier) =>
            {
                Console.WriteLine($"Message to {to} with identifier: {sentMessageIdentifier} was sent successfully. :-)");
            });

            await connection.StartAsync();
        }

    }
}
