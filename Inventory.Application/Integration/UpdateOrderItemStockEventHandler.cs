using EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Integration
{
    public class UpdateOrderItemStockEventHandler :
        IIntegrationEventHandler<UpdateOrderItemStockEvent>
    {
        public Task Handle(UpdateOrderItemStockEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
