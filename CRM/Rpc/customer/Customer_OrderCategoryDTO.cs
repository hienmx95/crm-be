using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_OrderCategoryDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Customer_OrderCategoryDTO() {}
        public Customer_OrderCategoryDTO(OrderCategory OrderCategory)
        {
            
            this.Id = OrderCategory.Id;
            
            this.Code = OrderCategory.Code;
            
            this.Name = OrderCategory.Name;
            
            this.Errors = OrderCategory.Errors;
        }
    }

    public class Customer_OrderCategoryFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public OrderCategoryOrder OrderBy { get; set; }
    }
}