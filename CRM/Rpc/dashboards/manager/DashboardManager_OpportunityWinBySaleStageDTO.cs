using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.dashboards.manager
{
    public class DashboardManager_OpportunityWinBySaleStageDTO : DataDTO
    {
        public decimal Revenue { get; set; }
        public List<DashboardManager_OpportunityWinRevenueBySaleStageDTO> OpportunityWinRevenueBySaleStages { get; set; }
    }

    public class DashboardManager_OpportunityWinRevenueBySaleStageDTO : DataDTO
    {
        public string SaleStage { get; set; }
        public decimal Revenue { get; set; }
    }
    public class DashboardManager_OpportunityWinBySaleStageFilterDTO : FilterDTO
    {
        public IdFilter Time { get; set; }
    }
}
