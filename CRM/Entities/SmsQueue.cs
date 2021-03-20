using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SmsQueue : DataEntity, IEquatable<SmsQueue>
    {
        public long Id { get; set; }
        public string Phone { get; set; }
        public string SmsCode { get; set; }
        public string SmsTitle { get; set; }
        public DateTime? SentDate { get; set; }
        public string SmsContent { get; set; }
        public long? SentByAppUserId { get; set; }
        public long? SmsQueueStatusId { get; set; } 
        public AppUser SentByAppUser { get; set; }
        public SmsQueueStatus SmsQueueStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
         

        public Mapping Mapping { get; set; }
        public List<string> ListSms { get; set; }
        public long? EntityReferenceId { get; set; }

        public long? CustomerLeadId { get; set; }
        public long? OpportunityId { get; set; }
        public long? CompanyId { get; set; }
        public long? ContactId { get; set; }
        public bool Equals(SmsQueue other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SmsQueueFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter SmsCode { get; set; }
        public StringFilter SmsTitle { get; set; }
        public DateFilter SentDate { get; set; }
        public StringFilter SmsContent { get; set; }
        public IdFilter SentByAppUserId { get; set; }
        public IdFilter SmsQueueStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SmsQueueFilter> OrFilter { get; set; }
        public SmsQueueOrder OrderBy { get; set; }
        public SmsQueueSelect Selects { get; set; }

    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SmsQueueOrder
    {
        Id = 0,
        Phone = 1,
        SmsCode = 2,
        SmsTitle = 3,
        SentDate = 4,
        SmsContent = 5,
        SentByAppUser = 6,
        SmsQueueStatus = 7,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SmsQueueSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Phone = E._1,
        SmsCode = E._2,
        SmsTitle = E._3,
        SentDate = E._4,
        SmsContent = E._5,
        SentByAppUser = E._6,
        SmsQueueStatus = E._7,
    }
}
