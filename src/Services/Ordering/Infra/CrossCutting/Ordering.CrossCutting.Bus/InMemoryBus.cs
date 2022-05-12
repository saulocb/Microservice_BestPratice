using System.Threading.Tasks;
using Ordering.Application.Contracts.Bus;

namespace Ordering.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediator
    {
        private readonly MediatR.IMediator _mediator;

        public InMemoryBus(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> SendCommand<TCommand, TResponse>(TCommand command) 
            where TCommand : IBusCommand<TResponse> 
            where TResponse : BaseBusResponse
        {
            return await _mediator.Send(command);
        }

        public Task RaiseEvent<TEvent>(TEvent domainEvent) 
            where TEvent : IBusDomainEvent
        {
            return _mediator.Publish(domainEvent);
        }
    }
}
