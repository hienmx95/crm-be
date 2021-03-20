using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.opportunity_report
{
    public class OpportunityReport_OpportunityDTO : DataDTO
    {
        public long STT { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string ProbabilityName { get; set; }
        public string Amount { get; set; }
        public DateTime? ClosingDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string SaleStageName { get; set; }
        public string CompanyName { get; set; }
        public string CountItem { get; set; }
        public string TotalRevenueOfItem { get; set; }
        public string AppUserName { get; set; }
        public string ResultName { get; set; }
        public string ContactName { get; set; }
        public string ForecastAmount { get; set; }
        public string LeadSourceName { get; set; }

        public List<OpportunityReport_OpportunityItemMappingDTO> OpportunityItemMappings { get; set; }


        public OpportunityReport_OpportunityDTO() { }

    }

    public class OpportunityReport_OpportunityFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter CompanyId { get; set; }
        public DateFilter ClosingDate { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter CurrencyId { get; set; }
        public IdFilter SaleStageId { get; set; }
        public IdFilter ProbabilityId { get; set; }
        public IdFilter PotentialResultId { get; set; }
        public IdFilter LeadSourceId { get; set; }
        public IdFilter AppUserId { get; set; }
        public DecimalFilter Amount { get; set; }
        public DecimalFilter ForecastAmount { get; set; }
        public StringFilter Description { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public OpportunityOrder OrderBy { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter ContactId { get; set; }
    }
}
