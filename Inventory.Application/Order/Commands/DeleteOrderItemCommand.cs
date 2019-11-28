using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Order.Commands
{
    public class DeleteOrderItemCommand : INotification
    {
        public Guid Id { get;private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid ProductSizeId { get; private set; }
        public short Quantity { get; private set; }
        public DeleteOrderItemCommand(Guid id, Guid orderId, Guid productId, Guid productSizeId, short quantity)
        {
            Id = id;
            OrderId = orderId;
            ProductSizeId = productSizeId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
