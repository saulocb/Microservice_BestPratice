using AutoMapper;
using EventBus.Messages.Events.Orde;
//using MediatR;
using Microsoft.Extensions.Logging;
using NServiceBus;
using NServiceBus.Logging;
using Ordering.Application.Contracts.Bus;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using System;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer
{
    public class OrderHandleConsumer : IHandleMessages<PlaceOrderMessage>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderHandleConsumer> _logger;
        public OrderHandleConsumer(IMediator mediator,IMapper mapper, ILogger<OrderHandleConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Handle(PlaceOrderMessage message, IMessageHandlerContext context)
        {
            //create the order and save it into the orderdb 
            _logger.LogInformation($"Received place oder, OrderId = {message.OrderId} - Placing order....");
            var orderComand = _mapper.Map<CheckoutOrderCommand>(message);
            await _mediator.SendCommand<CheckoutOrderCommand, CheckoutOderResponse>(orderComand);
             
            // send a comand to saga to let it know we finished here so them the saga can go to the next step.
            var orderPlaced = _mapper.Map<OrderPlacedMessage>(orderComand);
            _ = context.Publish(orderPlaced);
        }
    }
    
}
