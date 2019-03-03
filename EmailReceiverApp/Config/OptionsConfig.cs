using EmailSenderApp.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailReceiverApp.Config
{
    public static class OptionsConfig
    {
        public static void AddOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.Configure<EmailSenderOptions>(configuration.GetSection(nameof(EmailSenderOptions)));
        }
    }
}
