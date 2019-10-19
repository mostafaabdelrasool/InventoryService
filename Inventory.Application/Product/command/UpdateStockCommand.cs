using Inventory.Application.Product.model;
using Inventory.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Product.command
{
    public class UpdateStockCommand : IUpdateStockCommand
    {
        private readonly IMediator _mediator;

        public UpdateStockCommand(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void NotifyOrderSaved(List<OrderDetails> orderDetails)
        {
            _mediator.Send(new OrderUpdateMessage
            {
                OrderDetails = orderDetails
            });
        }
    }
}
