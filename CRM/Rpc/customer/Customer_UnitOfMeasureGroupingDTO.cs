using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_UnitOfMeasureGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public long UnitOfMeasureId { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        

        public Customer_UnitOfMeasureGroupingDTO() {}
        public Customer_UnitOfMeasureGroupingDTO(UnitOfMeasureGrouping UnitOfMeasureGrouping)
        {
            
            this.Id = UnitOfMeasureGrouping.Id;
            
            this.Code = UnitOfMeasureGrouping.Code;
            
            this.Name = UnitOfMeasureGrouping.Name;
            
            this.Description = UnitOfMeasureGrouping.Description;
            
            this.UnitOfMeasureId = UnitOfMeasureGrouping.UnitOfMeasureId;
            
            this.StatusId = UnitOfMeasureGrouping.StatusId;
            
            this.Used = UnitOfMeasureGrouping.Used;
            
            this.Errors = UnitOfMeasureGrouping.Errors;
        }
    }

    public class Customer_UnitOfMeasureGroupingFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Description { get; set; }
        
        public IdFilter UnitOfMeasureId { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public UnitOfMeasureGroupingOrder OrderBy { get; set; }
    }
}