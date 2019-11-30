using Inventory.Application.Order.Commands;
using Inventory.Application.Order.Query;
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

namespace Inventory.Application.Product.Handlers
{
    public class ProductUpdateOnUpdateOrderCommandHandler : INotificationHandler<UpdateOrderCommand>
    {
        private readonly IRepository<Products> _repository;
        private readonly IQueryOrderEvent _queryOrderEvent;

        public ProductUpdateOnUpdateOrderCommandHandler(IRepository<Products> repository, IQueryOrderEvent queryOrderEvent)
        {
            _repository = repository;
            _queryOrderEvent = queryOrderEvent;
        }

        public async Task Handle(UpdateOrderCommand notification, CancellationToken cancellationToken)
        {
            var lastEvent = await _queryOrderEvent.GetLastEvent(notification.Order.Id);
            var productIds = notification.Order.OrderDetails.ToList().GroupBy(x=>x.ProductId)
                .Select(x=>new { x.Key,Total=x.Sum(y=>y.Quantity)});
            var products = await _repository.GetWithIncludeAsync(x => productIds.Any(y => y.Key == x.Id));
            products.ToList().ForEach(x =>
            {
                var itemInLastOrder = lastEvent != null? lastEvent.OrderDetails.ToList().GroupBy(y => y.ProductId)
                .Select(y => new { y.Key, Total = y.Sum(z=> z.Quantity) }).FirstOrDefault()
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

                _repository.Update(x, "");
            });
            await _repository.SaveAsync();
        }
    }
}
