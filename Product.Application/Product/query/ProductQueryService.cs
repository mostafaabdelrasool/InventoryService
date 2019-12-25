
using Domain.Service.Repository;
using Product.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Product.query
{
    public class ProductQueryService : IProductQueryService
    {
        protected IReadRepository<Products> repository;
        public ProductQueryService(IReadRepository<Products> repository)
        {
            this.repository = repository;
        }
        public async Task<List<Products>> Search(string q)
        {
            var result = await repository.GetWithIncludeAsync(x => (x.ProductName.Contains(q) || x.ProductCode.Contains(q)) && 
            !x.IsDeleted);
            return result.ToList();
        }

        public async Task<bool> ValidateStock(int productId, int amount)
        {
            var result = await repository.GetWithIncludeAsync(x =>x.Id==productId && x.UnitsInStock<amount);
            return result == null;
        }
    }
}
