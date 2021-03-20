using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_sales_order
{
    public class CustomerSalesOrder_RequestStateDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public CustomerSalesOrder_RequestStateDTO() {}
        public CustomerSalesOrder_RequestStateDTO(RequestState RequestState)
        {
            
            this.Id = RequestState.Id;
            
            this.Code = RequestState.Code;
            
            this.Name = RequestState.Name;
            
            this.Errors = RequestState.Errors;
        }
    }

    public class CustomerSalesOrder_RequestStateFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public RequestStateOrder OrderBy { get; set; }
    }
}