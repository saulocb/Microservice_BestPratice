using Ordering.Application.Contracts.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public  class ListOrderQueryResponse : BaseBusResponse
    {
        public ListOrderQueryResponse(List<OrdersVm> listOrder)
        {
            ListOrder = listOrder ?? throw new ArgumentNullException(nameof(listOrder));
        }
        public List<OrdersVm> ListOrder { get; set; }
    }
}
