using MediatR;
using Ordering.Application.Contracts.Bus;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IBusCommand<BaseBusResponse>
    {
        public int Id { get; set; }
    }
}
