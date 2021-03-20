using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_ActivityPriorityDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Contact_ActivityPriorityDTO() {}
        public Contact_ActivityPriorityDTO(ActivityPriority ActivityPriority)
        {
            
            this.Id = ActivityPriority.Id;
            
            this.Code = ActivityPriority.Code;
            
            this.Name = ActivityPriority.Name;
            
            this.Errors = ActivityPriority.Errors;
        }
    }

    public class Contact_ActivityPriorityFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public ActivityPriorityOrder OrderBy { get; set; }
    }
}