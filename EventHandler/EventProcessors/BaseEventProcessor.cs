using Confluent.Kafka;

namespace EventHandler.EventProcessors
{
    public abstract class BaseEventProcessor : IEventProcessor
    {
        public abstract bool CanProcess(ConsumeResult<Ignore, string> message);

        public void Process(ConsumeResult<Ignore, string> message)
        {
            if (!CanProcess(message)) return;

            ProcessMessage(message);
        }

        protected abstract void ProcessMessage(ConsumeResult<Ignore, string> message);
    }
}
