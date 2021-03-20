using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class Opportunity : DataEntity, IEquatable<Opportunity>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? CompanyId { get; set; }
        public long? CustomerLeadId { get; set; }
        public DateTime ClosingDate { get; set; }
        public long? SaleStageId { get; set; }
        public long ProbabilityId { get; set; }
        public long? PotentialResultId { get; set; }
        public long? LeadSourceId { get; set; }
        public long AppUserId { get; set; }
        public long? CurrencyId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? ForecastAmount { get; set; }
        public string Description { get; set; }
        public bool? RefuseReciveSMS { get; set; }
        public bool? RefuseReciveEmail { get; set; }
        public long? OpportunityResultTypeId { get; set; }
        public long CreatorId { get; set; }
        public long OrganizationId { get; set; }
        public AppUser AppUser { get; set; }
        public Company Company { get; set; }
        public Currency Currency { get; set; }
        public CustomerLead CustomerLead { get; set; }
        public CustomerLeadSource LeadSource { get; set; }
        public Organization Organization { get; set; }
        public OpportunityResultType OpportunityResultType { get; set; }
        public PotentialResult PotentialResult { get; set; }
        public Probability Probability { get; set; }
        public SaleStage SaleStage { get; set; }
        public List<OpportunityActivity> OpportunityActivities { get; set; }
        public List<OpportunityCallLogMapping> OpportunityCallLogMappings { get; set; }
        public List<OpportunityContactMapping> OpportunityContactMappings { get; set; }
        public List<OpportunityEmail> OpportunityEmails { get; set; }
        public List<OpportunityFileGrouping> OpportunityFileGroupings { get; set; }
        public List<OpportunityItemMapping> OpportunityItemMappings { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool Equals(Opportunity other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class OpportunityFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public DateFilter ClosingDate { get; set; }
        public IdFilter SaleStageId { get; set; }
        public IdFilter ProbabilityId { get; set; }
        public IdFilter PotentialResultId { get; set; }
        public IdFilter LeadSourceId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter CurrencyId { get; set; }
        public DecimalFilter Amount { get; set; }
        public DecimalFilter ForecastAmount { get; set; }
        public StringFilter Description { get; set; }
        public IdFilter OpportunityResultTypeId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public IdFilter ContactId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<OpportunityFilter> OrFilter { get; set; }
        public OpportunityOrder OrderBy { get; set; }
        public OpportunitySelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OpportunityOrder
    {
        Id = 0,
        Name = 1,
        Company = 2,
        CustomerLead = 3,
        ClosingDate = 4,
        SaleStage = 5,
        Probability = 6,
        PotentialResult = 7,
        LeadSource = 8,
        AppUser = 9,
        Currency = 10,
        Amount = 11,
        ForecastAmount = 12,
        Description = 13,
        RefuseReciveSMS = 14,
        RefuseReciveEmail = 15,
        OpportunityResultType = 16,
        Creator = 17,
        Organization = 18,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum OpportunitySelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Company = E._2,
        CustomerLead = E._3,
        ClosingDate = E._4,
        SaleStage = E._5,
        Probability = E._6,
        PotentialResult = E._7,
        LeadSource = E._8,
        AppUser = E._9,
        Currency = E._10,
        Amount = E._11,
        ForecastAmount = E._12,
        Description = E._13,
        RefuseReciveSMS = E._14,
        RefuseReciveEmail = E._15,
        OpportunityResultType = E._16,
        Creator = E._17,
        Organization = E._18,
    }
}
