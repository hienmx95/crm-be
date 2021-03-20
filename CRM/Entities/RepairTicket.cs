using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class RepairTicket : DataEntity,  IEquatable<RepairTicket>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string DeviceSerial { get; set; }
        public long OrderId { get; set; }
        public long OrderCategoryId { get; set; }
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
        public AppUser Creator { get; set; }
        public Customer Customer { get; set; }
        public Item Item { get; set; }
        public OrderCategory OrderCategory { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public RepairStatus RepairStatus { get; set; }
        public CustomerSalesOrder CustomerSalesOrder { get; set; }
        public DirectSalesOrder DirectSalesOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(RepairTicket other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class RepairTicketFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter DeviceSerial { get; set; }
        public IdFilter OrderId { get; set; }
        public IdFilter OrderCategoryId { get; set; }
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
        public List<RepairTicketFilter> OrFilter { get; set; }
        public RepairTicketOrder OrderBy {get; set;}
        public RepairTicketSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepairTicketOrder
    {
        Id = 0,
        Code = 1,
        Order = 2,
        OrderCategory = 3,
        RepairDueDate = 4,
        Item = 5,
        IsRejectRepair = 6,
        RejectReason = 7,
        DeviceState = 8,
        RepairStatus = 9,
        RepairAddess = 10,
        ReceiveUser = 11,
        ReceiveDate = 12,
        RepairDate = 13,
        ReturnDate = 14,
        RepairSolution = 15,
        Note = 16,
        RepairCost = 17,
        PaymentStatus = 18,
        Customer = 19,
        Creator = 20,
        DeviceSerial = 21,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum RepairTicketSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Order = E._2,
        OrderCategory = E._3,
        RepairDueDate = E._4,
        Item = E._5,
        IsRejectRepair = E._6,
        RejectReason = E._7,
        DeviceState = E._8,
        RepairStatus = E._9,
        RepairAddess = E._10,
        ReceiveUser = E._11,
        ReceiveDate = E._12,
        RepairDate = E._13,
        ReturnDate = E._14,
        RepairSolution = E._15,
        Note = E._16,
        RepairCost = E._17,
        PaymentStatus = E._18,
        Customer = E._19,
        Creator = E._20,
        DeviceSerial = E._21,
    }
}
