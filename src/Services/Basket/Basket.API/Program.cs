using Common.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;
using NServiceBus;
using EventBus.Messages.Common;
using Microsoft.Extensions.Configuration;
using EventBus.Messages.Events.Orde;

namespace Basket.API
{

    public class Program
    {

        public IConfiguration Configuration { get; }
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
            
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(SeriLogger.Configure)
                 .UseNServiceBus(context =>
                 {
                     var endpointConfiguration = new EndpointConfiguration(EventBusConstants.BasketCheckoutEndPoint);
                     var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                     var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();
                     transport.ConnectionString(context.Configuration["EventBusSettings:HostAddress"]);
                     transport.UseConventionalRoutingTopology();
                     transport.Routing().RouteToEndpoint(
                         assembly: typeof(PlaceOrderMessage).Assembly,                     
                         destination: EventBusConstants.OrderCheckoutEndPoint);

                     endpointConfiguration.SendOnly();
                     endpointConfiguration.EnableInstallers();
                     return endpointConfiguration;
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
