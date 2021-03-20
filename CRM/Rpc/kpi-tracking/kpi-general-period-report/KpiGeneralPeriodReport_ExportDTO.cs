using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.kpi_tracking.kpi_general_period_report
{
    public class KpiGeneralPeriodReport_ExportDTO : DataDTO
    {
        public string OrganizationName { get; set; }
        public List<KpiGeneralPeriodReport_LineDTO> Lines { get; set; }
        public KpiGeneralPeriodReport_ExportDTO(KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO)
        {
            this.OrganizationName = KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO.OrganizationName;
            this.Lines = KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO.SaleEmployees?.Select(x => new KpiGeneralPeriodReport_LineDTO(x)).ToList();
        }
    }

    public class KpiGeneralPeriodReport_LineDTO : DataDTO
    {
        public long STT { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string OrganizationName { get; set; }

        //Số Lead
        public string TotalLeadPlanned { get; set; }
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
        public string TotalContractPlanned { get; set; }
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

        public KpiGeneralPeriodReport_LineDTO(KpiGeneralPeriodReport_SaleEmployeeDTO KpiGeneralPeriodReport_SaleEmployeeDTO)
        {
            this.STT = KpiGeneralPeriodReport_SaleEmployeeDTO.STT;
            this.Username = KpiGeneralPeriodReport_SaleEmployeeDTO.Username;
            this.DisplayName = KpiGeneralPeriodReport_SaleEmployeeDTO.DisplayName;
            this.OrganizationName = KpiGeneralPeriodReport_SaleEmployeeDTO.OrganizationName;

            this.TotalLeadPlanned = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalLeadPlanned == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalLeadPlanned.ToString();
            this.TotalLead = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalLead == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalLead.ToString();
            this.TotalLeadRatio = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalLeadRatio == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalLeadRatio.ToString();

            this.TotalOpportunityPlanned = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalOpportunityPlanned == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalOpportunityPlanned.ToString();
            this.TotalOpportunity = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalOpportunity == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalOpportunity.ToString();
            this.TotalOpportunityRatio = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalOpportunityRatio == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalOpportunityRatio.ToString();
            this.TotalCustomerPlanned = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalCustomerPlanned == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalCustomerPlanned.ToString();
            this.TotalCustomer = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalCustomer == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalCustomer.ToString();
            this.TotalCustomerRatio = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalCustomerRatio == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalCustomerRatio.ToString();
            this.TotalContractPlanned = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalContractPlanned == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalContractPlanned.ToString();
            this.TotalContract = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalContract == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalContract.ToString();
            this.TotalContractRatio = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalContractRatio == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalContractRatio.ToString();
            this.TotalOrderPlanned = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalOrderPlanned == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalOrderPlanned.ToString();
            this.TotalOrder = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalOrder == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalOrder.ToString();
            this.TotalOrderRatio = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalOrderRatio == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalOrderRatio.ToString();
            this.TotalSalesOfOrderPlanned = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalSalesOfOrderPlanned == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalSalesOfOrderPlanned.ToString();
            this.TotalSalesOfOrder = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalSalesOfOrder == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalSalesOfOrder.ToString();
            this.TotalSalesOfOrderRatio = KpiGeneralPeriodReport_SaleEmployeeDTO.TotalSalesOfOrderRatio == null ? "" : KpiGeneralPeriodReport_SaleEmployeeDTO.TotalSalesOfOrderRatio.ToString();
        }
    }
}
