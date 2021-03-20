using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerFeedback : DataEntity,  IEquatable<CustomerFeedback>
    {
        public long Id { get; set; }
        public bool IsSystemCustomer { get; set; }
        public long? CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long? CustomerFeedbackTypeId { get; set; }
        public string Title { get; set; }
        public DateTime? SendDate { get; set; }
        public string Content { get; set; }
        public long? StatusId { get; set; }
        public Customer Customer { get; set; }
        public CustomerFeedbackType CustomerFeedbackType { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(CustomerFeedback other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CustomerFeedbackFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter CustomerId { get; set; }
        public StringFilter FullName { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter PhoneNumber { get; set; }
        public IdFilter CustomerFeedbackTypeId { get; set; }
        public StringFilter Title { get; set; }
        public DateFilter SendDate { get; set; }
        public StringFilter Content { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<CustomerFeedbackFilter> OrFilter { get; set; }
        public CustomerFeedbackOrder OrderBy {get; set;}
        public CustomerFeedbackSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerFeedbackOrder
    {
        Id = 0,
        IsSystemCustomer = 1,
        Customer = 2,
        FullName = 3,
        Email = 4,
        PhoneNumber = 5,
        CustomerFeedbackType = 6,
        Title = 7,
        SendDate = 8,
        Content = 9,
        Status = 10,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CustomerFeedbackSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        IsSystemCustomer = E._1,
        Customer = E._2,
        FullName = E._3,
        Email = E._4,
        PhoneNumber = E._5,
        CustomerFeedbackType = E._6,
        Title = E._7,
        SendDate = E._8,
        Content = E._9,
        Status = E._10,
    }
}
