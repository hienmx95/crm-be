using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_ActivityPriorityDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Opportunity_ActivityPriorityDTO() {}
        public Opportunity_ActivityPriorityDTO(ActivityPriority ActivityPriority)
        {
            
            this.Id = ActivityPriority.Id;
            
            this.Code = ActivityPriority.Code;
            
            this.Name = ActivityPriority.Name;
            
            this.Errors = ActivityPriority.Errors;
        }
    }

    public class Opportunity_ActivityPriorityFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public ActivityPriorityOrder OrderBy { get; set; }
    }
}