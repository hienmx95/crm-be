using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerLevel : DataEntity,  IEquatable<CustomerLevel>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public long PointFrom { get; set; }
        public long PointTo { get; set; }
        public long StatusId { get; set; }
        public string Description { get; set; }
        public bool Used { get; set; }
        public Guid RowId { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(CustomerLevel other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CustomerLevelFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Color { get; set; }
        public LongFilter PointFrom { get; set; }
        public LongFilter PointTo { get; set; }
        public IdFilter StatusId { get; set; }
        public StringFilter Description { get; set; }
        public GuidFilter RowId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<CustomerLevelFilter> OrFilter { get; set; }
        public CustomerLevelOrder OrderBy {get; set;}
        public CustomerLevelSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerLevelOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
        Color = 3,
        PointFrom = 4,
        PointTo = 5,
        Status = 6,
        Description = 7,
        Used = 11,
        Row = 12,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CustomerLevelSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
        Color = E._3,
        PointFrom = E._4,
        PointTo = E._5,
        Status = E._6,
        Description = E._7,
        Used = E._11,
        Row = E._12,
    }
}
