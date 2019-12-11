using Domain.Service;
using Domain.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Persistance.Repository
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IEntity<int>, new()
    {
        protected ProductContext DatabaseContext;
        protected DbSet<T> Set;
        public ReadRepository(ProductContext DatabaseContext)
        {
            this.DatabaseContext = DatabaseContext ?? throw new ArgumentNullException(nameof(DatabaseContext));
            this.Set = DatabaseContext.Set<T>();
        }
        public Task<T> GetFirtOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = SetIncludeWithFilter(predicate, includes);
            return query.FirstOrDefaultAsync();
        }
        private IQueryable<T> SetIncludeWithFilter(Expression<Func<T, bool>> predicate,
             Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = this.Set;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return query;
        }
        public async Task<IEnumerable<T>> GetWithIncludeAsync(
             Expression<Func<T, bool>> predicate,
             params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = SetIncludeWithFilter(predicate, includes);
            return await query.ToListAsync();
        }
        public IEnumerable<T> GetWithInclude(
           Expression<Func<T, bool>> predicate,
           params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = SetIncludeWithFilter(predicate, includes);
            return query.ToList();
        }

        public IEnumerable<T> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

      

        public Task<List<T>> SelectQuery(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<T> ReadOneAsync(object id, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = this.Set;
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            var current = await query.FirstOrDefaultAsync(p => p.Id == (int)id) ??
                throw new Exception(nameof(T) + " does not contain an object with the id " + id);
            return current;
        }

        public T ReadOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> ReadOneAsync(object id, params string[] includes)
        {
            IQueryable<T> query = this.Set;
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            var current = await query.FirstOrDefaultAsync(p => p.Id == (int)id) ?? throw new Exception(nameof(T) + " does not contain an object with the id " + id);
            return current;
        }
    }
}
