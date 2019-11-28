using Inventory.Domain.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Order
{
    public class OrderEventType : Enumeration
    {
        public static OrderEventType OrderCreated = new OrderEventType(1, "OrderCreated");
        public static OrderEventType OrderUpdated = new OrderEventType(2, "OrderUpdated");
        public static OrderEventType OrderStatusChanged = new OrderEventType(3, "OrderStatusChanged");
        public static OrderEventType DeleteOrderItem = new OrderEventType(4, "DeleteOrderItem");
        public OrderEventType(int id, string name) : 
            base(id, name)
        {
        }
    }
}
