using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Dynamic;
using CRM.Entities;
using CRM.Services.MTicket;
using CRM.Services.MContractType;
using CRM.Services.MCompany;
using CRM.Services.MOpportunity;
using CRM.Services.MOrganization;
using CRM.Services.MAppUser;
using CRM.Models;
using Microsoft.AspNetCore.Http;

namespace CRM.Rpc.reports.contract_report
{
    public partial class ContractReportController : RpcController
    {
        private IContractTypeService ContractTypeService;
        private ICompanyService CompanyService;
        private IOpportunityService OpportunityService;
        private IOrganizationService OrganizationService;
        private IAppUserService AppUserService;
        private ICurrentContext CurrentContext;
        private DataContext DataContext;

        public ContractReportController(
            IContractTypeService ContractTypeService,
            ICompanyService CompanyService,
            IOpportunityService OpportunityService,
            IOrganizationService OrganizationService,
            IAppUserService AppUserService,
            ICurrentContext CurrentContext,
            DataContext DataContext
        ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.CompanyService = CompanyService;
            this.ContractTypeService = ContractTypeService;
            this.OpportunityService = OpportunityService;
            this.OrganizationService = OrganizationService;
            this.AppUserService = AppUserService;
            this.DataContext = DataContext;
            this.CurrentContext = CurrentContext;
        }

        [Route(ContractReportRoute.List), HttpPost]
        public async Task<ActionResult<object>> List([FromBody] ContractReport_ContractReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            var query = DataContext.Contract.Where(p => !p.DeletedAt.HasValue).AsQueryable();
            if (filter.Code.HasValue)
                query = query.Where(p => p.Code, filter.Code);
            if (filter.ContractTypeId.HasValue)
                query = query.Where(p => p.ContractTypeId, filter.ContractTypeId);
            if (filter.TotalValue.HasValue)
                query = query.Where(p => p.TotalValue, filter.TotalValue);
            if (filter.CompanyId.HasValue)
                query = query.Where(p => p.CompanyId, filter.CompanyId);
            if (filter.OpportunityId.HasValue)
                query = query.Where(p => p.OpportunityId, filter.OpportunityId);
            if (filter.OrganizationId.HasValue)
                query = query.Where(p => p.OrganizationId, filter.OrganizationId);
            if (filter.AppUserId.HasValue)
                query = query.Where(p => p.AppUserId, filter.AppUserId);
            if (filter.ExpirationDate.HasValue)
                query = query.Where(p => p.ExpirationDate, filter.ExpirationDate);
            if (filter.CreatedAt.HasValue)
                query = query.Where(p => p.CreatedAt, filter.CreatedAt);
            if (filter.PaymentPercentage.HasValue)
            {
                query = query.Where(p => p.ContractPaymentHistories.Sum(p => p.PaymentPercentage) >= filter.PaymentPercentage.GreaterEqual
                                        && p.ContractPaymentHistories.Sum(p => p.PaymentPercentage) <= filter.PaymentPercentage.LessEqual);
            }
            return query.Select(p => new
            {
                Id = p.Id,
                Code = p.Code,
                ContractTypeName = p.ContractType == null ? "" : p.ContractType.Name,
                TotalValue = p.TotalValue,
                ValidityDate = p.ValidityDate,
                ExpirationDate = p.ExpirationDate,
                PaymentAmount = p.ContractPaymentHistories.Sum(p => p.PaymentAmount),
                PaymentPercentage = p.ContractPaymentHistories.Sum(p => p.PaymentPercentage),
                CompanyName = p.Company == null ? "" : p.Company.Name,
                OpportunityName = p.Opportunity == null ? "" : p.Opportunity.Name,
                OrganizationName = p.Organization == null ? "" : p.Organization.Name,
                AppUserName = p.AppUser == null ? "" : p.AppUser.DisplayName,
            }).Skip(filter.Skip).Take(filter.Take).ToList();
        }

        [Route(ContractReportRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] ContractReport_ContractReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            var query = DataContext.Contract.Where(p => !p.DeletedAt.HasValue).AsQueryable();
            if (filter.Code.HasValue)
                query = query.Where(p => p.Code, filter.Code);
            if (filter.ContractTypeId.HasValue)
                query = query.Where(p => p.ContractTypeId, filter.ContractTypeId);
            if (filter.TotalValue.HasValue)
                query = query.Where(p => p.TotalValue, filter.TotalValue);
            if (filter.CompanyId.HasValue)
                query = query.Where(p => p.CompanyId, filter.CompanyId);
            if (filter.OpportunityId.HasValue)
                query = query.Where(p => p.OpportunityId, filter.OpportunityId);
            if (filter.OrganizationId.HasValue)
                query = query.Where(p => p.OrganizationId, filter.OrganizationId);
            if (filter.AppUserId.HasValue)
                query = query.Where(p => p.AppUserId, filter.AppUserId);
            if (filter.ExpirationDate.HasValue)
                query = query.Where(p => p.ExpirationDate, filter.ExpirationDate);
            if (filter.CreatedAt.HasValue)
                query = query.Where(p => p.CreatedAt, filter.CreatedAt);
            if (filter.PaymentPercentage.HasValue)
            {
                query = query.Where(p => p.ContractPaymentHistories.Sum(p => p.PaymentPercentage) >= filter.PaymentPercentage.GreaterEqual
                                        && p.ContractPaymentHistories.Sum(p => p.PaymentPercentage) <= filter.PaymentPercentage.LessEqual);
            }
            return query.Select(p => new
            {
                Id = p.Id,
                Code = p.Code,
                ContractTypeName = p.ContractType == null ? "" : p.ContractType.Name,
                TotalValue = p.TotalValue,
                ValidityDate = p.ValidityDate,
                ExpirationDate = p.ExpirationDate,
                PaymentAmount = p.ContractPaymentHistories.Sum(p => p.PaymentAmount),
                PaymentPercentage = p.ContractPaymentHistories.Sum(p => p.PaymentPercentage),
                CompanyName = p.Company == null ? "" : p.Company.Name,
                OpportunityName = p.Opportunity == null ? "" : p.Opportunity.Name,
                OrganizationName = p.Organization == null ? "" : p.Organization.Name,
                AppUserName = p.AppUser == null ? "" : p.AppUser.DisplayName,
            }).Count();
        }

