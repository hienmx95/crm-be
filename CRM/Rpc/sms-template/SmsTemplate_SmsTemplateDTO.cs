using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.sms_template
{
    public class SmsTemplate_SmsTemplateDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public long? StatusId { get; set; }
        public SmsTemplate_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public SmsTemplate_SmsTemplateDTO() {}
        public SmsTemplate_SmsTemplateDTO(SmsTemplate SmsTemplate)
        {
            this.Id = SmsTemplate.Id;
            this.Code = SmsTemplate.Code;
            this.Name = SmsTemplate.Name;
            this.Content = SmsTemplate.Content;
            this.StatusId = SmsTemplate.StatusId;
            this.Status = SmsTemplate.Status == null ? null : new SmsTemplate_StatusDTO(SmsTemplate.Status);
            this.CreatedAt = SmsTemplate.CreatedAt;
            this.UpdatedAt = SmsTemplate.UpdatedAt;
            this.Errors = SmsTemplate.Errors;
        }
    }

    public class SmsTemplate_SmsTemplateFilterDTO : FilterDTO
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
