using Inventory.Domain;
using System;
using System.Collections.Generic;

namespace Inventory.Domain.Models
{
    public partial class Region : Entity
    {
        public Region()
        {
            Territories = new HashSet<Territories>();
        }

        public string RegionDescription { get; set; }

        public ICollection<Territories> Territories { get; set; }
    }
}
