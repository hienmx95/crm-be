using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_StoreTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long? ColorId { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        
        public Guid RowId { get; set; }
        

        public Customer_StoreTypeDTO() {}
        public Customer_StoreTypeDTO(StoreType StoreType)
        {
            
            this.Id = StoreType.Id;
            
            this.Code = StoreType.Code;
            
            this.Name = StoreType.Name;
            
            this.ColorId = StoreType.ColorId;
            
            this.StatusId = StoreType.StatusId;
            
            this.Used = StoreType.Used;
            
            this.RowId = StoreType.RowId;
            
            this.Errors = StoreType.Errors;
        }
    }

    public class Customer_StoreTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public IdFilter ColorId { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public GuidFilter RowId { get; set; }
        
        public StoreTypeOrder OrderBy { get; set; }
    }
}