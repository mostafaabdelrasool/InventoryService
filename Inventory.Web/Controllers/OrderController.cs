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

        public OrderController(IService<Orders> service, IUpdateStockCommand updateStockCommand) : base(service)
        {
            _updateStockCommand = updateStockCommand;
        }
        public override async Task<IActionResult> Post([FromBody] Orders entity)
        {
            var result = await base.Post(entity);
            _updateStockCommand.Notify(entity.OrderDetails.ToList());
            return Ok(entity);
        }
    }
}