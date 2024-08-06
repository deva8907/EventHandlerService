using Confluent.Kafka;

namespace EventHandler
{
    public class KafkaProducer(string bootstrapServers)
    {
        private readonly string _bootstrapServers = bootstrapServers;

        public async Task ProduceAsync(string topic, string message)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var deliveryReport = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }
    }
}
