using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.dashboards.ticket
{
    public class DashboardTicket_TicketDTO : DataDTO
    {
        public string TicketType { get; set; }
        public string colorCode { get; set; }
        public List<DashboardTicket_TicketByMonthDTO> TicketByMonths { get; set; }
        public List<DashboardTicket_TicketByQuarterDTO> TicketByQuaters { get; set; }
        public List<DashboardTicket_TicketByYearDTO> TicketByYears { get; set; }
    }
    public class DashboardTicket_TicketByMonthDTO : DataDTO
    {
        public string Day { get; set; }
        public int Count { get; set; } 
    }
    public class DashboardTicket_TicketByQuarterDTO : DataDTO
    {
        public string Month { get; set; }
        public int Count { get; set; }
    }
    public class DashboardTicket_TicketByYearDTO : DataDTO
    {
        public string Month { get; set; }
        public int Count { get; set; }
    }
    public class DashboardTicket_TicketFilterDTO : FilterDTO
    {
        public IdFilter Time { get; set; }
        public IdFilter TicketStatusId { get; set; }
        public IdFilter TicketTypeId { get; set; }
    }
}
