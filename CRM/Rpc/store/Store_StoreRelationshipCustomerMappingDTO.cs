using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreRelationshipCustomerMappingDTO : DataDTO
    {
        public long StoreId { get; set; }
        public long RelationshipCustomerTypeId { get; set; }
        public RelationshipCustomerType RelationshipCustomerType { get; set; }
        public Store_StoreRelationshipCustomerMappingDTO()
        {

        }
        public Store_StoreRelationshipCustomerMappingDTO(StoreRelationshipCustomerMapping StoreRelationshipCustomerMapping)
        {
            this.StoreId = StoreRelationshipCustomerMapping.StoreId;
            this.RelationshipCustomerTypeId = StoreRelationshipCustomerMapping.RelationshipCustomerTypeId;
            this.RelationshipCustomerType = StoreRelationshipCustomerMapping.RelationshipCustomerType;
        }
    }

}
