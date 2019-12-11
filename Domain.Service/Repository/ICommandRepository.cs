using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Repository
{
    public interface ICommandRepository<T>
    {
        List<T> CreateRange(List<T> values, string createdBy);
        void Remove(object id, string removedBy);
        void Attach(T entity);
        Task<int> SaveAsync();
        T Update(T value, string updatedBy);
        T Create(T value, string createdBy);
        T Delete(object id, string deletdBy);
        void PartialUpdate(T value, List<string> properties);
    }
}
