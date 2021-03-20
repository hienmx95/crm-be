using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_CompanyStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Company_CompanyStatusDTO() {}
        public Company_CompanyStatusDTO(CompanyStatus CompanyStatus)
        {
            
            this.Id = CompanyStatus.Id;
            
            this.Code = CompanyStatus.Code;
            
            this.Name = CompanyStatus.Name;
            
            this.Errors = CompanyStatus.Errors;
        }
    }

    public class Company_CompanyStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public CompanyStatusOrder OrderBy { get; set; }
    }
}