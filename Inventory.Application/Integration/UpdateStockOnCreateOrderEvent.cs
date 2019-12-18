using EventBus.Events;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Integration
{
    public class UpdateStockOnCreateOrderEvent:IntegrationEvent
    {
        public Guid OrderId { get; }
        public List<OrderProductDetails> OrderDetail { get; }

        public UpdateStockOnCreateOrderEvent(Guid orderId,
            List<OrderProductDetails> orderDetail)
        {
            OrderId = orderId;
            OrderDetail = orderDetail;
        }
    }
}
