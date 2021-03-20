using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class StoreInfulenceLevelMarketMapping : DataEntity,  IEquatable<StoreInfulenceLevelMarketMapping>
    {
        public long StoreId { get; set; }
        public long InfulenceLevelMarketId { get; set; }
        public InfulenceLevelMarket InfulenceLevelMarket { get; set; }
        public Store Store { get; set; }
        
        public bool Equals(StoreInfulenceLevelMarketMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class StoreInfulenceLevelMarketMappingFilter : FilterEntity
    {
        public IdFilter StoreId { get; set; }
        public IdFilter InfulenceLevelMarketId { get; set; }
        public List<StoreInfulenceLevelMarketMappingFilter> OrFilter { get; set; }
        public StoreInfulenceLevelMarketMappingOrder OrderBy {get; set;}
        public StoreInfulenceLevelMarketMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StoreInfulenceLevelMarketMappingOrder
    {
        Store = 0,
        InfulenceLevelMarket = 1,
    }

    [Flags]
    public enum StoreInfulenceLevelMarketMappingSelect:long
    {
        ALL = E.ALL,
        Store = E._0,
        InfulenceLevelMarket = E._1,
    }
}
