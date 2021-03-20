using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_SLAPolicyDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public long? TicketIssueLevelId { get; set; }
        
        public long? TicketPriorityId { get; set; }
        
        public long? FirstResponseTime { get; set; }
        
        public long? FirstResponseUnitId { get; set; }
        
        public long? ResolveTime { get; set; }
        
        public long? ResolveUnitId { get; set; }
        
        public bool? IsAlert { get; set; }
        
        public bool? IsAlertFRT { get; set; }
        
        public bool? IsEscalation { get; set; }
        
        public bool? IsEscalationFRT { get; set; }
        

        public Customer_SLAPolicyDTO() {}
        public Customer_SLAPolicyDTO(SLAPolicy SLAPolicy)
        {
            
            this.Id = SLAPolicy.Id;
            
            this.TicketIssueLevelId = SLAPolicy.TicketIssueLevelId;
            
            this.TicketPriorityId = SLAPolicy.TicketPriorityId;
            
            this.FirstResponseTime = SLAPolicy.FirstResponseTime;
            
            this.FirstResponseUnitId = SLAPolicy.FirstResponseUnitId;
            
            this.ResolveTime = SLAPolicy.ResolveTime;
            
            this.ResolveUnitId = SLAPolicy.ResolveUnitId;
            
            this.IsAlert = SLAPolicy.IsAlert;
            
            this.IsAlertFRT = SLAPolicy.IsAlertFRT;
            
            this.IsEscalation = SLAPolicy.IsEscalation;
            
            this.IsEscalationFRT = SLAPolicy.IsEscalationFRT;
            
            this.Errors = SLAPolicy.Errors;
        }
    }

    public class Customer_SLAPolicyFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public IdFilter TicketIssueLevelId { get; set; }
        
        public IdFilter TicketPriorityId { get; set; }
        
        public LongFilter FirstResponseTime { get; set; }
        
        public IdFilter FirstResponseUnitId { get; set; }
        
        public LongFilter ResolveTime { get; set; }
        
        public IdFilter ResolveUnitId { get; set; }
        
        public SLAPolicyOrder OrderBy { get; set; }
    }
}