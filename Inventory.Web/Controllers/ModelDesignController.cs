using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interfaces;
using Inventory.Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    public class ModelDesignController : GenericController<ModelDesign>
    {
        // GET: api/<controller>
        public ModelDesignController(IService<ModelDesign> service) : base(service)
        {
        }
    }
}
