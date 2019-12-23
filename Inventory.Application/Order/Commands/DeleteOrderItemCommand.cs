using Inventory.Application.Order.model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Order.Commands
{
    public class DeleteOrderItemCommand : INotification
    {
        public OrderDTO Order { get; private set; }
        public DeleteOrderItemCommand(OrderDTO order) => Order = order;
      
    }
}
