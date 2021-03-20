using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_CustomerLeadStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }

        public CustomerLead_CustomerLeadStatusDTO() {}
        public CustomerLead_CustomerLeadStatusDTO(CustomerLeadStatus CustomerLeadStatus)
        {
            
            this.Id = CustomerLeadStatus.Id;
            
            this.Code = CustomerLeadStatus.Code;
            
            this.Name = CustomerLeadStatus.Name;

            this.Errors = CustomerLeadStatus.Errors;
        }
    }

    public class CustomerLead_CustomerLeadStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public CustomerLeadStatusOrder OrderBy { get; set; }
    }
}