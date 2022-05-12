using System;
using NServiceBus;
using System.Threading.Tasks;


namespace Ordering.Domain.Entities
{
    public  class OrderPolicyData : ContainSagaData
    {
        public Guid OrderId { get; set; }
        public bool IsOrderPlaced { get; set; }
        public bool IsOrderBilled { get; set; }
        public bool hasProduct { get; set; }
    }
}
