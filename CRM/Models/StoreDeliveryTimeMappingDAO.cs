using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class StoreDeliveryTimeMappingDAO
    {
        public long StoreId { get; set; }
        public long StoreDeliveryTimeId { get; set; }

        public virtual StoreDAO Store { get; set; }
        public virtual StoreDeliveryTimeDAO StoreDeliveryTime { get; set; }
    }
}
