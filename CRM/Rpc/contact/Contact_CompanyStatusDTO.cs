using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_CompanyStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Contact_CompanyStatusDTO() {}
        public Contact_CompanyStatusDTO(CompanyStatus CompanyStatus)
        {
            
            this.Id = CompanyStatus.Id;
            
            this.Code = CompanyStatus.Code;
            
            this.Name = CompanyStatus.Name;
            
            this.Errors = CompanyStatus.Errors;
        }
    }

    public class Contact_CompanyStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public CompanyStatusOrder OrderBy { get; set; }
    }
}