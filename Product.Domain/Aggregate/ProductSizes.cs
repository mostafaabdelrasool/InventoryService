using Domain.Service;
using System;

namespace Product.Domain.Aggregate
{
    public class ProductSizes:Entity
    {
        public Guid ProductId { get;private set; }
        public ProductSize Size { get; private set; }
        public string Dimensions { get; private set; }
        public int UnitInStock { get; private set; }
        public ProductSizes(Guid productId, ProductSize size, string dimensions, int unitInStock)
        {
            ProductId = productId;
            Size = size;
            Dimensions = dimensions;
            UnitInStock = unitInStock;
        }
        protected ProductSizes()
        {

        }
    }
    public class ProductSize : Enumeration
    {
        public static ProductSize OrderCreated = new ProductSize(1, "M");
        public static ProductSize OrderUpdated = new ProductSize(2, "L");
        public static ProductSize OrderStatusChanged = new ProductSize(3, "XL");
        public static ProductSize DeleteOrderItem = new ProductSize(4, "XXL");
        public ProductSize(int id, string name) :
            base(id, name)
        {
        }
    }
}