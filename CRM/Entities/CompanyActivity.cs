using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CompanyActivity : DataEntity,  IEquatable<CompanyActivity>
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
        public ActivityPriority ActivityPriority { get; set; }
        public ActivityStatus ActivityStatus { get; set; }
        public ActivityType ActivityType { get; set; }
        public AppUser AppUser { get; set; }
        public Company Company { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(CompanyActivity other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CompanyActivityFilter : FilterEntity
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
        public List<CompanyActivityFilter> OrFilter { get; set; }
        public CompanyActivityOrder OrderBy {get; set;}
        public CompanyActivitySelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CompanyActivityOrder
    {
        Id = 0,
        Title = 1,
        FromDate = 2,
        ToDate = 3,
        ActivityType = 4,
        ActivityPriority = 5,
        Description = 6,
        Address = 7,
        Company = 8,
        AppUser = 9,
        ActivityStatus = 10,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CompanyActivitySelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Title = E._1,
        FromDate = E._2,
        ToDate = E._3,
        ActivityType = E._4,
        ActivityPriority = E._5,
        Description = E._6,
        Address = E._7,
        Company = E._8,
        AppUser = E._9,
        ActivityStatus = E._10,
    }
}
