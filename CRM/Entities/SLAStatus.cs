using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAStatus : DataEntity,  IEquatable<SLAStatus>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
        
        public bool Equals(SLAStatus other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAStatusFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter ColorCode { get; set; }
        public List<SLAStatusFilter> OrFilter { get; set; }
        public SLAStatusOrder OrderBy {get; set;}
        public SLAStatusSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAStatusOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
        ColorCode = 3,
    }

    [Flags]
    public enum SLAStatusSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
        ColorCode = E._3,
    }
}
