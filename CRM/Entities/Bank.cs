using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class Bank : DataEntity,  IEquatable<Bank>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public bool Equals(Bank other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class BankFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<BankFilter> OrFilter { get; set; }
        public BankOrder OrderBy {get; set;}
        public BankSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum BankOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
    }

    [Flags]
    public enum BankSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
    }
}
