using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.repair_ticket
{
    public class RepairTicket_OrderCategoryDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public RepairTicket_OrderCategoryDTO() {}
        public RepairTicket_OrderCategoryDTO(OrderCategory OrderCategory)
        {
            
            this.Id = OrderCategory.Id;
            
            this.Code = OrderCategory.Code;
            
            this.Name = OrderCategory.Name;
            
            this.Errors = OrderCategory.Errors;
        }
    }

    public class RepairTicket_OrderCategoryFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public OrderCategoryOrder OrderBy { get; set; }
    }
}