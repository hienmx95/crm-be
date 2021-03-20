using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_CustomerLeadLevelDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public CustomerLead_CustomerLeadLevelDTO() {}
        public CustomerLead_CustomerLeadLevelDTO(CustomerLeadLevel CustomerLeadLevel)
        {
            
            this.Id = CustomerLeadLevel.Id;
            
            this.Code = CustomerLeadLevel.Code;
            
            this.Name = CustomerLeadLevel.Name;
            
            this.Errors = CustomerLeadLevel.Errors;
        }
    }

    public class CustomerLead_CustomerLeadLevelFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public CustomerLeadLevelOrder OrderBy { get; set; }
    }
}