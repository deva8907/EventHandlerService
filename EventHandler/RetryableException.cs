namespace EventHandler
{

    [Serializable]
    public class RetryableException : Exception
    {
        public RetryableException() { }
        public RetryableException(string message) : base(message) { }
        public RetryableException(string message, Exception inner) : base(message, inner) { }
        protected RetryableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
