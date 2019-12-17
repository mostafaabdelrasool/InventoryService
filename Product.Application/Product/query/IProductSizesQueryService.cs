using Product.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Product.query
{
    public interface IProductSizesQueryService
    {
        Task<List<ProductSize>> GetProductSizes(int id);
    }
}