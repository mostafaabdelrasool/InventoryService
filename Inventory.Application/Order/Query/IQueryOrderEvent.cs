using System;
using System.Threading.Tasks;
using Inventory.Application.Order.model;
using Inventory.Domain.Models;
using Inventory.Domain.Order;

namespace Inventory.Application.Order.Query
{
    public interface IQueryOrderEvent
    {
        Task<OrderDTO> GetLastEvent(Guid OrderId);
    }
}