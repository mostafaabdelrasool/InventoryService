using EventBus.Abstractions;
using Inventory.Application.Integration;
using Inventory.Application.Order.command;
using Inventory.Application.Order.Service;
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
        private readonly IEventBus _eventBus;

        public CreateNewOrderHandler(IOrderEventService orderEventService, IEventBus eventBus)
        {
            _orderEventService = orderEventService;
            _eventBus = eventBus;
        }
        public async Task Handle(UpdateStockCommand notification, CancellationToken cancellationToken)
        {
            await _orderEventService.SaveEvent(notification.Order, OrderEventType.OrderCreated);
            _eventBus.Publish(new UpdateStockOnCreateOrderEvent(notification.Order.Id,
                notification.Order.OrderDetails.ToList()));
        }
    }
}
