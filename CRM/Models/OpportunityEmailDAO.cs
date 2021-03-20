using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OpportunityEmailDAO
    {
        public OpportunityEmailDAO()
        {
            OpportunityEmailCCMappings = new HashSet<OpportunityEmailCCMappingDAO>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Reciepient { get; set; }
        public long OpportunityId { get; set; }
        public long CreatorId { get; set; }
        public long EmailStatusId { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        public virtual AppUserDAO Creator { get; set; }
        public virtual EmailStatusDAO EmailStatus { get; set; }
        public virtual OpportunityDAO Opportunity { get; set; }
        public virtual ICollection<OpportunityEmailCCMappingDAO> OpportunityEmailCCMappings { get; set; }
    }
}
