using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace CRM.Rpc.reports.opportunity_report
{
    public class OpportunityReport_OpportunityReportDTO : DataDTO
    {
        public List<OpportunityReport_OpportunityDTO> Opportunities { get; set; }
    }

    public class OpportunityReport_OpportunityReportFilterDTO : FilterDTO
    {
        public StringFilter OpportunityName { get; set; }
        public DecimalFilter OpportunityAmount { get; set; }
        public IdFilter OpportunityItemId { get; set; }
        public IdFilter OpportunityCompanyId { get; set; }
        public IdFilter OpportunitySaleStageId { get; set; }
        public IdFilter OpportunityProbabilityId { get; set; }
        public DateFilter OpportunityClosingDate { get; set; }
        public IdFilter AppUserId { get; set; }
        public DateFilter OpportunityCreatedAt { get; set; }
    }

}
