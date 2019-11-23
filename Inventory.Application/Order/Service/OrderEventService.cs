using Inventory.Domain.Models;
using Inventory.Domain.Order;
using Inventory.Infrastructrue.Json;
using Inventory.Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Order.Service
{
    public class OrderEventService : IOrderEventService
    {
        private readonly IRepository<OrderEvent> _repository;

        public OrderEventService(IRepository<OrderEvent> repository)
        {
            _repository = repository;
        }
        public async Task SaveEvent(Orders order,OrderEventType eventType)
        {
            var events = await _repository.GetWithIncludeAsync(x => x.AggregateId == order.Id);
            var result = events.ToList();
            await _repository.CreateAsync(new OrderEvent(order.Id, order.toJson(),
               eventType.Name, result.Count == 0 ? 1 : result.Max(x => x.Version)), "");
            await _repository.SaveAsync();
        }
    }
}
