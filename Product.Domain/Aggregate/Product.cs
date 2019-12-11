using Domain.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Product.Domain.Aggregate
{
    public class Products : Entity, IAggregateRoot
    {
        public string ProductName { get; private set; }
        public int? CategoryId { get; private set; }
        public string QuantityPerUnit { get; private set; }
        public decimal? UnitPrice { get; private set; }
        public short? UnitsInStock { get; private set; }
        public short? UnitsOnOrder { get; private set; }
        public short? ReorderLevel { get; private set; }
        public bool Discontinued { get; private set; }
        public decimal? CostPrice { get; private set; }
        public decimal? Discount { get; private set; }
        public Categories Category { get; private set; }
        public string image { get; private set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        private readonly List<ProductSizes> _productSizes;
        public IReadOnlyCollection<ProductSizes> ProductSizes => _productSizes;
        public void AddOrReductUnitInStockFromOrder(short currentQuantity, short lasQuantity)
        {
            var addOrReducedAmount = currentQuantity - lasQuantity;
            this.UnitsInStock = addOrReducedAmount > 0 ? (short)(this.UnitsInStock - addOrReducedAmount)
            : (short)(this.UnitsInStock + (addOrReducedAmount * -1));
        }
        public void ReductUnitInStockFromOrder(short currentQuantity)
        {
            this.UnitsInStock -= currentQuantity;
        }
        public void RevertItemToStock(short currentQuantity)
        {
            this.UnitsInStock += currentQuantity;
        }
        protected Products()
        {
            _productSizes = new List<ProductSizes>();
        }
    }
}
