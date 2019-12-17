using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Product.query;
using Product.Domain.Aggregate;

namespace Product.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizesController : GenericController<ProductSize,int>
    {
        private IProductSizesQueryService _queryService;

        public ProductSizesController(IService<ProductSize,int> service,
            IProductSizesQueryService queryService) : base(service)
        {
            _queryService = queryService;
        }

        [HttpGet]
        [Route("[action]")]
        public virtual async Task<IActionResult> GetProductSizes(int id)
        {
            var result = await _queryService.GetProductSizes(id);
            return Ok(result);
        }
    }
}