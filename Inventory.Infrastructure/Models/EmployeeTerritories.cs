using Inventory.Domain;
using System;
using System.Collections.Generic;

namespace Inventory.Infrastructure.Models
{
    public partial class EmployeeTerritories : Entity
    {
        public Guid EmployeeId { get; set; }
        public Guid TerritoryId { get; set; }

        public Employees Employee { get; set; }
        public Territories Territory { get; set; }
    }
}
