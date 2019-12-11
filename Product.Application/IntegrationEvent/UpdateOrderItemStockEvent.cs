using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.Integration
{
    public class UpdateOrderItemStockEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public IEnumerable<OrderItemDTO> OrderDetail { get; }

        public UpdateOrderItemStockEvent(Guid orderId,
            IEnumerable<OrderItemDTO> orderDetail)
        {
            OrderId = orderId;
            OrderDetail = orderDetail;
        }
    }
    public class OrderItemDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSizeId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal Total { get; set; }
    }
}
