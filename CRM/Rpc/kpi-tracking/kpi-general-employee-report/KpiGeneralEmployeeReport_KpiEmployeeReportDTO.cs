using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace CRM.Rpc.kpi_tracking.kpi_general_employee_report
{
    public class KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO : DataDTO
    {
        public long STT { get; set; }
        public long SaleEmployeeId { get; set; }
        public string KpiPeriodName { get; set; }
        public long KpiPeriodId { get; set; }
        public string KpiYearName { get; set; }
        public long KpiYearId { get; set; }
        //Số Lead
        public decimal? TotalLeadPLanned { get; set; }
        public decimal? TotalLead { get; set; }
        public decimal? TotalLeadRatio { get; set; }

        //Số cơ hội thành công
        public decimal? TotalOpportunityPlanned { get; set; }
        public decimal? TotalOpportunity { get; set; }
        public decimal? TotalOpportunityRatio { get; set; }

        //Số khách hàng
        public decimal? TotalCustomerPlanned { get; set; }
        public decimal? TotalCustomer { get; set; }
        public decimal? TotalCustomerRatio { get; set; }

        //Số đơn hàng
        public decimal? TotalOrderPlanned { get; set; }
        public decimal? TotalOrder { get; set; }
        public decimal? TotalOrderRatio { get; set; }

        //Doanh số theo đơn hàng
        public decimal? TotalSalesOfOrderPlanned { get; set; }
        public decimal? TotalSalesOfOrder { get; set; }
        public decimal? TotalSalesOfOrderRatio { get; set; }

        //Số hợp đồng
        public decimal? TotalContractPLanned { get; set; }
        public decimal? TotalContract { get; set; }
        public decimal? TotalContractRatio { get; set; }


        //Doanh số theo hợp đồng
        public decimal? TotalSalesOfContractPlanned { get; set; }
        public decimal? TotalSalesOfContract { get; set; }
        public decimal? TotalSalesOfContractRatio { get; set; }

        //Số ticket thành công
        public decimal? TotalTicketCompletedPlanned { get; set; }
        public decimal? TotalTicketCompleted { get; set; }
        public decimal? TotalTicketCompletedRatio { get; set; }

    }


    public class KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO : FilterDTO
    {
        public IdFilter OrganizationId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter KpiPeriodId { get; set; }
        public IdFilter KpiYearId { get; set; }
    }
}
