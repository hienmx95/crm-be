using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class StoreImproveQualityServingMapping : DataEntity,  IEquatable<StoreImproveQualityServingMapping>
    {
        public long StoreId { get; set; }
        public long ImproveQualityServingId { get; set; }
        public ImproveQualityServing ImproveQualityServing { get; set; }
        public Store Store { get; set; }
        
        public bool Equals(StoreImproveQualityServingMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class StoreImproveQualityServingMappingFilter : FilterEntity
    {
        public IdFilter StoreId { get; set; }
        public IdFilter ImproveQualityServingId { get; set; }
        public List<StoreImproveQualityServingMappingFilter> OrFilter { get; set; }
        public StoreImproveQualityServingMappingOrder OrderBy {get; set;}
        public StoreImproveQualityServingMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StoreImproveQualityServingMappingOrder
    {
        Store = 0,
        ImproveQualityServing = 1,
    }

    [Flags]
    public enum StoreImproveQualityServingMappingSelect:long
    {
        ALL = E.ALL,
        Store = E._0,
        ImproveQualityServing = E._1,
    }
}
