using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_sales_order
{
    public class CustomerSalesOrder_NationDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long? Priority { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        
        public Guid RowId { get; set; }
        

        public CustomerSalesOrder_NationDTO() {}
        public CustomerSalesOrder_NationDTO(Nation Nation)
        {
            
            this.Id = Nation.Id;
            
            this.Code = Nation.Code;
            
            this.Name = Nation.Name;
            
            this.Priority = Nation.Priority;
            
            this.StatusId = Nation.StatusId;
            
            this.Used = Nation.Used;
            
            this.RowId = Nation.RowId;
            
            this.Errors = Nation.Errors;
        }
    }

    public class CustomerSalesOrder_NationFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter Priority { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public GuidFilter RowId { get; set; }
        
        public NationOrder OrderBy { get; set; }
    }
}