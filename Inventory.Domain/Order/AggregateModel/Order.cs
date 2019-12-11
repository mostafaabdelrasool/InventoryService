using System;
using System.Collections.Generic;
using Domain.Service;

namespace Inventory.Domain.Order.AggregateModel
{
    public class Order:Entity,IAggregateRoot
    {
        private readonly List<OrderProductDetails> _orderDetails;

        public IReadOnlyCollection<OrderProductDetails> OrderItems => _orderDetails;
        protected Order()
        {
            _orderDetails = new List<OrderProductDetails>();
        }
    }
}
