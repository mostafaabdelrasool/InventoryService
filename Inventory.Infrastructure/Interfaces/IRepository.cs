using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Inventory.Domain;

namespace Inventory.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<T> CreateAsync(T value, string createdBy);
        Task<List<T>> CreateRangeAsync(List<T> values, string createdBy);
        List<T> CreateRange(List<T> values, string createdBy);
        Task<T> DeleteAsync(Guid id, string deletdBy);
        Task<T> RemoveAsync(Guid id, string removedBy);
        Task<IEnumerable<T>> ReadAllAsync();
        Task<T> ReadOneAsync(Guid id);
         void Attach(T entity);
        Task<int> SaveAsync();
        T Update(T value, string updatedBy);
        Task<IEnumerable<T>> GetWithIncludeAsync(
            Expression<Func<T, bool>> predicate,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);
        T Create(T value, string createdBy);
        T Delete(Guid id, string deletdBy);
        IEnumerable<T> ReadAll();
        T ReadOne(Guid id);
        int Save();
        IEnumerable<T> GetWithInclude(
            Expression<Func<T, bool>> predicate,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);
    }
}
