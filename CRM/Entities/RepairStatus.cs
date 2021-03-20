using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class RepairStatus : DataEntity,  IEquatable<RepairStatus>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        
        public bool Equals(RepairStatus other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class RepairStatusFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public List<RepairStatusFilter> OrFilter { get; set; }
        public RepairStatusOrder OrderBy {get; set;}
        public RepairStatusSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepairStatusOrder
    {
        Id = 0,
        Name = 1,
        Code = 2,
    }

    [Flags]
    public enum RepairStatusSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Code = E._2,
    }
}
