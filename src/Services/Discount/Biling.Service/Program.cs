using System;
using System.Threading.Tasks;
using EventBus.Messages.Common;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Biling.Service
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Sales";
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseNServiceBus(context =>
                {
                    var endpointConfiguration = new EndpointConfiguration(EventBusConstants.BillingEndPoint);
                    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                    var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();
                    transport.UseConventionalRoutingTopology();
                    transport.ConnectionString(context.Configuration["EventBusSettings:Host"]);
                    endpointConfiguration.EnableInstallers();
                    endpointConfiguration.EnableDurableMessages();
                    endpointConfiguration.SendFailedMessagesTo(EventBusConstants.BillingQueue + "-error-messages");
                    //transport.Routing().RouteToEndpoint(
                    //  assembly: typeof(ProductCheckMessage).Assembly,
                    //  destination: EventBusConstants.CatalogEndPoint);


                    return endpointConfiguration;
                });
        }
    }
}
