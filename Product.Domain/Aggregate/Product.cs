using Domain.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Product.Domain.Aggregate
{
    public class Products : Entity, IAggregateRoot
    {
        public string ProductName { get; private set; }
        public string ProductCode { get; private set; }
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
        public string ProductSize { get; private set; }

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
        public Products()
        {

        }
        public Products(string productName, decimal? unitPrice, short? unitsInStock, decimal? discount,
             int? categoryId,string productSize):this() 
        {
            ProductName = productName;
            UnitPrice = unitPrice;
            UnitsInStock = unitsInStock;
            Discount = discount;
            CategoryId = categoryId;
            ProductSize = productSize;
        }
        public void GetNewProductNumber()
        {
            var now = DateTime.Now.Ticks; // '1492341545873'
                                          // pad with extra random digit
            Random rnd = new Random();
            now += now + rnd.Next(1, 500);
            var stringNow = now.ToString();
            var dates = new List<string> { stringNow.Substring(0, 4),stringNow.Substring(4, 8), RandomString() };
            var newNumber = string.Join("-", dates);
            ProductCode = newNumber;
        }

        public string RandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 4)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
