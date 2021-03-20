using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAAlertUser : DataEntity,  IEquatable<SLAAlertUser>
    {
        public long Id { get; set; }
        public long? SLAAlertId { get; set; }
        public long? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public SLAAlert SLAAlert { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAAlertUser other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAAlertUserFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAAlertId { get; set; }
        public IdFilter AppUserId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAAlertUserFilter> OrFilter { get; set; }
        public SLAAlertUserOrder OrderBy {get; set;}
        public SLAAlertUserSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAAlertUserOrder
    {
        Id = 0,
        SLAAlert = 1,
        AppUser = 2,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAAlertUserSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        SLAAlert = E._1,
        AppUser = E._2,
    }
}
