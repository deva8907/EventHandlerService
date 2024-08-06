using EventHandler.EventProcessors;
using System.Diagnostics;

namespace EventHandler
{
    public class EventProcessorFactory
    {
        private Dictionary<string, IEventProcessor> _processors = [];

        public void Register(string topic, string eventType, IEventProcessor processor)
        {
            string key = ProcessorKey(topic, eventType);
            if (!_processors.ContainsKey(key))
            {
                _processors.Add(key, processor);
            }
        }

        public IEventProcessor GetEventProcessor(string topic, string eventType)
        {
            string key = ProcessorKey(topic, eventType);
            if (_processors.TryGetValue(key, out IEventProcessor? value))
            {
                return value;
            }
            throw new InvalidOperationException($"No event processor found for the messageType {eventType} and topic {topic}");
        }

        private static string ProcessorKey(string topic, string eventType) => topic + "-" + eventType;
    }
}
