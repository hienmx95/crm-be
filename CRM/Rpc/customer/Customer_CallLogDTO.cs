using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CallLogDTO : DataDTO
    {
        public long Id { get; set; }
        public long EntityReferenceId { get; set; }
        public long EntityId { get; set; }
        public long CallTypeId { get; set; }
        public long? CallCategoryId { get; set; }
        public long? CallEmotionId { get; set; }
        public long? CallStatusId { get; set; }
        public long AppUserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Phone { get; set; }
        public long CreatorId { get; set; }
        public DateTime CallTime { get; set; }
        public bool Used { get; set; }
        public Customer_AppUserDTO AppUser { get; set; }
        public Customer_AppUserDTO Creator { get; set; }
        public Customer_EntityReferenceDTO EntityReference { get; set; }
        public Customer_CallCategoryDTO CallCategory { get; set; }
        public Customer_CallStatusDTO CallStatus { get; set; }
        public Customer_CallTypeDTO CallType { get; set; }
        public Customer_CallEmotionDTO CallEmotion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Customer_CallLogDTO() { }
        public Customer_CallLogDTO(CallLog CallLog)
        {
            this.Id = CallLog.Id;
            this.EntityReferenceId = CallLog.EntityReferenceId;
            this.CallTypeId = CallLog.CallTypeId;
            this.CallCategoryId = CallLog.CallCategoryId;
            this.CallStatusId = CallLog.CallStatusId;
            this.CallEmotionId = CallLog.CallEmotionId;
            this.AppUserId = CallLog.AppUserId;
            this.Title = CallLog.Title;
            this.Content = CallLog.Content;
            this.Phone = CallLog.Phone;
            this.CallTime = CallLog.CallTime;
            this.CreatorId = CallLog.CreatorId;
            this.AppUser = CallLog.AppUser == null ? null : new Customer_AppUserDTO(CallLog.AppUser);
            this.Creator = CallLog.Creator == null ? null : new Customer_AppUserDTO(CallLog.Creator);
            this.EntityReference = CallLog.EntityReference == null ? null : new Customer_EntityReferenceDTO(CallLog.EntityReference);
            this.CallType = CallLog.CallType == null ? null : new Customer_CallTypeDTO(CallLog.CallType);
            this.CallCategory = CallLog.CallCategory == null ? null : new Customer_CallCategoryDTO(CallLog.CallCategory);
            this.CallStatus = CallLog.CallStatus == null ? null : new Customer_CallStatusDTO(CallLog.CallStatus);
            this.CallEmotion = CallLog.CallEmotion == null ? null : new Customer_CallEmotionDTO(CallLog.CallEmotion);
            this.AppUser = CallLog.AppUser == null ? null : new Customer_AppUserDTO(CallLog.AppUser);
            this.CreatedAt = CallLog.CreatedAt;
            this.UpdatedAt = CallLog.UpdatedAt;
            this.Errors = CallLog.Errors;
        }
    }

    public class Customer_CallLogFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Content { get; set; }
        public StringFilter Phone { get; set; }
        public DateFilter CallTime { get; set; }
        public IdFilter EntityReferenceId { get; set; }
        public IdFilter EntityId { get; set; }
        public IdFilter CallTypeId { get; set; }
        public IdFilter CallCategoryId { get; set; }
        public IdFilter CallEmotionId { get; set; }
        public IdFilter CallStatusId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter CreatorId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CallLogOrder OrderBy { get; set; }
    }
}
