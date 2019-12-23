using Inventory.Application.Extensions.Mapper;
using Inventory.Application.Order.model;
using Inventory.Domain.Models;
using Inventory.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Order.command
{
    public class UpdateStockCommand : INotification
    {
        public OrderDTO Order;
        public UpdateStockCommand(OrderDTO order)
        {
            Order = order;
        }

    }
   
}
