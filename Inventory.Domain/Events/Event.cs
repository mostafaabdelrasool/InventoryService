using System;
namespace Inventory.Domain.Events
{
    public class Event : Entity, Message
    {
        public Guid AggregateId { get; private set; }
        public int Version { get; private set; }
        public string EventType { get; private set; }
        public string Data { get; private set; }
        public Event(Guid aggregateId,string eventType, int version, string data)
        {
            Id = Guid.NewGuid();
            EventType = eventType;
            Version = version;
            Data = data;
            AggregateId = aggregateId;
        }
        public Event()
        {

        }
        public void SetVersion(int version)
        {
            Version = version;
        }
    }
}
