using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_OpportunityContactMappingDTO : DataDTO
    {
        public long ContactId { get; set; }
        public long OpportunityId { get; set; }
        public Opportunity_ContactDTO Contact { get; set; }   

        public Opportunity_OpportunityContactMappingDTO() {}
        public Opportunity_OpportunityContactMappingDTO(OpportunityContactMapping OpportunityContactMapping)
        {
            this.ContactId = OpportunityContactMapping.ContactId;
            this.OpportunityId = OpportunityContactMapping.OpportunityId;
            this.Contact = OpportunityContactMapping.Contact == null ? null : new Opportunity_ContactDTO(OpportunityContactMapping.Contact);
            this.Errors = OpportunityContactMapping.Errors;
        }
    }

    public class Opportunity_OpportunityContactMappingFilterDTO : FilterDTO
    {
        
        public IdFilter ContactId { get; set; }
        
        public IdFilter OpportunityId { get; set; }
        
        public OpportunityContactMappingOrder OrderBy { get; set; }
    }
}