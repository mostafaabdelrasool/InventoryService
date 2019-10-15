using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Application.Interfaces;
using Inventory.Domain;
using Microsoft.AspNetCore.Mvc;
namespace Inventory.Web.Controllers
{
    public class GenericController<T> : ControllerBase where T : class, IEntity, new()
    {
        private IService<T> service;

        public GenericController(IService<T> service)
        {
            this.service = service;
        }
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var result = await service.ListAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            var result = await service.GetAsync(id);
            return Ok(result);
        }
        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody]T entity)
        {
            await service.Update(entity, User.Identity.Name);
            return Ok();
        }
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]T entity)
        {
            var result = await service.CreateAsync(entity, User.Identity.Name);
            return Ok(result);
        }
        [HttpDelete]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            await service.DeleteAsync(id, User.Identity.Name);
            return Ok();
        }
        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> Filter([FromBody]List<string> filter)
        {
            var result = await service.Filter(filter);
            return Ok(result);
        }
    }
}