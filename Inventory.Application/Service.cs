using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Inventory.Application.Interfaces;
using Inventory.Domain;
using Inventory.Persistance.Interfaces;

namespace Inventory.Application
{
    public class Service<T> : IService<T> where T : class, IEntity, new()
    {
        protected IRepository<T> repository;

        public Service(IRepository<T> repository)
        {
            this.repository = repository;
        }
        public async Task<T> GetAsync(Guid id)
        {
            return await repository.ReadOneAsync(id);
        }
        public async Task<IEnumerable<T>> ListAsync(List<Expression<Func<T, object>>> includes)
        {
            return await repository.GetWithIncludeAsync(x => !x.IsDeleted, includes.ToArray());
        }
        /// <summary>
        /// Create an entity
        /// </summary>
        /// <param name="value">Value to Add</param>
        /// <param name="createdBy">who add the record</param>
        /// <param name="save">optional to save changes or not</param>
        /// <returns></returns>
        public async Task<T> CreateAsync(T value, string createdBy, bool save = true)
        {
            try
            {
                value.CreateDate = DateTime.Now;
                var result = await repository.CreateAsync(value, createdBy);
                if (save)
                {
                    await repository.SaveAsync();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<T> Update(T value, string updatedBy)
        {
            try
            {
                value.ModifyDate = DateTime.Now;
                repository.Update(value, updatedBy);
                await repository.SaveAsync();
                return value;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task DeleteAsync(Guid id, String deletedBy)
        {
            try
            {
                await repository.DeleteAsync(id, deletedBy);
                await repository.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<T> RemoveAsync(Guid id, string removedBy)
        {
            try
            {
                return await repository.RemoveAsync(id, removedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<T>> Filter(List<string> filter)
        {
            var result = await repository.DynamicQuery(filter);
            return result;
        }
        public async Task SaveAsync()
        {
            await repository.SaveAsync();
        }
    }
}
