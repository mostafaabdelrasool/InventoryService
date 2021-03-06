﻿using Inventory.Domain;
using System;
using System.Collections.Generic;

namespace Inventory.Domain.Models
{
    public  class OrderDetails : Entity
    {
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductSizeId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal Total { get; set; }
        public Orders Order { get; set; }
    }
}
