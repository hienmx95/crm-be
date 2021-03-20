using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_ActivityTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Company_ActivityTypeDTO() {}
        public Company_ActivityTypeDTO(ActivityType ActivityType)
        {
            
            this.Id = ActivityType.Id;
            
            this.Code = ActivityType.Code;
            
            this.Name = ActivityType.Name;
            
            this.Errors = ActivityType.Errors;
        }
    }

    public class Company_ActivityTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public ActivityTypeOrder OrderBy { get; set; }
    }
}