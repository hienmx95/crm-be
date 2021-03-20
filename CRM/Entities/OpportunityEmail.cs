using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class OpportunityEmail : DataEntity,  IEquatable<OpportunityEmail>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Reciepient { get; set; }
        public long OpportunityId { get; set; }
        public long CreatorId { get; set; }
        public long EmailStatusId { get; set; }
        public AppUser Creator { get; set; }
        public EmailStatus EmailStatus { get; set; }
        public Opportunity Opportunity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<OpportunityEmailCCMapping> OpportunityEmailCCMappings { get; set; }
        public bool Equals(OpportunityEmail other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class OpportunityEmailFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Content { get; set; }
        public StringFilter Reciepient { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter EmailStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<OpportunityEmailFilter> OrFilter { get; set; }
        public OpportunityEmailOrder OrderBy {get; set;}
        public OpportunityEmailSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OpportunityEmailOrder
    {
        Id = 0,
        Title = 1,
        Content = 2,
        Reciepient = 3,
        Opportunity = 4,
        Creator = 5,
        EmailStatus = 6,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum OpportunityEmailSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Title = E._1,
        Content = E._2,
        Reciepient = E._3,
        Opportunity = E._4,
        Creator = E._5,
        EmailStatus = E._6,
    }
}
