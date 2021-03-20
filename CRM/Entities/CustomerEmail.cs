using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerEmail : DataEntity,  IEquatable<CustomerEmail>
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Email { get; set; }
        public long EmailTypeId { get; set; }
        public Customer Customer { get; set; }
        public EmailType EmailType { get; set; }
        
        public bool Equals(CustomerEmail other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CustomerEmailFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter CustomerId { get; set; }
        public StringFilter Email { get; set; }
        public IdFilter EmailTypeId { get; set; }
        public List<CustomerEmailFilter> OrFilter { get; set; }
        public CustomerEmailOrder OrderBy {get; set;}
        public CustomerEmailSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerEmailOrder
    {
        Id = 0,
        Customer = 1,
        Email = 2,
        EmailType = 3,
    }

    [Flags]
    public enum CustomerEmailSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Customer = E._1,
        Email = E._2,
        EmailType = E._3,
    }
}
