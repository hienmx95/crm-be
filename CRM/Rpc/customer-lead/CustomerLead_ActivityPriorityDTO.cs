using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_ActivityPriorityDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public CustomerLead_ActivityPriorityDTO() {}
        public CustomerLead_ActivityPriorityDTO(ActivityPriority ActivityPriority)
        {
            
            this.Id = ActivityPriority.Id;
            
            this.Code = ActivityPriority.Code;
            
            this.Name = ActivityPriority.Name;
            
            this.Errors = ActivityPriority.Errors;
        }
    }

    public class CustomerLead_ActivityPriorityFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public ActivityPriorityOrder OrderBy { get; set; }
    }
}