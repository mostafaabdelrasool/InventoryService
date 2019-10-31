using Inventory.Domain;
using System;
using System.Collections.Generic;

namespace Inventory.Domain.Models
{
    public partial class OrderProductDetails : Entity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSizeId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal Total { get; set; }
        public Orders Order { get; set; }
        public Products Product { get; set; }
        public ProductSizes ProductSize { get; set; }
    }
}
