using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_CustomerLeadSourceDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Contact_CustomerLeadSourceDTO() {}
        public Contact_CustomerLeadSourceDTO(CustomerLeadSource CustomerLeadSource)
        {
            
            this.Id = CustomerLeadSource.Id;
            
            this.Code = CustomerLeadSource.Code;
            
            this.Name = CustomerLeadSource.Name;
            
            this.Errors = CustomerLeadSource.Errors;
        }
    }

    public class Contact_CustomerLeadSourceFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public CustomerLeadSourceOrder OrderBy { get; set; }
    }
}