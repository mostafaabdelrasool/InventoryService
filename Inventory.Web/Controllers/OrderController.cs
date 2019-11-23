using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interfaces;
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

        public OrderController(IService<Orders> service,
           IMediator mediator) : base(service)
        {
            _mediator = mediator;
            _service = service;
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
            await _service.Update(entity, User.Identity.Name);
            await _mediator.Publish(new UpdateStockCommand(entity));
            return Ok();
        }
        public async override Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetAsync(id, "OrderDetails", "Customer", "OrderDetails.Product.ProductSizes");
            return Ok(result);
        }
    }
}