using EmailSenderApp.Api.Config;
using EmailSenderApp.Api.Hubs;
using EmailSenderApp.Api.Hubs.Interfaces;
using EmailSenderApp.Api.MessagesHandlers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSenderApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSignalR();
            DependencyInjectionConfig.RegisterDependencies(services, Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMessageReceiver messageReceiver, IMessageHandler messageHandler)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSignalR(routes =>
            {
                routes.MapHub<MessagesHub>("/messagesHub");
            });

            MessageReceiverConfig.RegisterReceivingMessages(messageReceiver, messageHandler);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
