using Inventory.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Product.model
{
    public class OrderUpdateMessage :INotification
    {
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
