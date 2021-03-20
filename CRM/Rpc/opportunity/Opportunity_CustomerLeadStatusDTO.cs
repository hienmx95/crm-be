using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_CustomerLeadStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }

        public Opportunity_CustomerLeadStatusDTO() {}
        public Opportunity_CustomerLeadStatusDTO(CustomerLeadStatus CustomerLeadStatus)
        {
            
            this.Id = CustomerLeadStatus.Id;
            
            this.Code = CustomerLeadStatus.Code;
            
            this.Name = CustomerLeadStatus.Name;

            this.Errors = CustomerLeadStatus.Errors;
        }
    }

    public class Opportunity_CustomerLeadStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public CustomerLeadStatusOrder OrderBy { get; set; }
    }
}