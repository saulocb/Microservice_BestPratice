using System;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseMessage
    {
        public IntegrationBaseMessage()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
        
        public IntegrationBaseMessage(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }
        
        public Guid Id { get; private set; }
        
        public DateTime CreationDate { get; private set; }
    }
}
