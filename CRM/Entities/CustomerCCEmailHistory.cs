using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerCCEmailHistory : DataEntity,  IEquatable<CustomerCCEmailHistory>
    {
        public long Id { get; set; }
        public long CustomerEmailHistoryId { get; set; }
        public string CCEmail { get; set; }
        public CustomerEmailHistory CustomerEmailHistory { get; set; }
        
        public bool Equals(CustomerCCEmailHistory other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CustomerCCEmailHistoryFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter CustomerEmailHistoryId { get; set; }
        public StringFilter CCEmail { get; set; }
        public List<CustomerCCEmailHistoryFilter> OrFilter { get; set; }
        public CustomerCCEmailHistoryOrder OrderBy {get; set;}
        public CustomerCCEmailHistorySelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerCCEmailHistoryOrder
    {
        Id = 0,
        CustomerEmailHistory = 1,
        CCEmail = 2,
    }

    [Flags]
    public enum CustomerCCEmailHistorySelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        CustomerEmailHistory = E._1,
        CCEmail = E._2,
    }
}
