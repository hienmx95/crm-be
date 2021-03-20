using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class UnitOfMeasureGrouping : DataEntity,  IEquatable<UnitOfMeasureGrouping>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long StatusId { get; set; }
        public bool Used { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public List<UnitOfMeasureGroupingContent> UnitOfMeasureGroupingContents { get; set; }
        public Guid RowId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool Equals(UnitOfMeasureGrouping other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class UnitOfMeasureGroupingFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Description { get; set; }
        public IdFilter UnitOfMeasureId { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<UnitOfMeasureGroupingFilter> OrFilter { get; set; }
        public UnitOfMeasureGroupingOrder OrderBy {get; set;}
        public UnitOfMeasureGroupingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum UnitOfMeasureGroupingOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
        Description = 3,
        UnitOfMeasure = 4,
        Status = 5,
        Used = 9,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum UnitOfMeasureGroupingSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
        Description = E._3,
        UnitOfMeasure = E._4,
        Status = E._5,
        Used = E._9,
        UnitOfMeasureGroupingContents = E._10,
    }
}
