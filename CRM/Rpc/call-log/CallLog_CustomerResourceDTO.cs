using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.call_log
{
    public class CallLog_CustomerResourceDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public string Color { get; set; }
        
        public long? StatusId { get; set; }
        
        public long? DisplayOrder { get; set; }
        
        public string Description { get; set; }
        

        public CallLog_CustomerResourceDTO() {}
        public CallLog_CustomerResourceDTO(CustomerResource CustomerResource)
        {
            
            this.Id = CustomerResource.Id;
            
            this.Name = CustomerResource.Name;
            
            this.Code = CustomerResource.Code;
            
            this.StatusId = CustomerResource.StatusId;
            
            this.Description = CustomerResource.Description;
            
            this.Errors = CustomerResource.Errors;
        }
    }

    public class CallLog_CustomerResourceFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Color { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public LongFilter DisplayOrder { get; set; }
        
        public StringFilter Description { get; set; }
        
        public CustomerResourceOrder OrderBy { get; set; }
    }
}