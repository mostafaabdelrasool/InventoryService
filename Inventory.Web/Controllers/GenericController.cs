using System;
using System.Threading.Tasks;
using Inventory.Domain;
using Inventory.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Inventory.Web.Controllers
{
    public class GenericController<T> : ControllerBase where T : class, IEntity, new()
    {
        private IRepository<T> repository;

        public GenericController(IRepository<T> repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var result = await repository.ReadAllAsync();
            return Ok(result);
        }
        //[HttpGet]
        //[Route("{id:Guid}")]
        //public virtual async Task<IActionResult> Get(Guid id)
        //{
        //    var result = await repository.ReadOneAsync(id);
        //    return Ok(result);
        //}
        [HttpPut]
        public virtual  IActionResult Put(T entity)
        {
            repository.Update(entity, User.Identity.Name);
            return Ok();
        }
        [HttpPost]
        public virtual async Task<IActionResult> Post(T entity)
        {
            await repository.CreateAsync(entity, User.Identity.Name);
            return Ok();
        }
        [HttpDelete]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            await repository.DeleteAsync(id, User.Identity.Name);
            return Ok();
        }
    }
}