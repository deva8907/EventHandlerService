using Microsoft.Extensions.Logging;

namespace EventHandler.EventProcessors
{
    public class OrderCreatedEventProcessor(ILogger<OrderCreatedEventProcessor> logger) : IEventProcessor
    {
        private readonly ILogger _logger = logger;

        public void Process(OrderEvent orderEvent)
        {
            _logger.LogInformation($"Processing {orderEvent.EventType}");
        }
    }
}
