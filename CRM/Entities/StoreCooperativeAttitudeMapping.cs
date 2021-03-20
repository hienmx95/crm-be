using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class StoreCooperativeAttitudeMapping : DataEntity,  IEquatable<StoreCooperativeAttitudeMapping>
    {
        public long StoreId { get; set; }
        public long CooperativeAttitudeId { get; set; }
        public CooperativeAttitude CooperativeAttitude { get; set; }
        public Store Store { get; set; }
        
        public bool Equals(StoreCooperativeAttitudeMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class StoreCooperativeAttitudeMappingFilter : FilterEntity
    {
        public IdFilter StoreId { get; set; }
        public IdFilter CooperativeAttitudeId { get; set; }
        public List<StoreCooperativeAttitudeMappingFilter> OrFilter { get; set; }
        public StoreCooperativeAttitudeMappingOrder OrderBy {get; set;}
        public StoreCooperativeAttitudeMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StoreCooperativeAttitudeMappingOrder
    {
        Store = 0,
        CooperativeAttitude = 1,
    }

    [Flags]
    public enum StoreCooperativeAttitudeMappingSelect:long
    {
        ALL = E.ALL,
        Store = E._0,
        CooperativeAttitude = E._1,
    }
}
