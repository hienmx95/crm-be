using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.ticket_report
{
    public class TicketReport_CustomerTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public TicketReport_CustomerTypeDTO() {}
        public TicketReport_CustomerTypeDTO(CustomerType CustomerType)
        {
            
            this.Id = CustomerType.Id;
            
            this.Name = CustomerType.Name;
            
            this.Code = CustomerType.Code;
            
            this.Errors = CustomerType.Errors;
        }
    }

    public class TicketReport_CustomerTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Code { get; set; }
        
        public CustomerTypeOrder OrderBy { get; set; }
    }
}