        [Route(ContractReportRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] ContractReport_ContractReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            var query = DataContext.Contract.Where(p => !p.DeletedAt.HasValue).AsQueryable();
            if (filter.Code.HasValue)
                query = query.Where(p => p.Code, filter.Code);
            if (filter.ContractTypeId.HasValue)
                query = query.Where(p => p.ContractTypeId, filter.ContractTypeId);
            if (filter.TotalValue.HasValue)
                query = query.Where(p => p.TotalValue, filter.TotalValue);
            if (filter.CompanyId.HasValue)
                query = query.Where(p => p.CompanyId, filter.CompanyId);
            if (filter.OpportunityId.HasValue)
                query = query.Where(p => p.OpportunityId, filter.OpportunityId);
            if (filter.OrganizationId.HasValue)
                query = query.Where(p => p.OrganizationId, filter.OrganizationId);
            if (filter.AppUserId.HasValue)
                query = query.Where(p => p.AppUserId, filter.AppUserId);
            if (filter.ExpirationDate.HasValue)
                query = query.Where(p => p.ExpirationDate, filter.ExpirationDate);
            if (filter.CreatedAt.HasValue)
                query = query.Where(p => p.CreatedAt, filter.CreatedAt);
            if (filter.PaymentPercentage.HasValue)
            {
                query = query.Where(p => p.ContractPaymentHistories.Sum(p => p.PaymentPercentage) >= filter.PaymentPercentage.GreaterEqual
                                        && p.ContractPaymentHistories.Sum(p => p.PaymentPercentage) <= filter.PaymentPercentage.LessEqual);
            }
            int STT = 1;
            List<ContractReportExport> data = new List<ContractReportExport>();
            foreach (var p in query)
            {
                var ContractReportExport = new ContractReportExport();
                ContractReportExport.STT = (STT + 1).ToString();
                ContractReportExport.Code = p.Code;
                ContractReportExport.CreatedAt = p.CreatedAt;
                ContractReportExport.ContractTypeName = p.ContractType == null ? "" : p.ContractType.Name;
                ContractReportExport.TotalValue = string.Format(System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), "{0:c}", p.TotalValue);
                ContractReportExport.ValidityDate = p.ValidityDate.ToString("dd-MM-yyyy");
                ContractReportExport.ExpirationDate = p.ExpirationDate.ToString("dd-MM-yyyy");
                ContractReportExport.PaymentAmount = string.Format(System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), "{0:c}", p.ContractPaymentHistories.Sum(p => p.PaymentAmount));
                ContractReportExport.PaymentPercentage = p.ContractPaymentHistories.Sum(p => p.PaymentPercentage);
                ContractReportExport.CompanyName = p.Company == null ? "" : p.Company.Name;
                ContractReportExport.OpportunityName = p.Opportunity == null ? "" : p.Opportunity.Name;
                ContractReportExport.OrganizationName = p.Organization == null ? "" : p.Organization.Name;
                ContractReportExport.AppUserName = p.AppUser == null ? "" : p.AppUser.DisplayName;
                data.Add(ContractReportExport);
            }

            var maxDate = data.Max(p => p.CreatedAt).HasValue ? data.Max(p => p.CreatedAt).Value : default(DateTime);
            var minDate = data.Min(p => p.CreatedAt).HasValue ? data.Min(p => p.CreatedAt).Value : default(DateTime);
            DateTime Start = filter.CreatedAt?.GreaterEqual == null ?
                minDate :
                filter.CreatedAt.GreaterEqual.Value;

            DateTime End = filter.CreatedAt?.LessEqual == null ?
                maxDate :
                filter.CreatedAt.LessEqual.Value;

            string path = "Templates/ContractReport.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            Data.Start = Start.AddHours(CurrentContext.TimeZone).ToString("dd-MM-yyyy");
            Data.End = End.AddHours(CurrentContext.TimeZone).ToString("dd-MM-yyyy");
            Data.data = data;
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "BaoCaoTicket.xlsx");
        }
    }
}

public class ContractReportExport
{
    public string STT { get; set; }
    public string Code { get; set; }
    public string ContractTypeName { get; set; }
    public string TotalValue { get; set; }
    public string ValidityDate { get; set; }
    public string ExpirationDate { get; set; }
    public string PaymentAmount { get; set; }
    public decimal? PaymentPercentage { get; set; }
    public string CompanyName { get; set; }
    public string OpportunityName { get; set; }
    public string OrganizationName { get; set; }
    public string AppUserName { get; set; }
    public DateTime? CreatedAt { get; set; }
}