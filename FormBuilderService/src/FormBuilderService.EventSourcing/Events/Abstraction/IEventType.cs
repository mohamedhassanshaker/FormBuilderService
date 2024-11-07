namespace FormBuilderService.EventSourcing.Events.Abstraction
{
    public interface IEventType
    {
        public string EventType { get; set; }
    }
}