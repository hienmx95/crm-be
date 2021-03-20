using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OpportunityContactMappingDAO
    {
        public long ContactId { get; set; }
        public long OpportunityId { get; set; }

        public virtual ContactDAO Contact { get; set; }
        public virtual OpportunityDAO Opportunity { get; set; }
    }
}
