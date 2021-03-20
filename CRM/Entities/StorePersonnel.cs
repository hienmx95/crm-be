using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class StorePersonnel : DataEntity,  IEquatable<StorePersonnel>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Quantity { get; set; }
        public long? StoreId { get; set; }
        public Store Store { get; set; }
        
        public bool Equals(StorePersonnel other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class StorePersonnelFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter Quantity { get; set; }
        public IdFilter StoreId { get; set; }
        public List<StorePersonnelFilter> OrFilter { get; set; }
        public StorePersonnelOrder OrderBy {get; set;}
        public StorePersonnelSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StorePersonnelOrder
    {
        Id = 0,
        Name = 1,
        Quantity = 2,
        Store = 3,
    }

    [Flags]
    public enum StorePersonnelSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Quantity = E._2,
        Store = E._3,
    }
}
