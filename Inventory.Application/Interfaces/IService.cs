using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces
{
    public interface IService<T>
    {
        Task<T> CreateAsync(T value,string createdBy,bool save=true);
        Task DeleteAsync(Guid id, string deletedBy);
        Task<T> RemoveAsync(Guid id, string removedBy);
        Task<IEnumerable<T>> ListAsync(List<Expression<Func<T, object>>> includes=null);
        Task<T> GetAsync(Guid id, List<Expression<Func<T, object>>> includes=null);
        Task<T> Update(T value,string updatedBy);
        Task<List<T>> Filter(List<string> filter);
        Task SaveAsync();
        Task<T> PartialUpdate(T value, List<string> properties);
        Task<T> GetAsync(Guid id, params string[] includes);
    }

}
