using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class StoreRepresent : DataEntity,  IEquatable<StoreRepresent>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long? PositionId { get; set; }
        public long StoreId { get; set; }
        public Position Position { get; set; }
        public Store Store { get; set; }
        
        public bool Equals(StoreRepresent other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class StoreRepresentFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public DateFilter DateOfBirth { get; set; }
        public StringFilter Phone { get; set; }
        public IdFilter PositionId { get; set; }
        public IdFilter StoreId { get; set; }
        public List<StoreRepresentFilter> OrFilter { get; set; }
        public StoreRepresentOrder OrderBy {get; set;}
        public StoreRepresentSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StoreRepresentOrder
    {
        Id = 0,
        Name = 1,
        DateOfBirth = 2,
        Phone = 3,
        Position = 4,
        Store = 5,
    }

    [Flags]
    public enum StoreRepresentSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        DateOfBirth = E._2,
        Phone = E._3,
        Position = E._4,
        Store = E._5,
    }
}
