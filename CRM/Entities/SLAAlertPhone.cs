using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAAlertPhone : DataEntity,  IEquatable<SLAAlertPhone>
    {
        public long Id { get; set; }
        public long? SLAAlertId { get; set; }
        public string Phone { get; set; }
        public SLAAlert SLAAlert { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAAlertPhone other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAAlertPhoneFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAAlertId { get; set; }
        public StringFilter Phone { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAAlertPhoneFilter> OrFilter { get; set; }
        public SLAAlertPhoneOrder OrderBy {get; set;}
        public SLAAlertPhoneSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAAlertPhoneOrder
    {
        Id = 0,
        SLAAlert = 1,
        Phone = 2,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAAlertPhoneSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        SLAAlert = E._1,
        Phone = E._2,
    }
}
