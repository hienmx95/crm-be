using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_SexDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public CustomerLead_SexDTO() {}
        public CustomerLead_SexDTO(Sex Sex)
        {
            
            this.Id = Sex.Id;
            
            this.Code = Sex.Code;
            
            this.Name = Sex.Name;
            
            this.Errors = Sex.Errors;
        }
    }

    public class CustomerLead_SexFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public SexOrder OrderBy { get; set; }
    }
}