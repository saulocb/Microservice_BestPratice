using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Pipeline;
using Ordering.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    public class CustomErrorHandlingBehavior :
        Behavior<ITransportReceiveContext>
    {
        static ILog Log = LogManager.GetLogger(typeof(CustomErrorHandlingBehavior));

       
        public override async Task Invoke(ITransportReceiveContext context, Func<Task> next)
        {
            try
            {
                await next()
                    .ConfigureAwait(false);
            }
            catch (OrderException)
            {
                // Ignore the exception, avoid doing this in a production code base
                Log.WarnFormat("MyCustomException was thrown. Ignoring the error for message Id {0}.", context.Message.MessageId);
            }
            catch (MessageDeserializationException deserializationException)
            {
                // Custom processing that needs to occur when a serialization failure occurs.
                Log.Error("Message deserialization failed", deserializationException);
            }
            catch (Exception ex)
            {
                //Throwing will eventually send the message to the error queue
                Log.Error("Message failed.", ex);
                throw;
            }
        }
    }
}