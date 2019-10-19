using Inventory.Domain.Models;
using Inventory.Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Product.query
{
    public class ProductSizesQueryService : IProductSizesQueryService
    {
        protected IRepository<ProductSizes> repository;
        public ProductSizesQueryService(IRepository<ProductSizes> repository)
        {
            this.repository = repository;
        }

        public async Task<List<ProductSizes>> GetProductSizes(Guid id)
        {
            var result = await repository.GetWithIncludeAsync(x => x.ProductId == id);
            return result.ToList();
        }
    }
}
