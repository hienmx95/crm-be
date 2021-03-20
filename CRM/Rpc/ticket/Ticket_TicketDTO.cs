using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket
{
    public class Ticket_TicketDTO : DataDTO
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
        public bool Used { get; set; }
        public string Notes { get; set; }
        public string SLADatetime { get; set; }
        public bool? IsAlerted { get; set; }
        public bool? IsAlertedFRT { get; set; }
        public bool? IsEscalated { get; set; }
        public bool? IsEscalatedFRT { get; set; }
        public long? TicketResolveTypeId { get; set; }
        public string ResolveContent { get; set; }
        public DateTime? closedAt { get; set; }
        public long? AppUserClosedId { get; set; }
        public DateTime? FirstResponseAt { get; set; }
        public DateTime? LastResponseAt { get; set; }
        public DateTime? LastHoldingAt { get; set; }
        public long? ReraisedTimes { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public long? AppUserResolvedId { get; set; }
        public bool? IsClose { get; set; }
        public bool? IsOpen { get; set; }
        public bool? IsWait { get; set; }
        public bool? IsWork { get; set; }
        public long? SLAPolicyId { get; set; }
        public long? HoldingTime { get; set; }
        public DateTime? FirstResponeTime { get; set; }
        public DateTime? ResolveTime { get; set; }
        public long? FirstRespTimeRemaining { get; set; }
        public long? ResolveTimeRemaining { get; set; }
        public long? SLAStatusId { get; set; }
        public long? SLAOverTime { get; set; }
        public long? ResolveMinute { get; set; }
        public Ticket_TicketResolveTypeDTO TicketResolveType { get; set; }
        public Ticket_CustomerDTO Customer { get; set; }
        public Ticket_CustomerTypeDTO CustomerType { get; set; }
        public Ticket_OrganizationDTO Department { get; set; }
        public Ticket_ProductDTO Product { get; set; }
        public Ticket_CallLogDTO RelatedCallLog { get; set; }
        public Ticket_TicketDTO RelatedTicket { get; set; }
        public Ticket_StatusDTO Status { get; set; }
        public Ticket_TicketIssueLevelDTO TicketIssueLevel { get; set; }
        public Ticket_TicketPriorityDTO TicketPriority { get; set; }
        public Ticket_TicketSourceDTO TicketSource { get; set; }
        public Ticket_TicketStatusDTO TicketStatus { get; set; }
        public Ticket_AppUserDTO User { get; set; }
        public Ticket_AppUserDTO Creator { get; set; }
        public Ticket_AppUserDTO AppUserClosed { get; set; }
        public Ticket_AppUserDTO AppUserResolved { get; set; }
        public Ticket_SLAPolicyDTO SLAPolicy { get; set; }
        public Ticket_SLAStatusDTO SLAStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Ticket_TicketDTO() {}
        public Ticket_TicketDTO(Ticket Ticket)
        {
            this.Id = Ticket.Id;
            this.Name = Ticket.Name;
            this.Phone = Ticket.Phone;
            this.CustomerId = Ticket.CustomerId;
            this.UserId = Ticket.UserId;
            this.CustomerTypeId = Ticket.CustomerTypeId;
            this.CreatorId = Ticket.CreatorId;
            this.ProductId = Ticket.ProductId;
            this.ReceiveDate = Ticket.ReceiveDate;
            this.ProcessDate = Ticket.ProcessDate;
            this.FinishDate = Ticket.FinishDate;
            this.Subject = Ticket.Subject;
            this.Content = Ticket.Content;
            this.TicketIssueLevelId = Ticket.TicketIssueLevelId;
            this.TicketPriorityId = Ticket.TicketPriorityId;
            this.TicketStatusId = Ticket.TicketStatusId;
            this.TicketSourceId = Ticket.TicketSourceId;
            this.TicketTypeId = Ticket.TicketTypeId;
            this.TicketNumber = Ticket.TicketNumber;
            this.DepartmentId = Ticket.DepartmentId;
            this.RelatedTicketId = Ticket.RelatedTicketId;
            this.SLA = Ticket.SLA;
            this.RelatedCallLogId = Ticket.RelatedCallLogId;
            this.ResponseMethodId = Ticket.ResponseMethodId;
            this.StatusId = Ticket.StatusId;
            this.IsAlerted = Ticket.IsAlerted;
            this.IsAlertedFRT = Ticket.IsAlertedFRT;
            this.IsEscalated = Ticket.IsEscalated;
            this.IsEscalatedFRT = Ticket.IsEscalatedFRT;
            this.Used = Ticket.Used;
            this.Notes = Ticket.Notes;
            this.TicketResolveTypeId = Ticket.TicketResolveTypeId;
            this.ResolveContent = Ticket.ResolveContent;
            this.closedAt = Ticket.closedAt;
            this.AppUserClosedId = Ticket.AppUserClosedId;
            this.FirstResponseAt = Ticket.FirstResponseAt;
            this.LastResponseAt = Ticket.LastResponseAt;
            this.LastHoldingAt = Ticket.LastHoldingAt;
            this.ReraisedTimes = Ticket.ReraisedTimes;
            this.ResolvedAt = Ticket.ResolvedAt;
            this.AppUserResolvedId = Ticket.AppUserResolvedId;
            this.IsClose = Ticket.IsClose;
            this.IsOpen = Ticket.IsOpen;
            this.IsWait = Ticket.IsWait;
            this.IsWork = Ticket.IsWork;
            this.SLAPolicyId = Ticket.SLAPolicyId;
            this.HoldingTime = Ticket.HoldingTime;
            this.FirstResponeTime = Ticket.FirstResponeTime;
            this.ResolveTime = Ticket.ResolveTime;
            this.FirstRespTimeRemaining = Ticket.FirstRespTimeRemaining;
            this.ResolveTimeRemaining = Ticket.ResolveTimeRemaining;
            this.SLAStatusId = Ticket.SLAStatusId;
            this.SLAOverTime = Ticket.SLAOverTime;
            this.ResolveMinute = Ticket.ResolveMinute;
            this.ResolveTime = Ticket.ResolveTime;
            this.AppUserClosed = Ticket.AppUserClosed == null ? null : new Ticket_AppUserDTO(Ticket.AppUserClosed);
            this.AppUserResolved = Ticket.AppUserResolved == null ? null : new Ticket_AppUserDTO(Ticket.AppUserResolved);
            this.SLAPolicy = Ticket.SLAPolicy == null ? null : new Ticket_SLAPolicyDTO(Ticket.SLAPolicy);
            this.TicketResolveType = Ticket.TicketResolveType == null ? null : new Ticket_TicketResolveTypeDTO(Ticket.TicketResolveType);
            this.Customer = Ticket.Customer == null ? null : new Ticket_CustomerDTO(Ticket.Customer);
            this.CustomerType = Ticket.CustomerType == null ? null : new Ticket_CustomerTypeDTO(Ticket.CustomerType);
            this.Department = Ticket.Department == null ? null : new Ticket_OrganizationDTO(Ticket.Department);
            this.Product = Ticket.Product == null ? null : new Ticket_ProductDTO(Ticket.Product);
            this.RelatedCallLog = Ticket.RelatedCallLog == null ? null : new Ticket_CallLogDTO(Ticket.RelatedCallLog);
            this.RelatedTicket = Ticket.RelatedTicket == null ? null : new Ticket_TicketDTO(Ticket.RelatedTicket);
            this.Status = Ticket.Status == null ? null : new Ticket_StatusDTO(Ticket.Status);
            this.TicketIssueLevel = Ticket.TicketIssueLevel == null ? null : new Ticket_TicketIssueLevelDTO(Ticket.TicketIssueLevel);
            this.TicketPriority = Ticket.TicketPriority == null ? null : new Ticket_TicketPriorityDTO(Ticket.TicketPriority);
            this.TicketSource = Ticket.TicketSource == null ? null : new Ticket_TicketSourceDTO(Ticket.TicketSource);
            this.TicketStatus = Ticket.TicketStatus == null ? null : new Ticket_TicketStatusDTO(Ticket.TicketStatus);
            this.User = Ticket.User == null ? null : new Ticket_AppUserDTO(Ticket.User);
            this.Creator = Ticket.Creator == null ? null : new Ticket_AppUserDTO(Ticket.Creator);
            this.SLAStatus = Ticket.SLAStatus == null ? null : new Ticket_SLAStatusDTO(Ticket.SLAStatus);
            this.CreatedAt = Ticket.CreatedAt;
            this.UpdatedAt = Ticket.UpdatedAt;
            this.Errors = Ticket.Errors;
            this.SLADatetime = Ticket.SLADatetime;
        }
    }

    public class Ticket_TicketFilterDTO : FilterDTO
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
        public IdFilter TicketPriorityId { get; set; }
        public IdFilter TicketStatusId { get; set; }
        public IdFilter TicketSourceId { get; set; }
        public IdFilter TicketTypeId { get; set; }
        public StringFilter TicketNumber { get; set; }
        public IdFilter DepartmentId { get; set; }
        public IdFilter RelatedTicketId { get; set; }
        public LongFilter SLA { get; set; }
        public IdFilter RelatedCallLogId { get; set; }
        public IdFilter ResponseMethodId { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public TicketOrder OrderBy { get; set; }
        public IdFilter TicketResolveTypeId { get; set; }
        public StringFilter ResolveContent { get; set; }
        public IdFilter CustomerAgentId { get; set; }
        public IdFilter CustomerRetailId { get; set; }
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
    }
}
