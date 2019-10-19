using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interfaces;
using Inventory.Application.Product.command;
using Inventory.Application.Product.model;
using Inventory.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : GenericController<Orders>
    {
        private readonly IUpdateStockCommand _updateStockCommand;
        private readonly IService<Orders> _service;

        public OrderController(IService<Orders> service, IUpdateStockCommand updateStockCommand) : base(service)
        {
            _updateStockCommand = updateStockCommand;
            _service = service;
            includes.Add(x => x.Customer);
        }
        public override async Task<IActionResult> Post([FromBody] Orders entity)
        {
            var result = await _service.CreateAsync(entity, "", false);
            _updateStockCommand.NotifyOrderSaved(entity.OrderDetails.ToList());
            return Ok(entity);
        }
    }
}