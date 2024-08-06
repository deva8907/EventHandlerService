namespace EventHandler
{
    public readonly record struct OrderEvent(string OrderId, string EventType, object EventData, DateTimeOffset Timestamp)
    {
    }
}
