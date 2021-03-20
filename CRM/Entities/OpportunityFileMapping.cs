using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class OpportunityFileMapping : DataEntity,  IEquatable<OpportunityFileMapping>
    {
        public long OpportunityFileGroupingId { get; set; }
        public long FileId { get; set; }
        public File File { get; set; }
        public OpportunityFileGrouping OpportunityFileGrouping { get; set; }

        public bool Equals(OpportunityFileMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class OpportunityFileMappingFilter : FilterEntity
    {
        public IdFilter OpportunityFileGroupingId { get; set; }
        public IdFilter FileId { get; set; }
        public List<OpportunityFileMappingFilter> OrFilter { get; set; }
        public OpportunityFileMappingOrder OrderBy {get; set;}
        public OpportunityFileMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OpportunityFileMappingOrder
    {
        OpportunityFileGrouping = 0,
        File = 1,
    }

    [Flags]
    public enum OpportunityFileMappingSelect:long
    {
        ALL = E.ALL,
        OpportunityFileGrouping = E._0,
        File = E._1,
    }
}
