using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace CRM.Entities
{
    public class Pattern : DataEntity, IEquatable<Pattern>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string Position { get; set; }

        public bool Equals(Pattern other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class PatternFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }

        public StringFilter Position { get; set; }
        public List<PatternFilter> OrFilter { get; set; }
        public PatternOrder OrderBy { get; set; }
        public PatternSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PatternOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
        Position = 3,
        Status = 4,
    }

    [Flags]
    public enum PatternSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
        Position = E._3,
        Status = E._4,
    }
}
