using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interfaces;
using Inventory.Application.Order.Event;
using Inventory.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : GenericController<Orders>
    {
        private readonly IOrderSavedCommand _updateStockCommand;
        private readonly IService<Orders> _service;

        public OrderController(IService<Orders> service, IOrderSavedCommand updateStockCommand) : base(service)
        {
            _updateStockCommand = updateStockCommand;
            _service = service;
            includes.Add(x => x.Customer);
        }
        public override async Task<IActionResult> Post([FromBody] Orders entity)
        {
            var result = await _service.CreateAsync(entity, "", false);
            await _updateStockCommand.NotifyOrderSaved(entity.OrderDetails.ToList());
            return Ok(entity);
        }
    }
}