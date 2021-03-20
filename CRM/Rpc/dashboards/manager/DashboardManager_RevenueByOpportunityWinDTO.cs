using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.dashboards.manager
{
    public class DashboardManager_RevenueByOpportunityWinFilterDTO : FilterDTO
    {
        public IdFilter Time { get; set; }
    }
}
