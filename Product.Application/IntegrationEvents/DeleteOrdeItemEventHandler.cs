using Domain.Service.Repository;
using EventBus.Abstractions;
using Product.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.IntegrationEvents
{
    public class DeleteOrderItemEventHandler :
        IIntegrationEventHandler<DeleteOrderItemEvent>
    {
        private readonly ICommandRepository<Products> _commandRepository;
        private readonly IReadRepository<Products> _readRepository;

        public DeleteOrderItemEventHandler(ICommandRepository<Products> commandRepository,
            IReadRepository<Products> readRepository)
        {
            _commandRepository = commandRepository;
            _readRepository = readRepository;
        }
        public async Task Handle(DeleteOrderItemEvent @event)
        {
            var product = await _readRepository.GetFirtOrDefaultAsync(x => x.Id ==
            @event.OrderDetail.ProductId);
            product.RevertItemToStock(@event.OrderDetail.Quantity);
            _commandRepository.PartialUpdate(product, new List<string> { "UnitsInStock" });
            await _commandRepository.SaveAsync();
        }
    }
}
