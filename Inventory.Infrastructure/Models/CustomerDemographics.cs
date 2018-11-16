using Inventory.Domain;
using System;
using System.Collections.Generic;

namespace Inventory.Infrastructure.Models
{
    public partial class CustomerDemographics : Entity
    {
        public CustomerDemographics()
        {
            CustomerCustomerDemo = new HashSet<CustomerCustomerDemo>();
        }

        public string CustomerTypeId { get; set; }
        public string CustomerDesc { get; set; }

        public ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
    }
}
