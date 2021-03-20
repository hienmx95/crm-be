using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAAlertFRTMail : DataEntity,  IEquatable<SLAAlertFRTMail>
    {
        public long Id { get; set; }
        public long? SLAAlertFRTId { get; set; }
        public string Mail { get; set; }
        public SLAAlertFRT SLAAlertFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAAlertFRTMail other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAAlertFRTMailFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAAlertFRTId { get; set; }
        public StringFilter Mail { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAAlertFRTMailFilter> OrFilter { get; set; }
        public SLAAlertFRTMailOrder OrderBy {get; set;}
        public SLAAlertFRTMailSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAAlertFRTMailOrder
    {
        Id = 0,
        SLAAlertFRT = 1,
        Mail = 2,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAAlertFRTMailSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        SLAAlertFRT = E._1,
        Mail = E._2,
    }
}
