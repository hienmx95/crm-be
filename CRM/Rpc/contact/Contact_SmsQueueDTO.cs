using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_SmsQueueDTO : DataDTO
    {
        public long Id { get; set; }
        public string Phone { get; set; }
        public string SmsCode { get; set; }
        public string SmsTitle { get; set; }
        public DateTime? SentDate { get; set; }
        public string SmsContent { get; set; }
        public long? SentByAppUserId { get; set; }
        public long? SmsQueueStatusId { get; set; }
        public Contact_AppUserDTO SentByAppUser { get; set; }
        public Contact_SmsQueueStatusDTO SmsQueueStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<string> ListSms { get; set; }
        public long? EntityReferenceId { get; set; }


        
        public Contact_SmsQueueDTO() { }
        public Contact_SmsQueueDTO(SmsQueue SmsQueue)
        {
            this.Id = SmsQueue.Id;
            this.Phone = SmsQueue.Phone;
            this.SmsCode = SmsQueue.SmsCode;
            this.SmsTitle = SmsQueue.SmsTitle;
            this.SentDate = SmsQueue.SentDate;
            this.SmsContent = SmsQueue.SmsContent;
            this.SentByAppUserId = SmsQueue.SentByAppUserId;
            this.SmsQueueStatusId = SmsQueue.SmsQueueStatusId;
            this.SentByAppUser = SmsQueue.SentByAppUser == null ? null : new Contact_AppUserDTO(SmsQueue.SentByAppUser);
            this.SmsQueueStatus = SmsQueue.SmsQueueStatus == null ? null : new Contact_SmsQueueStatusDTO(SmsQueue.SmsQueueStatus);
            this.CreatedAt = SmsQueue.CreatedAt;
            this.UpdatedAt = SmsQueue.UpdatedAt;
            this.Errors = SmsQueue.Errors;
        }
    }


    public class CustomerLead_SmsQueueFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter SmsCode { get; set; }
        public StringFilter SmsTitle { get; set; }
        public DateFilter SentDate { get; set; }
        public StringFilter SmsContent { get; set; }
        public IdFilter SentByAppUserId { get; set; }
        public IdFilter SmsQueueStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SmsQueueOrder OrderBy { get; set; }

        public IdFilter CompanyId { get; set; }
        public IdFilter ContactId { get; set; }
        public IdFilter CustomerRetailId { get; set; }
        public IdFilter CustomerAgentId { get; set; }
    }
}
