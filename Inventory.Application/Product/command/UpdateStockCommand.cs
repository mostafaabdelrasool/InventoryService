using Inventory.Application.Extensions.Mapper;
using Inventory.Domain.Models;
using Inventory.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Product.command
{
    public class UpdateStockCommand : INotification
    {
        public IReadOnlyCollection<OrderDetailDTO> _orderDetails;
        public UpdateStockCommand(List<OrderProductDetails> OrderDetails)
        {
            _orderDetails = OrderDetails.ToOrderItemsDTO().ToList();
        }
       
    }
    public class OrderDetailDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSizeId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal Total { get; set; }
    }
}
