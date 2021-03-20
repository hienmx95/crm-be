using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.dashboards.manager
{
    public class DashboardManager_OpportunityWinAndLoseFluctuationDTO : DataDTO
    {
        public List<DashboardManager_OpportunityWinAndLoseByQuarterDTO> OpportunityWinAndLoseFluctuationByThisQuaters { get; set; }
        public List<DashboardManager_OpportunityWinAndLoseByQuarterDTO> OpportunityWinAndLoseFluctuationByLastQuaters { get; set; }
        public List<DashboardManager_OpportunityWinAndLoseByHalfYearDTO> OpportunityWinAndLoseByHalfYears { get; set; }
        public List<DashboardManager_OpportunityWinAndLoseByYearDTO> OpportunityWinAndLoseFluctuationByYears { get; set; }
    }

    public class DashboardManager_OpportunityWinAndLoseByQuarterDTO : DataDTO
    {
        public long Month { get; set; }
        public decimal Win { get; set; }
        public decimal Lose { get; set; }
    }

    public class DashboardManager_OpportunityWinAndLoseByHalfYearDTO : DataDTO
    {
        public long Month { get; set; }
        public decimal Win { get; set; }
        public decimal Lose { get; set; }
    }

    public class DashboardManager_OpportunityWinAndLoseByYearDTO : DataDTO
    {
        public long Month { get; set; }
        public decimal Win { get; set; }
        public decimal Lose { get; set; }
    }

    public class DashboardManager_OpportunityWinAndLoseFluctuationFilterDTO : FilterDTO
    {
        public IdFilter Time { get; set; }
    }
}
