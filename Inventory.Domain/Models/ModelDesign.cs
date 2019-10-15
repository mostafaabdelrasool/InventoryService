using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Models
{
    public class ModelDesign: Entity
    {
        public string Code { get; set; }

        public string Image { get; set; }

        public string Dimension { get; set; }

        public string Sizes { get; set; }

        public long Cost { get; set; }

        public long SalesPrice { get; set; }

        public string Manifature { get; set; }
    }
}
