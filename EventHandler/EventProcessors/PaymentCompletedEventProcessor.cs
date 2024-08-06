using Microsoft.Extensions.Logging;

namespace EventHandler.EventProcessors
{
    public class PaymentCompletedEventProcessor(ILogger<PaymentCompletedEventProcessor> logger) : IEventProcessor
    {
        private readonly ILogger _logger = logger;
        public void Process(OrderEvent orderEvent) 
        {
            _logger.LogInformation($"Processing {orderEvent.EventType}");
        }
    }
}
