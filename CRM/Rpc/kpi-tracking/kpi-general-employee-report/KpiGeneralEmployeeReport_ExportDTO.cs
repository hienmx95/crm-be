using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.kpi_tracking.kpi_general_employee_report
{
    public class KpiGeneralEmployeeReport_ExportDTO : DataDTO
    {
        public long STT { get; set; }
        public string KpiPeriod { get; set; }
        public string KpiYear { get; set; }

        //Số Lead
        public string TotalLeadPLanned { get; set; }
        public string TotalLead { get; set; }
        public string TotalLeadRatio { get; set; }

        //Số cơ hội thành công
        public string TotalOpportunityPlanned { get; set; }
        public string TotalOpportunity { get; set; }
        public string TotalOpportunityRatio { get; set; }

        //Doanh số theo khách hàng
        public string TotalCustomerPlanned { get; set; }
        public string TotalCustomer { get; set; }
        public string TotalCustomerRatio { get; set; }

        //Số hợp đồng
        public string TotalContractPLanned { get; set; }
        public string TotalContract { get; set; }
        public string TotalContractRatio { get; set; }

        //Số đơn hàng
        public string TotalOrderPlanned { get; set; }
        public string TotalOrder { get; set; }
        public string TotalOrderRatio { get; set; }


        //Doanh số theo đơn hàng
        public string TotalSalesOfOrderPlanned { get; set; }
        public string TotalSalesOfOrder { get; set; }
        public string TotalSalesOfOrderRatio { get; set; }

        public KpiGeneralEmployeeReport_ExportDTO(KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO)
        {
            this.STT = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.STT;
            this.KpiPeriod = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.KpiPeriodName;
            this.KpiYear = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.KpiYearName;

            this.TotalLeadPLanned = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalLeadPLanned == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalLeadPLanned.ToString();
            this.TotalLead = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalLead == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalLead.ToString();
            this.TotalLeadRatio = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalLeadRatio == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalLeadRatio.ToString();

            this.TotalOpportunityPlanned = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalOpportunityPlanned == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalOpportunityPlanned.ToString();
            this.TotalOpportunity = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalOpportunity == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalOpportunity.ToString();
            this.TotalOpportunityRatio = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalOpportunityRatio == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalOpportunityRatio.ToString();
            this.TotalCustomerPlanned = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalCustomerPlanned == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalCustomerPlanned.ToString();
            this.TotalCustomer = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalCustomer == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalCustomer.ToString();
            this.TotalCustomerRatio = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalCustomerRatio == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalCustomerRatio.ToString();
            this.TotalContractPLanned = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalContractPLanned == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalContractPLanned.ToString();
            this.TotalContract = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalContract == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalContract.ToString();
            this.TotalContractRatio = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalContractRatio == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalContractRatio.ToString();
            this.TotalOrderPlanned = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalOrderPlanned == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalOrderPlanned.ToString();
            this.TotalOrder = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalOrder == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalOrder.ToString();
            this.TotalOrderRatio = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalOrderRatio == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalOrderRatio.ToString();
            this.TotalSalesOfOrderPlanned = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalSalesOfOrderPlanned == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalSalesOfOrderPlanned.ToString();
            this.TotalSalesOfOrder = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalSalesOfOrder == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalSalesOfOrder.ToString();
            this.TotalSalesOfOrderRatio = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalSalesOfOrderRatio == null ? "" : KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.TotalSalesOfOrderRatio.ToString();
        }
    }
}
