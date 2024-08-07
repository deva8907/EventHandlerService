﻿using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EventHandler.EventProcessors
{
    public class OrderCreatedEventProcessor(ILogger<OrderCreatedEventProcessor> logger) : IEventProcessor
    {
        private readonly ILogger _logger = logger;

        public bool CanProcess(ConsumeResult<Ignore, string> message)
        {
            OrderEvent orderEvent = JsonSerializer.Deserialize<OrderEvent>(message.Message.Value);
            return message.Topic.Equals("Orders") && orderEvent.EventType.Equals("OrderCreated");
        }

        public void Process(ConsumeResult<Ignore, string> message)
        {
            if (!CanProcess(message)) return;

            OrderEvent orderEvent = JsonSerializer.Deserialize<OrderEvent>(message.Message.Value);
            _logger.LogInformation($"Processing {orderEvent.EventType}");
        }
    }
}
