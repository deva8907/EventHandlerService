using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace EventHandler
{
    public class KafkaProducerService(KafkaProducerClient producerClient) : BackgroundService
    {
        private readonly KafkaProducerClient _producerClient = producerClient;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string topicName = "Orders";
            var @event = new OrderEvent()
            {
                OrderId = "some-order-id",
                EventType = "OrderCreated",
                Timestamp = DateTimeOffset.Now
            };
            await _producerClient.ProduceAsync(topicName, JsonSerializer.Serialize(@event));

            topicName = "Payments";
            @event = new OrderEvent()
            {
                OrderId = "some-order-id",
                EventType = "PaymentCompleted",
                Timestamp = DateTimeOffset.Now
            };
            await _producerClient.ProduceAsync(topicName, JsonSerializer.Serialize(@event));

            topicName = "Shipping";
            @event = new OrderEvent()
            {
                OrderId = "some-order-id",
                EventType = "OrderShipped",
                Timestamp = DateTimeOffset.Now
            };
            await _producerClient.ProduceAsync(topicName, JsonSerializer.Serialize(@event));
        }
    }
}
