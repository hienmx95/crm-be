using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_BusinessTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Customer_BusinessTypeDTO() {}
        public Customer_BusinessTypeDTO(BusinessType BusinessType)
        {
            
            this.Id = BusinessType.Id;
            
            this.Code = BusinessType.Code;
            
            this.Name = BusinessType.Name;
            
            this.Errors = BusinessType.Errors;
        }
    }

    public class Customer_BusinessTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public BusinessTypeOrder OrderBy { get; set; }
    }
}