using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class RelationshipCustomerTypeDAO
    {
        public RelationshipCustomerTypeDAO()
        {
            StoreRelationshipCustomerMappings = new HashSet<StoreRelationshipCustomerMappingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StoreRelationshipCustomerMappingDAO> StoreRelationshipCustomerMappings { get; set; }
    }
}
