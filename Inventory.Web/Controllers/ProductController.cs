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
    public class ProductController : GenericController<Products>
    {
        private IProductQueryService _queryService;

        public ProductController(IService<Products> service,
            IProductQueryService queryService) : base(service)
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
      
        
    }
}