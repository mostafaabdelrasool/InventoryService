using System;
using System.Collections.Generic;
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
        public async Task<IEnumerable<T>> ListAsync()
        {
            return await repository.ReadAllAsync();
        }
        public async Task<T> CreateAsync(T value, string createdBy)
        {
            try
            {
                value.CreateDate = DateTime.Now;
                return await repository.CreateAsync(value, createdBy);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public  T Update(T value, string updatedBy)
        {
            try
            {
                value.ModifyDate = DateTime.Now;
                return repository.Update(value, updatedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<T> DeleteAsync(Guid id, String deletedBy)
        {
            try
            {
                return await repository.DeleteAsync(id, deletedBy);
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
    }
}
