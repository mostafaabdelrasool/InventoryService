using Inventory.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Order
{
    public class OrderEvent : Event
    {
        public OrderEvent(Guid AggregateId, string data,string eventName, int version)
            :base(AggregateId,eventName, version,data)
        {
        }
        public OrderEvent()
        {

        }
    }
}
