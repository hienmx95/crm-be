using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.call_log
{
    public class CallLog_SexDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public CallLog_SexDTO() {}
        public CallLog_SexDTO(Sex Sex)
        {
            
            this.Id = Sex.Id;
            
            this.Code = Sex.Code;
            
            this.Name = Sex.Name;
            
            this.Errors = Sex.Errors;
        }
    }

    public class CallLog_SexFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public SexOrder OrderBy { get; set; }
    }
}