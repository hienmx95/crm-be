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
using Microsoft.AspNetCore.Http;

namespace CRM.Rpc.reports.staff_report
{
    public partial class StaffReportController : RpcController
    {
        private IAppUserService AppUserService;
        private IOrganizationService OrganizationService;
        private ICurrentContext CurrentContext;
        private DataContext DataContext;
        public StaffReportController(
           IAppUserService AppUserService,
           IOrganizationService OrganizationService,
           ICurrentContext CurrentContext,
           DataContext DataContext
      ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.OrganizationService = OrganizationService;
            this.AppUserService = AppUserService;
            this.CurrentContext = CurrentContext;
            this.DataContext = DataContext;
        }

        [Route(StaffReportRoute.List), HttpPost]
        public async Task<ActionResult<object>> List([FromBody] StaffReport_StaffReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            var query = DataContext.AppUser.Where(p => !p.DeletedAt.HasValue);
            if (filter.Username.HasValue)
                query = query.Where(p => p.Username, filter.Username);
            if (filter.StaffId.HasValue)
                query = query.Where(p => p.Id, filter.StaffId);
            if (filter.OrganizationId.HasValue)
                query = query.Where(p => p.OrganizationId, filter.OrganizationId);
            if (filter.CreatedAt.HasValue)
                query = query.Where(p => p.CreatedAt, filter.CreatedAt);

            return query.Select(p => new
            {
                Username = p.Username,
                DisplayName = p.DisplayName,
                OrganizationName = p.Organization == null ? "" : p.Organization.Name,
                //CountLead = p.CustomerLeadCreators == null ? 0 : p.CustomerLeadCreators.Where(x => !x.DeletedAt.HasValue).Count(),
                //CountOpportunityComplete = p.OpportunityCreators == null ? 0 : p.OpportunityCreators
                //                            .Where(x => !x.DeletedAt.HasValue)
                //                            .Where(x => x.CreatorId == p.Id).Count(),
                //Ratio = (
                //            p.CustomerLeadCreators.Count() == 0
                //            ) ? 0 :
                //            (p.OpportunityCreators == null ? 0.00 : p.OpportunityCreators.Where(x => x.CreatorId == p.Id && x.CustomerLeadId.HasValue).Count()) * 100
                //            / p.CustomerLeadCreators.Count()
                //            ,
                CountContact = p.ContractAppUsers == null ? 0 : p.ContractAppUsers.Where(x => !x.DeletedAt.HasValue).Count(),
                CountOrder = (p.CustomerSalesOrderSalesEmployees == null ? 0 : p.CustomerSalesOrderSalesEmployees.Where(x => !x.DeletedAt.HasValue).Count())
                            + (p.DirectSalesOrderSaleEmployees == null ? 0 : p.DirectSalesOrderSaleEmployees.Where(x => !x.DeletedAt.HasValue).Count()),
                RevenueOrder = (p.CustomerSalesOrderSalesEmployees == null ? 0 : p.CustomerSalesOrderSalesEmployees.Where(x => !x.DeletedAt.HasValue).Sum(p => p.Total))
                                + (p.DirectSalesOrderSaleEmployees == null ? 0 : p.DirectSalesOrderSaleEmployees.Where(x => !x.DeletedAt.HasValue).Sum(p => p.Total)),
                CountContract = p.ContractAppUsers == null ? 0 : p.ContractAppUsers.Where(x => !x.DeletedAt.HasValue).Count(),
                RevenueContract = p.ContractAppUsers == null ? 0 : p.ContractAppUsers.Where(x => !x.DeletedAt.HasValue).Sum(p => p.Total),
                TotalTicket = p.TicketUsers == null ? 0 : p.TicketUsers.Where(x => !x.DeletedAt.HasValue).Count(),
                CountTicketClosed = p.TicketUsers == null ? 0 : p.TicketUsers.Where(x => !x.DeletedAt.HasValue).Where(p => p.TicketStatusId == Enums.TicketStatusEnum.CLOSED.Id).Count(),
                CountTicketFail = p.TicketUsers == null ? 0 : p.TicketUsers.Where(x => !x.DeletedAt.HasValue).Where(p => p.TicketStatusId == Enums.SLAStatusEnum.Fail.Id).Count(),
            }).ToList();
        }

