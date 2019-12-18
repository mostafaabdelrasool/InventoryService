using EventBus.Events;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Integration
{
    public class ProductUpdateOnUpdateOrderEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public IEnumerable<OrderProductDetails> OrderDetail { get; }

        public IEnumerable<OrderProductDetails> LastOrderDetail { get; }
        public ProductUpdateOnUpdateOrderEvent(Guid orderId,
            IEnumerable<OrderProductDetails> orderDetail, IEnumerable<OrderProductDetails> lastOrderDetail)
        {
            OrderId = orderId;
            OrderDetail = orderDetail;
            LastOrderDetail = lastOrderDetail;
        }
    }
}
