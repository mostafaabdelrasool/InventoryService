﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interfaces;
using Inventory.Domain.Models;
using Inventory.Persistance.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : GenericController<Categories>
    {
        public CategoryController(IService<Categories> service) : base(service)
        {
        }
    }
}