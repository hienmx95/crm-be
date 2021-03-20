using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class AuditLogProperty : DataEntity,  IEquatable<AuditLogProperty>
    {
        public long Id { get; set; }
        public long? AppUserId { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string ClassName { get; set; }
        public string ActionName { get; set; }
        public DateTime? Time { get; set; }
        public AppUser AppUser { get; set; }
        
        public bool Equals(AuditLogProperty other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class AuditLogPropertyFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter AppUserId { get; set; }
        public StringFilter Property { get; set; }
        public StringFilter OldValue { get; set; }
        public StringFilter NewValue { get; set; }
        public StringFilter ClassName { get; set; }
        public StringFilter ActionName { get; set; }
        public DateFilter Time { get; set; }
        public List<AuditLogPropertyFilter> OrFilter { get; set; }
        public AuditLogPropertyOrder OrderBy {get; set;}
        public AuditLogPropertySelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuditLogPropertyOrder
    {
        Id = 0,
        AppUser = 1,
        Property = 2,
        OldValue = 3,
        NewValue = 4,
        ClassName = 5,
        ActionName = 6,
        Time = 7,
    }

    [Flags]
    public enum AuditLogPropertySelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        AppUser = E._1,
        Property = E._2,
        OldValue = E._3,
        NewValue = E._4,
        ClassName = E._5,
        ActionName = E._6,
        Time = E._7,
    }
}
