using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class OrderQuoteStatus : DataEntity,  IEquatable<OrderQuoteStatus>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        
        public bool Equals(OrderQuoteStatus other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class OrderQuoteStatusFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<OrderQuoteStatusFilter> OrFilter { get; set; }
        public OrderQuoteStatusOrder OrderBy {get; set;}
        public OrderQuoteStatusSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderQuoteStatusOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
    }

    [Flags]
    public enum OrderQuoteStatusSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
    }
}
