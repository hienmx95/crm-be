using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ContractFileMapping : DataEntity,  IEquatable<ContractFileMapping>
    {
        public long ContractFileGroupingId { get; set; }
        public long FileId { get; set; }
        public ContractFileGrouping ContractFileGrouping { get; set; }
        public File File { get; set; }

        public bool Equals(ContractFileMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class ContractFileMappingFilter : FilterEntity
    {
        public IdFilter ContractFileGroupingId { get; set; }
        public IdFilter FileId { get; set; }
        public List<ContractFileMappingFilter> OrFilter { get; set; }
        public ContractFileMappingOrder OrderBy {get; set;}
        public ContractFileMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContractFileMappingOrder
    {
        ContractFileGrouping = 0,
        File = 1,
    }

    [Flags]
    public enum ContractFileMappingSelect:long
    {
        ALL = E.ALL,
        ContractFileGrouping = E._0,
        File = E._1,
    }
}
