using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class BusinessConcentrationLevel : DataEntity,  IEquatable<BusinessConcentrationLevel>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Branch { get; set; }
        public decimal? RevenueInYear { get; set; }
        public long? MarketingStaff { get; set; }
        public long? StoreId { get; set; }
        public Store Store { get; set; }
        
        public bool Equals(BusinessConcentrationLevel other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class BusinessConcentrationLevelFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Manufacturer { get; set; }
        public StringFilter Branch { get; set; }
        public DecimalFilter RevenueInYear { get; set; }
        public LongFilter MarketingStaff { get; set; }
        public IdFilter StoreId { get; set; }
        public List<BusinessConcentrationLevelFilter> OrFilter { get; set; }
        public BusinessConcentrationLevelOrder OrderBy {get; set;}
        public BusinessConcentrationLevelSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum BusinessConcentrationLevelOrder
    {
        Id = 0,
        Name = 1,
        Manufacturer = 2,
        Branch = 3,
        RevenueInYear = 4,
        MarketingStaff = 5,
        Store = 6,
    }

    [Flags]
    public enum BusinessConcentrationLevelSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Manufacturer = E._2,
        Branch = E._3,
        RevenueInYear = E._4,
        MarketingStaff = E._5,
        Store = E._6,
    }
}
