using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OpportunityFileGroupingDAO
    {
        public OpportunityFileGroupingDAO()
        {
            OpportunityFileMappings = new HashSet<OpportunityFileMappingDAO>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long OpportunityId { get; set; }
        public long CreatorId { get; set; }
        public long FileTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid RowId { get; set; }

        public virtual AppUserDAO Creator { get; set; }
        public virtual FileTypeDAO FileType { get; set; }
        public virtual OpportunityDAO Opportunity { get; set; }
        public virtual ICollection<OpportunityFileMappingDAO> OpportunityFileMappings { get; set; }
    }
}
