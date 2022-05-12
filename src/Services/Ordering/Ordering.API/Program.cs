using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Ordering.API.Extensions;
using Serilog;
using Common.Logging;
using NServiceBus;
using EventBus.Messages.Events;
using EventBus.Messages.Common;
using EventBus.Messages.Events.Catalog;
using Ordering.Application.Behaviours;
using Ordering.Infra.Infrastructure.Persistence;

namespace Ordering.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<OrderContext>((context, services) =>
                {
                    var logger = services.GetService<ILogger<OrderContextSeed>>();
                    OrderContextSeed
                        .SeedAsync(context, logger)
                        .Wait();
                }).Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(SeriLogger.Configure)
                .UseNServiceBus( context =>
                {
                    var endpointConfiguration = new EndpointConfiguration(EventBusConstants.OrderCheckoutEndPoint);
                    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                    var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();
                    transport.UseConventionalRoutingTopology();
                    transport.ConnectionString(context.Configuration["EventBusSettings:HostAddress"]);
                    endpointConfiguration.AuditProcessedMessagesTo("targetAuditQueue");
                    endpointConfiguration.EnableInstallers();
                    endpointConfiguration.EnableDurableMessages();
                    endpointConfiguration.SendFailedMessagesTo(EventBusConstants.OrderCheckoutQueue + "-error-messages");
                    transport.Routing().RouteToEndpoint(
                      assembly: typeof(ProductCheckMessage).Assembly,
                      destination: EventBusConstants.CatalogEndPoint);


                    var pipeline = endpointConfiguration.Pipeline;
                    pipeline.Register(
                        behavior: new CustomErrorHandlingBehavior(),
                        description: "Manages thrown exceptions instead of delayed retries.");

                    return endpointConfiguration;
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

}
