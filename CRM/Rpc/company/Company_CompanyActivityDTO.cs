using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_CompanyActivityDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long ActivityTypeId { get; set; }
        public long? ActivityPriorityId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public long CompanyId { get; set; }
        public long AppUserId { get; set; }
        public long ActivityStatusId { get; set; }
        public Company_ActivityPriorityDTO ActivityPriority { get; set; }   
        public Company_ActivityStatusDTO ActivityStatus { get; set; }   
        public Company_ActivityTypeDTO ActivityType { get; set; }   
        public Company_AppUserDTO AppUser { get; set; }   

        public Company_CompanyActivityDTO() {}
        public Company_CompanyActivityDTO(CompanyActivity CompanyActivity)
        {
            this.Id = CompanyActivity.Id;
            this.Title = CompanyActivity.Title;
            this.FromDate = CompanyActivity.FromDate;
            this.ToDate = CompanyActivity.ToDate;
            this.ActivityTypeId = CompanyActivity.ActivityTypeId;
            this.ActivityPriorityId = CompanyActivity.ActivityPriorityId;
            this.Description = CompanyActivity.Description;
            this.Address = CompanyActivity.Address;
            this.CompanyId = CompanyActivity.CompanyId;
            this.AppUserId = CompanyActivity.AppUserId;
            this.ActivityStatusId = CompanyActivity.ActivityStatusId;
            this.ActivityPriority = CompanyActivity.ActivityPriority == null ? null : new Company_ActivityPriorityDTO(CompanyActivity.ActivityPriority);
            this.ActivityStatus = CompanyActivity.ActivityStatus == null ? null : new Company_ActivityStatusDTO(CompanyActivity.ActivityStatus);
            this.ActivityType = CompanyActivity.ActivityType == null ? null : new Company_ActivityTypeDTO(CompanyActivity.ActivityType);
            this.AppUser = CompanyActivity.AppUser == null ? null : new Company_AppUserDTO(CompanyActivity.AppUser);
            this.Errors = CompanyActivity.Errors;
        }
    }

    public class Company_CompanyActivityFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Title { get; set; }
        
        public DateFilter FromDate { get; set; }
        
        public DateFilter ToDate { get; set; }
        
        public IdFilter ActivityTypeId { get; set; }
        
        public IdFilter ActivityPriorityId { get; set; }
        
        public StringFilter Description { get; set; }
        
        public StringFilter Address { get; set; }
        
        public IdFilter CompanyId { get; set; }
        
        public IdFilter AppUserId { get; set; }
        
        public IdFilter ActivityStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }

        public CompanyActivityOrder OrderBy { get; set; }
    }
}