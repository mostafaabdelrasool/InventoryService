using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.Integration
{
    public class ProductUpdateOnUpdateOrderEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public IEnumerable<OrderItemDTO> OrderDetail { get; }
        public IEnumerable<OrderItemDTO> LastOrderDetail { get; }
        public ProductUpdateOnUpdateOrderEvent(Guid orderId,
            IEnumerable<OrderItemDTO> orderDetail, IEnumerable<OrderItemDTO> lastOrderDetail)
        {
            OrderId = orderId;
            OrderDetail = orderDetail;
            LastOrderDetail= lastOrderDetail;
        }
    }
    public class OrderItemDTO
    {
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductSizeId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal Total { get; set; }
    }
}
