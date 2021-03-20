using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace CRM.Entities
{
    public class CustomerLeadEmailCCMapping : DataEntity, IEquatable<CustomerLeadEmailCCMapping>
    {
        public long CustomerLeadEmailId { get; set; }
        public long AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public CustomerLeadEmail CustomerLeadEmail { get; set; }

        public bool Equals(CustomerLeadEmailCCMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CustomerLeadEmailCCMappingFilter : FilterEntity
    {
        public IdFilter CustomerLeadEmailId { get; set; }
        public IdFilter AppUserId { get; set; }
        public List<CustomerLeadEmailCCMappingFilter> OrFilter { get; set; }
        public CustomerLeadEmailCCMappingOrder OrderBy { get; set; }
        public CustomerLeadEmailCCMappingSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerLeadEmailCCMappingOrder
    {
        CustomerLeadEmail = 0,
        AppUser = 1,
    }

    [Flags]
    public enum CustomerLeadEmailCCMappingSelect : long
    {
        ALL = E.ALL,
        CustomerLeadEmail = E._0,
        AppUser = E._1,
    }
}
