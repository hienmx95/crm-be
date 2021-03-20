using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class RatingStatus : DataEntity,  IEquatable<RatingStatus>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        
        public bool Equals(RatingStatus other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class RatingStatusFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public List<RatingStatusFilter> OrFilter { get; set; }
        public RatingStatusOrder OrderBy {get; set;}
        public RatingStatusSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum RatingStatusOrder
    {
        Id = 0,
        Name = 1,
        Code = 2,
    }

    [Flags]
    public enum RatingStatusSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Code = E._2,
    }
}
