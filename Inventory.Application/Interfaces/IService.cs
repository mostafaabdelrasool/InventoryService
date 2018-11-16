using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces
{
    public interface IService<T>
    {
        Task<T> CreateAsync(T value,string createdBy);
        Task<T> DeleteAsync(Guid id, string deletedBy);
        Task<T> RemoveAsync(Guid id, string removedBy);
        Task<IEnumerable<T>> ListAsync();
        Task<T> GetAsync(Guid id);
        T Update(T value,string updatedBy);
    }

}
