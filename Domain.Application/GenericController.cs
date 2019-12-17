using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Domain.Application
{
    public class GenericController<T,Tkey> : ControllerBase where T : class, IEntity<Tkey>, new()
    {
        private readonly IService<T,Tkey> service;
        public List<Expression<Func<T, object>>> includes;
        public GenericController(IService<T, Tkey> service)
        {
            this.service = service;
            includes = new List<Expression<Func<T, object>>>();
        }
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var result = await service.ListAsync(includes);
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> Get(Tkey id)
        {
            var result = await service.GetAsync(id,includes);
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
        public virtual async Task<IActionResult> Delete(Tkey id)
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
        [HttpPut]
        [Route("[action]")]
        public virtual async Task<IActionResult> PartialUpdate([FromBody]T entity, List<string> properties)
        {
            await service.PartialUpdate(entity, properties);
            return Ok();
        }
    }
}