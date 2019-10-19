using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Models
{
    public class ProductSizes:Entity
    {
        public Guid ProductId { get; set; }
        public string Size { get; set; }
        public string Dimensions { get; set; }
        public int UnitInStock { get; set; }
        public Products Product { get; set; }
    }
}
