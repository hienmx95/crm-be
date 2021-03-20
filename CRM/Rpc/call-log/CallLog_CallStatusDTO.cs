using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.call_log
{
    public class CallLog_CallStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public CallLog_CallStatusDTO() {}
        public CallLog_CallStatusDTO(CallStatus CallStatus)
        {
            
            this.Id = CallStatus.Id;
            
            this.Code = CallStatus.Code;
            
            this.Name = CallStatus.Name;
            
            this.Errors = CallStatus.Errors;
        }
    }

    public class CallLog_CallStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public CallStatusOrder OrderBy { get; set; }
    }
}