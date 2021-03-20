using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.dashboards.manager
{
    public class DashboardManager_RevenueOpportunityWinFluctuationDTO : DataDTO
    {
        public List<DashboardManager_RevenueOpportunityWinByQuarterDTO> RevenueOpportunityWinFluctuationByThisQuaters { get; set; }
        public List<DashboardManager_RevenueOpportunityWinByQuarterDTO> RevenueOpportunityWinFluctuationByLastQuaters { get; set; }
        public List<DashboardManager_RevenueOpportunityWinByHalfYearDTO> RevenueOpportunityWinByHalfYears { get; set; }
        public List<DashboardManager_RevenueOpportunityWinByYearDTO> RevenueOpportunityWinFluctuationByYears { get; set; }
    }

    public class DashboardManager_RevenueOpportunityWinByQuarterDTO : DataDTO
    {
        public long Month { get; set; }
        public decimal Revenue { get; set; }
    }

    public class DashboardManager_RevenueOpportunityWinByHalfYearDTO : DataDTO
    {
        public long Month { get; set; }
        public decimal Revenue { get; set; }
    }

    public class DashboardManager_RevenueOpportunityWinByYearDTO : DataDTO
    {
        public long Month { get; set; }
        public decimal Revenue { get; set; }
    }

    public class DashboardManager_RevenueOpportunityWinFluctuationFilterDTO : FilterDTO
    {
        public IdFilter Time { get; set; }
    }
}
