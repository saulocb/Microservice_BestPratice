using AutoMapper;
using Basket.API.Entities;
using EventBus.Messages.Events.Orde;

namespace Basket.API.Mapper
{
    public class BasketProfile : Profile
	{
		public BasketProfile()
		{
			CreateMap<BasketCheckout, BasketCheckoutMessage>().ReverseMap();
			CreateMap<BasketCheckout, PlaceOrderMessage>().ReverseMap();
		}
	}
}
