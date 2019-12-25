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
using Inventory.Application.Order.Query;
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
        private readonly IQueryOrderEvent _queryOrderEvent;

        public OrderController(IService<Orders> service,
           IMediator mediator, IQueryOrderEvent queryOrderEvent) : base(service)
        {
            _mediator = mediator;
            _service = service;
            _queryOrderEvent = queryOrderEvent;
            includes.Add(x => x.Customer);
        }
        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Add([FromBody] OrderDTO entity)
        {
            var result = await _service.CreateAsync(entity.ToOrderEntity(), User.Identity.Name);
            var saved = await _service.GetAsync(result.Id, "Customer", "OrderDetails");
            //assign product to order detail to save in the event store
            var dto = saved.ToOrderDTO();
            for (int i = 0; i < dto.OrderDetails.Count; i++)
            {
                dto.OrderDetails[i].Product = entity.OrderDetails.Find(x=>x.ProductId== 
                dto.OrderDetails[i].ProductId).Product;
            }
            await _mediator.Publish(new UpdateStockCommand(dto));
            return Ok(entity);
        }
        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Update([FromBody] OrderDTO entity)
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
            var result = await _queryOrderEvent.GetLastEvent(id);
            return Ok(result);
        }
    }
}