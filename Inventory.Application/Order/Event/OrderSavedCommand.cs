using Inventory.Application.Order.model;
using Inventory.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Order.Event
{
    public class OrderSavedCommand : IOrderSavedCommand
    {
        private readonly IMediator _mediator;

        public OrderSavedCommand(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task NotifyOrderSaved(List<OrderProductDetails> orderDetails)
        {
          await  _mediator.Publish(new OrderUpdateMessage
            {
                OrderDetails = orderDetails
            });
        }
    }
}
