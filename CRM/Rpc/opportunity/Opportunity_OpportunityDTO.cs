using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_OpportunityDTO : DataDTO
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
        public Opportunity_AppUserDTO AppUser { get; set; }
        public Opportunity_CompanyDTO Company { get; set; }
        public Opportunity_CurrencyDTO Currency { get; set; }
        public Opportunity_CustomerLeadDTO CustomerLead { get; set; }
        public Opportunity_CustomerLeadSourceDTO LeadSource { get; set; }
        public Opportunity_OpportunityResultTypeDTO OpportunityResultType { get; set; }
        public Opportunity_PotentialResultDTO PotentialResult { get; set; }
        public Opportunity_ProbabilityDTO Probability { get; set; }
        public Opportunity_SaleStageDTO SaleStage { get; set; }
        public List<Opportunity_OpportunityActivityDTO> OpportunityActivities { get; set; }
        public List<Opportunity_OpportunityCallLogMappingDTO> OpportunityCallLogMappings { get; set; }
        public List<Opportunity_OpportunityContactMappingDTO> OpportunityContactMappings { get; set; }
        public List<Opportunity_OpportunityEmailDTO> OpportunityEmails { get; set; }
        public List<Opportunity_OpportunityFileGroupingDTO> OpportunityFileGroupings { get; set; }
        public List<Opportunity_OpportunityItemMappingDTO> OpportunityItemMappings { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Opportunity_OpportunityDTO() { }
        public Opportunity_OpportunityDTO(Opportunity Opportunity)
        {
            this.Id = Opportunity.Id;
            this.Name = Opportunity.Name;
            this.CompanyId = Opportunity.CompanyId;
            this.CustomerLeadId = Opportunity.CustomerLeadId;
            this.ClosingDate = Opportunity.ClosingDate;
            this.SaleStageId = Opportunity.SaleStageId;
            this.ProbabilityId = Opportunity.ProbabilityId;
            this.PotentialResultId = Opportunity.PotentialResultId;
            this.LeadSourceId = Opportunity.LeadSourceId;
            this.AppUserId = Opportunity.AppUserId;
            this.CurrencyId = Opportunity.CurrencyId;
            this.Amount = Opportunity.Amount;
            this.ForecastAmount = Opportunity.ForecastAmount;
            this.Description = Opportunity.Description;
            this.RefuseReciveSMS = Opportunity.RefuseReciveSMS;
            this.RefuseReciveEmail = Opportunity.RefuseReciveEmail;
            this.OpportunityResultTypeId = Opportunity.OpportunityResultTypeId;
            this.CreatorId = Opportunity.CreatorId;
            this.AppUser = Opportunity.AppUser == null ? null : new Opportunity_AppUserDTO(Opportunity.AppUser);
            this.Company = Opportunity.Company == null ? null : new Opportunity_CompanyDTO(Opportunity.Company);
            this.Currency = Opportunity.Currency == null ? null : new Opportunity_CurrencyDTO(Opportunity.Currency);
            this.CustomerLead = Opportunity.CustomerLead == null ? null : new Opportunity_CustomerLeadDTO(Opportunity.CustomerLead);
            this.LeadSource = Opportunity.LeadSource == null ? null : new Opportunity_CustomerLeadSourceDTO(Opportunity.LeadSource);
            this.OpportunityResultType = Opportunity.OpportunityResultType == null ? null : new Opportunity_OpportunityResultTypeDTO(Opportunity.OpportunityResultType);
            this.PotentialResult = Opportunity.PotentialResult == null ? null : new Opportunity_PotentialResultDTO(Opportunity.PotentialResult);
            this.Probability = Opportunity.Probability == null ? null : new Opportunity_ProbabilityDTO(Opportunity.Probability);
            this.SaleStage = Opportunity.SaleStage == null ? null : new Opportunity_SaleStageDTO(Opportunity.SaleStage);
            this.OpportunityActivities = Opportunity.OpportunityActivities?.Select(x => new Opportunity_OpportunityActivityDTO(x)).ToList();
            this.OpportunityCallLogMappings = Opportunity.OpportunityCallLogMappings?.Select(x => new Opportunity_OpportunityCallLogMappingDTO(x)).ToList();
            this.OpportunityContactMappings = Opportunity.OpportunityContactMappings?.Select(x => new Opportunity_OpportunityContactMappingDTO(x)).ToList();
            this.OpportunityEmails = Opportunity.OpportunityEmails?.Select(x => new Opportunity_OpportunityEmailDTO(x)).ToList();
            this.OpportunityFileGroupings = Opportunity.OpportunityFileGroupings?.Select(x => new Opportunity_OpportunityFileGroupingDTO(x)).ToList();
            this.OpportunityItemMappings = Opportunity.OpportunityItemMappings?.Select(x => new Opportunity_OpportunityItemMappingDTO(x)).ToList();
            this.CreatedAt = Opportunity.CreatedAt;
            this.UpdatedAt = Opportunity.UpdatedAt;
            this.Errors = Opportunity.Errors;
        }
    }

    public class Opportunity_OpportunityFilterDTO : FilterDTO
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
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public OpportunityOrder OrderBy { get; set; }
    }
}
