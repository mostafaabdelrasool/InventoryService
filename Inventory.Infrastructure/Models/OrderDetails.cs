using Inventory.Domain;
using System;
using System.Collections.Generic;

namespace Inventory.Infrastructure.Models
{
    public partial class OrderDetails : Entity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public Orders Order { get; set; }
        public Products Product { get; set; }
    }
}
