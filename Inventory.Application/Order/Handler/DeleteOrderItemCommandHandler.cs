using Inventory.Application.Order.Commands;
using Inventory.Domain.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Order.Handler
{
    public class DeleteOrderItemCommandHandler : INotificationHandler<DeleteOrderItemCommand>
    {
        private readonly IOrderEventService _orderEventService;

        public DeleteOrderItemCommandHandler(IOrderEventService orderEventService)
        {
            _orderEventService = orderEventService;
        }
        public async Task Handle(DeleteOrderItemCommand notification, CancellationToken cancellationToken)
        {
            await _orderEventService.SaveEvent(notification, OrderEventType.DeleteOrderItem);
        }
    }
}
