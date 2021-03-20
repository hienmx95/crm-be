using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_OpportunityFileGroupingDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long OpportunityId { get; set; }
        public long CreatorId { get; set; }
        public long FileTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public Opportunity_AppUserDTO Creator { get; set; } 
        public Opportunity_FileTypeDTO FileType { get; set; } 

        public List<Opportunity_OpportunityFileMappingDTO> OpportunityFileMappings { get; set; }

        public Opportunity_OpportunityFileGroupingDTO() {}
        public Opportunity_OpportunityFileGroupingDTO(OpportunityFileGrouping OpportunityFileGrouping)
        {
            this.Id = OpportunityFileGrouping.Id;
            this.Title = OpportunityFileGrouping.Title;
            this.Description = OpportunityFileGrouping.Description;
            this.CreatorId = OpportunityFileGrouping.CreatorId;
            this.OpportunityId = OpportunityFileGrouping.OpportunityId;
            this.FileTypeId = OpportunityFileGrouping.FileTypeId;
            this.CreatedAt = OpportunityFileGrouping.CreatedAt;
            this.UpdatedAt = OpportunityFileGrouping.UpdatedAt;
            this.Errors = OpportunityFileGrouping.Errors;
            this.Creator = OpportunityFileGrouping.Creator == null ? null : new Opportunity_AppUserDTO(OpportunityFileGrouping.Creator);
            this.FileType = OpportunityFileGrouping.FileType == null ? null : new Opportunity_FileTypeDTO(OpportunityFileGrouping.FileType);
            this.OpportunityFileMappings = OpportunityFileGrouping.OpportunityFileMappings?.Select(x => new Opportunity_OpportunityFileMappingDTO(x)).ToList();
        }
    }

    public class Opportunity_OpportunityFileGroupingFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Description { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter FileTypeId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public OpportunityFileGroupingOrder OrderBy { get; set; }
    }
}
