using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.dashboards.manager
{
    public class DashboardManagerRoute : Root
    {
        public const string Parent = Module + "/dashboards";
        public const string Master = Module + "/dashboards/manager";
        private const string Default = Rpc + Module + "/dashboards/manager";
        public const string FilterListTime = Default + "/filter-list-time";
        public const string Top5RevenueByCompany = Default + "/top-5-revenue-by-company";
        public const string Top5RevenueByCompanyWin = Default + "/top-5-revenue-by-company-win";
        public const string RevenueByOpportunityWin = Default + "/revenue-by-opportunity-win";
        public const string RevenueOpportunityWinFluctuation = Default + "/revenue-opportunity-win-fluctuation";
        public const string OpportunityLoseByReason = Default + "/opportunity-lose-by-reason";
        public const string OpportunityWinBySaleStage = Default + "/opportunity-win-by-sale-stage";
        public const string OpportunityWinAndLoseFluctuation = Default + "/opportunity-win-and-lose-fluctuation";
        public const string Top20BusinessTracking = Default + "/top-20-business-tracking";
        public const string CustomerDistribute = Default + "/customer-distribute";


        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Hiển thị", new List<string> {
                Parent, Top5RevenueByCompanyWin, FilterListTime, RevenueOpportunityWinFluctuation, OpportunityWinAndLoseFluctuation,
                Master, Top5RevenueByCompany, RevenueByOpportunityWin, OpportunityLoseByReason, OpportunityWinBySaleStage, Top20BusinessTracking,
                CustomerDistribute
            } },
        };
    }
}
