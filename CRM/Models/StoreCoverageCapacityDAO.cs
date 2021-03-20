using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class StoreCoverageCapacityDAO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public long? StoreId { get; set; }

        public virtual StoreDAO Store { get; set; }
    }
}
