using EventBus.Events;
using Inventory.Application.Order.model;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Integration
{
    public class ProductUpdateOnUpdateOrderEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public IEnumerable<OrderDetailDTO> OrderDetail { get; }

        public IEnumerable<OrderDetailDTO> LastOrderDetail { get; }
        public ProductUpdateOnUpdateOrderEvent(Guid orderId,
            IEnumerable<OrderDetailDTO> orderDetail, IEnumerable<OrderDetailDTO> lastOrderDetail)
        {
            OrderId = orderId;
            OrderDetail = orderDetail;
            LastOrderDetail = lastOrderDetail;
        }
    }
}
