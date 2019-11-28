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
            var productIds = notification.Order.OrderDetails.ToList().Select(x => x.ProductId);
            var products = await _repository.GetWithIncludeAsync(x => productIds.Any(y => y == x.Id));
            products.ToList().ForEach(x =>
            {
                var lastOrder = lastEvent != null? lastEvent.OrderDetails.ToList().Find(i => i.ProductId == x.Id)
                :null;
                var currentOrder = notification.Order.OrderDetails.FirstOrDefault(p => p.ProductId == x.Id);
                if (lastOrder != null)
                {
                    x.AddOrReductUnitInStockFromOrder(currentOrder.Quantity, lastOrder.Quantity);
                }
                else
                {
                    x.ReductUnitInStockFromOrder(currentOrder.Quantity);
                }

                _repository.Update(x, "");
            });
            await _repository.SaveAsync();
        }
    }
}
