using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.mail_template
{
    public class MailTemplate_PatternDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }

        public string Position { get; set; }
        

        public MailTemplate_PatternDTO() {}
        public MailTemplate_PatternDTO(Pattern Pattern)
        {
            
            this.Id = Pattern.Id;
            
            this.Code = Pattern.Code;
            
            this.Name = Pattern.Name;

            this.Position = Pattern.Position;
            
            this.Errors = Pattern.Errors;
        }
    }

    public class MailTemplate_PatternFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }

        public StringFilter Position { get; set; }
        
        public SexOrder OrderBy { get; set; }
    }
}