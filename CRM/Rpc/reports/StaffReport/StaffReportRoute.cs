using System.Collections.Generic;
using CRM.Common;

namespace CRM.Rpc.reports.staff_report
{
    public class StaffReportRoute : Root
    {
        public const string Parent = Module + "/reports";
        public const string Master = Module + "/reports/staff-report/staff-report-master";

        private const string Default = Rpc + Module + "/staff-report";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Total = Default + "/total";
        public const string Export = Default + "/export";

        public const string FilterListAppUser = Default + "/filter-list-app-user";
        public const string FilterListOrganization = Default + "/filter-list-organization";

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List,
                FilterListAppUser,FilterListOrganization,Total  } },
             { "Export", new List<string> {
                Parent,
                Master, Count, List ,Export,
                FilterListAppUser,FilterListOrganization,Total } },

        };
    }
}
