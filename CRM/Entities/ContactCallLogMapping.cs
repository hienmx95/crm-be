using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ContactCallLogMapping : DataEntity,  IEquatable<ContactCallLogMapping>
    {
        public long ContactId { get; set; }
        public long CallLogId { get; set; }
        public CallLog CallLog { get; set; }
        public Contact Contact { get; set; }

        public bool Equals(ContactCallLogMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class ContactCallLogMappingFilter : FilterEntity
    {
        public IdFilter ContactId { get; set; }
        public IdFilter CallLogId { get; set; }
        public List<ContactCallLogMappingFilter> OrFilter { get; set; }
        public ContactCallLogMappingOrder OrderBy {get; set;}
        public ContactCallLogMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContactCallLogMappingOrder
    {
        Contact = 0,
        CallLog = 1,
    }

    [Flags]
    public enum ContactCallLogMappingSelect:long
    {
        ALL = E.ALL,
        Contact = E._0,
        CallLog = E._1,
    }
}
