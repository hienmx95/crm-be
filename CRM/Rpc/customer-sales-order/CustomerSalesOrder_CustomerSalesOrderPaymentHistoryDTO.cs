using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_sales_order
{
    public class CustomerSalesOrder_CustomerSalesOrderPaymentHistoryDTO : DataDTO
    {
        public long Id { get; set; }
        public long CustomerSalesOrderId { get; set; }
        public string PaymentMilestone { get; set; }
        public decimal? PaymentPercentage { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string Description { get; set; }
        public bool? IsPaid { get; set; }

        public CustomerSalesOrder_CustomerSalesOrderPaymentHistoryDTO() {}
        public CustomerSalesOrder_CustomerSalesOrderPaymentHistoryDTO(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory)
        {
            this.Id = CustomerSalesOrderPaymentHistory.Id;
            this.CustomerSalesOrderId = CustomerSalesOrderPaymentHistory.CustomerSalesOrderId;
            this.PaymentMilestone = CustomerSalesOrderPaymentHistory.PaymentMilestone;
            this.PaymentPercentage = CustomerSalesOrderPaymentHistory.PaymentPercentage;
            this.PaymentAmount = CustomerSalesOrderPaymentHistory.PaymentAmount;
            this.Description = CustomerSalesOrderPaymentHistory.Description;
            this.IsPaid = CustomerSalesOrderPaymentHistory.IsPaid;
            this.Errors = CustomerSalesOrderPaymentHistory.Errors;
        }
    }

    public class CustomerSalesOrder_CustomerSalesOrderPaymentHistoryFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public IdFilter CustomerSalesOrderId { get; set; }
        
        public StringFilter PaymentMilestone { get; set; }
        
        public DecimalFilter PaymentPercentage { get; set; }
        
        public DecimalFilter PaymentAmount { get; set; }
        
        public StringFilter Description { get; set; }
        
        public CustomerSalesOrderPaymentHistoryOrder OrderBy { get; set; }
    }
}