using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class StoreMarketPriceMapping : DataEntity,  IEquatable<StoreMarketPriceMapping>
    {
        public long StoreId { get; set; }
        public long MarketPriceId { get; set; }
        public MarketPrice MarketPrice { get; set; }
        public Store Store { get; set; }
        
        public bool Equals(StoreMarketPriceMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class StoreMarketPriceMappingFilter : FilterEntity
    {
        public IdFilter StoreId { get; set; }
        public IdFilter MarketPriceId { get; set; }
        public List<StoreMarketPriceMappingFilter> OrFilter { get; set; }
        public StoreMarketPriceMappingOrder OrderBy {get; set;}
        public StoreMarketPriceMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StoreMarketPriceMappingOrder
    {
        Store = 0,
        MarketPrice = 1,
    }

    [Flags]
    public enum StoreMarketPriceMappingSelect:long
    {
        ALL = E.ALL,
        Store = E._0,
        MarketPrice = E._1,
    }
}
