using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerPointHistoryDTO : DataDTO
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long TotalPoint { get; set; }
        public long CurrentPoint { get; set; }
        public long ChangePoint { get; set; }
        public bool IsIncrease { get; set; }
        public string Description { get; set; }
        public bool ReduceTotal { get; set; }
        public Customer_CustomerDTO Customer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Customer_CustomerPointHistoryDTO() {}
        public Customer_CustomerPointHistoryDTO(CustomerPointHistory CustomerPointHistory)
        {
            this.Id = CustomerPointHistory.Id;
            this.CustomerId = CustomerPointHistory.CustomerId;
            this.TotalPoint = CustomerPointHistory.TotalPoint;
            this.CurrentPoint = CustomerPointHistory.CurrentPoint;
            this.ChangePoint = CustomerPointHistory.ChangePoint;
            this.IsIncrease = CustomerPointHistory.IsIncrease;
            this.Description = CustomerPointHistory.Description;
            this.ReduceTotal = CustomerPointHistory.ReduceTotal;
            this.Customer = CustomerPointHistory.Customer == null ? null : new Customer_CustomerDTO(CustomerPointHistory.Customer);
            this.CreatedAt = CustomerPointHistory.CreatedAt;
            this.UpdatedAt = CustomerPointHistory.UpdatedAt;
            this.Errors = CustomerPointHistory.Errors;
        }
    }

    public class Customer_CustomerPointHistoryFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter CustomerId { get; set; }
        public LongFilter TotalPoint { get; set; }
        public LongFilter CurrentPoint { get; set; }
        public LongFilter ChangePoint { get; set; }
        public StringFilter Description { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerPointHistoryOrder OrderBy { get; set; }
    }
}
