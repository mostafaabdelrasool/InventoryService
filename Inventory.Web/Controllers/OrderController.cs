using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Abstractions;
using Inventory.Application.Extensions.Mapper;
using Inventory.Application.Integration;
using Inventory.Application.IntegrationEvents;
using Inventory.Application.Interfaces;
using Inventory.Application.Order.command;
using Inventory.Application.Order.Commands;
using Inventory.Application.Order.model;
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

        public OrderController(IService<Orders> service,
           IMediator mediator) : base(service)
        {
            _mediator = mediator;
            _service = service;
           
            includes.Add(x => x.Customer);
        }
        [HttpPost]
        [Route("Post")]
        public  async Task<IActionResult> Add([FromBody] OrderDTO entity)
        {
            await _service.CreateAsync(entity.ToOrderEntity(), User.Identity.Name);
            await _mediator.Publish(new UpdateStockCommand(entity));
            return Ok(entity);
        }
        [HttpPut]
        [Route("Put")]
        public  async Task<IActionResult> Update([FromBody] OrderDTO entity)
        {
            await _service.Update(entity.ToOrderEntity(), User.Identity.Name);
            await _mediator.Publish(new UpdateOrderCommand(entity));
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> DeleteOrderItem([FromBody] OrderDTO entity)
        {
            await _mediator.Publish(new DeleteOrderItemCommand(entity));
            
            return Ok();
        }
        public async override Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetAsync(id, "OrderDetails", "Customer");
            return Ok(result);
        }
    }
}