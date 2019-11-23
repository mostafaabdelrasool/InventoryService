using Inventory.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Order
{
    public class OrderEvent : Event
    {
        public OrderEvent(string data,string eventName, int version)
            :base(eventName, version,data)
        {
        }
    }
}
