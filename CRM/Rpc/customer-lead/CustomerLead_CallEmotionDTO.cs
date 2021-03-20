using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_CallEmotionDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public long StatusId { get; set; }
        
        public string Description { get; set; }
        

        public CustomerLead_CallEmotionDTO() {}
        public CustomerLead_CallEmotionDTO(CallEmotion CallEmotion)
        {
            
            this.Id = CallEmotion.Id;
            
            this.Name = CallEmotion.Name;
            
            this.Code = CallEmotion.Code;
            
            this.StatusId = CallEmotion.StatusId;
            
            this.Description = CallEmotion.Description;
            
            this.Errors = CallEmotion.Errors;
        }
    }

    public class CustomerLead_CallEmotionFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Code { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public StringFilter Description { get; set; }
        
        public CallEmotionOrder OrderBy { get; set; }
    }
}