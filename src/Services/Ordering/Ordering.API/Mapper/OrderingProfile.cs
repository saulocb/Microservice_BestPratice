using AutoMapper;
using EventBus.Messages.Events.Orde;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.Mapper
{
    public class OrderingProfile : Profile
	{
		public OrderingProfile()
		{
			CreateMap<CheckoutOrderCommand, BasketCheckoutMessage>().ReverseMap();
			CreateMap<PlaceOrderMessage, CheckoutOrderCommand>().ReverseMap();
			CreateMap<CheckoutOrderCommand, OrderPlacedMessage>().ReverseMap();
		}
	}
}
