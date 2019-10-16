using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Domain.Models;

namespace Inventory.Application.Product.query
{
    public interface IProductQueryService
    {
        Task<List<Products>> Search(string q);
    }
}