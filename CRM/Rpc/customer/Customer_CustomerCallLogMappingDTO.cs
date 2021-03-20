using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerCallLogMappingDTO : DataDTO
    {
        public long CustomerId { get; set; }
        public long CallLogId { get; set; }
        public Customer_CallLogDTO CallLog { get; set; }   

        public Customer_CustomerCallLogMappingDTO() {}
        public Customer_CustomerCallLogMappingDTO(CustomerCallLogMapping CustomerCallLogMapping)
        {
            this.CustomerId = CustomerCallLogMapping.CustomerId;
            this.CallLogId = CustomerCallLogMapping.CallLogId;
            this.CallLog = CustomerCallLogMapping.CallLog == null ? null : new Customer_CallLogDTO(CustomerCallLogMapping.CallLog);
            this.Errors = CustomerCallLogMapping.Errors;
        }
    }

    public class Customer_CustomerCallLogMappingFilterDTO : FilterDTO
    {
        
        public IdFilter CustomerId { get; set; }
        
        public IdFilter CallLogId { get; set; }
        
        public CustomerCallLogMappingOrder OrderBy { get; set; }
    }
}