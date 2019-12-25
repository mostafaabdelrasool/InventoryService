using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Application;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Product.query;
using Product.Domain.Aggregate;

namespace Product.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : GenericController<Products, int>
    {
        private readonly IProductQueryService _queryService;

        public ProductController(IService<Products, int> service, IProductQueryService queryService)
            : base(service)
        {
            _queryService = queryService;
        }
        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> Search(string q)
        {
            var result = await _queryService.Search(q);
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]")]
        public virtual async Task<IActionResult> ValidateStock(int productId,int amount)
        {
            var result = await _queryService.ValidateStock(productId, amount);
            return Ok(result);
        }
    }
}