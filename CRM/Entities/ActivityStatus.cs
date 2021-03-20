using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ActivityStatus : DataEntity,  IEquatable<ActivityStatus>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        
        public bool Equals(ActivityStatus other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ActivityStatusFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<ActivityStatusFilter> OrFilter { get; set; }
        public ActivityStatusOrder OrderBy {get; set;}
        public ActivityStatusSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActivityStatusOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
    }

    [Flags]
    public enum ActivityStatusSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
    }
}
