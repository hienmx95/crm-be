using CRM.Common;
using System.Collections.Generic; 

namespace CRM.Rpc.dashboards.manager
{
    public class DashboardManager_CustomerDistributeDTO : DataDTO
    {
        public List<DashboardManager_CustomerDistributeByQuarterDTO> CustomerDistributeByQuarters { get; set; }
        public List<DashboardManager_CustomerDistributeByYearDTO> CustomerDistributeByYears { get; set; }

    }
    //Quý này, quý trước, 6 tháng
    public class DashboardManager_CustomerDistributeByQuarterDTO : DataDTO
    {
        public long Month { get; set; }
        public long New { get; set; }
        public long Old { get; set; }
        public long TotalCurrent { get; set; }
    }
    // Cả năm
    public class DashboardManager_CustomerDistributeByYearDTO : DataDTO
    {
        public long Quarter { get; set; }
        public long New { get; set; }
        public long Old { get; set; }
        public long TotalCurrent { get; set; }
    }





    public class DashboardManager_CustomerDistributeByMonthDTO : DataDTO
    {
        public long Day { get; set; }
        public long New { get; set; }
        public long Old { get; set; }
    }

 



    public class DashboardManager_CustomerDistributeFilterDTO : FilterDTO
    {
        public IdFilter Time { get; set; }
    }
}
