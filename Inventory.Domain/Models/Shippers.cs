using Inventory.Domain;
using System;
using System.Collections.Generic;

namespace Inventory.Domain.Models
{
    public partial class Shippers : Entity
    {
        public Shippers()
        {
            Orders = new HashSet<Orders>();
        }

        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
