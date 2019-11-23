using System;
namespace Inventory.Domain.Events
{
    public class Event:Message
    {
        public Guid Id { get; private set; }
        public int Version { get; private set; }
        public string EventName { get; private set; }
        public string Data { get; private set; }
        public Event(string eventName,int version,string data)
        {
            Id = Guid.NewGuid();
            EventName = eventName;
            Version = version;
            Data = data;
        }
        public void SetVersion(int version)
        {
            Version = version;
        }
    }
}
