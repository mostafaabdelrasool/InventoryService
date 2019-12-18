using EventBus.Events;
using Product.Application.Integration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.IntegrationEvents
{
    public class DeleteOrderItemEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public OrderItemDTO OrderDetail { get; }

        public DeleteOrderItemEvent(Guid orderId,
            OrderItemDTO orderDetail)
        {
            OrderId = orderId;
            OrderDetail = orderDetail;
        }
    }
}
