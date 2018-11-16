using Inventory.Domain;
using System;
using System.Collections.Generic;

namespace Inventory.Infrastructure.Models
{
    public partial class CustomerCustomerDemo : Entity
    {
        public Guid CustomerId { get; set; }
        public string CustomerTypeId { get; set; }

        public Customers Customer { get; set; }
        public CustomerDemographics CustomerType { get; set; }
    }
}
