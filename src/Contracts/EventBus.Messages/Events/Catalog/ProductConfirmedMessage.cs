using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events.Catalog
{
    public class ProductConfirmedMessage: IEvent
    {
        public string ProductId { get; set; }
        public Guid OrderId { get; set; }
        public bool hasProduct { get; set; }
    }
}
