using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace CRM.Rpc.reports.ticket_report
{
    public class TicketReport_TicketReportDTO : DataDTO
    {
        public List<TicketReport_TicketDTO> Tickets { get; set; }
    }

    public class TicketReport_TicketReportFilterDTO : FilterDTO
    {
        public StringFilter TicketNumber { get; set; }
        public IdFilter TicketTypeId { get; set; }
        public IdFilter TicketGroupId { get; set; }
        public IdFilter TicketStatusId { get; set; }
        public IdFilter SLAStatusId { get; set; }
        public IdFilter TicketPriorityId { get; set; }
        public IdFilter CustomerTypeId { get; set; }
        public IdFilter CustomerId { get; set; }
        public IdFilter UserId { get; set; }

        public DateFilter CreatedAt { get; set; }
    }

}
