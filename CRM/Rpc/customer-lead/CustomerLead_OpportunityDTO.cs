using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_OpportunityDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? CompanyId { get; set; }
        public long? ContactId { get; set; }
        public DateTime ClosingDate { get; set; }
        public long? SaleStageId { get; set; }
        public long ProbabilityId { get; set; }
        public long? PotentialResultId { get; set; }
        public long? CustomerLeadId { get; set; }
        public long? LeadSourceId { get; set; }
        public long AppUserId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? ForecastAmount { get; set; }
        public string Description { get; set; }
        public CustomerLead_AppUserDTO AppUser { get; set; }
        public CustomerLead_CustomerLeadSourceDTO LeadSource { get; set; }
        public CustomerLead_ProbabilityDTO Probability { get; set; }
        public CustomerLead_SaleStageDTO SaleStage { get; set; }
        public CustomerLead_CustomerLeadDTO CustomerLead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CustomerLead_OpportunityDTO() {}
        public CustomerLead_OpportunityDTO(Opportunity Opportunity)
        {
            this.Id = Opportunity.Id;
            this.Name = Opportunity.Name;
            this.CompanyId = Opportunity.CompanyId;
            this.ClosingDate = Opportunity.ClosingDate;
            this.SaleStageId = Opportunity.SaleStageId;
            this.ProbabilityId = Opportunity.ProbabilityId;
            this.PotentialResultId = Opportunity.PotentialResultId;
            this.LeadSourceId = Opportunity.LeadSourceId;
            this.AppUserId = Opportunity.AppUserId;
            this.Amount = Opportunity.Amount;
            this.ForecastAmount = Opportunity.ForecastAmount;
            this.Description = Opportunity.Description;
            this.CustomerLeadId = Opportunity.CustomerLeadId;
            this.AppUser = Opportunity.AppUser == null ? null : new CustomerLead_AppUserDTO(Opportunity.AppUser);
            this.LeadSource = Opportunity.LeadSource == null ? null : new CustomerLead_CustomerLeadSourceDTO(Opportunity.LeadSource);
            this.Probability = Opportunity.Probability == null ? null : new CustomerLead_ProbabilityDTO(Opportunity.Probability);
            this.SaleStage = Opportunity.SaleStage == null ? null : new CustomerLead_SaleStageDTO(Opportunity.SaleStage);
            this.CustomerLead = Opportunity.CustomerLead == null ? null : new CustomerLead_CustomerLeadDTO(Opportunity.CustomerLead);
            this.CreatedAt = Opportunity.CreatedAt;
            this.UpdatedAt = Opportunity.UpdatedAt;
            this.Errors = Opportunity.Errors;
        }
    }

    public class CustomerLead_OpportunityFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter ContactId { get; set; }
        public DateFilter ClosingDate { get; set; }
        public IdFilter SaleStageId { get; set; }
        public IdFilter ProbabilityId { get; set; }
        public IdFilter PotentialResultId { get; set; }
        public IdFilter LeadSourceId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter CreatorId { get; set; }
        public DecimalFilter Amount { get; set; }
        public DecimalFilter ForecastAmount { get; set; }
        public StringFilter Description { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public OpportunityOrder OrderBy { get; set; }
    }
}
