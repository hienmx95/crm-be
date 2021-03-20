using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CallLogDAO
    {
        public CallLogDAO()
        {
            CompanyCallLogMappings = new HashSet<CompanyCallLogMappingDAO>();
            ContactCallLogMappings = new HashSet<ContactCallLogMappingDAO>();
            CustomerCallLogMappings = new HashSet<CustomerCallLogMappingDAO>();
            CustomerLeadCallLogMappings = new HashSet<CustomerLeadCallLogMappingDAO>();
            OpportunityCallLogMappings = new HashSet<OpportunityCallLogMappingDAO>();
            Tickets = new HashSet<TicketDAO>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Phone { get; set; }
        public DateTime CallTime { get; set; }
        public long EntityReferenceId { get; set; }
        public long EntityId { get; set; }
        public long CallTypeId { get; set; }
        public long? CallCategoryId { get; set; }
        public long? CallEmotionId { get; set; }
        public long? CallStatusId { get; set; }
        public long AppUserId { get; set; }
        public long CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CallCategoryDAO CallCategory { get; set; }
        public virtual CallEmotionDAO CallEmotion { get; set; }
        public virtual CallStatusDAO CallStatus { get; set; }
        public virtual CallTypeDAO CallType { get; set; }
        public virtual AppUserDAO Creator { get; set; }
        public virtual EntityReferenceDAO EntityReference { get; set; }
        public virtual ICollection<CompanyCallLogMappingDAO> CompanyCallLogMappings { get; set; }
        public virtual ICollection<ContactCallLogMappingDAO> ContactCallLogMappings { get; set; }
        public virtual ICollection<CustomerCallLogMappingDAO> CustomerCallLogMappings { get; set; }
        public virtual ICollection<CustomerLeadCallLogMappingDAO> CustomerLeadCallLogMappings { get; set; }
        public virtual ICollection<OpportunityCallLogMappingDAO> OpportunityCallLogMappings { get; set; }
        public virtual ICollection<TicketDAO> Tickets { get; set; }
    }
}
