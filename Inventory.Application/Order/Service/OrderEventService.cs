using Inventory.Application.Order.Commands;
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
        public async Task SaveEvent(Orders order, OrderEventType eventType)
        {
            List<OrderEvent> result = await GetOrderEvent(order.Id);
            _repository.Create(new OrderEvent(order.Id, order.ToJson(),
              eventType.Name, result.Count == 0 ? 1 : (result.Max(x => x.Version)) + 1), "");
            await _repository.SaveAsync();
        }
        public async Task SaveEvent(dynamic data, OrderEventType eventType)
        {
            List<OrderEvent> result = await GetOrderEvent(data.OrderId);
            _repository.Create(new OrderEvent(data.OrderId, data.ToJson(),
              eventType.Name, result.Count == 0 ? 1 : (result.Max(x => x.Version)) + 1), "");
            await _repository.SaveAsync();
        }
        private async Task<List<OrderEvent>> GetOrderEvent(Guid orderId)
        {
            var events = await _repository.GetWithIncludeAsync(x => x.AggregateId == orderId);
            var result = events.ToList();
            return result;
        }
    }
}
