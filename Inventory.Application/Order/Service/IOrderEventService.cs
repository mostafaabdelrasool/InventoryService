using Inventory.Application.Order.model;
using Inventory.Domain.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Order.Service
{
    public interface IOrderEventService
    {
        Task SaveEvent(OrderDTO order, OrderEventType eventType);
    }
}
