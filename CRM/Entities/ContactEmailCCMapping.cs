using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ContactEmailCCMapping : DataEntity,  IEquatable<ContactEmailCCMapping>
    {
        public long ContactEmailId { get; set; }
        public long AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ContactEmail ContactEmail { get; set; }
        
        public bool Equals(ContactEmailCCMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class ContactEmailCCMappingFilter : FilterEntity
    {
        public IdFilter ContactEmailId { get; set; }
        public IdFilter AppUserId { get; set; }
        public List<ContactEmailCCMappingFilter> OrFilter { get; set; }
        public ContactEmailCCMappingOrder OrderBy {get; set;}
        public ContactEmailCCMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContactEmailCCMappingOrder
    {
        ContactEmail = 0,
        AppUser = 1,
    }

    [Flags]
    public enum ContactEmailCCMappingSelect:long
    {
        ALL = E.ALL,
        ContactEmail = E._0,
        AppUser = E._1,
    }
}
