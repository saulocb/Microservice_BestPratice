using MediatR;

namespace Ordering.Application.Contracts.Bus
{
    public interface IBusCommand<T> : IRequest<T> where T : BaseBusResponse
    {
     
    }
}