using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EventHandler.EventProcessors
{
    public class PaymentWithCashCompletedEventProcessor(ILogger<PaymentWithCashCompletedEventProcessor> logger) : BaseEventProcessor
    {
        private readonly ILogger _logger = logger;

        public override bool CanProcess(ConsumeResult<Ignore, string> message)
        {
            OrderEvent orderEvent = JsonSerializer.Deserialize<OrderEvent>(message.Message.Value);
            return message.Topic.Equals("Payments") && orderEvent.EventType.Equals("PaymentCompleted") && orderEvent.EventData.PaymentMethod.Equals("Cash");
        }

        protected override void ProcessMessage(ConsumeResult<Ignore, string> message)
        {
            OrderEvent orderEvent = JsonSerializer.Deserialize<OrderEvent>(message.Message.Value);
            _logger.LogInformation($"Processing {orderEvent.EventType}");
        }
    }
}
