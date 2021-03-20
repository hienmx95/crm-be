using CRM.Common;
using System.Collections.Generic;

namespace CRM.Rpc.reports.opportunity_report
{
    public class OpportunityReportRoute : Root
    {
        public const string Parent = Module + "/reports";
        public const string Master = Module + "/reports/opportunity-report/opportunity-report-master";

        private const string Default = Rpc + Module + "/opportunity-report";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Total = Default + "/total";
        public const string Export = Default + "/export";

        public const string FilterListItem = Default + "/filter-list-item";
        public const string FilterListCompany = Default + "/filter-list-company";
        public const string FilterListSaleStage = Default + "/filter-list-sale-stage";
        public const string FilterListProbability = Default + "/filter-list-probability";
        public const string FilterListAppUser = Default + "/filter-list-app-user";

        public const string ListItem = Default + "/list-item";
        public const string CountItem = Default + "/count-item";
         

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List,Total,
                FilterListItem,FilterListCompany,FilterListSaleStage,FilterListProbability,FilterListAppUser,ListItem,CountItem  } },
             { "Export", new List<string> {
                Parent,
                Master, Count, List,Total ,Export,
                FilterListItem,FilterListCompany,FilterListSaleStage,FilterListProbability,FilterListAppUser,ListItem,CountItem  } },

        };
    }
}