        [Route(StaffReportRoute.Total), HttpPost]
        public async Task<ActionResult<object>> Total([FromBody] StaffReport_StaffReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            var query = DataContext.AppUser.Where(p => !p.DeletedAt.HasValue);
            if (filter.Username.HasValue)
                query = query.Where(p => p.Username, filter.Username);
            if (filter.StaffId.HasValue)
                query = query.Where(p => p.Id, filter.StaffId);
            if (filter.OrganizationId.HasValue)
                query = query.Where(p => p.OrganizationId, filter.OrganizationId);
            if (filter.CreatedAt.HasValue)
                query = query.Where(p => p.CreatedAt, filter.CreatedAt);

            var data = query.Select(p => new
            {
                Username = p.Username,
                DisplayName = p.DisplayName,
                OrganizationName = p.Organization == null ? "" : p.Organization.Name,
                //CountLead = p.CustomerLeadCreators == null ? 0 : p.CustomerLeadCreators.Where(x => !x.DeletedAt.HasValue).Count(),
                //CountOpportunityComplete = p.OpportunityCreators == null ? 0 : p.OpportunityCreators
                //                            .Where(x => !x.DeletedAt.HasValue)
                //                            .Where(x => x.CreatorId == p.Id).Count(),
                //Ratio = (
                //            p.CustomerLeadCreators.Count() == 0
                //            ) ? 0 :
                //            (p.OpportunityCreators == null ? 0.00 : p.OpportunityCreators.Where(x => x.CreatorId == p.Id && x.CustomerLeadId.HasValue).Count()) * 100
                //            / p.CustomerLeadCreators.Count()
                //            ,
                CountContact = p.ContractAppUsers == null ? 0 : p.ContractAppUsers.Where(x => !x.DeletedAt.HasValue).Count(),
                CountOrder = (p.CustomerSalesOrderSalesEmployees == null ? 0 : p.CustomerSalesOrderSalesEmployees.Where(x => !x.DeletedAt.HasValue).Count())
                            + (p.DirectSalesOrderSaleEmployees == null ? 0 : p.DirectSalesOrderSaleEmployees.Where(x => !x.DeletedAt.HasValue).Count()),
                RevenueOrder = (p.CustomerSalesOrderSalesEmployees == null ? 0 : p.CustomerSalesOrderSalesEmployees.Where(x => !x.DeletedAt.HasValue).Sum(p => p.Total))
                                + (p.DirectSalesOrderSaleEmployees == null ? 0 : p.DirectSalesOrderSaleEmployees.Where(x => !x.DeletedAt.HasValue).Sum(p => p.Total)),
                CountContract = p.ContractAppUsers == null ? 0 : p.ContractAppUsers.Where(x => !x.DeletedAt.HasValue).Count(),
                RevenueContract = p.ContractAppUsers == null ? 0 : p.ContractAppUsers.Where(x => !x.DeletedAt.HasValue).Sum(p => p.Total),
                TotalTicket = p.TicketUsers == null ? 0 : p.TicketUsers.Where(x => !x.DeletedAt.HasValue).Count(),
                CountTicketClosed = p.TicketUsers == null ? 0 : p.TicketUsers.Where(x => !x.DeletedAt.HasValue).Where(p => p.TicketStatusId == Enums.TicketStatusEnum.CLOSED.Id).Count(),
                CountTicketFail = p.TicketUsers == null ? 0 : p.TicketUsers.Where(x => !x.DeletedAt.HasValue).Where(p => p.TicketStatusId == Enums.SLAStatusEnum.Fail.Id).Count(),
            }).ToList();
            return new
            {
                //TotalLead = data.Sum(p => p.CountLead),
                //TotalOpportunity = data.Sum(p => p.CountOpportunityComplete),
                TotalContact = data.Sum(p => p.CountContact),
                TotalOrder = data.Sum(p => p.CountOrder),
                TotalRevenueOrder = data.Sum(p => p.RevenueOrder),
                TotalContract = data.Sum(p => p.CountContract),
                TotalRevenueContract = data.Sum(p => p.RevenueContract),
                TotalTicket = data.Sum(p => p.TotalTicket),
                TotalTicketClosed = data.Sum(p => p.CountTicketClosed),
                TotalTicketFail = data.Sum(p => p.CountTicketFail),
            };
        }


