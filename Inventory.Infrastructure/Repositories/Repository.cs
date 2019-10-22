using Inventory.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Inventory.Persistance.Models;
using Inventory.Persistance.Utility;
using System.Linq.Dynamic.Core;
using System.Threading;

namespace Inventory.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        protected NorthwindContext DatabaseContext;
        protected DbSet<T> Set;
        public Repository(NorthwindContext DatabaseContext)
        {
            this.DatabaseContext = DatabaseContext ?? throw new ArgumentNullException(nameof(DatabaseContext));
            this.Set = DatabaseContext.Set<T>();
        }

        public async Task<T> CreateAsync(T value, string createdBy)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            value.CreateDate = DateTime.Now;
            await Set.AddAsync(value);
            return value;
        }
        public NorthwindContext GetContext()
        {
            return DatabaseContext;
        }
        public T Create(T value, string createdBy)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            value.CreateDate = DateTime.Now;
            Set.Add(value);
            return value;
        }
        public async Task<T> ReadOneAsync(Guid id)
        {
            var current = await Set.SingleOrDefaultAsync(p => p.Id == id) ?? throw new Exception(nameof(T) + " does not contain an object with the id " + id);
            return current;
        }
        public T ReadOne(Guid id)
        {
            var current = Set.FirstOrDefault(p => p.Id == id) ?? throw new Exception(nameof(T) + " does not contain an object with the id " + id);
            return current;
        }

        public void Attach(T entity)
        {
            Set.Attach(entity);
        }
        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await Set.ToListAsync();
        }
        public IEnumerable<T> ReadAll()
        {
            return Set.ToList();
        }
        public T Update(T value, string updatedBy)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                value.ModifyDate = DateTime.Now;
                DatabaseContext.Update(value);
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception("When trying to update an object the following exception occurred " + ex.StackTrace);
            }
        }
        public async Task<T> DeleteAsync(Guid id, string deletedBy)
        {
            try
            {
                var current = await ReadOneAsync(id);
                SetAsDelete(current);
                return current;
            }
            catch (Exception ex)
            {
                throw new Exception("When trying to delete an object the following exception occurred " + ex.StackTrace);
            }
        }
        public T Delete(Guid id, string deletedBy)
        {
            try
            {
                var current = ReadOne(id);
                SetAsDelete(current);
                return current;
            }
            catch (Exception ex)
            {
                throw new Exception("When trying to delete an object the following exception occurred " + ex.StackTrace);
            }
        }
        private void SetAsDelete(T value)
        {
            value.IsDeleted = true;
            value.DeleteDate = DateTime.Now;
            var entity = DatabaseContext.Entry(value);
            entity.State = EntityState.Modified;
        }

        public async Task<T> RemoveAsync(Guid id, string removedBy)
        {
            try
            {
                T value = new T { Id = id };
                DatabaseContext.Remove(value);
                await SaveAsync();
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception("When trying to remove an object the following exception occurred " + ex.StackTrace);
            }
        }
        public async Task<int> SaveAsync()
        {
            return await DatabaseContext.SaveChangesAsync();
        }
        public int Save()
        {
            return DatabaseContext.SaveChanges();
        }
        public async Task<IEnumerable<T>> GetWithIncludeAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = SetIncludeWithFilter(predicate,null, includes);
            return await query.ToListAsync();
        }
        public IEnumerable<T> GetWithInclude(
           Expression<Func<T, bool>> predicate,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = SetIncludeWithFilter(predicate, orderBy, includes);
            return query.ToList();
        }
        private IQueryable<T> SetIncludeWithFilter(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Expression<Func<T, object>>[] includes)
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
            if (orderBy != null)
                query = orderBy(query);
            return query;
        }



        public List<T> CreateRange(List<T> values, string createdBy)
        {
            if (values == null)
            {
                throw new ArgumentNullException();
            }
            Set.AddRange(values);
            return values;
        }

        public async Task<List<T>> CreateRangeAsync(List<T> values, string createdBy)
        {
            if (values == null)
            {
                throw new ArgumentNullException();
            }
            await Set.AddRangeAsync(values);
            return values;
        }
        public async Task<List<T>> DynamicQuery(List<string> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = this.Set;
            var builder = QueryFunctions.PredicateBuilder(filter);
            if (builder != null)
            {
                try
                {
                    return await query.Where(builder.Item1, builder.Item2).ToListAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return null;
        }
    }
}
