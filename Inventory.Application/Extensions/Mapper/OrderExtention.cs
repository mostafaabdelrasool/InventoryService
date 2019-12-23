using Inventory.Application.Order.command;
using Inventory.Application.Order.model;
using Inventory.Domain.Models;
using Inventory.Infrastructrue.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Extensions.Mapper
{
    public static class OrderExtention
    {
        public static Orders ToOrderEntity(this OrderDTO order)
        {
            var json = order.ToJson();
            return json.ToType<Orders>(); 
        }
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