        [Route(StaffReportRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] StaffReport_StaffReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            var query = DataContext.AppUser.Where(p => !p.DeletedAt.HasValue);
            if (filter.Username.HasValue)
                query = query.Where(p => p.Username, filter.Username);
            if (filter.StaffId.HasValue)
                query = query.Where(p => p.Id, filter.StaffId);
            if (filter.OrganizationId.HasValue)
                query = query.Where(p => p.OrganizationId, filter.OrganizationId);
            if (filter.CreatedAt.HasValue)
                query = query.Where(p => p.CreatedAt, filter.CreatedAt);

            return query.Count();
        }

        [Route(StaffReportRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] StaffReport_StaffReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            var info_vn = System.Globalization.CultureInfo.GetCultureInfo("vi-VN"); 

            var query = DataContext.AppUser.Where(p => !p.DeletedAt.HasValue);
            if (filter.Username.HasValue)
                query = query.Where(p => p.Username, filter.Username);
            if (filter.StaffId.HasValue)
                query = query.Where(p => p.Id, filter.StaffId);
            if (filter.OrganizationId.HasValue)
                query = query.Where(p => p.OrganizationId, filter.OrganizationId);
            if (filter.CreatedAt.HasValue)
                query = query.Where(p => p.CreatedAt, filter.CreatedAt);

            var data = query.Select(p => new StaffReportExport
            {
                Username = p.Username,
                DisplayName = p.DisplayName,
                OrganizationName = p.Organization == null ? "" : p.Organization.Name,
                //CountLead = p.CustomerLeadCreators == null ? 0 : p.CustomerLeadCreators.Where(x => !x.DeletedAt.HasValue).Count(),
                //CountOpportunityComplete = p.CustomerLeadCreators == null ? 0 : p.CustomerLeadCreators
                //                            .Where(x => !x.DeletedAt.HasValue)
                //                            .Where(x => x.CreatorId == p.Id).Count(),
                //Ratio = (
                //            p.CustomerLeadCreators.Count() == 0
                //            ) ? 0 :
                //            (p.OpportunityCreators == null ? 0.00 : p.OpportunityCreators.Where(x => x.CreatorId == p.Id && x.CustomerLeadId.HasValue).Count()) * 100
                //            / p.CustomerLeadCreators.Count()
                //            ,
                CountContact = p.ContractAppUsers == null ? 0 : p.ContractAppUsers.Where(x => !x.DeletedAt.HasValue).Count(),
                CountOrder = (p.CustomerSalesOrderSalesEmployees == null ? 0 : p.CustomerSalesOrderSalesEmployees.Where(x => !x.DeletedAt.HasValue).Count())
                            + (p.DirectSalesOrderSaleEmployees == null ? 0 : p.DirectSalesOrderSaleEmployees.Where(x => !x.DeletedAt.HasValue).Count()),
                RevenueOrder = (p.CustomerSalesOrderSalesEmployees == null ? 0 : p.CustomerSalesOrderSalesEmployees.Where(x => !x.DeletedAt.HasValue).Sum(p => p.Total))
                                + (p.DirectSalesOrderSaleEmployees == null ? 0 : p.DirectSalesOrderSaleEmployees.Where(x => !x.DeletedAt.HasValue).Sum(p => p.Total)),
                CountContract = p.ContractAppUsers == null ? 0 : p.ContractAppUsers.Where(x => !x.DeletedAt.HasValue).Count(),
                RevenueContract = p.ContractAppUsers == null ? 0 : p.ContractAppUsers.Where(x => !x.DeletedAt.HasValue).Sum(p => p.Total),
                TotalTicket = p.TicketUsers == null ? 0 : p.TicketUsers.Where(x => !x.DeletedAt.HasValue).Count(),
                CountTicketClosed = p.TicketUsers == null ? 0 : p.TicketUsers.Where(x => !x.DeletedAt.HasValue).Where(p => p.TicketStatusId == Enums.TicketStatusEnum.CLOSED.Id).Count(),
                CountTicketFail = p.TicketUsers == null ? 0 : p.TicketUsers.Where(x => !x.DeletedAt.HasValue).Where(p => p.TicketStatusId == Enums.SLAStatusEnum.Fail.Id).Count(),
                CreatedAt = p.CreatedAt
            }).ToList();

            var maxDate = data.Max(p => p.CreatedAt);
            var minDate = data.Min(p => p.CreatedAt);
            DateTime Start = filter.CreatedAt?.GreaterEqual == null ?
                minDate :
                filter.CreatedAt.GreaterEqual.Value;
            DateTime End = filter.CreatedAt?.LessEqual == null ?
                maxDate :
                filter.CreatedAt.LessEqual.Value;

            var total = new StaffTotalReportExport
            {
                TotalLead = data.Sum(p => p.CountLead),
                TotalOpportunity = data.Sum(p => p.CountOpportunityComplete),
                TotalCustomer = data.Sum(p => p.CountCustomer),
                TotalCompany = data.Sum(p => p.CountCompany),
                TotalContact = data.Sum(p => p.CountContact),
                TotalOrder = data.Sum(p => p.CountOrder),
                TotalRevenueOrder = data.Sum(p => p.RevenueOrder),
                TotalContract = data.Sum(p => p.CountContract),
                TotalRevenueContract = data.Sum(p => p.RevenueContract),
                TotalTicket = data.Sum(p => p.TotalTicket),
                TotalTicketClosed = data.Sum(p => p.CountTicketClosed),
                TotalTicketFail = data.Sum(p => p.CountTicketFail),
            };

            long STT = 0;
            foreach (var item in data)
            {
                item.STT = STT + 1;
                STT++;
            }
            string path = "Templates/StaffReport.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();

            Data.NOW = StaticParams.DateTimeNow.ToString("dd-MM-yyyy");
            Data.Start = Start.AddHours(CurrentContext.TimeZone).ToString("dd-MM-yyyy");
            Data.End = End.AddHours(CurrentContext.TimeZone).ToString("dd-MM-yyyy");
            Data.data = data;
            Data.total = total;

            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "BaoCaoNhanVien.xlsx");
        }
    }
}
public class StaffReportExport
{
    public long STT { get; set; }
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string OrganizationName { get; set; }
    public int CountLead { get; set; }
    public Double Ratio { get; set; }
    public int CountOpportunityComplete { get; set; }
    public int CountCustomer { get; set; }
    public int CountOrder { get; set; }
    public decimal RevenueOrder { get; set; }
    public int CountCompany { get; set; }
    public int CountContact { get; set; }
    public int CountContract { get; set; }
    public decimal? RevenueContract { get; set; }
    public int TotalTicket { get; set; }
    public int CountTicketClosed { get; set; }
    public int CountTicketFail { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class StaffTotalReportExport
{
    public int TotalLead { get; set; }
    public int TotalOpportunity { get; set; }
    public int TotalCustomer { get; set; }
    public int TotalCompany { get; set; }
    public int TotalContact { get; set; }
    public int TotalOrder { get; set; }
    public decimal TotalRevenueOrder { get; set; }
    public int TotalContract { get; set; }
    public decimal? TotalRevenueContract { get; set; }
    public int TotalTicket { get; set; }
    public int TotalTicketClosed { get; set; }
    public int TotalTicketFail { get; set; }
}