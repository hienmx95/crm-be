using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class OpportunityEmailCCMapping : DataEntity,  IEquatable<OpportunityEmailCCMapping>
    {
        public long OpportunityEmailId { get; set; }
        public long AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public OpportunityEmail OpportunityEmail { get; set; }
        
        public bool Equals(OpportunityEmailCCMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class OpportunityEmailCCMappingFilter : FilterEntity
    {
        public IdFilter OpportunityEmailId { get; set; }
        public IdFilter AppUserId { get; set; }
        public List<OpportunityEmailCCMappingFilter> OrFilter { get; set; }
        public OpportunityEmailCCMappingOrder OrderBy {get; set;}
        public OpportunityEmailCCMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OpportunityEmailCCMappingOrder
    {
        OpportunityEmail = 0,
        AppUser = 1,
    }

    [Flags]
    public enum OpportunityEmailCCMappingSelect:long
    {
        ALL = E.ALL,
        OpportunityEmail = E._0,
        AppUser = E._1,
    }
}
