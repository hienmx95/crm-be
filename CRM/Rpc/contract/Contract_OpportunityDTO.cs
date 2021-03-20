using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_OpportunityDTO : DataDTO
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
        public Contract_AppUserDTO AppUser { get; set; }
        public Contract_CompanyDTO Company { get; set; }
        public Contract_CurrencyDTO Currency { get; set; }
        public Contract_CustomerLeadDTO CustomerLead { get; set; }
        public Contract_CustomerLeadSourceDTO LeadSource { get; set; }
        public Contract_OpportunityResultTypeDTO OpportunityResultType { get; set; }
        public Contract_PotentialResultDTO PotentialResult { get; set; }
        public Contract_ProbabilityDTO Probability { get; set; }
        public Contract_SaleStageDTO SaleStage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Contract_OpportunityDTO() { }
        public Contract_OpportunityDTO(Opportunity Opportunity)
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
            this.AppUser = Opportunity.AppUser == null ? null : new Contract_AppUserDTO(Opportunity.AppUser);
            this.Company = Opportunity.Company == null ? null : new Contract_CompanyDTO(Opportunity.Company);
            this.Currency = Opportunity.Currency == null ? null : new Contract_CurrencyDTO(Opportunity.Currency);
            this.CustomerLead = Opportunity.CustomerLead == null ? null : new Contract_CustomerLeadDTO(Opportunity.CustomerLead);
            this.LeadSource = Opportunity.LeadSource == null ? null : new Contract_CustomerLeadSourceDTO(Opportunity.LeadSource);
            this.OpportunityResultType = Opportunity.OpportunityResultType == null ? null : new Contract_OpportunityResultTypeDTO(Opportunity.OpportunityResultType);
            this.PotentialResult = Opportunity.PotentialResult == null ? null : new Contract_PotentialResultDTO(Opportunity.PotentialResult);
            this.Probability = Opportunity.Probability == null ? null : new Contract_ProbabilityDTO(Opportunity.Probability);
            this.SaleStage = Opportunity.SaleStage == null ? null : new Contract_SaleStageDTO(Opportunity.SaleStage);
            this.CreatedAt = Opportunity.CreatedAt;
            this.UpdatedAt = Opportunity.UpdatedAt;
            this.Errors = Opportunity.Errors;
        }
    }

    public class Contract_OpportunityFilterDTO : FilterDTO
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