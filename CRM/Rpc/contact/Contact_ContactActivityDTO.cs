using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_ContactActivityDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long ActivityTypeId { get; set; }
        public long? ActivityPriorityId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public long ContactId { get; set; }
        public long AppUserId { get; set; }
        public long ActivityStatusId { get; set; }
        public Contact_ActivityPriorityDTO ActivityPriority { get; set; }
        public Contact_ActivityStatusDTO ActivityStatus { get; set; }
        public Contact_ActivityTypeDTO ActivityType { get; set; }
        public Contact_AppUserDTO AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Contact_ContactActivityDTO() {}
        public Contact_ContactActivityDTO(ContactActivity ContactActivity)
        {
            this.Id = ContactActivity.Id;
            this.Title = ContactActivity.Title;
            this.FromDate = ContactActivity.FromDate;
            this.ToDate = ContactActivity.ToDate;
            this.ActivityTypeId = ContactActivity.ActivityTypeId;
            this.ActivityPriorityId = ContactActivity.ActivityPriorityId;
            this.Description = ContactActivity.Description;
            this.Address = ContactActivity.Address;
            this.ContactId = ContactActivity.ContactId;
            this.AppUserId = ContactActivity.AppUserId;
            this.ActivityStatusId = ContactActivity.ActivityStatusId;
            this.ActivityPriority = ContactActivity.ActivityPriority == null ? null : new Contact_ActivityPriorityDTO(ContactActivity.ActivityPriority);
            this.ActivityStatus = ContactActivity.ActivityStatus == null ? null : new Contact_ActivityStatusDTO(ContactActivity.ActivityStatus);
            this.ActivityType = ContactActivity.ActivityType == null ? null : new Contact_ActivityTypeDTO(ContactActivity.ActivityType);
            this.AppUser = ContactActivity.AppUser == null ? null : new Contact_AppUserDTO(ContactActivity.AppUser);
            this.CreatedAt = ContactActivity.CreatedAt;
            this.UpdatedAt = ContactActivity.UpdatedAt;
            this.Errors = ContactActivity.Errors;
        }
    }

    public class Contact_ContactActivityFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public DateFilter FromDate { get; set; }
        public DateFilter ToDate { get; set; }
        public IdFilter ActivityTypeId { get; set; }
        public IdFilter ActivityPriorityId { get; set; }
        public StringFilter Description { get; set; }
        public StringFilter Address { get; set; }
        public IdFilter ContactId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter ActivityStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public ContactActivityOrder OrderBy { get; set; }
    }
}
