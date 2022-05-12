using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class OrderException :
        Exception
    {
        public OrderException()
        {
        }

        public OrderException(string message) :
            base(message)
        {
        }

        public OrderException(string message, Exception innerException) :
            base(message, innerException)
        {
        }

        protected OrderException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
    }
}
