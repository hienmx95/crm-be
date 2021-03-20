using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.call_log
{
    public class CallLog_CustomerLevelDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public string Color { get; set; }
        
        public long? StatusId { get; set; }
        
        public string Description { get; set; }
        
        public long PointFrom { get; set; }
        
        public long PointTo { get; set; }
        

        public CallLog_CustomerLevelDTO() {}
        public CallLog_CustomerLevelDTO(CustomerLevel CustomerLevel)
        {
            
            this.Id = CustomerLevel.Id;
            
            this.Name = CustomerLevel.Name;
            
            this.Code = CustomerLevel.Code;
            
            this.Color = CustomerLevel.Color;
            
            this.StatusId = CustomerLevel.StatusId;
            
            this.Description = CustomerLevel.Description;
            
            this.PointFrom = CustomerLevel.PointFrom;
            
            this.PointTo = CustomerLevel.PointTo;
            
            this.Errors = CustomerLevel.Errors;
        }
    }

    public class CallLog_CustomerLevelFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Color { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public StringFilter Description { get; set; }
        
        public LongFilter PointFrom { get; set; }
        
        public LongFilter PointTo { get; set; }
        
        public CustomerLevelOrder OrderBy { get; set; }
    }
}