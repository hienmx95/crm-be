using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerPhone : DataEntity,  IEquatable<CustomerPhone>
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Phone { get; set; }
        public long PhoneTypeId { get; set; }
        public Customer Customer { get; set; }
        public PhoneType PhoneType { get; set; }
        
        public bool Equals(CustomerPhone other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CustomerPhoneFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter CustomerId { get; set; }
        public StringFilter Phone { get; set; }
        public IdFilter PhoneTypeId { get; set; }
        public List<CustomerPhoneFilter> OrFilter { get; set; }
        public CustomerPhoneOrder OrderBy {get; set;}
        public CustomerPhoneSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerPhoneOrder
    {
        Id = 0,
        Customer = 1,
        Phone = 2,
        PhoneType = 3,
    }

    [Flags]
    public enum CustomerPhoneSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Customer = E._1,
        Phone = E._2,
        PhoneType = E._3,
    }
}
