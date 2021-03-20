using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class StoreMeansOfDelivery : DataEntity,  IEquatable<StoreMeansOfDelivery>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Quantity { get; set; }
        public long? Owned { get; set; }
        public long? Rent { get; set; }
        public long? StoreId { get; set; }
        public Store Store { get; set; }
        
        public bool Equals(StoreMeansOfDelivery other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class StoreMeansOfDeliveryFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter Quantity { get; set; }
        public LongFilter Owned { get; set; }
        public LongFilter Rent { get; set; }
        public IdFilter StoreId { get; set; }
        public List<StoreMeansOfDeliveryFilter> OrFilter { get; set; }
        public StoreMeansOfDeliveryOrder OrderBy {get; set;}
        public StoreMeansOfDeliverySelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StoreMeansOfDeliveryOrder
    {
        Id = 0,
        Name = 1,
        Quantity = 2,
        Owned = 3,
        Rent = 4,
        Store = 5,
    }

    [Flags]
    public enum StoreMeansOfDeliverySelect:long
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
