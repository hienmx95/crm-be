using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class Ticket : DataEntity,  IEquatable<Ticket>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public long CustomerId { get; set; }
        public long UserId { get; set; }
        public long CustomerTypeId { get; set; }
        public long CreatorId { get; set; }
        public long? ProductId { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime? ProcessDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public long TicketIssueLevelId { get; set; }
        public long TicketPriorityId { get; set; }
        public long TicketStatusId { get; set; }
        public long TicketSourceId { get; set; }
        public long TicketTypeId { get; set; }
        public string TicketNumber { get; set; }
        public long? DepartmentId { get; set; }
        public long? RelatedTicketId { get; set; }
        public long SLA { get; set; }
        public long? RelatedCallLogId { get; set; }
        public long? ResponseMethodId { get; set; }
        public long StatusId { get; set; }
        public long EntityReferenceId { get; set; }
        public bool Used { get; set; }
        public string Notes { get; set; }
        public string SLADatetime { get; set; }
        public bool? IsAlerted { get; set; }
        public bool? IsAlertedFRT { get; set; }
        public bool? IsEscalated { get; set; }
        public bool? IsEscalatedFRT { get; set; }
        public DateTime? closedAt { get; set; }
        public long? AppUserClosedId { get; set; }
        public DateTime? FirstResponseAt { get; set; }
        public DateTime? LastResponseAt { get; set; }
        public DateTime? LastHoldingAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public long? AppUserResolvedId { get; set; }
        public long? HoldingTime { get; set; }
        public long? ReraisedTimes { get; set; }
        public long? ResolveMinute { get; set; }
        public bool? IsClose { get; set; }
        public bool? IsOpen { get; set; }
        public bool? IsWait { get; set; }
        public bool? IsWork { get; set; }
        public long? SLAPolicyId { get; set; }
        public DateTime? FirstResponeTime { get; set; }
        public DateTime? ResolveTime { get; set; }
        public long? FirstRespTimeRemaining { get; set; }
        public long? ResolveTimeRemaining { get; set; }
        public long? SLAStatusId { get; set; }
        public long? SLAOverTime { get; set; }
        public AppUser AppUserClosed { get; set; }
        public AppUser AppUserResolved { get; set; }
        public SLAPolicy SLAPolicy { get; set; }
        public Customer Customer { get; set; }
        public CustomerType CustomerType { get; set; }
        public EntityReference EntityReference { get; set; }
        public Organization Department { get; set; }
        public Product Product { get; set; }
        public CallLog RelatedCallLog { get; set; }
        public Ticket RelatedTicket { get; set; }
        public Status Status { get; set; }
        public TicketIssueLevel TicketIssueLevel { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public TicketSource TicketSource { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public AppUser User { get; set; }
        public AppUser Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long? TicketResolveTypeId { get; set; }
        public string ResolveContent { get; set; }
        public List<TicketOfUser> TicketOfUsers { get; set; }
        public TicketResolveType TicketResolveType { get; set; }
        public SLAStatus SLAStatus { get; set; }
        public bool Equals(Ticket other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class TicketFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Phone { get; set; }
        public IdFilter CustomerId { get; set; }
        public IdFilter UserId { get; set; }
        public IdFilter CustomerTypeId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter ProductId { get; set; }
        public DateFilter ReceiveDate { get; set; }
        public DateFilter ProcessDate { get; set; }
        public DateFilter FinishDate { get; set; }
        public StringFilter Subject { get; set; }
        public StringFilter Content { get; set; }
        public IdFilter TicketIssueLevelId { get; set; }
        public IdFilter TicketResolveTypeId { get; set; }
        public StringFilter ResolveContent { get; set; }
        public IdFilter TicketPriorityId { get; set; }
        public IdFilter TicketStatusId { get; set; }
        public IdFilter TicketGroupId { get; set; }
        public IdFilter TicketSourceId { get; set; }
        public IdFilter TicketTypeId { get; set; }
        public StringFilter TicketNumber { get; set; }
        public IdFilter DepartmentId { get; set; }
        public IdFilter RelatedTicketId { get; set; }
        public LongFilter SLA { get; set; }
        public IdFilter RelatedCallLogId { get; set; }
        public IdFilter ResponseMethodId { get; set; }
        public IdFilter StatusId { get; set; }
        public IdFilter SLAStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public DateFilter closedAt { get; set; }
        public IdFilter AppUserClosedId { get; set; }
        public DateFilter FirstResponseAt { get; set; }
        public DateFilter LastResponseAt { get; set; }
        public DateFilter LastHoldingAt { get; set; }
        public LongFilter ReraisedTimes { get; set; }
        public DateFilter ResolvedAt { get; set; }
        public IdFilter AppUserResolvedId { get; set; }
        public IdFilter SLAPolicyId { get; set; }
        public LongFilter HoldingTime { get; set; }
        public DateFilter ResolveTime { get; set; }
        public List<TicketFilter> OrFilter { get; set; }
        public TicketOrder OrderBy {get; set;}
        public TicketSelect Selects {get; set;}

        public IdFilter CustomerRetailId { get; set; } 
        public IdFilter CustomerAgentId { get; set; } 
        public IdFilter AppUserId { get; set; } 
        public IdFilter OrganizationId { get; set; } 
        public IdFilter CurrentUserId { get; set; } 
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketOrder
    {
        Id = 0,
        Name = 1,
        Phone = 2,
        Customer = 3,
        User = 8,
        Creator = 9,
        Product = 10,
        ReceiveDate = 11,
        ProcessDate = 12,
        FinishDate = 13,
        Subject = 14,
        Content = 15,
        TicketIssueLevel = 16,
        TicketPriority = 17,
        TicketStatus = 18,
        TicketSource = 19,
        TicketNumber = 20,
        Department = 21,
        RelatedTicket = 22,
        SLA = 23,
        RelatedCallLog = 24,
        ResponseMethod = 25,
        Status = 26,
        CustomerType = 27,
        Used = 28,
        IsAlerted = 29,
        IsAlertedFRT = 30,
        IsEscalated = 31,
        IsEscalatedFRT = 32,
        TicketResolveType = 33,
        ResolveContent = 34,
        closedAt = 36,
        AppUserClosed = 37,
        FirstResponseAt = 38,
        LastResponseAt = 39,
        LastHoldingAt = 40,
        ReraisedTimes = 41,
        ResolvedAt = 42,
        AppUserResolved = 43,
        IsClose = 44,
        IsOpen = 45,
        IsWait = 46,
        IsWork = 47,
        SLAPolicy = 48,
        HoldingTime = 49,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum TicketSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Phone = E._2,
        Customer = E._3,
        User = E._8,
        Creator = E._9,
        Product = E._10,
        ReceiveDate = E._11,
        ProcessDate = E._12,
        FinishDate = E._13,
        Subject = E._14,
        Content = E._15,
        TicketIssueLevel = E._16,
        TicketPriority = E._17,
        TicketStatus = E._18,
        TicketSource = E._19,
        TicketNumber = E._20,
        Department = E._21,
        RelatedTicket = E._22,
        SLA = E._23,
        RelatedCallLog = E._24,
        ResponseMethod = E._25,
        Status = E._26,
        CustomerType = E._27,
        Used = E._28,
        IsAlerted = E._29,
        IsAlertedFRT = E._30,
        IsEscalated = E._31,
        IsEscalatedFRT = E._32,
        TicketResolveType = E._33,
        ResolveContent = E._34,
        closedAt = E._36,
        AppUserClosed = E._37,
        FirstResponseAt = E._38,
        LastResponseAt = E._39,
        LastHoldingAt = E._40,
        ReraisedTimes = E._41,
        ResolvedAt = E._42,
        AppUserResolved = E._43,
        IsClose = E._44,
        IsOpen = E._45,
        IsWait = E._46,
        IsWork = E._47,
        SLAPolicy = E._48,
        HoldingTime = E._49,
        SLAStatusId = E._52,
        SLAStatus = E._53,
        ResolveTime = E._53,

    }
}
