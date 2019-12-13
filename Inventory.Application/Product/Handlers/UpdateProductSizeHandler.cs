﻿using Inventory.Application.Product.command;
using Inventory.Domain.Models;
using Inventory.Persistance.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Product.Handlers
{
    public class UpdateProductSizeHandler : INotificationHandler<UpdateStockCommand>
    {
        private IRepository<ProductSizes> _repository;

        public UpdateProductSizeHandler(IRepository<ProductSizes> repository)
        {

            _repository = repository;
        }
        public async Task Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var productIds = request.Order.OrderDetails.ToList().Select(x => x.ProductSizeId);
            var products = await _repository.GetWithIncludeAsync(x => productIds.Any(y => y == x.Id));
            products.ToList().ForEach(x =>
            {
                x.UnitInStock -= request.Order.OrderDetails.FirstOrDefault(p => p.ProductSizeId == x.Id).Quantity;
                _repository.Update(x, "");
            });
            await _repository.SaveAsync();
        }
    }
}
