using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerCCEmailHistoryDTO : DataDTO
    {
        public long Id { get; set; }
        public long CustomerEmailHistoryId { get; set; }
        public string CCEmail { get; set; }
        public Customer_CustomerEmailHistoryDTO CustomerEmailHistory { get; set; }
        public Customer_CustomerCCEmailHistoryDTO() {}
        public Customer_CustomerCCEmailHistoryDTO(CustomerCCEmailHistory CustomerCCEmailHistory)
        {
            this.Id = CustomerCCEmailHistory.Id;
            this.CustomerEmailHistoryId = CustomerCCEmailHistory.CustomerEmailHistoryId;
            this.CCEmail = CustomerCCEmailHistory.CCEmail;
            this.CustomerEmailHistory = CustomerCCEmailHistory.CustomerEmailHistory == null ? null : new Customer_CustomerEmailHistoryDTO(CustomerCCEmailHistory.CustomerEmailHistory);
            this.Errors = CustomerCCEmailHistory.Errors;
        }
    }

    public class Company_CompanyEmailFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter CustomerEmailHistoryId { get; set; }
        public StringFilter CCEmail { get; set; }
        public CompanyEmailOrder OrderBy { get; set; }
    }
}