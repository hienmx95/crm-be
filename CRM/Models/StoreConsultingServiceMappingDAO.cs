using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class StoreConsultingServiceMappingDAO
    {
        public long StoreId { get; set; }
        public long ConsultingServiceId { get; set; }

        public virtual ConsultingServiceDAO ConsultingService { get; set; }
        public virtual StoreDAO Store { get; set; }
    }
}
