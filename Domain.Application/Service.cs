using Domain.Application;
using Domain.Service;
using Domain.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Application
{
    public class Service<T, TKey> : IService<T, TKey> where T : class, IEntity<int>, new()
    {
        private readonly IReadRepository<T> _readRepository;
        private readonly ICommandRepository<T> _commandRepository;

        public Service(IReadRepository<T> readRepository, ICommandRepository<T> commandRepository)
        {
            _readRepository = readRepository;
            _commandRepository = commandRepository;
        }

        public async Task<T> CreateAsync(T value, string createdBy, bool save = true)
        {
            T result = _commandRepository.Create(value, createdBy);
            await _commandRepository.SaveAsync();
            return result;
        }

        public async Task DeleteAsync(TKey id, string deletedBy)
        {
            _commandRepository.Delete(id, deletedBy);
            await _commandRepository.SaveAsync();
        }

        public Task<List<T>> Filter(List<string> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync(TKey id, List<Expression<Func<T, object>>> includes = null)
        {
            return await _readRepository.ReadOneAsync(id, includes);
        }

        public async Task<T> GetAsync(TKey id, params string[] includes)
        {
            return await _readRepository.ReadOneAsync(id, includes);
        }

        public async Task<IEnumerable<T>> ListAsync(List<Expression<Func<T, object>>> includes = null)
        {
            return await _readRepository.GetWithIncludeAsync(x => !x.IsDeleted, includes.ToArray());
        }

        public async Task<T> PartialUpdate(T value, List<string> properties)
        {
            value.ModifyDate = DateTime.Now;
            _commandRepository.PartialUpdate(value, properties);
            await _commandRepository.SaveAsync();
            return value;
        }

        public async Task RemoveAsync(TKey id, string removedBy)
        {
            _commandRepository.Remove(id, removedBy);
            await _commandRepository.SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _commandRepository.SaveAsync();
        }

        public async Task<T> Update(T value, string updatedBy)
        {
            _commandRepository.Update(value, updatedBy);
            await _commandRepository.SaveAsync();
            return value;
        }
    }
}
