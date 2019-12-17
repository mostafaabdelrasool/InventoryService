using Domain.Service.Repository;
using Product.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Product.query
{
    public class ProductSizesQueryService : IProductSizesQueryService
    {
        protected IReadRepository<ProductSize> repository;
        public ProductSizesQueryService(IReadRepository<ProductSize> repository)
        {
            this.repository = repository;
        }

        public async Task<List<ProductSize>> GetProductSizes(int id)
        {
            var result = await repository.GetWithIncludeAsync(x => x.ProductId == id);
            return result.ToList();
        }
    }
}
