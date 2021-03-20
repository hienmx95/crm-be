using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ConsultingServiceDAO
    {
        public ConsultingServiceDAO()
        {
            StoreConsultingServiceMappings = new HashSet<StoreConsultingServiceMappingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StoreConsultingServiceMappingDAO> StoreConsultingServiceMappings { get; set; }
    }
}
