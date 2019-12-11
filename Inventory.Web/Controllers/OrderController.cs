using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Abstractions;
using Inventory.Application.Interfaces;
using Inventory.Application.Order.Commands;
using Inventory.Application.Product.command;
using Inventory.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : GenericController<Orders>
    {
        private readonly IMediator _mediator;
        private readonly IService<Orders> _service;
        private readonly IEventBus _eventBus;

        public OrderController(IService<Orders> service,
           IMediator mediator,IEventBus eventBus) : base(service)
        {
            _mediator = mediator;
            _service = service;
            _eventBus = eventBus;
            includes.Add(x => x.Customer);
        }
        public override async Task<IActionResult> Post([FromBody] Orders entity)
        {
            await _service.CreateAsync(entity, User.Identity.Name, false);
            await _mediator.Publish(new UpdateStockCommand(entity));
            return Ok(entity);
        }
        public override async Task<IActionResult> Put([FromBody] Orders entity)
        {
            entity.OrderDetails.ToList().ForEach(x =>
            {
                x.Product = null;
                x.ProductSize = null;
            });
            await _service.Update(entity, User.Identity.Name);
            await _mediator.Publish(new UpdateOrderCommand(entity));
            _eventBus.Publish(new Application.Integration.UpdateOrderItemStockEvent(entity.Id, entity.OrderDetails));
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> DeleteOrderItem([FromBody] Orders entity)
        {
            await _mediator.Publish(new DeleteOrderItemCommand(entity));
            return Ok();
        }
        public async override Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetAsync(id, "OrderDetails", "Customer", "OrderDetails.Product.ProductSizes");
            return Ok(result);
        }
    }
}