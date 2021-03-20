using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CallEmotion : DataEntity,  IEquatable<CallEmotion>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long StatusId { get; set; }
        public string Description { get; set; }
        public bool Used { get; set; }
        public Guid RowId { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(CallEmotion other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CallEmotionFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter StatusId { get; set; }
        public StringFilter Description { get; set; }
        public GuidFilter RowId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<CallEmotionFilter> OrFilter { get; set; }
        public CallEmotionOrder OrderBy {get; set;}
        public CallEmotionSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CallEmotionOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
        Status = 3,
        Description = 4,
        Used = 8,
        Row = 9,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CallEmotionSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
        Status = E._3,
        Description = E._4,
        Used = E._8,
        Row = E._9,
    }
}
