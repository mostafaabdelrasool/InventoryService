using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interfaces;
using Inventory.Application.Product.query;
using Inventory.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizesController : GenericController<ProductSizes>
    {
        private IProductSizesQueryService _queryService;

        public ProductSizesController(IService<ProductSizes> service,
            IProductSizesQueryService queryService) : base(service)
        {
            _queryService = queryService;
        }

        [HttpGet]
        [Route("[action]")]
        public virtual async Task<IActionResult> GetProductSizes(Guid id)
        {
            var result = await _queryService.GetProductSizes(id);
            return Ok(result);
        }
    }
}