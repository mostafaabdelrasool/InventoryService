using System.Collections.Generic;
using Inventory.Domain.Models;

namespace Inventory.Application.Product.command
{
    public interface IUpdateStockCommand
    {
        void NotifyOrderSaved(List<OrderDetails> orderDetails);
    }
}