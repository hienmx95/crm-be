using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class StoreConsultingServiceMapping : DataEntity,  IEquatable<StoreConsultingServiceMapping>
    {
        public long StoreId { get; set; }
        public long ConsultingServiceId { get; set; }
        public ConsultingService ConsultingService { get; set; }
        public Store Store { get; set; }
        
        public bool Equals(StoreConsultingServiceMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class StoreConsultingServiceMappingFilter : FilterEntity
    {
        public IdFilter StoreId { get; set; }
        public IdFilter ConsultingServiceId { get; set; }
        public List<StoreConsultingServiceMappingFilter> OrFilter { get; set; }
        public StoreConsultingServiceMappingOrder OrderBy {get; set;}
        public StoreConsultingServiceMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StoreConsultingServiceMappingOrder
    {
        Store = 0,
        ConsultingService = 1,
    }

    [Flags]
    public enum StoreConsultingServiceMappingSelect:long
    {
        ALL = E.ALL,
        Store = E._0,
        ConsultingService = E._1,
    }
}
