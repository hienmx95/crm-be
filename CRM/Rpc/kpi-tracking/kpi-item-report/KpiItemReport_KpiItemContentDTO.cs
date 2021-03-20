using CRM.Common;
using System.Collections.Generic;

namespace CRM.Rpc.kpi_tracking.kpi_item_report
{
    public class KpiItemReport_KpiItemContentDTO : DataDTO
    {
        public long STT { get; set; }
        public long SaleEmployeeId { get; set; }
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        //Sản lượng theo đơn hàng
        public decimal? OrderOutputPlanned { get; set; }
        public decimal? OrderOutput { get; set; }
        public decimal? OrderOutputRatio { get; set; }

        //Doanh số theo đơn hàng
        public decimal? SalesPlanned { get; set; }
        public decimal? Sales { get; set; }
        public decimal? SalesRatio { get; set; }

        //Số đơn hàng
        public decimal? SalesOrderPlanned { get; set; }
        internal HashSet<long> SalesOrderIds { get; set; }
        public decimal? SalesOrder
        {
            get
            {
                if (SalesOrderIds == null || SalesOrderPlanned == null) return null;
                return SalesOrderIds.Count;
            }
        }
        public decimal? SalesOrderRatio { get; set; }

        //Số khách hàng
        public decimal? NumberOfCustomerPlanned { get; set; }
        internal HashSet<long> NumberOfCustomerIds { get; set; }
        public decimal? NumberOfCustomer
        {
            get
            {
                if (NumberOfCustomerIds == null || NumberOfCustomerPlanned == null) return null;
                return NumberOfCustomerIds.Count;
            }
        }
        public decimal? NumberOfCustomerRatio { get; set; }



        //Sản lượng theo hợp đồng
        public decimal? CountItemInContractPlanned { get; set; }
        public decimal? CountItemInContract { get; set; }
        public decimal? CountItemInContractRatio { get; set; }

        //Số lượng hợp đồng
        public decimal? CountContractPlanned { get; set; }
        public decimal? CountContract { get; set; }
        public decimal? CountContractRatio { get; set; }

        //Doanh số theo hợp đồng
        public decimal? ReveuneContractPlanned { get; set; }
        public decimal? ReveuneContract { get; set; }
        public decimal? ReveuneContractRatio { get; set; }


    }
}
