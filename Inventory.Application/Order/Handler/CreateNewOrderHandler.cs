using Inventory.Application.Product.command;
using Inventory.Domain.Events;
using Inventory.Domain.Order;
using Inventory.Infrastructrue.Json;
using Inventory.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Order.Handler
{
    public class CreateNewOrderHandler : INotificationHandler<UpdateStockCommand>
    {
        private readonly IOrderEventService _orderEventService;

        public CreateNewOrderHandler(IOrderEventService orderEventService)
        {
            _orderEventService = orderEventService;
        }
        public async Task Handle(UpdateStockCommand notification, CancellationToken cancellationToken)
        {
            await _orderEventService.SaveEvent(notification.Order, OrderEventType.OrderCreated);
        }
    }
}
