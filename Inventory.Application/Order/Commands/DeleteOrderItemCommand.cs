using Inventory.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Order.Commands
{
    public class DeleteOrderItemCommand : INotification
    {
        public Orders Order { get; private set; }
        public DeleteOrderItemCommand(Orders order) => Order = order;
      
    }
}
