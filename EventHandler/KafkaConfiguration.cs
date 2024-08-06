namespace EventHandler
{
    public class KafkaConfiguration
    {
        public IEnumerable<string> Topics { get; set; }

        public string ServerUrl { get; set; }

        public int MaxRetry { get; set; }
    }
}
