using System;
namespace Inventory.Domain.Order.AggregateModel
{
    public class OrderDetailss
    {
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductSizeId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal Total { get; set; }
        public Order Order { get; set; }
        public OrderDetails()
        {
            
        }
    }
}
