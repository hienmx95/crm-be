using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class StoreAssets : DataEntity,  IEquatable<StoreAssets>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Quantity { get; set; }
        public long? Owned { get; set; }
        public long? Rent { get; set; }
        public long? StoreId { get; set; }
        public Store Store { get; set; }
        
        public bool Equals(StoreAssets other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class StoreAssetsFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter Quantity { get; set; }
        public LongFilter Owned { get; set; }
        public LongFilter Rent { get; set; }
        public IdFilter StoreId { get; set; }
        public List<StoreAssetsFilter> OrFilter { get; set; }
        public StoreAssetsOrder OrderBy {get; set;}
        public StoreAssetsSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StoreAssetsOrder
    {
        Id = 0,
        Name = 1,
        Quantity = 2,
        Owned = 3,
        Rent = 4,
        Store = 5,
    }

    [Flags]
    public enum StoreAssetsSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Quantity = E._2,
        Owned = E._3,
        Rent = E._4,
        Store = E._5,
    }
}
