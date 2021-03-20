using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Dynamic;
using CRM.Models;
using CRM.Services.MCustomerType;
using CRM.Services.MAppUser;
using CRM.Services.MOrganization;
using CRM.Services.MPaymentStatus;
using CRM.Services.MCompany;
using CRM.Services.MOpportunity;
using CRM.Services.MOrderCategory;
using Microsoft.AspNetCore.Http;

namespace CRM.Rpc.reports.order_report
{
    public partial class OrderReportController : RpcController
    {
        private IAppUserService AppUserService;
        private IOrganizationService OrganizationService;
        private IPaymentStatusService PaymentStatusService;
        private ICompanyService CompanyService;
        private IOpportunityService OpportunityService;
        private IOrderCategoryService OrderCategoryService;
        private ICurrentContext CurrentContext;
        private DataContext DataContext;
        public OrderReportController(
           IAppUserService AppUserService,
           IOrganizationService OrganizationService,
           IPaymentStatusService PaymentStatusService,
           ICompanyService CompanyService,
           IOpportunityService OpportunityService,
           IOrderCategoryService OrderCategoryService,
           ICurrentContext CurrentContext,
           DataContext DataContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.OrganizationService = OrganizationService;
            this.AppUserService = AppUserService;
            this.PaymentStatusService = PaymentStatusService;
            this.CompanyService = CompanyService;
            this.OpportunityService = OpportunityService;
            this.OrderCategoryService = OrderCategoryService;
            this.CurrentContext = CurrentContext;
            this.DataContext = DataContext;
        }

