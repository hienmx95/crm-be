using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ActivityPriority : DataEntity,  IEquatable<ActivityPriority>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        
        public bool Equals(ActivityPriority other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ActivityPriorityFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<ActivityPriorityFilter> OrFilter { get; set; }
        public ActivityPriorityOrder OrderBy {get; set;}
        public ActivityPrioritySelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActivityPriorityOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
    }

    [Flags]
    public enum ActivityPrioritySelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
    }
}
