using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class RepairTicketDAO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string DeviceSerial { get; set; }
        public long OrderCategoryId { get; set; }
        public long OrderId { get; set; }
        public DateTime? RepairDueDate { get; set; }
        public long? ItemId { get; set; }
        public bool? IsRejectRepair { get; set; }
        public string RejectReason { get; set; }
        public string DeviceState { get; set; }
        public long? RepairStatusId { get; set; }
        public string RepairAddess { get; set; }
        public string ReceiveUser { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public DateTime? RepairDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string RepairSolution { get; set; }
        public string Note { get; set; }
        public decimal? RepairCost { get; set; }
        public long? PaymentStatusId { get; set; }
        public long CustomerId { get; set; }
        public long CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AppUserDAO Creator { get; set; }
        public virtual CustomerDAO Customer { get; set; }
        public virtual ItemDAO Item { get; set; }
        public virtual OrderCategoryDAO OrderCategory { get; set; }
        public virtual PaymentStatusDAO PaymentStatus { get; set; }
        public virtual RepairStatusDAO RepairStatus { get; set; }
    }
}
