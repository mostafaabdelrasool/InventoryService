using Inventory.Application.Order.Commands;
using Inventory.Application.Order.Service;
using Inventory.Domain.Models;
using Inventory.Domain.Order;
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
    public class DeleteOrderItemCommandHandler : INotificationHandler<DeleteOrderItemCommand>
    {
        private readonly IOrderEventService _orderEventService;
        private readonly IRepository<Orders> _repository;
        private readonly IRepository<OrderProductDetails> _orderDetailRepository;

        public DeleteOrderItemCommandHandler(IOrderEventService orderEventService,
            IRepository<Orders> repository,IRepository<OrderProductDetails>orderDetailRepository)
        {
            _orderEventService = orderEventService;
            _repository =repository;
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task Handle(DeleteOrderItemCommand notification, CancellationToken cancellationToken)
        {
            _repository.PartialUpdate(new Orders
            {
                Id = notification.Order.Id,
                Total = notification.Order.Total,
                OverallTotal = notification.Order.OverallTotal
            },new List<string> { "Total", "OverallTotal" });
            _orderDetailRepository.Remove(notification.Order.OrderDetails.ToList()[0].Id,"");
            await _repository.SaveAsync();
            await _orderEventService.SaveEvent(notification.Order, OrderEventType.DeleteOrderItem);
        }
    }
}
