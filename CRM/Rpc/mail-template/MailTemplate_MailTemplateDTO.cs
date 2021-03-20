using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.mail_template
{
    public class MailTemplate_MailTemplateDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public long? StatusId { get; set; }
        public MailTemplate_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public MailTemplate_MailTemplateDTO() {}
        public MailTemplate_MailTemplateDTO(MailTemplate MailTemplate)
        {
            this.Id = MailTemplate.Id;
            this.Code = MailTemplate.Code;
            this.Name = MailTemplate.Name;
            this.Content = MailTemplate.Content;
            this.StatusId = MailTemplate.StatusId;
            this.Status = MailTemplate.Status == null ? null : new MailTemplate_StatusDTO(MailTemplate.Status);
            this.CreatedAt = MailTemplate.CreatedAt;
            this.UpdatedAt = MailTemplate.UpdatedAt;
            this.Errors = MailTemplate.Errors;
        }
    }

    public class MailTemplate_MailTemplateFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Content { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public MailTemplateOrder OrderBy { get; set; }
    }
}
