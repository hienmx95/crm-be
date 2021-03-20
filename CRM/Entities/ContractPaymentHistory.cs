using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ContractPaymentHistory : DataEntity,  IEquatable<ContractPaymentHistory>
    {
        public long Id { get; set; }
        public long ContractId { get; set; }
        public string PaymentMilestone { get; set; }
        public decimal? PaymentPercentage { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string Description { get; set; }
        public bool? IsPaid { get; set; }
        public Contract Contract { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(ContractPaymentHistory other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ContractPaymentHistoryFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter ContractId { get; set; }
        public StringFilter PaymentMilestone { get; set; }
        public DecimalFilter PaymentPercentage { get; set; }
        public DecimalFilter PaymentAmount { get; set; }
        public StringFilter Description { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<ContractPaymentHistoryFilter> OrFilter { get; set; }
        public ContractPaymentHistoryOrder OrderBy {get; set;}
        public ContractPaymentHistorySelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContractPaymentHistoryOrder
    {
        Id = 0,
        Contract = 1,
        PaymentMilestone = 2,
        PaymentPercentage = 3,
        PaymentAmount = 4,
        Description = 5,
        IsPaid = 6,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum ContractPaymentHistorySelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Contract = E._1,
        PaymentMilestone = E._2,
        PaymentPercentage = E._3,
        PaymentAmount = E._4,
        Description = E._5,
        IsPaid = E._6,
    }
}
