using EventBus.Abstractions;
using Inventory.Application.Integration;
using Inventory.Application.Order.Commands;
using Inventory.Application.Order.Query;
using Inventory.Application.Order.Service;
using Inventory.Domain.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Order.Handler
{
    public class UpdateOrderHandler : INotificationHandler<UpdateOrderCommand>
    {
        private readonly IOrderEventService _orderEventService;
        private readonly IEventBus _eventBus;
        private readonly IQueryOrderEvent _queryOrderEvent;

        public UpdateOrderHandler(IOrderEventService orderEventService,
            IQueryOrderEvent queryOrderEvent, IEventBus eventBus)
        {
            _orderEventService = orderEventService;
            _eventBus = eventBus;
            _queryOrderEvent = queryOrderEvent;
        }
        public async Task Handle(UpdateOrderCommand notification, CancellationToken cancellationToken)
        {
            var lastEvent=await _queryOrderEvent.GetLastEvent(notification.Order.Id);
            _eventBus.Publish(new ProductUpdateOnUpdateOrderEvent(notification.Order.Id,
               notification.Order.OrderDetails.ToList(), lastEvent.OrderDetails));
            await _orderEventService.SaveEvent(notification.Order, OrderEventType.OrderUpdated);

        }
    }
}
