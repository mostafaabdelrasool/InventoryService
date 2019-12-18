using EventBus.Events;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.IntegrationEvents
{
    public class DeleteOrderItemEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public OrderProductDetails OrderDetail { get; }

        public DeleteOrderItemEvent(Guid orderId,
            OrderProductDetails orderDetail)
        {
            OrderId = orderId;
            OrderDetail = orderDetail;
        }
    }
}
