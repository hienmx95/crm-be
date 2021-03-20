using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_NationDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long? DisplayOrder { get; set; }
        
        public long StatusId { get; set; }
        

        public CustomerLead_NationDTO() {}
        public CustomerLead_NationDTO(Nation Nation)
        {
            
            this.Id = Nation.Id;
            
            this.Code = Nation.Code;
            
            this.Name = Nation.Name;
            
            this.DisplayOrder = Nation.Priority;
            
            this.StatusId = Nation.StatusId;
            
            this.Errors = Nation.Errors;
        }
    }

    public class CustomerLead_NationFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter DisplayOrder { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public NationOrder OrderBy { get; set; }
    }
}