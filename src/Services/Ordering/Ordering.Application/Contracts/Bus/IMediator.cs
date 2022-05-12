using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Bus
{
    public interface IMediator
    {
        Task<TResponse> SendCommand<TCommand, TResponse>(TCommand command) where TCommand : IBusCommand<TResponse> where TResponse : BaseBusResponse;
        Task RaiseEvent<TEvent>(TEvent domainEvent) where TEvent : IBusDomainEvent;
    }
}