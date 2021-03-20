using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_ContractPaymentHistoryDTO : DataDTO
    {
        public long Id { get; set; }
        public long ContractId { get; set; }
        public string PaymentMilestone { get; set; }
        public decimal? PaymentPercentage { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string Description { get; set; }
        public bool? IsPaid { get; set; }

        public Contract_ContractPaymentHistoryDTO() {}
        public Contract_ContractPaymentHistoryDTO(ContractPaymentHistory ContractPaymentHistory)
        {
            this.Id = ContractPaymentHistory.Id;
            this.ContractId = ContractPaymentHistory.ContractId;
            this.PaymentMilestone = ContractPaymentHistory.PaymentMilestone;
            this.PaymentPercentage = ContractPaymentHistory.PaymentPercentage;
            this.PaymentAmount = ContractPaymentHistory.PaymentAmount;
            this.Description = ContractPaymentHistory.Description;
            this.IsPaid = ContractPaymentHistory.IsPaid;
            this.Errors = ContractPaymentHistory.Errors;
        }
    }

    public class Contract_ContractPaymentHistoryFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public IdFilter ContractId { get; set; }
        
        public StringFilter PaymentMilestone { get; set; }
        
        public DecimalFilter PaymentPercentage { get; set; }
        
        public DecimalFilter PaymentAmount { get; set; }
        
        public StringFilter Description { get; set; }
        
        public ContractPaymentHistoryOrder OrderBy { get; set; }
    }
}