using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAAlertFRTUser : DataEntity,  IEquatable<SLAAlertFRTUser>
    {
        public long Id { get; set; }
        public long? SLAAlertFRTId { get; set; }
        public long? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public SLAAlertFRT SLAAlertFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAAlertFRTUser other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAAlertFRTUserFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAAlertFRTId { get; set; }
        public IdFilter AppUserId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAAlertFRTUserFilter> OrFilter { get; set; }
        public SLAAlertFRTUserOrder OrderBy {get; set;}
        public SLAAlertFRTUserSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAAlertFRTUserOrder
    {
        Id = 0,
        SLAAlertFRT = 1,
        AppUser = 2,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAAlertFRTUserSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        SLAAlertFRT = E._1,
        AppUser = E._2,
    }
}
