using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.ticket_report
{
    public class TicketReport_SLAStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }

        public string ColorCode { get; set; }
        

        public TicketReport_SLAStatusDTO() {}
        public TicketReport_SLAStatusDTO(SLAStatus SLAStatus)
        {
            
            this.Id = SLAStatus.Id;
            
            this.Code = SLAStatus.Code;
            
            this.Name = SLAStatus.Name;

            this.ColorCode = SLAStatus.ColorCode;
            
            this.Errors = SLAStatus.Errors;
        }
    }

    public class TicketReport_SLAStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }

        public StringFilter ColorCode { get; set; }
        
        public SLAStatusOrder OrderBy { get; set; }
    }
}