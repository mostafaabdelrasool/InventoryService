using Inventory.Application.Order.Commands;
using Inventory.Application.Order.Query;
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

        public UpdateOrderHandler(IOrderEventService orderEventService)
        {
            _orderEventService = orderEventService;
        }
        public async Task Handle(UpdateOrderCommand notification, CancellationToken cancellationToken)
        {
            await _orderEventService.SaveEvent(notification.Order, OrderEventType.OrderUpdated);
        }
    }
}
