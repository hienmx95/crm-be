using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class OpportunityCallLogMapping : DataEntity,  IEquatable<OpportunityCallLogMapping>
    {
        public long OpportunityId { get; set; }
        public long CallLogId { get; set; }
        public CallLog CallLog { get; set; }
        public Opportunity Opportunity { get; set; }

        public bool Equals(OpportunityCallLogMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class OpportunityCallLogMappingFilter : FilterEntity
    {
        public IdFilter OpportunityId { get; set; }
        public IdFilter CallLogId { get; set; }
        public List<OpportunityCallLogMappingFilter> OrFilter { get; set; }
        public OpportunityCallLogMappingOrder OrderBy {get; set;}
        public OpportunityCallLogMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OpportunityCallLogMappingOrder
    {
        Opportunity = 0,
        CallLog = 1,
    }

    [Flags]
    public enum OpportunityCallLogMappingSelect:long
    {
        ALL = E.ALL,
        Opportunity = E._0,
        CallLog = E._1,
    }
}
