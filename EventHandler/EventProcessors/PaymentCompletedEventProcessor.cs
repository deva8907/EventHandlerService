namespace EventHandler.EventProcessors
{
    public class PaymentCompletedEventProcessor : IEventProcessor
    {
        public void Process(OrderEvent orderEvent) 
        {
            Console.WriteLine($"Processing {orderEvent.EventType}");
        }
    }
}
