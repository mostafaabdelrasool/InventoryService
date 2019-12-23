using Inventory.Application.Order.model;
using Inventory.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Order.Commands
{
    public class UpdateOrderCommand : INotification
    {
        public OrderDTO Order;
        public UpdateOrderCommand(OrderDTO order)
        {
            Order = order;
        }
    }
}
