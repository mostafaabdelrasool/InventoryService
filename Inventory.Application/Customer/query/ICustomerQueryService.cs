using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Domain.Models;

namespace Inventory.Application.Customer.query
{
    public interface ICustomerQueryService
    {
        Task<List<Customers>> Search(string q);
    }
}