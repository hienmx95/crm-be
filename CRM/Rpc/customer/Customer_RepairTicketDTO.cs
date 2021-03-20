using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_RepairTicketDTO : DataDTO
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
        public Customer_AppUserDTO Creator { get; set; }
        public Customer_CustomerDTO Customer { get; set; }
        public Customer_ItemDTO Item { get; set; }
        public Customer_OrderCategoryDTO OrderCategory { get; set; }
        public Customer_PaymentStatusDTO PaymentStatus { get; set; }
        public Customer_RepairStatusDTO RepairStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Customer_RepairTicketDTO() {}
        public Customer_RepairTicketDTO(RepairTicket RepairTicket)
        {
            this.Id = RepairTicket.Id;
            this.Code = RepairTicket.Code;
            this.DeviceSerial = RepairTicket.DeviceSerial;
            this.OrderCategoryId = RepairTicket.OrderCategoryId;
            this.OrderId = RepairTicket.OrderId;
            this.RepairDueDate = RepairTicket.RepairDueDate;
            this.ItemId = RepairTicket.ItemId;
            this.IsRejectRepair = RepairTicket.IsRejectRepair;
            this.RejectReason = RepairTicket.RejectReason;
            this.DeviceState = RepairTicket.DeviceState;
            this.RepairStatusId = RepairTicket.RepairStatusId;
            this.RepairAddess = RepairTicket.RepairAddess;
            this.ReceiveUser = RepairTicket.ReceiveUser;
            this.ReceiveDate = RepairTicket.ReceiveDate;
            this.RepairDate = RepairTicket.RepairDate;
            this.ReturnDate = RepairTicket.ReturnDate;
            this.RepairSolution = RepairTicket.RepairSolution;
            this.Note = RepairTicket.Note;
            this.RepairCost = RepairTicket.RepairCost;
            this.PaymentStatusId = RepairTicket.PaymentStatusId;
            this.CustomerId = RepairTicket.CustomerId;
            this.CreatorId = RepairTicket.CreatorId;
            this.Creator = RepairTicket.Creator == null ? null : new Customer_AppUserDTO(RepairTicket.Creator);
            this.Customer = RepairTicket.Customer == null ? null : new Customer_CustomerDTO(RepairTicket.Customer);
            this.Item = RepairTicket.Item == null ? null : new Customer_ItemDTO(RepairTicket.Item);
            this.OrderCategory = RepairTicket.OrderCategory == null ? null : new Customer_OrderCategoryDTO(RepairTicket.OrderCategory);
            this.PaymentStatus = RepairTicket.PaymentStatus == null ? null : new Customer_PaymentStatusDTO(RepairTicket.PaymentStatus);
            this.RepairStatus = RepairTicket.RepairStatus == null ? null : new Customer_RepairStatusDTO(RepairTicket.RepairStatus);
            this.CreatedAt = RepairTicket.CreatedAt;
            this.UpdatedAt = RepairTicket.UpdatedAt;
            this.Errors = RepairTicket.Errors;
        }
    }

    public class Customer_RepairTicketFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter DeviceSerial { get; set; }
        public IdFilter OrderCategoryId { get; set; }
        public IdFilter OrderId { get; set; }
        public DateFilter RepairDueDate { get; set; }
        public IdFilter ItemId { get; set; }
        public StringFilter RejectReason { get; set; }
        public StringFilter DeviceState { get; set; }
        public IdFilter RepairStatusId { get; set; }
        public StringFilter RepairAddess { get; set; }
        public StringFilter ReceiveUser { get; set; }
        public DateFilter ReceiveDate { get; set; }
        public DateFilter RepairDate { get; set; }
        public DateFilter ReturnDate { get; set; }
        public StringFilter RepairSolution { get; set; }
        public StringFilter Note { get; set; }
        public DecimalFilter RepairCost { get; set; }
        public IdFilter PaymentStatusId { get; set; }
        public IdFilter CustomerId { get; set; }
        public IdFilter CreatorId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public RepairTicketOrder OrderBy { get; set; }
    }
}
