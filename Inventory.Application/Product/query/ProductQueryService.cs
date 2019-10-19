using Inventory.Domain.Models;
using Inventory.Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Product.query
{
    public class ProductQueryService : IProductQueryService
    {
        protected IRepository<Products> repository;
        public ProductQueryService(IRepository<Products> repository)
        {
            this.repository = repository;
        }

      

        public async Task<List<Products>> Search(string q)
        {
            var result = await repository.GetWithIncludeAsync(x => x.ProductName.Contains(q));
            return result.ToList();
        }
    }
}
