using Ordering.Application.Contracts.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOderResponse : BaseBusResponse
    {
        public CheckoutOderResponse(string orderId)
        {
            OrderId = orderId;
        }

        public string OrderId { get; set; }


    }
}
