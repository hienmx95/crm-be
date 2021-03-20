using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_CallEmotionDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long StatusId { get; set; }
        
        public string Description { get; set; }
        
        public bool Used { get; set; }
        
        public Guid RowId { get; set; }
        

        public Company_CallEmotionDTO() {}
        public Company_CallEmotionDTO(CallEmotion CallEmotion)
        {
            
            this.Id = CallEmotion.Id;
            
            this.Code = CallEmotion.Code;
            
            this.Name = CallEmotion.Name;
            
            this.StatusId = CallEmotion.StatusId;
            
            this.Description = CallEmotion.Description;
            
            this.Used = CallEmotion.Used;
            
            this.RowId = CallEmotion.RowId;
            
            this.Errors = CallEmotion.Errors;
        }
    }

    public class Company_CallEmotionFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public StringFilter Description { get; set; }
        
        public GuidFilter RowId { get; set; }
        
        public CallEmotionOrder OrderBy { get; set; }
    }
}