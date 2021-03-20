using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerPointHistory : DataEntity,  IEquatable<CustomerPointHistory>
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long TotalPoint { get; set; }
        public long CurrentPoint { get; set; }
        public long ChangePoint { get; set; }
        public bool IsIncrease { get; set; }
        public string Description { get; set; }
        public bool ReduceTotal { get; set; }
        public Customer Customer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(CustomerPointHistory other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CustomerPointHistoryFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter CustomerId { get; set; }
        public LongFilter TotalPoint { get; set; }
        public LongFilter CurrentPoint { get; set; }
        public LongFilter ChangePoint { get; set; }
        public StringFilter Description { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<CustomerPointHistoryFilter> OrFilter { get; set; }
        public CustomerPointHistoryOrder OrderBy {get; set;}
        public CustomerPointHistorySelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerPointHistoryOrder
    {
        Id = 0,
        Customer = 1,
        TotalPoint = 2,
        CurrentPoint = 3,
        ChangePoint = 4,
        IsIncrease = 5,
        Description = 6,
        ReduceTotal = 7,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CustomerPointHistorySelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Customer = E._1,
        TotalPoint = E._2,
        CurrentPoint = E._3,
        ChangePoint = E._4,
        IsIncrease = E._5,
        Description = E._6,
        ReduceTotal = E._7,
    }
}
