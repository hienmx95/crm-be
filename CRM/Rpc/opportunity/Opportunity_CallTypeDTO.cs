using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_CallTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public string ColorCode { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        

        public Opportunity_CallTypeDTO() {}
        public Opportunity_CallTypeDTO(CallType CallType)
        {
            
            this.Id = CallType.Id;
            
            this.Code = CallType.Code;
            
            this.Name = CallType.Name;
            
            this.ColorCode = CallType.ColorCode;
            
            this.StatusId = CallType.StatusId;
            
            this.Used = CallType.Used;
            
            this.Errors = CallType.Errors;
        }
    }

    public class Opportunity_CallTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter ColorCode { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public CallTypeOrder OrderBy { get; set; }
    }
}