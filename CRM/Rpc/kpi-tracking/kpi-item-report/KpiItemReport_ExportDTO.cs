using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.kpi_tracking.kpi_item_report
{
    public class KpiItemReport_ExportDTO : DataDTO
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public List<KpiItemReport_LineDTO> Lines { get; set; }
        public KpiItemReport_ExportDTO(KpiItemReport_KpiItemReportDTO KpiItemReport_KpiItemReportDTO)
        {
            this.Username = KpiItemReport_KpiItemReportDTO.Username;
            this.DisplayName = KpiItemReport_KpiItemReportDTO.DisplayName;
            this.Lines = KpiItemReport_KpiItemReportDTO.ItemContents?.Select(x => new KpiItemReport_LineDTO(x)).ToList();
        }
    }

    public class KpiItemReport_LineDTO : DataDTO
    {
        public long STT { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        //Sản lượng theo đơn hàng
        public string OrderOutputPlanned { get; set; }
        public string OrderOutput { get; set; }
        public string OrderOutputRatio { get; set; }

        //Doanh số theo đơn hàng
        public string SalesPlanned { get; set; }
        public string Sales { get; set; }
        public string SalesRatio { get; set; }

        //Số đơn hàng
        public string SalesOrderPlanned { get; set; }
        public string SalesOrder { get; set; }
        public string SalesOrderRatio { get; set; }

        //Số khách hàng
        public string NumberOfCustomerPlanned { get; set; }
        public string NumberOfCustomer { get; set; }
        public string NumberOfCustomerRatio { get; set; }

        public KpiItemReport_LineDTO(KpiItemReport_KpiItemContentDTO KpiItemReport_KpiItemContentDTO)
        {
            this.STT = KpiItemReport_KpiItemContentDTO.STT;
            this.ItemCode = KpiItemReport_KpiItemContentDTO.ItemCode;
            this.ItemName = KpiItemReport_KpiItemContentDTO.ItemName;
            this.OrderOutputPlanned = KpiItemReport_KpiItemContentDTO.OrderOutputPlanned == null ? "" : KpiItemReport_KpiItemContentDTO.OrderOutputPlanned.ToString();
            this.OrderOutput = KpiItemReport_KpiItemContentDTO.OrderOutput == null ? "" : KpiItemReport_KpiItemContentDTO.OrderOutput.ToString();
            this.OrderOutputRatio = KpiItemReport_KpiItemContentDTO.OrderOutputRatio == null ? "" : KpiItemReport_KpiItemContentDTO.OrderOutputRatio.ToString();
            this.SalesPlanned = KpiItemReport_KpiItemContentDTO.SalesPlanned == null ? "" : KpiItemReport_KpiItemContentDTO.SalesPlanned.ToString();
            this.Sales = KpiItemReport_KpiItemContentDTO.Sales == null ? "" : KpiItemReport_KpiItemContentDTO.Sales.ToString();
            this.SalesRatio = KpiItemReport_KpiItemContentDTO.SalesRatio == null ? "" : KpiItemReport_KpiItemContentDTO.SalesRatio.ToString();
            this.SalesOrderPlanned = KpiItemReport_KpiItemContentDTO.SalesOrderPlanned == null ? "" : KpiItemReport_KpiItemContentDTO.SalesOrderPlanned.ToString();
            this.SalesOrderRatio = KpiItemReport_KpiItemContentDTO.SalesOrderRatio == null ? "" : KpiItemReport_KpiItemContentDTO.SalesOrderRatio.ToString();
            this.SalesOrder = KpiItemReport_KpiItemContentDTO.SalesOrder == null ? "" : KpiItemReport_KpiItemContentDTO.SalesOrder.ToString();
            this.NumberOfCustomerPlanned = KpiItemReport_KpiItemContentDTO.NumberOfCustomerPlanned == null ? "" : KpiItemReport_KpiItemContentDTO.NumberOfCustomerPlanned.ToString();
            this.NumberOfCustomer = KpiItemReport_KpiItemContentDTO.NumberOfCustomer == null ? "" : KpiItemReport_KpiItemContentDTO.NumberOfCustomer.ToString();
            this.NumberOfCustomerRatio = KpiItemReport_KpiItemContentDTO.NumberOfCustomerRatio == null ? "" : KpiItemReport_KpiItemContentDTO.NumberOfCustomerRatio.ToString();
        }
    }
}
