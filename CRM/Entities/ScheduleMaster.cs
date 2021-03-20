using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ScheduleMaster : DataEntity,  IEquatable<ScheduleMaster>
    {
        public long Id { get; set; }
        public long? ManagerId { get; set; }
        public long? SalerId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public DateTime? RecurDays { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? NoEndDate { get; set; }
        public DateTime? StartDayOfWeek { get; set; }
        public long? DisplayOrder { get; set; }
        public string Description { get; set; }
        public AppUser Manager { get; set; }
        public AppUser Saler { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(ScheduleMaster other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ScheduleMasterFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter ManagerId { get; set; }
        public IdFilter SalerId { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter RecurDays { get; set; }
        public DateFilter StartDate { get; set; }
        public DateFilter EndDate { get; set; }
        public DateFilter StartDayOfWeek { get; set; }
        public LongFilter DisplayOrder { get; set; }
        public StringFilter Description { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<ScheduleMasterFilter> OrFilter { get; set; }
        public ScheduleMasterOrder OrderBy {get; set;}
        public ScheduleMasterSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ScheduleMasterOrder
    {
        Id = 0,
        Manager = 1,
        Saler = 2,
        Name = 3,
        Code = 4,
        Status = 5,
        RecurDays = 6,
        StartDate = 7,
        EndDate = 8,
        NoEndDate = 9,
        StartDayOfWeek = 10,
        DisplayOrder = 11,
        Description = 12,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum ScheduleMasterSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Manager = E._1,
        Saler = E._2,
        Name = E._3,
        Code = E._4,
        Status = E._5,
        RecurDays = E._6,
        StartDate = E._7,
        EndDate = E._8,
        NoEndDate = E._9,
        StartDayOfWeek = E._10,
        DisplayOrder = E._11,
        Description = E._12,
    }
}
