using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Application
{
    public interface IService<T,Tkey>
    {
        Task<T> CreateAsync(T value,string createdBy,bool save=true);
        Task DeleteAsync(Tkey id, string deletedBy);
        Task RemoveAsync(Tkey id, string removedBy);
        Task<IEnumerable<T>> ListAsync(List<Expression<Func<T, object>>> includes=null);
        Task<T> GetAsync(Tkey id, List<Expression<Func<T, object>>> includes=null);
        Task<T> Update(T value,string updatedBy);
        Task<List<T>> Filter(List<string> filter);
        Task SaveAsync();
        Task<T> PartialUpdate(T value, List<string> properties);
        Task<T> GetAsync(Tkey id, params string[] includes);
    }

}
