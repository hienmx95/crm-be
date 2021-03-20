using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class RelationshipCustomerTypeType : DataEntity,  IEquatable<RelationshipCustomerTypeType>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        
        public bool Equals(RelationshipCustomerTypeType other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class RelationshipCustomerTypeTypeFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<RelationshipCustomerTypeTypeFilter> OrFilter { get; set; }
        public RelationshipCustomerTypeTypeOrder OrderBy {get; set;}
        public RelationshipCustomerTypeTypeSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum RelationshipCustomerTypeTypeOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
    }

    [Flags]
    public enum RelationshipCustomerTypeTypeSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
    }
}
