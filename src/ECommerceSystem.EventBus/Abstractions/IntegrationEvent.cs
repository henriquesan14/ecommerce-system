﻿using ECommerceSystem.EventBus.Events;
using System.Text.Json.Serialization;

namespace ECommerceSystem.EventBus.Abstractions
{
    [JsonDerivedType(typeof(OrderCreatedEvent))]
    public abstract class IntegrationEvent
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
