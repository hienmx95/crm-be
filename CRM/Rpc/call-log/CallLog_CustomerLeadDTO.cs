using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.call_log
{
    public class CallLog_CustomerLeadDTO : DataDTO
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public long? UserId { get; set; }
        public long? ActivityTypeId { get; set; }
        public long? EntityReferenceId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }


        public CallLog_CustomerLeadDTO() {}
        public CallLog_CustomerLeadDTO(CustomerLead CustomerLead)
        {
            
            this.Id = CustomerLead.Id;
            this.Name = CustomerLead.Name;
            this.UserId = CustomerLead.AppUserId;

        }
    }

    public class CallLog_CustomerLeadFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        public IdFilter UserId { get; set; }

        public CustomerLeadOrder OrderBy { get; set; }
    }
}