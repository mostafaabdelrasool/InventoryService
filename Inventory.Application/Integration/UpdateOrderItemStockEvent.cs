using EventBus.Events;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Integration
{
    public class UpdateOrderItemStockEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public IEnumerable<OrderProductDetails> OrderDetail { get; }

        public UpdateOrderItemStockEvent(Guid orderId,
            IEnumerable<OrderProductDetails> orderDetail)
        {
            OrderId = orderId;
            OrderDetail = orderDetail;
        }
    }
}
