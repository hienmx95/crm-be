using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class StoreDeliveryTimeMapping : DataEntity,  IEquatable<StoreDeliveryTimeMapping>
    {
        public long StoreId { get; set; }
        public long StoreDeliveryTimeId { get; set; }
        public Store Store { get; set; }
        public StoreDeliveryTime StoreDeliveryTime { get; set; }
        
        public bool Equals(StoreDeliveryTimeMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class StoreDeliveryTimeMappingFilter : FilterEntity
    {
        public IdFilter StoreId { get; set; }
        public LongFilter StoreDeliveryTimeId { get; set; }
        public List<StoreDeliveryTimeMappingFilter> OrFilter { get; set; }
        public StoreDeliveryTimeMappingOrder OrderBy {get; set;}
        public StoreDeliveryTimeMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StoreDeliveryTimeMappingOrder
    {
        Store = 0,
        StoreDeliveryTime = 1,
    }

    [Flags]
    public enum StoreDeliveryTimeMappingSelect:long
    {
        ALL = E.ALL,
        Store = E._0,
        StoreDeliveryTime = E._1,
    }
}
