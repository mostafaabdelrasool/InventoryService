using Inventory.Domain.Models;
using Inventory.Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Product.command
{
    public class UpdateStockCommand : IUpdateStockCommand
    {
        private IRepository<Products> _repository;

        public UpdateStockCommand(IRepository<Products> repository)
        {

            _repository = repository;
        }
      
    }
}
