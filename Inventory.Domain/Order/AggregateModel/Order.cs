using System;
using System.Collections.Generic;
using Inventory.Domain.Service;

namespace Inventory.Domain.Order.AggregateModel
{
    public class Order:Entity,IAggregateRoot
    {
        private readonly List<OrderProductDetails> _orderDetails;
        private bool _isDraft;

        public IReadOnlyCollection<OrderProductDetails> OrderItems => _orderDetails;
        protected Order()
        {
            _orderDetails = new List<OrderProductDetails>();
            _isDraft = false;
        }
    }
}
