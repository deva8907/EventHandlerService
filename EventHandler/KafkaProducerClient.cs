using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace EventHandler
{
    public class KafkaProducerClient : IDisposable
    {
        private readonly IProducer<Null, string> _producer;
        public KafkaProducerClient(IOptions<KafkaConfiguration> options)
        {
            var config = new ProducerConfig() { BootstrapServers = options.Value.ServerUrl };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task ProduceAsync(string topic, string message)
        {
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }

        public void Dispose()
        {
            _producer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
