using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class PotentialResultDAO
    {
        public PotentialResultDAO()
        {
            Opportunities = new HashSet<OpportunityDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OpportunityDAO> Opportunities { get; set; }
    }
}
