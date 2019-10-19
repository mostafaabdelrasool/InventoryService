using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Domain.Models;

namespace Inventory.Application.Product.query
{
    public interface IProductSizesQueryService
    {
        Task<List<ProductSizes>> GetProductSizes(Guid id);
    }
}