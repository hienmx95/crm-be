using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CallType : DataEntity,  IEquatable<CallType>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public long StatusId { get; set; }
        public bool Used { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(CallType other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CallTypeFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter ColorCode { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<CallTypeFilter> OrFilter { get; set; }
        public CallTypeOrder OrderBy {get; set;}
        public CallTypeSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CallTypeOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
        ColorCode = 3,
        Status = 4,
        Used = 8,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CallTypeSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
        ColorCode = E._3,
        Status = E._4,
        Used = E._8,
    }
}
