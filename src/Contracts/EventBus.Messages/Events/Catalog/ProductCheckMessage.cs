using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events.Catalog
{
    public class ProductCheckMessage : ICommand
    {

        public List<string> ItemsId { get; set; }
        public Guid OrderId { get; set; }
    }
}
