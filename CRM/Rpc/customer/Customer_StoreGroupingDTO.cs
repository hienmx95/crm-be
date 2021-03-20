using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_StoreGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long? ParentId { get; set; }
        
        public string Path { get; set; }
        
        public long Level { get; set; }
        
        public long StatusId { get; set; }
        
        public Guid RowId { get; set; }
        
        public bool Used { get; set; }
        

        public Customer_StoreGroupingDTO() {}
        public Customer_StoreGroupingDTO(StoreGrouping StoreGrouping)
        {
            
            this.Id = StoreGrouping.Id;
            
            this.Code = StoreGrouping.Code;
            
            this.Name = StoreGrouping.Name;
            
            this.ParentId = StoreGrouping.ParentId;
            
            this.Path = StoreGrouping.Path;
            
            this.Level = StoreGrouping.Level;
            
            this.StatusId = StoreGrouping.StatusId;
            
            this.RowId = StoreGrouping.RowId;
            
            this.Used = StoreGrouping.Used;
            
            this.Errors = StoreGrouping.Errors;
        }
    }

    public class Customer_StoreGroupingFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public IdFilter ParentId { get; set; }
        
        public StringFilter Path { get; set; }
        
        public LongFilter Level { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public GuidFilter RowId { get; set; }
        
        public StoreGroupingOrder OrderBy { get; set; }
    }
}