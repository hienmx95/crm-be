using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_CallLogDTO : DataDTO
    {
        public long Id { get; set; }
        public long EntityReferenceId { get; set; }
        public long CallTypeId { get; set; }
        public long? CallEmotionId { get; set; }
        public long AppUserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Phone { get; set; }
        public DateTime CallTime { get; set; }
        public bool Used { get; set; }
        public CustomerLead_AppUserDTO AppUser { get; set; }
        public CustomerLead_EntityReferenceDTO EntityReference { get; set; }
        public CustomerLead_CallTypeDTO CallType { get; set; }
        public CustomerLead_CallEmotionDTO CallEmotion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public CustomerLead_CallLogDTO() { }
        public CustomerLead_CallLogDTO(CallLog CallLog)
        {
            this.Id = CallLog.Id;
            this.EntityReferenceId = CallLog.EntityReferenceId;
            this.CallTypeId = CallLog.CallTypeId;
            this.CallEmotionId = CallLog.CallEmotionId;
            this.AppUserId = CallLog.AppUserId;
            this.Title = CallLog.Title;
            this.Content = CallLog.Content;
            this.Phone = CallLog.Phone;
            this.CallTime = CallLog.CallTime;
            this.AppUser = CallLog.AppUser == null ? null : new CustomerLead_AppUserDTO(CallLog.AppUser);
            this.EntityReference = CallLog.EntityReference == null ? null : new CustomerLead_EntityReferenceDTO(CallLog.EntityReference);
            this.CallType = CallLog.CallType == null ? null : new CustomerLead_CallTypeDTO(CallLog.CallType);
            this.CallEmotion = CallLog.CallEmotion == null ? null : new CustomerLead_CallEmotionDTO(CallLog.CallEmotion);
            this.CreatedAt = CallLog.CreatedAt;
            this.UpdatedAt = CallLog.UpdatedAt;
            this.Errors = CallLog.Errors;
        }
    }

    public class CustomerLead_CallLogFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter EntityReferenceId { get; set; }
        public IdFilter CallTypeId { get; set; }
        public IdFilter CallEmotionId { get; set; }
        public IdFilter AppUserId { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Content { get; set; }
        public StringFilter Phone { get; set; }
        public DateFilter CallTime { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CallLogOrder OrderBy { get; set; }

        public IdFilter CompanyId { get; set; }
        public IdFilter ContactId { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter CustomerRetailId { get; set; }
        public IdFilter CustomerAgentId { get; set; }
        public IdFilter CustomerExportId { get; set; }
        public IdFilter CustomerProjectId { get; set; }
        public StringFilter Key { get; set; }
    }
}
