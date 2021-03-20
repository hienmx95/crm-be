using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket
{
    public class Ticket_CallLogDTO : DataDTO
    {

        public long Id { get; set; }

        public long EntityReferenceId { get; set; }

        public long CallTypeId { get; set; }

        public long? CallEmotionId { get; set; }

        public long AppUserId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Phone { get; set; }

        public DateTime CallTime { get; set; }

        public bool Used { get; set; }


        public Ticket_CallLogDTO() { }
        public Ticket_CallLogDTO(CallLog CallLog)
        {

            this.Id = CallLog.Id;

            this.EntityReferenceId = CallLog.EntityReferenceId;

            this.CallTypeId = CallLog.CallTypeId;

            this.CallEmotionId = CallLog.CallEmotionId;

            this.AppUserId = CallLog.AppUserId;

            this.Title = CallLog.Title;

            this.Content = CallLog.Content;

            this.Phone = CallLog.Phone;

            this.CallTime = CallLog.CallTime;


            this.Errors = CallLog.Errors;
        }
    }

    public class Ticket_CallLogFilterDTO : FilterDTO
    {

        public IdFilter Id { get; set; }

        public IdFilter EntityReferenceId { get; set; }

        public IdFilter CallTypeId { get; set; }

        public IdFilter CallEmotionId { get; set; }

        public IdFilter AppUserId { get; set; }

        public StringFilter Title { get; set; }

        public StringFilter Content { get; set; }

        public StringFilter Phone { get; set; }

        public DateFilter CallTime { get; set; }

        public CallLogOrder OrderBy { get; set; }
    }
}