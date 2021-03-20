using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class EmailStatusDAO
    {
        public EmailStatusDAO()
        {
            CompanyEmails = new HashSet<CompanyEmailDAO>();
            ContactEmails = new HashSet<ContactEmailDAO>();
            CustomerEmailHistories = new HashSet<CustomerEmailHistoryDAO>();
            CustomerLeadEmails = new HashSet<CustomerLeadEmailDAO>();
            OpportunityEmails = new HashSet<OpportunityEmailDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompanyEmailDAO> CompanyEmails { get; set; }
        public virtual ICollection<ContactEmailDAO> ContactEmails { get; set; }
        public virtual ICollection<CustomerEmailHistoryDAO> CustomerEmailHistories { get; set; }
        public virtual ICollection<CustomerLeadEmailDAO> CustomerLeadEmails { get; set; }
        public virtual ICollection<OpportunityEmailDAO> OpportunityEmails { get; set; }
    }
}
