using System.Collections.Generic;
using Inventory.Domain.Models;

namespace Inventory.Application.Product.command
{
    public interface IUpdateStockCommand
    {
        void Notify(List<OrderDetails> orderDetails);
    }
}