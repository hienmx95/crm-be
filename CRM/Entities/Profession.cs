using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class Profession : DataEntity,  IEquatable<Profession>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long StatusId { get; set; }
        public Guid RowId { get; set; }
        public bool Used { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(Profession other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ProfessionFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter StatusId { get; set; }
        public GuidFilter RowId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<ProfessionFilter> OrFilter { get; set; }
        public ProfessionOrder OrderBy {get; set;}
        public ProfessionSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProfessionOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
        Status = 3,
        Row = 7,
        Used = 8,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum ProfessionSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
        Status = E._3,
        Row = E._7,
        Used = E._8,
    }
}
