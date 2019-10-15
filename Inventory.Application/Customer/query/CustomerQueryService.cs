using Inventory.Domain.Models;
using Inventory.Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Customer.query
{
    public class CustomerQueryService : ICustomerQueryService
    {
        protected IRepository<Customers> repository;
        public CustomerQueryService(IRepository<Customers> repository)
        {
            this.repository = repository;
        }
        public async Task<List<Customers>> Search(string q)
        {
            var result=await repository.GetWithIncludeAsync(x => x.CompanyName.Contains(q));
            return result.ToList();
        }
    }
}
