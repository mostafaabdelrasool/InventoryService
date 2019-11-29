using Inventory.Application.Order.Commands;
using Inventory.Domain.Models;
using Inventory.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Product.Handlers
{
    public class DeleteOrderItemCommandHandler : INotificationHandler<DeleteOrderItemCommand>
    {
        private readonly IRepository<Products> _repository;
        public DeleteOrderItemCommandHandler(IRepository<Products> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteOrderItemCommand notification, CancellationToken cancellationToken)
        {
            var orderItem = notification.Order.OrderDetails.ToList()[0];
            var product = await _repository.GetFirtOrDefaultAsync(x => x.Id == orderItem.ProductId);
            product.RevertItemToStock(orderItem.Quantity);
            _repository.PartialUpdate(product, new List<string> { "UnitsInStock" });
            await _repository.SaveAsync();

        }
    }
}
