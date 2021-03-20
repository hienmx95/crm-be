using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_ActivityTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Opportunity_ActivityTypeDTO() {}
        public Opportunity_ActivityTypeDTO(ActivityType ActivityType)
        {
            
            this.Id = ActivityType.Id;
            
            this.Code = ActivityType.Code;
            
            this.Name = ActivityType.Name;
            
            this.Errors = ActivityType.Errors;
        }
    }

    public class Opportunity_ActivityTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public ActivityTypeOrder OrderBy { get; set; }
    }
}