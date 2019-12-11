using Domain.Service;
using Domain.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Product.Persistance.Repository
{
    public class CommandRepository<T> : ICommandRepository<T>
        where T : class, IEntity<int>, new()
    {
        protected ProductContext DatabaseContext;
        protected DbSet<T> Set;
        public CommandRepository(ProductContext DatabaseContext)
        {
            this.DatabaseContext = DatabaseContext ?? throw new ArgumentNullException(nameof(DatabaseContext));
            this.Set = DatabaseContext.Set<T>();
        }
        public void Attach(T entity)
        {
            Set.Attach(entity);
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

        public List<T> CreateRange(List<T> values, string createdBy)
        {
            if (values == null)
            {
                throw new ArgumentNullException();
            }
            Set.AddRange(values);
            return values;
        }

        public T Delete(object id, string deletedBy)
        {
            try
            {
                T value = new T();
                value.IsDeleted = true;
                value.DeleteDate = DateTime.Now;
                value.Id = (int)id;
                PartialUpdate(value, new List<string> { "IsDeleted", "DeleteDate" });
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception("When trying to delete an object the following exception occurred " + ex.StackTrace);
            }
        }


        public void Remove(object id, string removedBy)
        {
            try
            {
                T value = new T { Id = (int)id };
                DatabaseContext.Remove(value);
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
        public void PartialUpdate(T value, List<string> properties)
        {
            var dbEntityEntry = DatabaseContext.Entry(value);
            foreach (var property in properties)
            {
                dbEntityEntry.Property(property).IsModified = true;
            }
        }

    }
}
