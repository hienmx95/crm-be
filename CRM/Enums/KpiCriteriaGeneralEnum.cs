using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class KpiCriteriaGeneralEnum
    {
        public static GenericEnum TOTAL_LEAD = new GenericEnum { Id = 1, Code = "TotalLead", Name = "Số Lead" };
        public static GenericEnum TOTAL_OPPORTUNITY = new GenericEnum { Id = 2, Code = "TotalOpportunity", Name = "Số cơ hội thành công" };
        public static GenericEnum TOTAL_CUSTOMER = new GenericEnum { Id = 3, Code = "TotalCustomer", Name = "Số khách hàng" };
        public static GenericEnum TOTAl_ORDER = new GenericEnum { Id = 4, Code = "TotalOrder", Name = "Số đơn hàng" };
        public static GenericEnum SALES_BY_ORDER = new GenericEnum { Id = 5, Code = "SalesByOrder", Name = "Doanh số theo đơn hàng" };
        public static GenericEnum TOTAL_CONTRACT = new GenericEnum { Id = 6, Code = "TotalContract", Name = "Số hợp đồng" };
        public static GenericEnum SALES_BY_CONTRACT = new GenericEnum { Id = 7, Code = "SalesByContract", Name = "Doanh số theo hợp đồng" };
        public static GenericEnum TOTAL_TICKET_COMPLETE = new GenericEnum { Id = 8, Code = "TotalTicketComplete", Name = "Số ticket hoàn thành" };

        public static List<GenericEnum> KpiCriteriaGeneralEnumList = new List<GenericEnum>()
        {
             TOTAL_LEAD,
             TOTAL_OPPORTUNITY,
             TOTAL_CUSTOMER,
             TOTAl_ORDER,
             SALES_BY_ORDER,
             TOTAL_CONTRACT,
             SALES_BY_CONTRACT,
             TOTAL_TICKET_COMPLETE
        };
    }
}
