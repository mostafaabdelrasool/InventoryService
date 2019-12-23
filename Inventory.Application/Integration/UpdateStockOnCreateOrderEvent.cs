using EventBus.Events;
using Inventory.Application.Order.model;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Integration
{
    public class UpdateStockOnCreateOrderEvent:IntegrationEvent
    {
        public Guid OrderId { get; }
        public List<OrderDetailDTO> OrderDetail { get; }

        public UpdateStockOnCreateOrderEvent(Guid orderId,
            List<OrderDetailDTO> orderDetail)
        {
            OrderId = orderId;
            OrderDetail = orderDetail;
        }
    }
}
