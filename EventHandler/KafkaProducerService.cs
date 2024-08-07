using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EventHandler
{
    public class KafkaProducerService : BackgroundService
    {
        private readonly string _bootstrapServers;

        public KafkaProducerService(IOptions<KafkaConfiguration> options)
        {
            _bootstrapServers = options.Value.ServerUrl;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string topicName = "Orders";
            var @event = new OrderEvent()
            {
                OrderId = "some-order-id",
                EventType = "OrderCreated",
                Timestamp = DateTimeOffset.Now
            };
            await ProduceAsync(topicName, JsonSerializer.Serialize(@event));

            topicName = "Payments";
            @event = new OrderEvent()
            {
                OrderId = "some-order-id",
                EventType = "PaymentCompleted",
                EventData = new EventPayload()
                {
                    PaymentMethod = "Cash"
                },
                Timestamp = DateTimeOffset.Now
            };
            await ProduceAsync(topicName, JsonSerializer.Serialize(@event));

            topicName = "Shipping";
            @event = new OrderEvent()
            {
                OrderId = "some-order-id",
                EventType = "OrderShipped",
                Timestamp = DateTimeOffset.Now
            };
            await ProduceAsync(topicName, JsonSerializer.Serialize(@event));
        }

        private async Task ProduceAsync(string topic, string message)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var deliveryReport = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }
    }
}
