using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class OpportunityContactMapping : DataEntity,  IEquatable<OpportunityContactMapping>
    {
        public long ContactId { get; set; }
        public long OpportunityId { get; set; }
        public Contact Contact { get; set; }
        public Opportunity Opportunity { get; set; }

        public bool Equals(OpportunityContactMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class OpportunityContactMappingFilter : FilterEntity
    {
        public IdFilter ContactId { get; set; }
        public IdFilter OpportunityId { get; set; }
        public List<OpportunityContactMappingFilter> OrFilter { get; set; }
        public OpportunityContactMappingOrder OrderBy {get; set;}
        public OpportunityContactMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OpportunityContactMappingOrder
    {
        Contact = 0,
        Opportunity = 1,
    }

    [Flags]
    public enum OpportunityContactMappingSelect:long
    {
        ALL = E.ALL,
        Contact = E._0,
        Opportunity = E._1,
    }
}
