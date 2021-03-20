using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAAlertFRTPhone : DataEntity,  IEquatable<SLAAlertFRTPhone>
    {
        public long Id { get; set; }
        public long? SLAAlertFRTId { get; set; }
        public string Phone { get; set; }
        public SLAAlertFRT SLAAlertFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAAlertFRTPhone other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAAlertFRTPhoneFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAAlertFRTId { get; set; }
        public StringFilter Phone { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAAlertFRTPhoneFilter> OrFilter { get; set; }
        public SLAAlertFRTPhoneOrder OrderBy {get; set;}
        public SLAAlertFRTPhoneSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAAlertFRTPhoneOrder
    {
        Id = 0,
        SLAAlertFRT = 1,
        Phone = 2,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAAlertFRTPhoneSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        SLAAlertFRT = E._1,
        Phone = E._2,
    }
}
