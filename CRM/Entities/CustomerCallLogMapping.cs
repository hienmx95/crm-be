using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerCallLogMapping : DataEntity,  IEquatable<CustomerCallLogMapping>
    {
        public long CustomerId { get; set; }
        public long CallLogId { get; set; }
        public CallLog CallLog { get; set; }
        public Customer Customer { get; set; }
        
        public bool Equals(CustomerCallLogMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CustomerCallLogMappingFilter : FilterEntity
    {
        public IdFilter CustomerId { get; set; }
        public IdFilter CallLogId { get; set; }
        public List<CustomerCallLogMappingFilter> OrFilter { get; set; }
        public CustomerCallLogMappingOrder OrderBy {get; set;}
        public CustomerCallLogMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerCallLogMappingOrder
    {
        Customer = 0,
        CallLog = 1,
    }

    [Flags]
    public enum CustomerCallLogMappingSelect:long
    {
        ALL = E.ALL,
        Customer = E._0,
        CallLog = E._1,
    }
}
