namespace EventHandler.EventProcessors
{
    public class OrderCreatedEventProcessor : IEventProcessor
    {
        public void Process(OrderEvent orderEvent)
        {
            Console.WriteLine($"Processing {orderEvent.EventType}");
        }
    }
}
