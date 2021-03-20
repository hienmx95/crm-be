using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_OpportunityActivityDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long ActivityTypeId { get; set; }
        public long? ActivityPriorityId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public long OpportunityId { get; set; }
        public long AppUserId { get; set; }
        public long ActivityStatusId { get; set; }
        public Opportunity_ActivityPriorityDTO ActivityPriority { get; set; }
        public Opportunity_ActivityStatusDTO ActivityStatus { get; set; }
        public Opportunity_ActivityTypeDTO ActivityType { get; set; }
        public Opportunity_AppUserDTO AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Opportunity_OpportunityActivityDTO() {}
        public Opportunity_OpportunityActivityDTO(OpportunityActivity OpportunityActivity)
        {
            this.Id = OpportunityActivity.Id;
            this.Title = OpportunityActivity.Title;
            this.FromDate = OpportunityActivity.FromDate;
            this.ToDate = OpportunityActivity.ToDate;
            this.ActivityTypeId = OpportunityActivity.ActivityTypeId;
            this.ActivityPriorityId = OpportunityActivity.ActivityPriorityId;
            this.Description = OpportunityActivity.Description;
            this.Address = OpportunityActivity.Address;
            this.OpportunityId = OpportunityActivity.OpportunityId;
            this.AppUserId = OpportunityActivity.AppUserId;
            this.ActivityStatusId = OpportunityActivity.ActivityStatusId;
            this.ActivityPriority = OpportunityActivity.ActivityPriority == null ? null : new Opportunity_ActivityPriorityDTO(OpportunityActivity.ActivityPriority);
            this.ActivityStatus = OpportunityActivity.ActivityStatus == null ? null : new Opportunity_ActivityStatusDTO(OpportunityActivity.ActivityStatus);
            this.ActivityType = OpportunityActivity.ActivityType == null ? null : new Opportunity_ActivityTypeDTO(OpportunityActivity.ActivityType);
            this.AppUser = OpportunityActivity.AppUser == null ? null : new Opportunity_AppUserDTO(OpportunityActivity.AppUser);
            this.CreatedAt = OpportunityActivity.CreatedAt;
            this.UpdatedAt = OpportunityActivity.UpdatedAt;
            this.Errors = OpportunityActivity.Errors;
        }
    }

    public class Opportunity_OpportunityActivityFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public DateFilter FromDate { get; set; }
        public DateFilter ToDate { get; set; }
        public IdFilter ActivityTypeId { get; set; }
        public IdFilter ActivityPriorityId { get; set; }
        public StringFilter Description { get; set; }
        public StringFilter Address { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter ActivityStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public OpportunityActivityOrder OrderBy { get; set; }
    }
}
