using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Customer_CustomerTypeDTO() {}
        public Customer_CustomerTypeDTO(CustomerType CustomerType)
        {
            
            this.Id = CustomerType.Id;
            
            this.Code = CustomerType.Code;
            
            this.Name = CustomerType.Name;
            
            this.Errors = CustomerType.Errors;
        }
    }

    public class Customer_CustomerTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public CustomerTypeOrder OrderBy { get; set; }
    }
}