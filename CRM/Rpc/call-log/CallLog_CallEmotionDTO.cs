using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.call_log
{
    public class CallLog_CallEmotionDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public long StatusId { get; set; }
        
        public string Description { get; set; }
        

        public CallLog_CallEmotionDTO() {}
        public CallLog_CallEmotionDTO(CallEmotion CallEmotion)
        {
            
            this.Id = CallEmotion.Id;
            
            this.Name = CallEmotion.Name;
            
            this.Code = CallEmotion.Code;
            
            this.StatusId = CallEmotion.StatusId;
            
            this.Description = CallEmotion.Description;
            
            this.Errors = CallEmotion.Errors;
        }
    }

    public class CallLog_CallEmotionFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Code { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public LongFilter DisplayOrder { get; set; }
        
        public StringFilter Description { get; set; }
        
        public CallEmotionOrder OrderBy { get; set; }
    }
}