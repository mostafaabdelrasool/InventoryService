using Inventory.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Order.Commands
{
    public class UpdateOrderCommand : INotification
    {
        public Orders Order;
        public UpdateOrderCommand(Orders order)
        {
            Order = order;
        }
    }
}
