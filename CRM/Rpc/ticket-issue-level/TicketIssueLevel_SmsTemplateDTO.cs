using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SmsTemplateDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public long? StatusId { get; set; }
        public TicketIssueLevel_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SmsTemplateDTO() {}
        public TicketIssueLevel_SmsTemplateDTO(SmsTemplate SmsTemplate)
        {
            this.Id = SmsTemplate.Id;
            this.Code = SmsTemplate.Code;
            this.Name = SmsTemplate.Name;
            this.Content = SmsTemplate.Content;
            this.StatusId = SmsTemplate.StatusId;
            this.Status = SmsTemplate.Status == null ? null : new TicketIssueLevel_StatusDTO(SmsTemplate.Status);
            this.CreatedAt = SmsTemplate.CreatedAt;
            this.UpdatedAt = SmsTemplate.UpdatedAt;
            this.Errors = SmsTemplate.Errors;
        }
    }

    public class TicketIssueLevel_SmsTemplateFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Content { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SmsTemplateOrder OrderBy { get; set; }
    }
}
