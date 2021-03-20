using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class TicketDAO
    {
        public TicketDAO()
        {
            InverseRelatedTicket = new HashSet<TicketDAO>();
            TicketOfDepartments = new HashSet<TicketOfDepartmentDAO>();
            TicketOfUsers = new HashSet<TicketOfUserDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long CustomerTypeId { get; set; }
        public long CustomerId { get; set; }
        public long UserId { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }
        public string Phone { get; set; }

        public virtual AppUserDAO AppUserClosed { get; set; }
        public virtual AppUserDAO AppUserResolved { get; set; }
        public virtual AppUserDAO Creator { get; set; }
        public virtual CustomerDAO Customer { get; set; }
        public virtual CustomerTypeDAO CustomerType { get; set; }
        public virtual OrganizationDAO Department { get; set; }
        public virtual EntityReferenceDAO EntityReference { get; set; }
        public virtual ProductDAO Product { get; set; }
        public virtual CallLogDAO RelatedCallLog { get; set; }
        public virtual TicketDAO RelatedTicket { get; set; }
        public virtual SLAPolicyDAO SLAPolicy { get; set; }
        public virtual SLAStatusDAO SLAStatus { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual TicketIssueLevelDAO TicketIssueLevel { get; set; }
        public virtual TicketPriorityDAO TicketPriority { get; set; }
        public virtual TicketResolveTypeDAO TicketResolveType { get; set; }
        public virtual TicketSourceDAO TicketSource { get; set; }
        public virtual TicketStatusDAO TicketStatus { get; set; }
        public virtual AppUserDAO User { get; set; }
        public virtual ICollection<TicketDAO> InverseRelatedTicket { get; set; }
        public virtual ICollection<TicketOfDepartmentDAO> TicketOfDepartments { get; set; }
        public virtual ICollection<TicketOfUserDAO> TicketOfUsers { get; set; }
    }
}
