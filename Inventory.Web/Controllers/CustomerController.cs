using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Customer.query;
using Inventory.Application.Interfaces;
using Inventory.Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : GenericController<Customers>
    {
        private ICustomerQueryService _queryService;

        public CustomerController(IService<Customers> service,
            ICustomerQueryService queryService) : base(service)
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
