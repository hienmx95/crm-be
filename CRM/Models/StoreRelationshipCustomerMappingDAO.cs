using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class StoreRelationshipCustomerMappingDAO
    {
        public long RelationshipCustomerTypeId { get; set; }
        public long StoreId { get; set; }

        public virtual RelationshipCustomerTypeDAO RelationshipCustomerType { get; set; }
        public virtual StoreDAO Store { get; set; }
    }
}
