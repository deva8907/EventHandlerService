using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventHandler.EventProcessors
{
    public class OrderCreatedEventProcessor(ILogger<OrderCreatedEventProcessor> logger, IOptions<KafkaConfiguration> options, KafkaProducerClient kafkaProducer) : 
        DeadLetterQueueTemplateProcessor( options, logger, kafkaProducer)
    {
        private readonly ILogger _logger = logger;

        protected override void ProcessEvent(OrderEvent orderEvent)
        {
            _logger.LogInformation($"Processing {orderEvent.EventType}");
            var response = GetShippingInformation(orderEvent);
            if (!response.IsSuccessStatusCode)
            {
                throw new RetryableException("Shipping information not available");
            }
        }

        private HttpResponseMessage GetShippingInformation(OrderEvent orderEvent)
        {
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
