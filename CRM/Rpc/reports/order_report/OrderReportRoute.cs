using System.Collections.Generic;
using CRM.Common;

namespace CRM.Rpc.reports.order_report
{
    public class OrderReportRoute : Root
    {
        public const string Parent = Module + "/reports";
        public const string Master = Module + "/reports/order-report/order-report-master";

        private const string Default = Rpc + Module + "/order-report";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Export = Default + "/export";

        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListOrganization = Default + "/filter-list-organization";
        public const string FilterListPaymentStatus = Default + "/filter-list-payment-status";
        public const string FilterListCompany = Default + "/filter-list-company";
        public const string FilterListOpportunity = Default + "/filter-list-opportuniy";
        public const string FilterListOrderCategory = Default + "/filter-list-order-category";

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List,
                FilterListAppUser,FilterListOrganization,FilterListPaymentStatus,FilterListCompany,FilterListOpportunity,FilterListOrderCategory  } },
             { "Export", new List<string> {
                Parent,
                Master, Count, List ,Export,
                FilterListAppUser,FilterListOrganization ,FilterListPaymentStatus,FilterListCompany,FilterListOpportunity,FilterListOrderCategory} },

        };
    }
}
