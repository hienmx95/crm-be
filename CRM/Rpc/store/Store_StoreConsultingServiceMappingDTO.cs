using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreConsultingServiceMappingDTO : DataDTO
    {
        public long StoreId { get; set; }
        public long ConsultingServiceId { get; set; }

        public ConsultingService ConsultingService { get; set; }

        public Store_StoreConsultingServiceMappingDTO()
        {

        }
        public Store_StoreConsultingServiceMappingDTO(StoreConsultingServiceMapping StoreConsultingServiceMapping)
        {
            this.StoreId = StoreConsultingServiceMapping.StoreId;
            this.ConsultingServiceId = StoreConsultingServiceMapping.ConsultingServiceId;
            this.ConsultingService = StoreConsultingServiceMapping.ConsultingService;
        }
    }

}
