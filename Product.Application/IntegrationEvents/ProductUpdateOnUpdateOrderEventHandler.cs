using Domain.Service.Repository;
using EventBus.Abstractions;
using Product.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Integration
{
    public class ProductUpdateOnUpdateOrderEventHandler :
        IIntegrationEventHandler<ProductUpdateOnUpdateOrderEvent>
    {
        private readonly ICommandRepository<Products> _commandRepository;
        private readonly IReadRepository<Products> _readRepository;

        public ProductUpdateOnUpdateOrderEventHandler(ICommandRepository<Products> commandRepository,
            IReadRepository<Products> readRepository)
        {
            _commandRepository = commandRepository;
            _readRepository = readRepository;
        }
        public async Task Handle(ProductUpdateOnUpdateOrderEvent @event)
        {
            var productIds = @event.OrderDetail.ToList().GroupBy(x => x.ProductId)
               .Select(x => new { x.Key, Total = x.Sum(y => y.Quantity) });
            var products = await _readRepository.GetWithIncludeAsync(x => productIds.Any(y => y.Key == x.Id));
            products.ToList().ForEach(x =>
            {
                var itemInLastOrder = @event.LastOrderDetail != null ? @event.LastOrderDetail.ToList().GroupBy(y => y.ProductId)
                .Select(y => new { y.Key, Total = y.Sum(z => z.Quantity) }).FirstOrDefault()
                : null;
                var itemInCurrentOrder = productIds.FirstOrDefault(p => p.Key == x.Id);

                if (itemInLastOrder != null)
                {
                    x.AddOrReductUnitInStockFromOrder((short)itemInCurrentOrder.Total,
                        (short)itemInLastOrder.Total);
                }
                else
                {
                    x.ReductUnitInStockFromOrder((short)itemInCurrentOrder.Total);
                }

                _commandRepository.Update(x, "");
            });
            await _commandRepository.SaveAsync();
        }
    }
}
