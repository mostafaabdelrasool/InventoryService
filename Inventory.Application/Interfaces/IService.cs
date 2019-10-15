using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces
{
    public interface IService<T>
    {
        Task<T> CreateAsync(T value,string createdBy);
        Task DeleteAsync(Guid id, string deletedBy);
        Task<T> RemoveAsync(Guid id, string removedBy);
        Task<IEnumerable<T>> ListAsync();
        Task<T> GetAsync(Guid id);
        Task<T> Update(T value,string updatedBy);
        Task<List<T>> Filter(List<string> filter);
    }

}
