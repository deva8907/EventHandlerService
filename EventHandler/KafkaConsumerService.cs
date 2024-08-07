using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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

            foreach (var processor in _factory.GetEventProcessors())
            {
                processor.Process(result);
            }
        }
    }
}
