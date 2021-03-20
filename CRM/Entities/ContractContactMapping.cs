using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ContractContactMapping : DataEntity,  IEquatable<ContractContactMapping>
    {
        public long ContractId { get; set; }
        public long ContactId { get; set; }
        public Contact Contact { get; set; }
        public Contract Contract { get; set; }

        public bool Equals(ContractContactMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class ContractContactMappingFilter : FilterEntity
    {
        public IdFilter ContractId { get; set; }
        public IdFilter ContactId { get; set; }
        public List<ContractContactMappingFilter> OrFilter { get; set; }
        public ContractContactMappingOrder OrderBy {get; set;}
        public ContractContactMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContractContactMappingOrder
    {
        Contract = 0,
        Contact = 1,
    }

    [Flags]
    public enum ContractContactMappingSelect:long
    {
        ALL = E.ALL,
        Contract = E._0,
        Contact = E._1,
    }
}
