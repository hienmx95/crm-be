using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CallCategoryDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Customer_CallCategoryDTO() {}
        public Customer_CallCategoryDTO(CallCategory CallCategory)
        {
            
            this.Id = CallCategory.Id;
            
            this.Code = CallCategory.Code;
            
            this.Name = CallCategory.Name;
            
            this.Errors = CallCategory.Errors;
        }
    }

    public class Customer_CallCategoryFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public CallCategoryOrder OrderBy { get; set; }
    }
}