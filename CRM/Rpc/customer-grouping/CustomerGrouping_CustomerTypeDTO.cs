using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_grouping
{
    public class CustomerGrouping_CustomerTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        public CustomerGrouping_CustomerTypeDTO() {}
        public CustomerGrouping_CustomerTypeDTO(CustomerType CustomerType)
        {
            
            this.Id = CustomerType.Id;
            
            this.Code = CustomerType.Code;
            
            this.Name = CustomerType.Name;
            
            this.Errors = CustomerType.Errors;
        }
    }

    public class CustomerGrouping_CustomerTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        public CustomerTypeOrder OrderBy { get; set; }
    }
}