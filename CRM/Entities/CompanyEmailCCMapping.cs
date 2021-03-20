using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CompanyEmailCCMapping : DataEntity,  IEquatable<CompanyEmailCCMapping>
    {
        public long CompanyEmailId { get; set; }
        public long AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public CompanyEmail CompanyEmail { get; set; }
        
        public bool Equals(CompanyEmailCCMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CompanyEmailCCMappingFilter : FilterEntity
    {
        public IdFilter CompanyEmailId { get; set; }
        public IdFilter AppUserId { get; set; }
        public List<CompanyEmailCCMappingFilter> OrFilter { get; set; }
        public CompanyEmailCCMappingOrder OrderBy {get; set;}
        public CompanyEmailCCMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CompanyEmailCCMappingOrder
    {
        CompanyEmail = 0,
        AppUser = 1,
    }

    [Flags]
    public enum CompanyEmailCCMappingSelect:long
    {
        ALL = E.ALL,
        CompanyEmail = E._0,
        AppUser = E._1,
    }
}
