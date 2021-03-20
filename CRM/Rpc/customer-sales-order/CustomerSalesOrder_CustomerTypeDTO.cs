using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_sales_order
{
    public class CustomerSalesOrder_CustomerTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }

        public CustomerSalesOrder_CustomerTypeDTO() {}
        public CustomerSalesOrder_CustomerTypeDTO(CustomerType CustomerType)
        {
            
            this.Id = CustomerType.Id;
            
            this.Code = CustomerType.Code;
            
            this.Name = CustomerType.Name;
            
            this.Errors = CustomerType.Errors;
        }
    }

    public class CustomerSalesOrder_CustomerTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        public CustomerTypeOrder OrderBy { get; set; }
    }
}