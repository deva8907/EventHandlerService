namespace EventHandler.EventProcessors
{
    public class OrderShippedEventProcessor : IEventProcessor
    {
        public void Process(OrderEvent orderEvent)
        {
            Console.WriteLine($"Processing {orderEvent.EventType}");
        }
    }
}
