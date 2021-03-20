using CRM.Common;

namespace CRM.Rpc.reports.order_report
{
    public class OrderReport_OrderReportFilterDTO : FilterDTO
    {
        public StringFilter Code { get; set; }
        public IdFilter OrderCategoryId { get; set; }
        public DecimalFilter Total { get; set; }
        public IdFilter PaymentStatusId { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public IdFilter CreatorId { get; set; }
        public DateFilter CreatedAt { get; set; }
    }

}
