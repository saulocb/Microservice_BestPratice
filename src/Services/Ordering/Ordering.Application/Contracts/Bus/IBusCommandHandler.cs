using MediatR;

namespace Ordering.Application.Contracts.Bus
{
    public interface IBusCommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> 
        where TCommand : IBusCommand<TResponse> where TResponse : BaseBusResponse
    {

    }
}