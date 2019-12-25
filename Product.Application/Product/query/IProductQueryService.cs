using Product.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Product.query
{
    public interface IProductQueryService
    {
        Task<List<Products>> Search(string q);
        Task<bool> ValidateStock(int productId, int amount);
    }
}