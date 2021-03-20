using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_CustomerLeadActivityDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long ActivityTypeId { get; set; }
        public long? ActivityPriorityId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public long CustomerLeadId { get; set; }
        public long AppUserId { get; set; }
        public long ActivityStatusId { get; set; }
        public CustomerLead_ActivityPriorityDTO ActivityPriority { get; set; }
        public CustomerLead_ActivityStatusDTO ActivityStatus { get; set; }
        public CustomerLead_ActivityTypeDTO ActivityType { get; set; }
        public CustomerLead_AppUserDTO AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CustomerLead_CustomerLeadActivityDTO() {}
        public CustomerLead_CustomerLeadActivityDTO(CustomerLeadActivity CustomerLeadActivity)
        {
            this.Id = CustomerLeadActivity.Id;
            this.Title = CustomerLeadActivity.Title;
            this.FromDate = CustomerLeadActivity.FromDate;
            this.ToDate = CustomerLeadActivity.ToDate;
            this.ActivityTypeId = CustomerLeadActivity.ActivityTypeId;
            this.ActivityPriorityId = CustomerLeadActivity.ActivityPriorityId;
            this.Description = CustomerLeadActivity.Description;
            this.Address = CustomerLeadActivity.Address;
            this.CustomerLeadId = CustomerLeadActivity.CustomerLeadId;
            this.AppUserId = CustomerLeadActivity.AppUserId;
            this.ActivityStatusId = CustomerLeadActivity.ActivityStatusId;
            this.ActivityPriority = CustomerLeadActivity.ActivityPriority == null ? null : new CustomerLead_ActivityPriorityDTO(CustomerLeadActivity.ActivityPriority);
            this.ActivityStatus = CustomerLeadActivity.ActivityStatus == null ? null : new CustomerLead_ActivityStatusDTO(CustomerLeadActivity.ActivityStatus);
            this.ActivityType = CustomerLeadActivity.ActivityType == null ? null : new CustomerLead_ActivityTypeDTO(CustomerLeadActivity.ActivityType);
            this.AppUser = CustomerLeadActivity.AppUser == null ? null : new CustomerLead_AppUserDTO(CustomerLeadActivity.AppUser);
            this.CreatedAt = CustomerLeadActivity.CreatedAt;
            this.UpdatedAt = CustomerLeadActivity.UpdatedAt;
            this.Errors = CustomerLeadActivity.Errors;
        }
    }

    public class CustomerLead_CustomerLeadActivityFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public DateFilter FromDate { get; set; }
        public DateFilter ToDate { get; set; }
        public IdFilter ActivityTypeId { get; set; }
        public IdFilter ActivityPriorityId { get; set; }
        public StringFilter Description { get; set; }
        public StringFilter Address { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter ActivityStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerLeadActivityOrder OrderBy { get; set; }
    }
}
