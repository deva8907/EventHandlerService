using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Numerics;
using System.Text.Json;

namespace EventHandler.EventProcessors
{
    public abstract class DeadLetterQueueTemplateProcessor(IOptions<KafkaConfiguration> options, ILogger<DeadLetterQueueTemplateProcessor> logger, 
        KafkaProducerClient kafkaProducer) : IEventProcessor
    {
        private readonly KafkaProducerClient _kafkaProducer = kafkaProducer;
        private readonly int _maxRetry = options.Value.MaxRetry;
        private readonly ILogger _logger = logger;
        
        public void Process(OrderEvent orderEvent)
        {
            int retryCount = 0;

            while (retryCount < _maxRetry)
            {
                try
                {
                    ProcessEvent(orderEvent);
                    break;
                }
                catch (RetryableException ex)
                {
                    _logger.LogError(ex, "Failed to process message");
                    retryCount++;
                } 
            }
            if (retryCount == _maxRetry)
            {
                _kafkaProducer.ProduceAsync("Orders-DLQ", JsonSerializer.Serialize(orderEvent)).GetAwaiter().GetResult();
            }
        }

        protected abstract void ProcessEvent(OrderEvent orderEvent);
    }
}
