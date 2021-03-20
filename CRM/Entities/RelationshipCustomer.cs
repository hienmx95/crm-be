using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class RelationshipCustomerType : DataEntity,  IEquatable<RelationshipCustomerType>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        
        public bool Equals(RelationshipCustomerType other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class RelationshipCustomerTypeFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public List<RelationshipCustomerTypeFilter> OrFilter { get; set; }
        public RelationshipCustomerTypeOrder OrderBy {get; set;}
        public RelationshipCustomerTypeSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum RelationshipCustomerTypeOrder
    {
        Id = 0,
        Name = 1,
    }

    [Flags]
    public enum RelationshipCustomerTypeSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
    }
}
