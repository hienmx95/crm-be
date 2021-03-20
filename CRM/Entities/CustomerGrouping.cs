using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerGrouping : DataEntity,  IEquatable<CustomerGrouping>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long CustomerTypeId { get; set; }
        public long? ParentId { get; set; }
        public string Path { get; set; }
        public long Level { get; set; }
        public long StatusId { get; set; }
        public string Description { get; set; }
        public CustomerType CustomerType { get; set; }
        public CustomerGrouping Parent { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(CustomerGrouping other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CustomerGroupingFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter CustomerTypeId { get; set; }
        public IdFilter ParentId { get; set; }
        public StringFilter Path { get; set; }
        public LongFilter Level { get; set; }
        public IdFilter StatusId { get; set; }
        public StringFilter Description { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<CustomerGroupingFilter> OrFilter { get; set; }
        public CustomerGroupingOrder OrderBy {get; set;}
        public CustomerGroupingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerGroupingOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
        CustomerType = 3,
        Parent = 4,
        Path = 5,
        Level = 6,
        Status = 7,
        Description = 8,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CustomerGroupingSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
        CustomerType = E._3,
        Parent = E._4,
        Path = E._5,
        Level = E._6,
        Status = E._7,
        Description = E._8,
    }
}
