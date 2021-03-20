using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ActivityPriorityDAO
    {
        public ActivityPriorityDAO()
        {
            CompanyActivities = new HashSet<CompanyActivityDAO>();
            ContactActivities = new HashSet<ContactActivityDAO>();
            CustomerLeadActivities = new HashSet<CustomerLeadActivityDAO>();
            OpportunityActivities = new HashSet<OpportunityActivityDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompanyActivityDAO> CompanyActivities { get; set; }
        public virtual ICollection<ContactActivityDAO> ContactActivities { get; set; }
        public virtual ICollection<CustomerLeadActivityDAO> CustomerLeadActivities { get; set; }
        public virtual ICollection<OpportunityActivityDAO> OpportunityActivities { get; set; }
    }
}
