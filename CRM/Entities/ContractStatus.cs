using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ContractStatus : DataEntity,  IEquatable<ContractStatus>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        
        public bool Equals(ContractStatus other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ContractStatusFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public List<ContractStatusFilter> OrFilter { get; set; }
        public ContractStatusOrder OrderBy {get; set;}
        public ContractStatusSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContractStatusOrder
    {
        Id = 0,
        Name = 1,
        Code = 2,
    }

    [Flags]
    public enum ContractStatusSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Code = E._2,
    }
}
