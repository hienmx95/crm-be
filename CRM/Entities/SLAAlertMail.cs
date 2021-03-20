using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAAlertMail : DataEntity,  IEquatable<SLAAlertMail>
    {
        public long Id { get; set; }
        public long? SLAAlertId { get; set; }
        public string Mail { get; set; }
        public SLAAlert SLAAlert { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAAlertMail other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAAlertMailFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAAlertId { get; set; }
        public StringFilter Mail { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAAlertMailFilter> OrFilter { get; set; }
        public SLAAlertMailOrder OrderBy {get; set;}
        public SLAAlertMailSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAAlertMailOrder
    {
        Id = 0,
        SLAAlert = 1,
        Mail = 2,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAAlertMailSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        SLAAlert = E._1,
        Mail = E._2,
    }
}
