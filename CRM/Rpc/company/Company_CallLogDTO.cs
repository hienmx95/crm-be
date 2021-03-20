using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_CallLogDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Phone { get; set; }
        public DateTime CallTime { get; set; }
        public long EntityReferenceId { get; set; }
        public long EntityId { get; set; }
        public long CallTypeId { get; set; }
        public long? CallEmotionId { get; set; }
        public long AppUserId { get; set; }
        public long CreatorId { get; set; }
        public Company_AppUserDTO AppUser { get; set; }
        public Company_CallEmotionDTO CallEmotion { get; set; }
        public Company_CallTypeDTO CallType { get; set; }
        public Company_AppUserDTO Creator { get; set; }
        public Company_EntityReferenceDTO EntityReference { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Company_CallLogDTO() { }
        public Company_CallLogDTO(CallLog CallLog)
        {
            this.Id = CallLog.Id;
            this.Title = CallLog.Title;
            this.Content = CallLog.Content;
            this.Phone = CallLog.Phone;
            this.CallTime = CallLog.CallTime;
            this.EntityReferenceId = CallLog.EntityReferenceId;
            this.EntityId = CallLog.EntityId;
            this.CallTypeId = CallLog.CallTypeId;
            this.CallEmotionId = CallLog.CallEmotionId;
            this.AppUserId = CallLog.AppUserId;
            this.CreatorId = CallLog.CreatorId;
            this.AppUser = CallLog.AppUser == null ? null : new Company_AppUserDTO(CallLog.AppUser);
            this.CallEmotion = CallLog.CallEmotion == null ? null : new Company_CallEmotionDTO(CallLog.CallEmotion);
            this.CallType = CallLog.CallType == null ? null : new Company_CallTypeDTO(CallLog.CallType);
            this.Creator = CallLog.Creator == null ? null : new Company_AppUserDTO(CallLog.Creator);
            this.EntityReference = CallLog.EntityReference == null ? null : new Company_EntityReferenceDTO(CallLog.EntityReference);
            this.CreatedAt = CallLog.CreatedAt;
            this.UpdatedAt = CallLog.UpdatedAt;
            this.Errors = CallLog.Errors;
        }
    }

    public class Company_CallLogFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Title { get; set; }
        
        public StringFilter Content { get; set; }
        
        public StringFilter Phone { get; set; }
        
        public DateFilter CallTime { get; set; }
        
        public IdFilter EntityReferenceId { get; set; }
        
        public IdFilter EntityId { get; set; }
        
        public IdFilter CallTypeId { get; set; }
        
        public IdFilter CallEmotionId { get; set; }
        
        public IdFilter AppUserId { get; set; }
        
        public IdFilter CreatorId { get; set; }
        
        public CallLogOrder OrderBy { get; set; }
    }
}