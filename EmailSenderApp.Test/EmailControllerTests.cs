using System.Net;
using EmailSenderApp.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Threading.Tasks;
using EmailSender.Dtos.Requests;
using Xunit;
using Newtonsoft.Json;
using System.Net.Http;
using FluentAssertions;

namespace EmailSenderApp.Tests
{
    public class EmailControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private const string API_BASE = "/api/message";

        public EmailControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test()
        {
            //Arrange
            var client = _factory.CreateClient();
            var message = new EmailMessageDto
            {
                Body = "Hi",
                Title = "Welcome",
                To = "justa96.s1@gmail.com"
            };

            //Act
            var stringContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(API_BASE, stringContent);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        }

    }
}
