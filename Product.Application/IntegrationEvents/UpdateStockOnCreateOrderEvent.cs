using EventBus.Events;
using Product.Application.Integration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.IntegrationEvents
{
    public class UpdateStockOnCreateOrderEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public List<OrderItemDTO> OrderDetail { get; }

        public UpdateStockOnCreateOrderEvent(Guid orderId,
            List<OrderItemDTO> orderDetail)
        {
            OrderId = orderId;
            OrderDetail = orderDetail;
        }
    }
}
