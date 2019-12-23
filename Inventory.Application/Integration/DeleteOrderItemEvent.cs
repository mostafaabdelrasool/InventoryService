using EventBus.Events;
using Inventory.Application.Order.model;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.IntegrationEvents
{
    public class DeleteOrderItemEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public OrderDetailDTO OrderDetail { get; }

        public DeleteOrderItemEvent(Guid orderId,
            OrderDetailDTO orderDetail)
        {
            OrderId = orderId;
            OrderDetail = orderDetail;
        }
    }
}
