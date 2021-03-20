using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.dashboards.ticket
{
    public class DashboardTicketRoute : Root
    {
        public const string Parent = Module + "/dashboards";
        public const string Master = Module + "/dashboards/user";
        private const string Default = Rpc + Module + "/dashboards/user";
        public const string CountTicket = Default + "/count-ticket";

        public const string SingleListTicketStatus = Default + "/single-list-ticket-status";
        public const string SingleListTicketType = Default + "/single-list-ticket-type";


        public const string FilterListTime1 = Default + "/filter-list-time-1";
        public const string FilterListTime2 = Default + "/filter-list-time-2";
        public const string FilterListTicketStatus = Default + "/filter-list-ticket-status";
        public const string FilterListTicketType = Default + "/filter-list-ticket-type";

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Hiển thị", new List<string> {
                Parent,
                Master,
                CountTicket, SingleListTicketStatus, SingleListTicketType, FilterListTime1, FilterListTime2, FilterListTicketStatus,
                FilterListTicketType
            } },
        };
    }
}
