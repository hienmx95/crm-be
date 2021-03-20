using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerLeadCallLogMapping : DataEntity,  IEquatable<CustomerLeadCallLogMapping>
    {
        public long CustomerLeadId { get; set; }
        public long CallLogId { get; set; }
        public CallLog CallLog { get; set; }
        public CustomerLead CustomerLead { get; set; }

        public bool Equals(CustomerLeadCallLogMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CustomerLeadCallLogMappingFilter : FilterEntity
    {
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter CallLogId { get; set; }
        public List<CustomerLeadCallLogMappingFilter> OrFilter { get; set; }
        public CustomerLeadCallLogMappingOrder OrderBy {get; set;}
        public CustomerLeadCallLogMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerLeadCallLogMappingOrder
    {
        CustomerLead = 0,
        CallLog = 1,
    }

    [Flags]
    public enum CustomerLeadCallLogMappingSelect:long
    {
        ALL = E.ALL,
        CustomerLead = E._0,
        CallLog = E._1,
    }
}
