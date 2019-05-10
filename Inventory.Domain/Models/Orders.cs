using Inventory.Domain;
using System;
using System.Collections.Generic;

namespace Inventory.Domain.Models
{
    public partial class Orders : Entity
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public Guid CustomerId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public Guid? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public Customers Customer { get; set; }
        public Employees Employee { get; set; }
        public Shippers ShipViaNavigation { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
