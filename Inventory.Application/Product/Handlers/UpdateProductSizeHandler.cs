using Inventory.Application.Order.model;
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
    public class UpdateProductSizeHandler : IRequestHandler<OrderUpdateMessage, int>
    {
        private IRepository<ProductSizes> _repository;

        public UpdateProductSizeHandler(IRepository<ProductSizes> repository)
        {

            _repository = repository;
        }
        public async Task<int> Handle(OrderUpdateMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var productIds = request.OrderDetails.ToList().Select(x => x.ProductSizeId);
                var products = await _repository.GetWithIncludeAsync(x => productIds.Any(y => y == x.Id));
                products.ToList().ForEach(x =>
                {
                    x.UnitInStock -= request.OrderDetails.FirstOrDefault(p => p.ProductSizeId == x.Id).Quantity;
                    _repository.Update(x, "");
                });
                return await _repository.SaveAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
