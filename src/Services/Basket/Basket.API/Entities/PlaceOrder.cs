using System;

namespace Basket.API.Entities
{
    public class PlaceOrder
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomeId { get; set; }
    }
}
