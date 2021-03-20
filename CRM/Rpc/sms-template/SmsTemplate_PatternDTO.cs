using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.sms_template
{
    public class SmsTemplate_PatternDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }

        public string Position { get; set; }
        

        public SmsTemplate_PatternDTO() {}
        public SmsTemplate_PatternDTO(Pattern Pattern)
        {
            
            this.Id = Pattern.Id;
            
            this.Code = Pattern.Code;
            
            this.Name = Pattern.Name;

            this.Position = Pattern.Position;
            
            this.Errors = Pattern.Errors;
        }
    }

    public class SmsTemplate_PatternFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }

        public StringFilter Position { get; set; }
        
        public SexOrder OrderBy { get; set; }
    }
}