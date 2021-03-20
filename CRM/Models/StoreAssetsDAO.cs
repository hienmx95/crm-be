using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class StoreAssetsDAO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Quantity { get; set; }
        public long? Owned { get; set; }
        public long? Rent { get; set; }
        public long? StoreId { get; set; }

        public virtual StoreDAO Store { get; set; }
    }
}
