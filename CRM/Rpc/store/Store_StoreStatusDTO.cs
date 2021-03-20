using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Store_StoreStatusDTO() {}
        public Store_StoreStatusDTO(StoreStatus StoreStatus)
        {
            
            this.Id = StoreStatus.Id;
            
            this.Code = StoreStatus.Code;
            
            this.Name = StoreStatus.Name;
            
            this.Errors = StoreStatus.Errors;
        }
    }

    public class Store_StoreStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StoreStatusOrder OrderBy { get; set; }
    }
}