using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.opportunity_report
{
    public class OpportunityReport_ContactMappingDTO : DataDTO
    {
        public long OpportunityId { get; set; }
        public long ContactId { get; set; }
        public OpportunityReport_ContactDTO Contact { get; set; }
        public OpportunityReport_ContactMappingDTO() {}
        public OpportunityReport_ContactMappingDTO(OpportunityContactMapping OpportunityContactMapping)
        {
            this.OpportunityId = OpportunityContactMapping.OpportunityId;
            this.ContactId = OpportunityContactMapping.ContactId;
            this.Contact = OpportunityContactMapping.Contact == null ? null : new OpportunityReport_ContactDTO(OpportunityContactMapping.Contact);
        }
    }

    public class OpportunityReport_ContactMappingFilterDTO : FilterDTO
    {
        public long OpportunityId { get; set; }
        public long ContactId { get; set; }
        public OpportunityContactMappingOrder OrderBy { get; set; }
    }
}
