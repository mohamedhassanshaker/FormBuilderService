using FormBuilderService.EventSourcing.Events.Abstraction;
using SharedKernel.Utilities.Logging.Services;

namespace FormBuilderService.EventSourcing.Events
{
    public class EventsHandler
    {
        public void PublishEvent(IEventType eventType, object data)
        {
            try
            {
                var newMsg = new DomainEventMessage(eventType, data);
            }
            catch (Exception ex)
            {
                LogService.LogError(ex);
            }
        }
    }
}