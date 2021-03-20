using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OpportunityActivityDAO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long ActivityTypeId { get; set; }
        public long? ActivityPriorityId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public long OpportunityId { get; set; }
        public long AppUserId { get; set; }
        public long ActivityStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ActivityPriorityDAO ActivityPriority { get; set; }
        public virtual ActivityStatusDAO ActivityStatus { get; set; }
        public virtual ActivityTypeDAO ActivityType { get; set; }
        public virtual AppUserDAO AppUser { get; set; }
        public virtual OpportunityDAO Opportunity { get; set; }
    }
}
