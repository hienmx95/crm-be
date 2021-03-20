using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.role
{
    public class Role_CustomerGroupingDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public string Path { get; set; }
        
        public long Level { get; set; }
        
        public long? StatusId { get; set; }
        public long? TypeId { get; set; }
        
        public string Color { get; set; }
        
        public string Description { get; set; }
        
        public long? ParentId { get; set; }
        

        public Role_CustomerGroupingDTO() {}
        public Role_CustomerGroupingDTO(CustomerGrouping CustomerGrouping)
        {
            
            this.Id = CustomerGrouping.Id;
            
            this.Name = CustomerGrouping.Name;
            
            this.Code = CustomerGrouping.Code;
            
            this.Path = CustomerGrouping.Path;
            
            this.Level = CustomerGrouping.Level;
            
            this.StatusId = CustomerGrouping.StatusId;
            
            this.Description = CustomerGrouping.Description;
            
            this.ParentId = CustomerGrouping.ParentId;
            
            this.Errors = CustomerGrouping.Errors;
        }
    }

    public class Role_CustomerGroupingFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Path { get; set; }
        
        public LongFilter Level { get; set; }
        
        public IdFilter StatusId { get; set; }
        public IdFilter CustomerTypeId { get; set; }
        
        public StringFilter Color { get; set; }
        
        public StringFilter Description { get; set; }
        
        public IdFilter ParentId { get; set; }
        
        public DecimalFilter Credit { get; set; }
        
        public CustomerGroupingOrder OrderBy { get; set; }
    }
}