using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.order_report
{
    public class OrderReport_OrderCategoryDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public OrderReport_OrderCategoryDTO() {}
        public OrderReport_OrderCategoryDTO(OrderCategory OrderCategory)
        {
            
            this.Id = OrderCategory.Id;
            
            this.Code = OrderCategory.Code;
            
            this.Name = OrderCategory.Name;
            
            this.Errors = OrderCategory.Errors;
        }
    }

    public class OrderReport_OrderCategoryFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public OrderCategoryOrder OrderBy { get; set; }
    }
}