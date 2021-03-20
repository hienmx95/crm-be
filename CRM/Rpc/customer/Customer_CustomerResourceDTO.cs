using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerResourceDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long StatusId { get; set; }
        
        public string Description { get; set; }
        
        public bool Used { get; set; }
        
        public Guid RowId { get; set; }
        

        public Customer_CustomerResourceDTO() {}
        public Customer_CustomerResourceDTO(CustomerResource CustomerResource)
        {
            
            this.Id = CustomerResource.Id;
            
            this.Code = CustomerResource.Code;
            
            this.Name = CustomerResource.Name;
            
            this.StatusId = CustomerResource.StatusId;
            
            this.Description = CustomerResource.Description;
            
            this.Used = CustomerResource.Used;
            
            this.RowId = CustomerResource.RowId;
            
            this.Errors = CustomerResource.Errors;
        }
    }

    public class Customer_CustomerResourceFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public StringFilter Description { get; set; }
        
        public GuidFilter RowId { get; set; }
        
        public CustomerResourceOrder OrderBy { get; set; }
    }
}