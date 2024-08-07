using EventHandler.EventProcessors;

namespace EventHandler
{
    public class EventProcessorFactory
    {
        private HashSet<IEventProcessor> _processors = [];

        public void Register(IEventProcessor processor)
        {
            _processors.Add(processor);
        }

        public IEnumerable<IEventProcessor> GetEventProcessors()
        {
            return _processors;
        }
    }
}
