using Confluent.Kafka;

namespace EventHandler.EventProcessors
{
    public interface IEventProcessor
    {
        bool CanProcess(ConsumeResult<Ignore, string> message);
        void Process(ConsumeResult<Ignore, string> message);
    }
}
