using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class InfulenceLevelMarket : DataEntity,  IEquatable<InfulenceLevelMarket>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        
        public bool Equals(InfulenceLevelMarket other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class InfulenceLevelMarketFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<InfulenceLevelMarketFilter> OrFilter { get; set; }
        public InfulenceLevelMarketOrder OrderBy {get; set;}
        public InfulenceLevelMarketSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum InfulenceLevelMarketOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
    }

    [Flags]
    public enum InfulenceLevelMarketSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
    }
}
