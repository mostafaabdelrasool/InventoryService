using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Order
{
    public interface IOrderEventService
    {
        Task SaveEvent(Orders order, OrderEventType eventType);
       
    }
}
