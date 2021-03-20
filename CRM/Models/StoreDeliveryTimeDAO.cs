using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class StoreDeliveryTimeDAO
    {
        public StoreDeliveryTimeDAO()
        {
            StoreDeliveryTimeMappings = new HashSet<StoreDeliveryTimeMappingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StoreDeliveryTimeMappingDAO> StoreDeliveryTimeMappings { get; set; }
    }
}
