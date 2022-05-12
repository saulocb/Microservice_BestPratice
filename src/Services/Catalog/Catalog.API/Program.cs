
using Common.Logging;
using EventBus.Messages.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using Serilog;

namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(SeriLogger.Configure)
                .UseNServiceBus(context =>
                {
                    var endpointConfiguration = new EndpointConfiguration(EventBusConstants.CatalogEndPoint);
                    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                    var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();
                    transport.UseConventionalRoutingTopology();
                    transport.ConnectionString(context.Configuration["EventBusSettings:HostAddress"]);

                    endpointConfiguration.SendFailedMessagesTo("CatalogErrorQueue");
                    endpointConfiguration.EnableInstallers();

                    return endpointConfiguration;
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
