using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_ConsultingServiceDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        

        public Store_ConsultingServiceDTO() {}
        public Store_ConsultingServiceDTO(ConsultingService ConsultingService)
        {
            
            this.Id = ConsultingService.Id;
            
            
            this.Name = ConsultingService.Name;
            
            this.Errors = ConsultingService.Errors;
        }
    }

    public class Store_ConsultingServiceFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        
        public StringFilter Name { get; set; }
        
        public ConsultingServiceOrder OrderBy { get; set; }
    }
}