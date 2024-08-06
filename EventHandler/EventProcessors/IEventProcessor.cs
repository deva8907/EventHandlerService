namespace EventHandler.EventProcessors
{
    public interface IEventProcessor
    {
        void Process(OrderEvent orderEvent);
    }
}
