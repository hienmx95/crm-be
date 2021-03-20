using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_OpportunityEmailDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Reciepient { get; set; }
        public long OpportunityId { get; set; }
        public long CreatorId { get; set; }
        public long EmailStatusId { get; set; }
        public Opportunity_AppUserDTO Creator { get; set; }
        public Opportunity_EmailStatusDTO EmailStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Opportunity_OpportunityEmailCCMappingDTO> OpportunityEmailCCMappings { get; set; }
        public Opportunity_OpportunityEmailDTO() {}
        public Opportunity_OpportunityEmailDTO(OpportunityEmail OpportunityEmail)
        {
            this.Id = OpportunityEmail.Id;
            this.Title = OpportunityEmail.Title;
            this.Content = OpportunityEmail.Content;
            this.Reciepient = OpportunityEmail.Reciepient;
            this.OpportunityId = OpportunityEmail.OpportunityId;
            this.CreatorId = OpportunityEmail.CreatorId;
            this.EmailStatusId = OpportunityEmail.EmailStatusId;
            this.Creator = OpportunityEmail.Creator == null ? null : new Opportunity_AppUserDTO(OpportunityEmail.Creator);
            this.EmailStatus = OpportunityEmail.EmailStatus == null ? null : new Opportunity_EmailStatusDTO(OpportunityEmail.EmailStatus);
            this.OpportunityEmailCCMappings = OpportunityEmail.OpportunityEmailCCMappings?.Select(x => new Opportunity_OpportunityEmailCCMappingDTO(x)).ToList();
            this.CreatedAt = OpportunityEmail.CreatedAt;
            this.UpdatedAt = OpportunityEmail.UpdatedAt;
            this.Errors = OpportunityEmail.Errors;
        }
    }

    public class Opportunity_OpportunityEmailFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Content { get; set; }
        public StringFilter Reciepient { get; set; }
        public StringFilter Email { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter EmailStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public OpportunityEmailOrder OrderBy { get; set; }
    }
}
