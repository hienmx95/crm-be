using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class StoreRelationshipCustomerMapping : DataEntity,  IEquatable<StoreRelationshipCustomerMapping>
    {
        public long RelationshipCustomerTypeId { get; set; }
        public long StoreId { get; set; }
        public RelationshipCustomerType RelationshipCustomerType { get; set; }
        public Store Store { get; set; }
        
        public bool Equals(StoreRelationshipCustomerMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class StoreRelationshipCustomerMappingFilter : FilterEntity
    {
        public IdFilter RelationshipCustomerTypeId { get; set; }
        public IdFilter StoreId { get; set; }
        public List<StoreRelationshipCustomerMappingFilter> OrFilter { get; set; }
        public StoreRelationshipCustomerMappingOrder OrderBy {get; set;}
        public StoreRelationshipCustomerMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StoreRelationshipCustomerMappingOrder
    {
        RelationshipCustomerType = 0,
        Store = 1,
    }

    [Flags]
    public enum StoreRelationshipCustomerMappingSelect:long
    {
        ALL = E.ALL,
        RelationshipCustomerType = E._0,
        Store = E._1,
    }
}
