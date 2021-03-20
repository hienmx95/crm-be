using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CallLog : DataEntity,  IEquatable<CallLog>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Phone { get; set; }
        public DateTime CallTime { get; set; }
        public long EntityReferenceId { get; set; }
        public long EntityId { get; set; }
        public long CallTypeId { get; set; }
        public long? CallCategoryId { get; set; }
        public long? CallEmotionId { get; set; }
        public long? CallStatusId { get; set; }
        public long AppUserId { get; set; }
        public long CreatorId { get; set; }
        public AppUser AppUser { get; set; }
        public CallCategory CallCategory { get; set; }
        public CallStatus CallStatus { get; set; }
        public CallEmotion CallEmotion { get; set; }
        public CallType CallType { get; set; }
        public AppUser Creator { get; set; }
        public EntityReference EntityReference { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(CallLog other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CallLogFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Content { get; set; }
        public StringFilter Phone { get; set; }
        public DateFilter CallTime { get; set; }
        public IdFilter EntityReferenceId { get; set; }
        public IdFilter EntityId { get; set; }
        public IdFilter CallTypeId { get; set; }
        public IdFilter CallCategoryId { get; set; }
        public IdFilter CallStatusId { get; set; }
        public IdFilter CallEmotionId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter CreatorId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<CallLogFilter> OrFilter { get; set; }
        public CallLogOrder OrderBy {get; set;}
        public CallLogSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CallLogOrder
    {
        Id = 0,
        Title = 1,
        Content = 2,
        Phone = 3,
        CallTime = 4,
        EntityReference = 5,
        Entity = 6,
        CallType = 7,
        CallEmotion = 8,
        AppUser = 9,
        Creator = 10,
        CallCategory = 11,
        CallStatus = 12,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CallLogSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Title = E._1,
        Content = E._2,
        Phone = E._3,
        CallTime = E._4,
        EntityReference = E._5,
        Entity = E._6,
        CallType = E._7,
        CallEmotion = E._8,
        AppUser = E._9,
        Creator = E._10,
        CallCategory = E._11,
        CallStatus = E._12,
    }
}
