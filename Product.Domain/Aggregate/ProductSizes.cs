using Domain.Service;
using System;

namespace Product.Domain.Aggregate
{
    public class ProductSize:Entity
    {
        public int ProductId { get;private set; }
        public string Size { get; private set; }
        public string Dimensions { get; private set; }
        public int UnitInStock { get; private set; }
        public Products Products { get; private set; }
        public ProductSize(int productId, string size, string dimensions, int unitInStock)
        {
            ProductId = productId;
            Size = size;
            Dimensions = dimensions;
            UnitInStock = unitInStock;
        }
        public ProductSize()
        {

        }
    }
}