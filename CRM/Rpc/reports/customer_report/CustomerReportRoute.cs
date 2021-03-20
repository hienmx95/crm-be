using System.Collections.Generic;
using CRM.Common;

namespace CRM.Rpc.reports.customer_report
{
    public class CustomerReportRoute : Root
    {
        public const string Parent = Module + "/reports";
        public const string Master = Module + "/reports/customer-report/customer-report-master";

        private const string Default = Rpc + Module + "/customer-report";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Export = Default + "/export";

        public const string FilterListCustomerType = Default + "/filter-customer-type";

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List,
                FilterListCustomerType  } },
             { "Export", new List<string> {
                Parent,
                Master, Count, List ,Export,
                FilterListCustomerType } },

        };
    }
}
