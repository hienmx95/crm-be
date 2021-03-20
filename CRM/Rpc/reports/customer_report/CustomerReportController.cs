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
using Microsoft.AspNetCore.Http;

namespace CRM.Rpc.reports.customer_report
{
    public partial class CustomerReportController : RpcController
    {
        private ICustomerTypeService CustomerTypeService;
        private ICurrentContext CurrentContext;
        private DataContext DataContext;
        public CustomerReportController(
           ICustomerTypeService CustomerTypeService,
           ICurrentContext CurrentContext,
           DataContext DataContext
      ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.CustomerTypeService = CustomerTypeService;
            this.CurrentContext = CurrentContext;
            this.DataContext = DataContext;
        }


        [Route(CustomerReportRoute.List), HttpPost]
        public async Task<ActionResult<object>> List([FromBody] CustomerReport_CustomerReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            var query = DataContext.Customer.Where(p => !p.DeletedAt.HasValue);
            //from q in DataContext.Customer select q;
            if (filter.Code != null)
                query = query.Where(p => p.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(p => p.Name, filter.Name);
            if (filter.CreatedAt != null)
                query = query.Where(p => p.CreatedAt, filter.CreatedAt);
            if (filter.CustomerTypeId != null)
                query = query.Where(p => p.CustomerTypeId, filter.CustomerTypeId);

            return query.Skip(filter.Skip).Take(filter.Take).Select(p => new
            {
                Code = p.Code,
                Name = p.Name,
                CustomerTypeName = p.CustomerType == null ? null : p.CustomerType.Name,
                CountTicket = p.Tickets.Count(),
                CountCallLog = DataContext.CallLog.Where(x =>
                x.EntityReferenceId == Enums.EntityReferenceEnum.CUSTOMER_RETAIL.Id
                || x.EntityReferenceId == Enums.EntityReferenceEnum.CUSTOMER_AGENT.Id
                || x.EntityReferenceId == Enums.EntityReferenceEnum.CUSTOMER_PROJECT.Id
                || x.EntityReferenceId == Enums.EntityReferenceEnum.CUSTOMER_EXPORT.Id
                  ).Count(),
                CountOpportunity = 0,
                CountOrderQuote = 0,
                CountOrder = DataContext.CustomerSalesOrder.Where(p => p.CustomerId == p.Id).Count()
                              + DataContext.DirectSalesOrder.Where(p => p.BuyerStoreId == p.Id).Count(),
                RevenueOrder = DataContext.CustomerSalesOrder.Where(p => p.CustomerId == p.Id).Sum(p => p.Total)
                              + DataContext.DirectSalesOrder.Where(p => p.BuyerStoreId == p.Id).Sum(p => p.Total),
                CountContract = p.Contracts.Count(),
                RevenueContract = p.Contracts.Sum(p => p.Total),
            }).ToList();
        }
        [Route(CustomerReportRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] CustomerReport_CustomerReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            var query = DataContext.Customer.Where(p => !p.DeletedAt.HasValue);
            //from q in DataContext.Customer select q;
            if (filter.Code != null)
                query = query.Where(p => p.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(p => p.Name, filter.Name);
            if (filter.CreatedAt != null)
                query = query.Where(p => p.CreatedAt, filter.CreatedAt);
            if (filter.CustomerTypeId != null)
                query = query.Where(p => p.CustomerTypeId, filter.CustomerTypeId);

            return query.Count();
        }
        [Route(CustomerReportRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] CustomerReport_CustomerReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            var info_vn = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            var query = DataContext.Customer.Where(p => !p.DeletedAt.HasValue);
            //from q in DataContext.Customer select q;
            if (filter.Code != null)
                query = query.Where(p => p.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(p => p.Name, filter.Name);
            if (filter.CreatedAt != null)
                query = query.Where(p => p.CreatedAt, filter.CreatedAt);
            if (filter.CustomerTypeId != null)
                query = query.Where(p => p.CustomerTypeId, filter.CustomerTypeId);
            var data = query.Select(p => new CustomerReportExport
            {
                Code = p.Code,
                Name = p.Name,
                CustomerTypeName = p.CustomerType == null ? null : p.CustomerType.Name,
                CountTicket = p.Tickets.Count().ToString(),
                CountCallLog = DataContext.CallLog.Where(x =>
                x.EntityReferenceId == Enums.EntityReferenceEnum.CUSTOMER_RETAIL.Id
                || x.EntityReferenceId == Enums.EntityReferenceEnum.CUSTOMER_AGENT.Id
                || x.EntityReferenceId == Enums.EntityReferenceEnum.CUSTOMER_PROJECT.Id
                || x.EntityReferenceId == Enums.EntityReferenceEnum.CUSTOMER_EXPORT.Id
                  ).Count().ToString(),
                CountOpportunity = "0",
                CountOrderQuote = "0",
                CountOrder = (DataContext.CustomerSalesOrder.Where(p => p.CustomerId == p.Id).Count()
                              + DataContext.DirectSalesOrder.Where(p => p.BuyerStoreId == p.Id).Count()).ToString(),
                RevenueOrder = (DataContext.CustomerSalesOrder.Where(p => p.CustomerId == p.Id).Sum(p => p.Total)
                              + DataContext.DirectSalesOrder.Where(p => p.BuyerStoreId == p.Id).Sum(p => p.Total)).ToString(),
                CountContract = p.Contracts.Count().ToString(),
                RevenueContract = p.Contracts.Sum(p => p.Total).ToString(),
                CreatedAt = p.CreatedAt
            }).ToList();

            var maxDate = data.Max(p => p.CreatedAt).HasValue ? data.Max(p => p.CreatedAt).Value : default(DateTime);
            var minDate = data.Min(p => p.CreatedAt).HasValue ? data.Min(p => p.CreatedAt).Value : default(DateTime);

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
            string path = "Templates/CustomerReport.xlsx";
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
            return File(output.ToArray(), "application/octet-stream", "BaoCaoKhachHang.xlsx");
        }
    }
}
public class CustomerReportExport
{
    public long STT { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string CustomerTypeName { get; set; }
    public string CountTicket { get; set; }
    public string CountCallLog { get; set; }
    public string CountOpportunity { get; set; }
    public string CountOrderQuote { get; set; }
    public string CountOrder { get; set; }
    public string RevenueOrder { get; set; }
    public string CountContract { get; set; }
    public string RevenueContract { get; set; }
    public DateTime? CreatedAt { get; set; }
}