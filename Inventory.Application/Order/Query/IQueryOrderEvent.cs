using System;
using System.Threading.Tasks;
using Inventory.Domain.Models;
using Inventory.Domain.Order;

namespace Inventory.Application.Order.Query
{
    public interface IQueryOrderEvent
    {
        Task<Orders> GetLastEvent(Guid OrderId);
    }
}