using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAEscalation : DataEntity,  IEquatable<SLAEscalation>
    {
        public long Id { get; set; }
        public long? TicketIssueLevelId { get; set; }
        public bool? IsNotification { get; set; }
        public bool? IsMail { get; set; }
        public bool? IsSMS { get; set; }
        public long? Time { get; set; }
        public long? TimeUnitId { get; set; }
        public bool? IsAssignedToUser { get; set; }
        public bool? IsAssignedToGroup { get; set; }
        public long? SmsTemplateId { get; set; }
        public long? MailTemplateId { get; set; }
        public Guid RowId { get; set; }
        public MailTemplate MailTemplate { get; set; }
        public SmsTemplate SmsTemplate { get; set; }
        public TicketIssueLevel TicketIssueLevel { get; set; }
        public SLATimeUnit TimeUnit { get; set; }
        public List<SLAEscalationMail> SLAEscalationMails { get; set; }
        public List<SLAEscalationPhone> SLAEscalationPhones { get; set; }
        public List<SLAEscalationUser> SLAEscalationUsers { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAEscalation other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAEscalationFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter TicketIssueLevelId { get; set; }
        public LongFilter Time { get; set; }
        public IdFilter TimeUnitId { get; set; }
        public IdFilter SmsTemplateId { get; set; }
        public IdFilter MailTemplateId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAEscalationFilter> OrFilter { get; set; }
        public SLAEscalationOrder OrderBy {get; set;}
        public SLAEscalationSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAEscalationOrder
    {
        Id = 0,
        TicketIssueLevel = 1,
        IsNotification = 2,
        IsMail = 3,
        IsSMS = 4,
        Time = 5,
        TimeUnit = 6,
        IsAssignedToUser = 7,
        IsAssignedToGroup = 8,
        SmsTemplate = 9,
        MailTemplate = 10,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAEscalationSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        TicketIssueLevel = E._1,
        IsNotification = E._2,
        IsMail = E._3,
        IsSMS = E._4,
        Time = E._5,
        TimeUnit = E._6,
        IsAssignedToUser = E._7,
        IsAssignedToGroup = E._8,
        SmsTemplate = E._9,
        MailTemplate = E._10,
    }
}
