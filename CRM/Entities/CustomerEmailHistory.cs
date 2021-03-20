using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerEmailHistory : DataEntity,  IEquatable<CustomerEmailHistory>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Reciepient { get; set; }
        public long CustomerId { get; set; }
        public long CreatorId { get; set; }
        public long EmailStatusId { get; set; }
        public AppUser Creator { get; set; }
        public Customer Customer { get; set; }
        public EmailStatus EmailStatus { get; set; }
        public List<CustomerCCEmailHistory> CustomerCCEmailHistories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(CustomerEmailHistory other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CustomerEmailHistoryFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Content { get; set; }
        public StringFilter Reciepient { get; set; }
        public IdFilter CustomerId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter EmailStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<CustomerEmailHistoryFilter> OrFilter { get; set; }
        public CustomerEmailHistoryOrder OrderBy {get; set;}
        public CustomerEmailHistorySelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerEmailHistoryOrder
    {
        Id = 0,
        Title = 1,
        Content = 2,
        Reciepient = 3,
        Customer = 4,
        Creator = 5,
        EmailStatus = 6,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CustomerEmailHistorySelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Title = E._1,
        Content = E._2,
        Reciepient = E._3,
        Customer = E._4,
        Creator = E._5,
        EmailStatus = E._6,
    }
}
