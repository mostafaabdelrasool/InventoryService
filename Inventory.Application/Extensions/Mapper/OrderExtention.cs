using Inventory.Application.Order.command;
using Inventory.Application.Order.model;
using Inventory.Domain.Models;
using Inventory.Infrastructrue.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Application.Extensions.Mapper
{
    public static class OrderExtention
    {
        public static Orders ToOrderEntity(this OrderDTO order)
        {
            var json = order.ToJson();
            var result = json.ToType<Orders>();
            result.Customer = null;
            return result; 
        }
        public static OrderDTO ToOrderDTO(this Orders order)
        {
            var json = order.ToJson();
            var result = json.ToType<OrderDTO>();
            return result;
        }
        public static IEnumerable<OrderDetailDTO> ToOrderItemsDTO(this IEnumerable<OrderDetails> items)
        {
            foreach (var item in items)
            {
                yield return item.ToOrderItemDTO();
            }
        }
        public static OrderDetailDTO ToOrderItemDTO(this OrderDetails item)
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
