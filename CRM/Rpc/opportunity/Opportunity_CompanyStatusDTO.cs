using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_CompanyStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long? DisplayOrder { get; set; }
        
        public long StatusId { get; set; }
        

        public Opportunity_CompanyStatusDTO() {}
        public Opportunity_CompanyStatusDTO(CompanyStatus CompanyStatus)
        {
            
            this.Id = CompanyStatus.Id;
            
            this.Code = CompanyStatus.Code;
            
            this.Name = CompanyStatus.Name;  
            
            this.Errors = CompanyStatus.Errors;
        }
    }

    public class Opportunity_CompanyStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter DisplayOrder { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public CompanyStatusOrder OrderBy { get; set; }
    }
}