using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_of_user
{
    public class TicketOfUser_TicketDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Phone { get; set; }
        
        public long CustomerId { get; set; }
        
        public long UserId { get; set; }
        
        public long? ProductId { get; set; }
        
        public DateTime ReceiveDate { get; set; }
        
        public DateTime? ProcessDate { get; set; }
        
        public DateTime? FinishDate { get; set; }
        
        public string Subject { get; set; }
        
        public string Content { get; set; }
        
        public long TicketIssueLevelId { get; set; }
        
        public long TicketPriorityId { get; set; }
        
        public long TicketStatusId { get; set; }
        
        public long TicketSourceId { get; set; }
        
        public string TicketNumber { get; set; }
        
        public long? DepartmentId { get; set; }
        
        public long? RelatedTicketId { get; set; }
        
        public long SLA { get; set; }
        
        public long? RelatedCallLogId { get; set; }
        
        public long? ResponseMethodId { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        

        public TicketOfUser_TicketDTO() {}
        public TicketOfUser_TicketDTO(Ticket Ticket)
        {
            
            this.Id = Ticket.Id;
            
            this.Name = Ticket.Name;
            
            this.Phone = Ticket.Phone;
            
            this.CustomerId = Ticket.CustomerId;
            
            this.UserId = Ticket.UserId;
            
            this.ProductId = Ticket.ProductId;
            
            this.ReceiveDate = Ticket.ReceiveDate;
            
            this.ProcessDate = Ticket.ProcessDate;
            
            this.FinishDate = Ticket.FinishDate;
            
            this.Subject = Ticket.Subject;
            
            this.Content = Ticket.Content;
            
            this.TicketIssueLevelId = Ticket.TicketIssueLevelId;
            
            this.TicketPriorityId = Ticket.TicketPriorityId;
            
            this.TicketStatusId = Ticket.TicketStatusId;
            
            this.TicketSourceId = Ticket.TicketSourceId;
            
            this.TicketNumber = Ticket.TicketNumber;
            
            this.DepartmentId = Ticket.DepartmentId;
            
            this.RelatedTicketId = Ticket.RelatedTicketId;
            
            this.SLA = Ticket.SLA;
            
            this.RelatedCallLogId = Ticket.RelatedCallLogId;
            
            this.ResponseMethodId = Ticket.ResponseMethodId;
            
            this.StatusId = Ticket.StatusId;
            
            this.Used = Ticket.Used;
            
            this.Errors = Ticket.Errors;
        }
    }

    public class TicketOfUser_TicketFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Phone { get; set; }
        
        public IdFilter CustomerId { get; set; }
        
        public IdFilter UserId { get; set; }
        
        public IdFilter ProductId { get; set; }
        
        public DateFilter ReceiveDate { get; set; }
        
        public DateFilter ProcessDate { get; set; }
        
        public DateFilter FinishDate { get; set; }
        
        public StringFilter Subject { get; set; }
        
        public StringFilter Content { get; set; }
        
        public IdFilter TicketIssueLevelId { get; set; }
        
        public IdFilter TicketPriorityId { get; set; }
        
        public IdFilter TicketStatusId { get; set; }
        
        public IdFilter TicketSourceId { get; set; }
        
        public StringFilter TicketNumber { get; set; }
        
        public IdFilter DepartmentId { get; set; }
        
        public IdFilter RelatedTicketId { get; set; }
        
        public LongFilter SLA { get; set; }
        
        public IdFilter RelatedCallLogId { get; set; }
        
        public IdFilter ResponseMethodId { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public TicketOrder OrderBy { get; set; }
    }
}