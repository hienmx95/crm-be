using CRM.Common;
using CRM.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_OpportunityEmailCCMappingDTO : DataDTO
    {
        public long OpportunityEmailId { get; set; }
        public long AppUserId { get; set; }
        public Opportunity_AppUserDTO AppUser { get; set; }

        public Opportunity_OpportunityEmailCCMappingDTO() { }
        public Opportunity_OpportunityEmailCCMappingDTO(OpportunityEmailCCMapping OpportunityEmailCCMapping)
        {
            this.OpportunityEmailId = OpportunityEmailCCMapping.OpportunityEmailId;
            this.AppUserId = OpportunityEmailCCMapping.AppUserId;
            this.AppUser = OpportunityEmailCCMapping.AppUser == null ? null : new Opportunity_AppUserDTO(OpportunityEmailCCMapping.AppUser);
            this.Errors = OpportunityEmailCCMapping.Errors;
        }
    }

    public class Opportunity_OpportunityEmailCCMappingFilter : FilterDTO
    {
        public IdFilter OpportunityEmailId { get; set; }
        public IdFilter AppUserId { get; set; }
        public OpportunityEmailCCMappingOrder OrderBy { get; set; }
    }
}
