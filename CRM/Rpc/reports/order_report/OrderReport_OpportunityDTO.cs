using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.order_report
{
    public class OrderReport_OpportunityDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? ClosingDate { get; set; }
        public long? SaleStageId { get; set; }
        public long? ProbabilityId { get; set; }
        public long? PotentialResultId { get; set; }
        public long? LeadSourceId { get; set; }
        public long? CreatorId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? ForecastAmount { get; set; }
        public string Description { get; set; }
        //public OrderReport_CustomerLeadSourceDTO LeadSource { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        //public OrderReport_AppUserDTO AppUser { get; set; }
        //public OrderReport_ProbabilityDTO Probability { get; set; }
        //public OrderReport_SaleStageDTO SaleStage { get; set; }
        public OrderReport_OpportunityDTO() { }
        public OrderReport_OpportunityDTO(Opportunity Opportunity)
        {
            this.Id = Opportunity.Id;
            this.Name = Opportunity.Name;
            this.CompanyId = Opportunity.CompanyId;
            this.ClosingDate = Opportunity.ClosingDate;
            this.SaleStageId = Opportunity.SaleStageId;
            this.ProbabilityId = Opportunity.ProbabilityId;
            this.PotentialResultId = Opportunity.PotentialResultId;
            this.LeadSourceId = Opportunity.LeadSourceId;
            this.CreatorId = Opportunity.CreatorId;
            this.Amount = Opportunity.Amount;
            this.ForecastAmount = Opportunity.ForecastAmount;
            this.Description = Opportunity.Description;
            //this.LeadSource = Opportunity.LeadSource == null ? null : new OrderReport_CustomerLeadSourceDTO(Opportunity.LeadSource);
            this.CreatedAt = Opportunity.CreatedAt;
            this.UpdatedAt = Opportunity.UpdatedAt;
            this.Errors = Opportunity.Errors;

            //this.AppUser = Opportunity.AppUser == null ? null : new OrderReport_AppUserDTO(Opportunity.AppUser);
            //this.Probability = Opportunity.Probability == null ? null : new OrderReport_ProbabilityDTO(Opportunity.Probability);
            //this.SaleStage = Opportunity.SaleStage == null ? null : new OrderReport_SaleStageDTO(Opportunity.SaleStage);
        }
    }

    public class OrderReport_OpportunityFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter CompanyId { get; set; }
        public DateFilter ClosingDate { get; set; }
        public IdFilter SaleStageId { get; set; }
        public IdFilter ProbabilityId { get; set; }
        public IdFilter PotentialResultId { get; set; }
        public IdFilter LeadSourceId { get; set; }
        public IdFilter CreatorId { get; set; }
        public DecimalFilter Amount { get; set; }
        public DecimalFilter ForecastAmount { get; set; }
        public StringFilter Description { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public OpportunityOrder OrderBy { get; set; }
    }
}
