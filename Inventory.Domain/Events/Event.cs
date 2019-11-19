using System;
namespace Inventory.Domain.Events
{
    public class Event:Message
    {
        string Id { get; }
        string CorrelationId { get; }
        string CausationId { get; }
        public int Version;
    }
}
