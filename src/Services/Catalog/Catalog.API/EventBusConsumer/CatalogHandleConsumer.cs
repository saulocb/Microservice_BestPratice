using Catalog.Aplication.Features.Product.Queries.GetProductByListId;
using EventBus.Messages.Events.Catalog;
using MediatR;
using NServiceBus;
using System.Threading.Tasks;

namespace Catalog.API.EventBusConsumer
{
    public class CatalogHandleConsumer : IHandleMessages<ProductCheckMessage>
    {
        private readonly IMediator _mediator;

        public CatalogHandleConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(ProductCheckMessage message, IMessageHandlerContext context)
        {
            var hasproduct = true;
            var product = await _mediator.Send(new GetProductByListIdsQuery(message.ItemsId));
            if (product.Count > 0) hasproduct = false;

            if (message.ItemsId.Count != product.Count)
            {
                // send a message to UpdateOrderHandleMessage with the new itens, it will handle the message and send a midiator message to UpdateOrderHandleComand. 
            }
            await context.Publish(new ProductConfirmedMessage() { OrderId =  message.OrderId, hasProduct = hasproduct });
        }
    }
}
