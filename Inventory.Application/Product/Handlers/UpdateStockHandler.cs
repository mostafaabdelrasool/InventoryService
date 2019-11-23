using Inventory.Application.Product.command;
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
    public class UpdateStockHandler : INotificationHandler<UpdateStockCommand>
    {
        private IRepository<Products> _repository;

        public UpdateStockHandler(IRepository<Products> repository)
        {

            _repository = repository;
        }
        public async Task Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var productIds = request.Order.OrderDetails.ToList().Select(x => x.ProductId);
            var products = await _repository.GetWithIncludeAsync(x => productIds.Any(y => y == x.Id));
            products.ToList().ForEach(x =>
            {
                x.UnitsInStock -= request.Order.OrderDetails.FirstOrDefault(p => p.ProductId == x.Id).Quantity;
                _repository.Update(x, "");
            });
             await _repository.SaveAsync();
        }
    }
}
