using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EventHandler
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly ConsumerConfig _config;
        private readonly EventProcessorFactory _factory;
        private readonly IEnumerable<string> _topics;

        public KafkaConsumerService(IOptions<KafkaConfiguration> options, EventProcessorFactory factory)
        {
            _factory = factory;
            _config = new ConsumerConfig()
            {
                BootstrapServers = options.Value.ServerUrl,
                GroupId = "test-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _topics = options.Value.Topics;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
            consumer.Subscribe(_topics);
            while (true)
            {
                var result = consumer.Consume(stoppingToken);
                Process(result);
            }
        }

        private void Process(ConsumeResult<Ignore, string> result)
        {
            if (result == null)
                return;

            OrderEvent orderEvent = DeserializeEvent(result.Message.Value);
            var processor = _factory.GetEventProcessor(result.Topic, orderEvent.EventType);
            processor.Process(orderEvent);
        }

        private static OrderEvent DeserializeEvent(string value)
        {
            return JsonSerializer.Deserialize<OrderEvent>(value);
        }
    }
}
