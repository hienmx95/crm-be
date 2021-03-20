using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_ActivityStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public CustomerLead_ActivityStatusDTO() {}
        public CustomerLead_ActivityStatusDTO(ActivityStatus ActivityStatus)
        {
            
            this.Id = ActivityStatus.Id;
            
            this.Code = ActivityStatus.Code;
            
            this.Name = ActivityStatus.Name;
            
            this.Errors = ActivityStatus.Errors;
        }
    }

    public class CustomerLead_ActivityStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public ActivityStatusOrder OrderBy { get; set; }
    }
}