using Inventory.Application.Product.command;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Extensions.Mapper
{
    public static class OrderDetailExtention
    {
        public static IEnumerable<OrderDetailDTO> ToOrderItemsDTO(this IEnumerable<OrderProductDetails> items)
        {
            foreach (var item in items)
            {
                yield return item.ToOrderItemDTO();
            }
        }
        public static OrderDetailDTO ToOrderItemDTO(this OrderProductDetails item)
        {
            return new OrderDetailDTO()
            {
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                ProductSizeId = item.ProductSizeId,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                Discount = item.Discount,
                Total = item.Total,
            };
        }
    }
}
