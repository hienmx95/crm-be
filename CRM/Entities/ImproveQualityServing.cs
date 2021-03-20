using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ImproveQualityServing : DataEntity,  IEquatable<ImproveQualityServing>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public long? StoreId { get; set; }
        public Store Store { get; set; }
        
        public bool Equals(ImproveQualityServing other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ImproveQualityServingFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Detail { get; set; }
        public IdFilter StoreId { get; set; }
        public List<ImproveQualityServingFilter> OrFilter { get; set; }
        public ImproveQualityServingOrder OrderBy {get; set;}
        public ImproveQualityServingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ImproveQualityServingOrder
    {
        Id = 0,
        Name = 1,
        Detail = 2,
        Store = 3,
    }

    [Flags]
    public enum ImproveQualityServingSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Detail = E._2,
        Store = E._3,
    }
}
