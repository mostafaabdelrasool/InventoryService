using Domain.Service.Repository;
using EventBus.Abstractions;
using Product.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.IntegrationEvents
{
    public class UpdateStockOnCreateOrderEventHandler :
        IIntegrationEventHandler<UpdateStockOnCreateOrderEvent>
    {
        private readonly ICommandRepository<Products> _commandRepository;
        private readonly IReadRepository<Products> _readRepository;

        public UpdateStockOnCreateOrderEventHandler(ICommandRepository<Products> commandRepository,
            IReadRepository<Products> readRepository)
        {
            _commandRepository = commandRepository;
            _readRepository = readRepository;
        }

        public async Task Handle(UpdateStockOnCreateOrderEvent @event)
        {
            var productIds = @event.OrderDetail.ToList().Select(x => x.ProductId);
            var products = await _readRepository.GetWithIncludeAsync(x => productIds.Any(y => y == x.Id));
            products.ToList().ForEach(x =>
            {
                x.ReductUnitInStockFromOrder(@event.OrderDetail.FirstOrDefault(p => p.ProductId == x.Id).Quantity);
                _commandRepository.Update(x, "");
            });
            await _commandRepository.SaveAsync();
        }
    }
}
