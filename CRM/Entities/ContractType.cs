using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ContractType : DataEntity,  IEquatable<ContractType>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        
        public bool Equals(ContractType other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ContractTypeFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public List<ContractTypeFilter> OrFilter { get; set; }
        public ContractTypeOrder OrderBy {get; set;}
        public ContractTypeSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContractTypeOrder
    {
        Id = 0,
        Name = 1,
        Code = 2,
    }

    [Flags]
    public enum ContractTypeSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Code = E._2,
    }
}
