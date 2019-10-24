using Inventory.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Order.model
{
    public class OrderUpdateMessage :INotification
    {
        public List<OrderProductDetails> OrderDetails { get; set; }
    }
}
