using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_EmailStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Contact_EmailStatusDTO() {}
        public Contact_EmailStatusDTO(EmailStatus EmailStatus)
        {
            
            this.Id = EmailStatus.Id;
            
            this.Code = EmailStatus.Code;
            
            this.Name = EmailStatus.Name;
            
            this.Errors = EmailStatus.Errors;
        }
    }

    public class Contact_EmailStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public EmailStatusOrder OrderBy { get; set; }
    }
}