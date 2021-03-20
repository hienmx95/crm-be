using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace CRM.Entities
{
    public class CustomerLeadEmail : DataEntity, IEquatable<CustomerLeadEmail>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Reciepient { get; set; }
        public long CustomerLeadId { get; set; }
        public long CreatorId { get; set; }
        public long EmailStatusId { get; set; }
        public CustomerLead CustomerLead { get; set; }
        public AppUser Creator { get; set; }
        public EmailStatus EmailStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<CustomerLeadEmailCCMapping> CustomerLeadEmailCCMappings { get; set; }

        public bool Equals(CustomerLeadEmail other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CustomerLeadEmailFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Content { get; set; }
        public StringFilter Reciepient { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter EmailStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<CustomerLeadEmailFilter> OrFilter { get; set; }
        public CustomerLeadEmailOrder OrderBy { get; set; }
        public CustomerLeadEmailSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerLeadEmailOrder
    {
        Id = 0,
        Title = 1,
        Content = 2,
        CustomerLead = 3,
        Creator = 4,
        EmailStatus = 5,
        Reciepient = 6,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CustomerLeadEmailSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Title = E._1,
        Content = E._2,
        CustomerLead = E._3,
        Creator = E._4,
        EmailStatus = E._5,
        Reciepient = E._6,
    }
}
