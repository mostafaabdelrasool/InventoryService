using Inventory.Domain;
using System;
using System.Collections.Generic;

namespace Inventory.Infrastructure.Models
{
    public partial class Territories : Entity
    {
        public Territories()
        {
            EmployeeTerritories = new HashSet<EmployeeTerritories>();
        }

        public string TerritoryDescription { get; set; }
        public Guid RegionId { get; set; }

        public Region Region { get; set; }
        public ICollection<EmployeeTerritories> EmployeeTerritories { get; set; }
    }
}
