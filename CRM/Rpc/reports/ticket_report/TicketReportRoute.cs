using CRM.Common;
using System.Collections.Generic;

namespace CRM.Rpc.reports.ticket_report
{
    public class TicketReportRoute : Root
    {
        public const string Parent = Module + "/reports";
        public const string Master = Module + "/reports/ticket-report/ticket-report-master";

        private const string Default = Rpc + Module + "/ticket-report";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Export = Default + "/export";

        public const string FilterListTicketType = Default + "/filter-list-ticket-type";
        public const string FilterListTicketGroup = Default + "/filter-list-ticket-group";
        public const string FilterListTicketStatus = Default + "/filter-list-ticket-status";
        public const string FilterListSLAStatus = Default + "/filter-list-s-l-a-status";
        public const string FilterListTicketPriority = Default + "/filter-list-ticket-priority";
        public const string FilterListCustomer = Default + "/filter-list-customer";
        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListCustomerType = Default + "/filter-list-customer-type";


        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(TicketReport_TicketReportFilterDTO.TicketNumber), FieldTypeEnum.ID.Id },
            { nameof(TicketReport_TicketReportFilterDTO.TicketTypeId), FieldTypeEnum.ID.Id },
            { nameof(TicketReport_TicketReportFilterDTO.TicketGroupId), FieldTypeEnum.ID.Id },
            { nameof(TicketReport_TicketReportFilterDTO.TicketStatusId), FieldTypeEnum.ID.Id },
            { nameof(TicketReport_TicketReportFilterDTO.SLAStatusId), FieldTypeEnum.ID.Id },
            { nameof(TicketReport_TicketReportFilterDTO.TicketPriorityId), FieldTypeEnum.ID.Id },
            { nameof(TicketReport_TicketReportFilterDTO.CustomerTypeId), FieldTypeEnum.ID.Id },
            { nameof(TicketReport_TicketReportFilterDTO.CustomerId), FieldTypeEnum.ID.Id },
            //{ nameof(TicketReport_TicketReportFilterDTO.UserId), FieldTypeEnum.ID.Id },
            { nameof(TicketReport_TicketReportFilterDTO.CreatedAt), FieldTypeEnum.ID.Id },
            { nameof(CurrentContext.UserId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List,
                FilterListTicketType,FilterListTicketGroup,FilterListTicketStatus,FilterListSLAStatus,FilterListTicketPriority,FilterListCustomer,FilterListAppUser,FilterListCustomerType  } },
             { "Export", new List<string> {
                Parent,
                Master, Count, List, Export,
                FilterListTicketType,FilterListTicketGroup,FilterListTicketStatus,FilterListSLAStatus,FilterListTicketPriority,FilterListCustomer,FilterListAppUser,FilterListCustomerType  } },

        };
    }
}
