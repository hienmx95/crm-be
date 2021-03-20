using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_SmsQueueStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public CustomerLead_SmsQueueStatusDTO() {}
        public CustomerLead_SmsQueueStatusDTO(SmsQueueStatus SmsQueueStatus)
        {
            
            this.Id = SmsQueueStatus.Id;
            
            this.Code = SmsQueueStatus.Code;
            
            this.Name = SmsQueueStatus.Name;
            
            this.Errors = SmsQueueStatus.Errors;
        }
    }

    public class CustomerLead_SmsQueueStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public SmsQueueStatusOrder OrderBy { get; set; }
    }
}