        [Route(OrderReportRoute.List), HttpPost]
        public async Task<ActionResult<object>> List([FromBody] OrderReport_OrderReportFilterDTO OrderReport_OrderReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            var filter = new OrderReport_OrderReportFilterDTO();
            filter.Code = OrderReport_OrderReportFilterDTO.Code;
            filter.Total = OrderReport_OrderReportFilterDTO.Total;
            filter.PaymentStatusId = OrderReport_OrderReportFilterDTO.PaymentStatusId;
            filter.CompanyId = OrderReport_OrderReportFilterDTO.CompanyId;
            filter.OpportunityId = OrderReport_OrderReportFilterDTO.OpportunityId;
            filter.OrganizationId = OrderReport_OrderReportFilterDTO.OrganizationId;
            filter.CreatorId = OrderReport_OrderReportFilterDTO.CreatorId;
            filter.CreatedAt = OrderReport_OrderReportFilterDTO.CreatedAt;
            filter.OrderCategoryId = OrderReport_OrderReportFilterDTO.OrderCategoryId;

            var queryCustomerSalesOrder = DataContext.CustomerSalesOrder.Where(p => !p.DeletedAt.HasValue);
            var queryOrderAgent = DataContext.DirectSalesOrder.Where(p => !p.DeletedAt.HasValue);
            if (filter.Code.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.Code, filter.Code);
                queryOrderAgent = queryOrderAgent.Where(p => p.Code, filter.Code);
            }
            if (filter.Total.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.Total, filter.Total);
                queryOrderAgent = queryOrderAgent.Where(p => p.Total, filter.Total);
            }
            if (filter.PaymentStatusId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.OrderPaymentStatusId, filter.PaymentStatusId);
                queryOrderAgent = queryOrderAgent.Where(p => p.Id != p.Id);
            }
            if (filter.CompanyId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.Id != p.Id);
                queryOrderAgent = queryOrderAgent.Where(p => p.Id != p.Id);
            }
            if (filter.OpportunityId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.Id != p.Id);
                queryOrderAgent = queryOrderAgent.Where(p => p.Id != p.Id);
            }
            if (filter.OrganizationId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.OrganizationId, filter.OrganizationId);
                queryOrderAgent = queryOrderAgent.Where(p => p.OrganizationId, filter.OrganizationId);
            }
            if (filter.CreatorId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.CreatorId, filter.CreatorId);
                queryOrderAgent = queryOrderAgent.Where(p => p.SaleEmployeeId, filter.CreatorId);
            }
            if (filter.CreatedAt.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.CreatedAt, filter.CreatedAt);
                queryOrderAgent = queryOrderAgent.Where(p => p.CreatedAt, filter.CreatedAt);
            }


            var CustomerSalesOrderDAOs = queryCustomerSalesOrder.Select(p => new OrderReportView
            {
                Code = p.Code,
                OrderTypeName = "Đơn hàng bán lẻ",
                Total = p.Total,
                PaymentStatusName = p.OrderPaymentStatus == null ? "" : p.OrderPaymentStatus.Name,
                PaymentPercentage = p.CustomerSalesOrderPaymentHistories == null ? default(decimal) : p.CustomerSalesOrderPaymentHistories.Where(p => p.PaymentPercentage.HasValue).Sum(p => p.PaymentPercentage.Value),
                CustomerName = p.Customer == null ? "" : p.Customer.Name,
                CreatedAt = p.CreatedAt,
                OpportunityName = "",
                OrganizationName = p.Organization == null ? "" : p.Organization.Name,
                AppUserAssignedName = p.SalesEmployee == null ? "" : p.SalesEmployee.DisplayName,
            }).ToList();

            var OrderAgentDAOs = queryOrderAgent == null ? null : queryOrderAgent.Select(p => new OrderReportView
            {
                Code = p.Code,
                OrderTypeName = "Đơn hàng đại lý",
                Total = p.Total,
                PaymentStatusName = p.RequestState == null ? "" : p.RequestState.Name,
                PaymentPercentage = 0,
                CompanyName = "Đang cập nhật",
                CustomerName = p.BuyerStore == null ? "" : (p.BuyerStore == null ? "" : p.BuyerStore.Name),
                CreatedAt = p.CreatedAt,
                OpportunityName = "Đang cập nhật",
                OrganizationName = p.Organization == null ? "" : p.Organization.Name,
                AppUserAssignedName = p.SaleEmployee == null ? "" : p.SaleEmployee.DisplayName,
            }).ToList();

            if (filter.OrderCategoryId != null && filter.OrderCategoryId.Equal == Enums.OrderCategoryEnum.ORDER_CUSTOMER.Id)
            {
                return CustomerSalesOrderDAOs.Skip(filter.Skip).Take(filter.Take).ToList();
            }
            else if (filter.OrderCategoryId != null && filter.OrderCategoryId.Equal == Enums.OrderCategoryEnum.ORDER_DIRECT.Id)
            {
                return OrderAgentDAOs.Skip(filter.Skip).Take(filter.Take).ToList();
            }

            return CustomerSalesOrderDAOs.Concat(OrderAgentDAOs)
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip(filter.Skip).Take(filter.Take).ToList();
        }

        [Route(OrderReportRoute.Count), HttpPost]
        public async Task<ActionResult<object>> Count([FromBody] OrderReport_OrderReportFilterDTO OrderReport_OrderReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            var filter = new OrderReport_OrderReportFilterDTO();
            filter.Code = OrderReport_OrderReportFilterDTO.Code;
            filter.Total = OrderReport_OrderReportFilterDTO.Total;
            filter.PaymentStatusId = OrderReport_OrderReportFilterDTO.PaymentStatusId;
            filter.CompanyId = OrderReport_OrderReportFilterDTO.CompanyId;
            filter.OpportunityId = OrderReport_OrderReportFilterDTO.OpportunityId;
            filter.OrganizationId = OrderReport_OrderReportFilterDTO.OrganizationId;
            filter.CreatorId = OrderReport_OrderReportFilterDTO.CreatorId;
            filter.CreatedAt = OrderReport_OrderReportFilterDTO.CreatedAt;
            filter.OrderCategoryId = OrderReport_OrderReportFilterDTO.OrderCategoryId;

            var queryCustomerSalesOrder = DataContext.CustomerSalesOrder.Where(p => !p.DeletedAt.HasValue);
            var queryOrderAgent = DataContext.DirectSalesOrder.Where(p => !p.DeletedAt.HasValue);
            if (filter.Code.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.Code, filter.Code);
                queryOrderAgent = queryOrderAgent.Where(p => p.Code, filter.Code);
            }
            if (filter.Total.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.Total, filter.Total);
                queryOrderAgent = queryOrderAgent.Where(p => p.Total, filter.Total);
            }
            if (filter.PaymentStatusId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.OrderPaymentStatusId, filter.PaymentStatusId);
                queryOrderAgent = queryOrderAgent.Where(p => p.Id != p.Id);
            }
            if (filter.CompanyId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.Id != p.Id);
                queryOrderAgent = queryOrderAgent.Where(p => p.Id != p.Id);
            }
            if (filter.OpportunityId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.Id != p.Id);
                queryOrderAgent = queryOrderAgent.Where(p => p.Id != p.Id);
            }
            if (filter.OrganizationId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.OrganizationId, filter.OrganizationId);
                queryOrderAgent = queryOrderAgent.Where(p => p.OrganizationId, filter.OrganizationId);
            }
            if (filter.CreatorId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.CreatorId, filter.CreatorId);
                queryOrderAgent = queryOrderAgent.Where(p => p.SaleEmployeeId, filter.CreatorId);
            }
            if (filter.CreatedAt.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.CreatedAt, filter.CreatedAt);
                queryOrderAgent = queryOrderAgent.Where(p => p.CreatedAt, filter.CreatedAt);
            }

            var CustomerSalesOrderDAOs = queryCustomerSalesOrder.Select(p => new OrderReportView
            {
                Code = p.Code,
                OrderTypeName = "Đơn hàng bán lẻ",
                Total = p.Total,
                PaymentStatusName = p.OrderPaymentStatus == null ? "" : p.OrderPaymentStatus.Name,
                PaymentPercentage = p.CustomerSalesOrderPaymentHistories == null ? default(decimal) : p.CustomerSalesOrderPaymentHistories.Where(p => p.PaymentPercentage.HasValue).Sum(p => p.PaymentPercentage.Value),
                CustomerName = p.Customer.Name == null ? "" : p.Customer.Name,
                CreatedAt = p.CreatedAt,
                OpportunityName = "",
                OrganizationName = p.Organization == null ? "" : p.Organization.Name,
                AppUserAssignedName = p.SalesEmployee == null ? "" : p.SalesEmployee.DisplayName,
            }).ToList();

            var OrderAgentDAOs = queryOrderAgent.Select(p => new OrderReportView
            {
                Code = p.Code,
                OrderTypeName = "Đơn hàng đại lý",
                Total = p.Total,
                PaymentStatusName = p.RequestState == null ? "" : p.RequestState.Name,
                PaymentPercentage = 0,
                CompanyName = "Đang cập nhật",
                CustomerName = p.BuyerStore == null ? "" : (p.BuyerStore == null ? "" : p.BuyerStore.Name),
                CreatedAt = p.CreatedAt,
                OpportunityName = "Đang cập nhật",
                OrganizationName = p.Organization == null ? "" : p.Organization.Name,
                AppUserAssignedName = p.SaleEmployee == null ? "" : p.SaleEmployee.DisplayName,
            }).ToList();

            if (filter.OrderCategoryId != null && filter.OrderCategoryId.Equal == Enums.OrderCategoryEnum.ORDER_CUSTOMER.Id)
            {
                return CustomerSalesOrderDAOs.Count();
            }
            else if (filter.OrderCategoryId != null && filter.OrderCategoryId.Equal == Enums.OrderCategoryEnum.ORDER_DIRECT.Id)
            {
                return OrderAgentDAOs.Count();
            }

            return CustomerSalesOrderDAOs.Concat(OrderAgentDAOs)
                    .Count();
        }

        [Route(OrderReportRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] OrderReport_OrderReportFilterDTO OrderReport_OrderReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            var filter = new OrderReport_OrderReportFilterDTO();
            filter.Code = OrderReport_OrderReportFilterDTO.Code;
            filter.Total = OrderReport_OrderReportFilterDTO.Total;
            filter.PaymentStatusId = OrderReport_OrderReportFilterDTO.PaymentStatusId;
            filter.CompanyId = OrderReport_OrderReportFilterDTO.CompanyId;
            filter.OpportunityId = OrderReport_OrderReportFilterDTO.OpportunityId;
            filter.OrganizationId = OrderReport_OrderReportFilterDTO.OrganizationId;
            filter.CreatorId = OrderReport_OrderReportFilterDTO.CreatorId;
            filter.CreatedAt = OrderReport_OrderReportFilterDTO.CreatedAt;
            filter.OrderCategoryId = OrderReport_OrderReportFilterDTO.OrderCategoryId;

            var info_vn = System.Globalization.CultureInfo.GetCultureInfo("vi-VN"); 
            var data = new List<OrderReportView>();
            var queryCustomerSalesOrder = DataContext.CustomerSalesOrder.Where(p => !p.DeletedAt.HasValue);
            var queryOrderAgent = DataContext.DirectSalesOrder.Where(p => !p.DeletedAt.HasValue);
            if (filter.Code.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.Code, filter.Code);
                queryOrderAgent = queryOrderAgent.Where(p => p.Code, filter.Code);
            }
            if (filter.Total.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.Total, filter.Total);
                queryOrderAgent = queryOrderAgent.Where(p => p.Total, filter.Total);
            }
            if (filter.PaymentStatusId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.OrderPaymentStatusId, filter.PaymentStatusId);
                queryOrderAgent = queryOrderAgent.Where(p => p.Id != p.Id);
            }
            if (filter.CompanyId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.Id != p.Id);
                queryOrderAgent = queryOrderAgent.Where(p => p.Id != p.Id);
            }
            if (filter.OpportunityId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.Id != p.Id);
                queryOrderAgent = queryOrderAgent.Where(p => p.Id != p.Id);
            }
            if (filter.OrganizationId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.OrganizationId, filter.OrganizationId);
                queryOrderAgent = queryOrderAgent.Where(p => p.OrganizationId, filter.OrganizationId);
            }
            if (filter.CreatorId.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.CreatorId, filter.CreatorId);
                queryOrderAgent = queryOrderAgent.Where(p => p.SaleEmployeeId, filter.CreatorId);
            }
            if (filter.CreatedAt.HasValue)
            {
                queryCustomerSalesOrder = queryCustomerSalesOrder.Where(p => p.CreatedAt, filter.CreatedAt);
                queryOrderAgent = queryOrderAgent.Where(p => p.CreatedAt, filter.CreatedAt);
            }

            var CustomerSalesOrderDAOs = queryCustomerSalesOrder.Select(p => new OrderReportView
            {
                Code = p.Code,
                OrderTypeName = "Đơn hàng bán lẻ",
                Total = p.Total,
                PaymentStatusName = p.OrderPaymentStatus == null ? "" : p.OrderPaymentStatus.Name,
                PaymentPercentage = p.CustomerSalesOrderPaymentHistories == null ? default(decimal) : p.CustomerSalesOrderPaymentHistories.Where(p => p.PaymentPercentage.HasValue).Sum(p => p.PaymentPercentage.Value),
                CustomerName = p.Customer == null ? "" : p.Customer.Name,
                CreatedAt = p.CreatedAt,
                OpportunityName = "",
                OrganizationName = p.Organization == null ? "" : p.Organization.Name,
                AppUserAssignedName = p.SalesEmployee == null ? "" : p.SalesEmployee.DisplayName,
            }).ToList();

            var OrderAgentDAOs = queryOrderAgent.Select(p => new OrderReportView
            {
                Code = p.Code,
                OrderTypeName = "Đơn hàng đại lý",
                Total = p.Total,
                PaymentStatusName = p.RequestState == null ? "" : p.RequestState.Name,
                PaymentPercentage = 0,
                CompanyName = "Đang cập nhật",
                CustomerName = p.BuyerStore == null ? "" : (p.BuyerStore == null ? "" : p.BuyerStore.Name),
                CreatedAt = p.CreatedAt,
                OpportunityName = "Đang cập nhật",
                OrganizationName = p.Organization == null ? "" : p.Organization.Name,
                AppUserAssignedName = p.SaleEmployee == null ? "" : p.SaleEmployee.DisplayName,
            }).ToList();

            if (filter.OrderCategoryId != null && filter.OrderCategoryId.Equal == Enums.OrderCategoryEnum.ORDER_CUSTOMER.Id)
            {
                data = CustomerSalesOrderDAOs.ToList();
            }
            else if (filter.OrderCategoryId != null && filter.OrderCategoryId.Equal == Enums.OrderCategoryEnum.ORDER_DIRECT.Id)
            {
                data = OrderAgentDAOs.ToList();
            }

            data = CustomerSalesOrderDAOs.Concat(OrderAgentDAOs)
                    .OrderByDescending(p => p.CreatedAt).ToList();

            var maxDate = data.Max(p => p.CreatedAt);
            var minDate = data.Min(p => p.CreatedAt);
            DateTime Start = filter.CreatedAt?.GreaterEqual == null ?
                minDate :
                filter.CreatedAt.GreaterEqual.Value;

            DateTime End = filter.CreatedAt?.LessEqual == null ?
                maxDate :
                filter.CreatedAt.LessEqual.Value;
            long STT = 0;
            foreach (var item in data)
            {
                item.STT = STT + 1;
                STT++;
            }
            string path = "Templates/OrderReport.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();

            Data.NOW = StaticParams.DateTimeNow.ToString("dd-MM-yyyy");
            Data.Start = Start.AddHours(CurrentContext.TimeZone).ToString("dd-MM-yyyy");
            Data.End = End.AddHours(CurrentContext.TimeZone).ToString("dd-MM-yyyy");
            Data.data = data;

            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "BaoCaoDonHang.xlsx");
        }
    }
}
public class OrderReportView
{
    public long STT { get; set; }
    public string Code { get; set; }
    public string OrderTypeName { get; set; }
    public decimal Total { get; set; }
    public string PaymentStatusName { get; set; }
    public decimal PaymentPercentage { get; set; }
    public string CompanyName { get; set; }
    public string CustomerName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string OpportunityName { get; set; }
    public string OrganizationName { get; set; }
    public string AppUserAssignedName { get; set; }

}