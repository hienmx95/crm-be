using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_PhoneTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        
        public Guid RowId { get; set; }
        

        public Customer_PhoneTypeDTO() {}
        public Customer_PhoneTypeDTO(PhoneType PhoneType)
        {
            
            this.Id = PhoneType.Id;
            
            this.Code = PhoneType.Code;
            
            this.Name = PhoneType.Name;
            
            this.StatusId = PhoneType.StatusId;
            
            this.Used = PhoneType.Used;
            
            this.RowId = PhoneType.RowId;
            
            this.Errors = PhoneType.Errors;
        }
    }

    public class Customer_PhoneTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public GuidFilter RowId { get; set; }
        
        public PhoneTypeOrder OrderBy { get; set; }
    }
}