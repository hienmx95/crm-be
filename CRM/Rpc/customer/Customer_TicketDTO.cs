using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_TicketDTO : DataDTO
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
        public string TicketNumber { get; set; }
        public long? DepartmentId { get; set; }
        public long? RelatedTicketId { get; set; }
        public long SLA { get; set; }
        public long? RelatedCallLogId { get; set; }
        public long? ResponseMethodId { get; set; }
        public long? EntityReferenceId { get; set; }
        public long? TicketResolveTypeId { get; set; }
        public string ResolveContent { get; set; }
        public long StatusId { get; set; }
        public bool? IsAlerted { get; set; }
        public bool? IsAlertedFRT { get; set; }
        public bool? IsEscalated { get; set; }
        public bool? IsEscalatedFRT { get; set; }
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
        public long? ResolveMinute { get; set; }
        public long? SLAOverTime { get; set; }
        public bool Used { get; set; }
        public Customer_AppUserDTO AppUserClosed { get; set; }
        public Customer_AppUserDTO AppUserResolved { get; set; }
        public Customer_AppUserDTO Creator { get; set; }
        public Customer_CustomerDTO Customer { get; set; }
        public Customer_CustomerTypeDTO CustomerType { get; set; }
        public Customer_OrganizationDTO Department { get; set; }
        public Customer_EntityReferenceDTO EntityReference { get; set; }
        public Customer_ProductDTO Product { get; set; }
        public Customer_CallLogDTO RelatedCallLog { get; set; }
        public Customer_TicketDTO RelatedTicket { get; set; }
        public Customer_SLAPolicyDTO SLAPolicy { get; set; }
        public Customer_SLAStatusDTO SLAStatus { get; set; }
        public Customer_StatusDTO Status { get; set; }
        public Customer_TicketGroupDTO TicketGroup { get; set; }
        public Customer_TicketIssueLevelDTO TicketIssueLevel { get; set; }
        public Customer_TicketPriorityDTO TicketPriority { get; set; }
        public Customer_TicketResolveTypeDTO TicketResolveType { get; set; }
        public Customer_TicketSourceDTO TicketSource { get; set; }
        public Customer_TicketStatusDTO TicketStatus { get; set; }
        public Customer_TicketTypeDTO TicketType { get; set; }
        public Customer_AppUserDTO User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Customer_TicketDTO() {}
        public Customer_TicketDTO(Ticket Ticket)
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
            this.TicketNumber = Ticket.TicketNumber;
            this.DepartmentId = Ticket.DepartmentId;
            this.RelatedTicketId = Ticket.RelatedTicketId;
            this.SLA = Ticket.SLA;
            this.RelatedCallLogId = Ticket.RelatedCallLogId;
            this.ResponseMethodId = Ticket.ResponseMethodId;
            this.EntityReferenceId = Ticket.EntityReferenceId;
            this.TicketResolveTypeId = Ticket.TicketResolveTypeId;
            this.ResolveContent = Ticket.ResolveContent;
            this.StatusId = Ticket.StatusId;
            this.IsAlerted = Ticket.IsAlerted;
            this.IsAlertedFRT = Ticket.IsAlertedFRT;
            this.IsEscalated = Ticket.IsEscalated;
            this.IsEscalatedFRT = Ticket.IsEscalatedFRT;
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
            this.ResolveMinute = Ticket.ResolveMinute;
            this.SLAOverTime = Ticket.SLAOverTime;
            this.Used = Ticket.Used;
            this.AppUserClosed = Ticket.AppUserClosed == null ? null : new Customer_AppUserDTO(Ticket.AppUserClosed);
            this.AppUserResolved = Ticket.AppUserResolved == null ? null : new Customer_AppUserDTO(Ticket.AppUserResolved);
            this.Creator = Ticket.Creator == null ? null : new Customer_AppUserDTO(Ticket.Creator);
            this.Customer = Ticket.Customer == null ? null : new Customer_CustomerDTO(Ticket.Customer);
            this.CustomerType = Ticket.CustomerType == null ? null : new Customer_CustomerTypeDTO(Ticket.CustomerType);
            this.Department = Ticket.Department == null ? null : new Customer_OrganizationDTO(Ticket.Department);
            this.EntityReference = Ticket.EntityReference == null ? null : new Customer_EntityReferenceDTO(Ticket.EntityReference);
            this.Product = Ticket.Product == null ? null : new Customer_ProductDTO(Ticket.Product);
            this.RelatedCallLog = Ticket.RelatedCallLog == null ? null : new Customer_CallLogDTO(Ticket.RelatedCallLog);
            this.RelatedTicket = Ticket.RelatedTicket == null ? null : new Customer_TicketDTO(Ticket.RelatedTicket);
            this.SLAPolicy = Ticket.SLAPolicy == null ? null : new Customer_SLAPolicyDTO(Ticket.SLAPolicy);
            this.SLAStatus = Ticket.SLAStatus == null ? null : new Customer_SLAStatusDTO(Ticket.SLAStatus);
            this.Status = Ticket.Status == null ? null : new Customer_StatusDTO(Ticket.Status);
            this.TicketIssueLevel = Ticket.TicketIssueLevel == null ? null : new Customer_TicketIssueLevelDTO(Ticket.TicketIssueLevel);
            this.TicketPriority = Ticket.TicketPriority == null ? null : new Customer_TicketPriorityDTO(Ticket.TicketPriority);
            this.TicketResolveType = Ticket.TicketResolveType == null ? null : new Customer_TicketResolveTypeDTO(Ticket.TicketResolveType);
            this.TicketSource = Ticket.TicketSource == null ? null : new Customer_TicketSourceDTO(Ticket.TicketSource);
            this.TicketStatus = Ticket.TicketStatus == null ? null : new Customer_TicketStatusDTO(Ticket.TicketStatus);
            this.User = Ticket.User == null ? null : new Customer_AppUserDTO(Ticket.User);
            this.CreatedAt = Ticket.CreatedAt;
            this.UpdatedAt = Ticket.UpdatedAt;
            this.Errors = Ticket.Errors;
        }
    }

    public class Customer_TicketFilterDTO : FilterDTO
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
        public StringFilter TicketNumber { get; set; }
        public IdFilter DepartmentId { get; set; }
        public IdFilter RelatedTicketId { get; set; }
        public LongFilter SLA { get; set; }
        public IdFilter RelatedCallLogId { get; set; }
        public IdFilter ResponseMethodId { get; set; }
        public IdFilter EntityReferenceId { get; set; }
        public IdFilter TicketResolveTypeId { get; set; }
        public StringFilter ResolveContent { get; set; }
        public IdFilter StatusId { get; set; }
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
        public DateFilter FirstResponeTime { get; set; }
        public DateFilter ResolveTime { get; set; }
        public LongFilter FirstRespTimeRemaining { get; set; }
        public LongFilter ResolveTimeRemaining { get; set; }
        public IdFilter SLAStatusId { get; set; }
        public LongFilter ResolveMinute { get; set; }
        public LongFilter SLAOverTime { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public TicketOrder OrderBy { get; set; }
    }
}
