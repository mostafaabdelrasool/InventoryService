using Inventory.Application.Product.model;
using Inventory.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Product.Handlers
{
    public class UpdateStockHandler : INotificationHandler<OrderUpdateMessage>
    {
        public Task Handle(OrderUpdateMessage notification, CancellationToken cancellationToken)
        {
            var products = new List<Products>();
            notification.OrderDetails.ForEach(x =>
            {
                products.Add(new Products { Id = x.ProductId, UnitsInStock = x.Quantity });
            });
            return Task.CompletedTask;
        }

    }
}
