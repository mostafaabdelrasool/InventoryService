using Inventory.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Order.model
{
    public class OrderUpdateMessage :IRequest<int>
    {
        public List<OrderProductDetails> OrderDetails { get; set; }
    }
}
