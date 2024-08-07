namespace EventHandler
{
    public readonly record struct OrderEvent(string OrderId, string EventType, EventPayload EventData, DateTimeOffset Timestamp)
    {
    }
}
