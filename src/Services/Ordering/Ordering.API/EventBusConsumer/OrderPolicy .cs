using EventBus.Messages.Common;
using EventBus.Messages.Events;
using EventBus.Messages.Events.Catalog;
using EventBus.Messages.Events.Orde;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Ordering.Domain.Entities;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer
{
    public class OrderPolicy : Saga<OrderPolicyData>,
          IAmStartedByMessages<OrderPlacedMessage>,
          IAmStartedByMessages<ProductConfirmedMessage>
    {
        private readonly ILogger<OrderPolicy> _logger;
        public OrderPolicy(ILogger<OrderPolicy> logger)
        {
            _logger = logger;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderPolicyData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.OrderId)
                    .ToMessage<OrderPlacedMessage>(message => message.OrderId)
                    .ToMessage<ProductConfirmedMessage>(message => message.OrderId);

        }

        public  async Task Handle(OrderPlacedMessage message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"oder placed, OrderId = {message.OrderId} - Sending message to Stock API....");
            Data.IsOrderPlaced = true;
            
            await context.Send(new ProductCheckMessage() { ItemsId = message.ItemsId, OrderId = message.OrderId });
            _ = ProcessOrder(context).ConfigureAwait(false);
        }

        public Task Handle(ProductConfirmedMessage message, IMessageHandlerContext context)
        {
            _logger.LogInformation($"Product ={message.ProductId} has in stock , OrderId = {message.OrderId} - Sending order to billing....");
            Data.hasProduct = message.hasProduct;
            return ProcessOrder(context);
        }

        private async Task ProcessOrder(IMessageHandlerContext context)
        {
            if (Data.IsOrderPlaced && Data.hasProduct && Data.IsOrderBilled )
            {
                await context.SendLocal(new PlaceOrderMessage() { OrderId = Data.OrderId });
                MarkAsComplete();
            }
        }
    }
}
