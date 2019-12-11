using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Repository
{
    public interface IReadRepository<T>
    {
        Task<IEnumerable<T>> ReadAllAsync();
        Task<T> ReadOneAsync(object id, List<Expression<Func<T, object>>> includes = null);
        Task<T> GetFirtOrDefaultAsync(Expression<Func<T, bool>> predicate, 
            params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetWithIncludeAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);
        IEnumerable<T> ReadAll();
        T ReadOne(Guid id);
        IEnumerable<T> GetWithInclude(
           Expression<Func<T, bool>> predicate,
           params Expression<Func<T, object>>[] includes);
        Task<T> ReadOneAsync(object id, params string[] includes);
        Task<List<T>> SelectQuery(string query, params object[] parameters);
    }
}
