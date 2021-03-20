using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_OpportunityFileMappingDTO : DataDTO
    {
        public long FileId { get; set; }
        public long OpportunityFileGroupingId { get; set; }
        public Opportunity_FileDTO File { get; set; }
        public Opportunity_OpportunityFileMappingDTO() { }
        public Opportunity_OpportunityFileMappingDTO(OpportunityFileMapping OpportunityFileMapping)
        {
            this.FileId = OpportunityFileMapping.FileId;
            this.OpportunityFileGroupingId = OpportunityFileMapping.OpportunityFileGroupingId;
            this.File = OpportunityFileMapping.File == null ? null : new Opportunity_FileDTO(OpportunityFileMapping.File);
        }
    }

    public class Opportunity_OpportunityFileDTOFilterDTO : FilterDTO
    {

        public IdFilter FileId { get; set; }

        public IdFilter OpportunityFileGroupingId { get; set; }

        public OpportunityFileMappingOrder OrderBy { get; set; }
    }
}