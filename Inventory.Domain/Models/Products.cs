using Inventory.Domain;
using System;
using System.Collections.Generic;

namespace Inventory.Domain.Models
{
    public partial class Products : Entity
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public string ProductName { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public decimal? CostPrice { get; set; }
        public Categories Category { get; set; }
        public Suppliers Supplier { get; set; }
        public string image { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
