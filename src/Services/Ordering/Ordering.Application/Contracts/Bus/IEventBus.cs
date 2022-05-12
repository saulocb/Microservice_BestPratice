using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Bus
{
    public interface IEventBus
    {
        void AddEvent<TEvent>(TEvent integrationEvent) where TEvent : IBusIntegrationEvent;
        Task PublishEvents();
    }
}