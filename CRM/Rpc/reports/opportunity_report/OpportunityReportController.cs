using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Helpers;
using CRM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using System.Dynamic;
using CRM.Entities;
using CRM.Services.MOpportunity;
using CRM.Services.MAppUser;
using CRM.Models;
using CRM.Services.MCompany;
using CRM.Services.MSaleStage;
using CRM.Services.MProbability;
using CRM.Services.MProduct;
using CRM.Services.MOrganization;

namespace CRM.Rpc.reports.opportunity_report
{
    public partial class OpportunityReportController : RpcController
    {
        private IOpportunityService Opportunitieservice;
        private IAppUserService AppUserService;
        private ICompanyService CompanyService;
        private IOrganizationService OrganizationService;
        private ISaleStageService SaleStageService;
        private IProbabilityService ProbabilityService;
        private IItemService ItemService;
        private ICurrentContext CurrentContext;
        private DataContext DataContext;


        public OpportunityReportController(
            IOpportunityService Opportunitieservice,
            IAppUserService AppUserService,
            ICompanyService CompanyService,
            IOrganizationService OrganizationService,
            ISaleStageService SaleStageService,
            IProbabilityService ProbabilityService,
            IItemService ItemService,
            ICurrentContext CurrentContext,
            DataContext DataContext
      ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.Opportunitieservice = Opportunitieservice;
            this.AppUserService = AppUserService;
            this.CompanyService = CompanyService;
            this.OrganizationService = OrganizationService;
            this.SaleStageService = SaleStageService;
            this.ProbabilityService = ProbabilityService;
            this.ItemService = ItemService;
            this.CurrentContext = CurrentContext;
            this.DataContext = DataContext;
        }

        [Route(OpportunityReportRoute.List), HttpPost]
        public async Task<ActionResult<OpportunityReport_OpportunityReportDTO>> List([FromBody] OpportunityReport_OpportunityReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            List<long> ItemIds = new List<long>();

            // Lấy danh sách Opportunity
            var query = from t1 in DataContext.Opportunity select t1;
            if (filter.OpportunityName != null)
            {
                query = query.Where(p => p.Name, filter.OpportunityName);
            }
            if (filter.OpportunityAmount != null)
            {
                query = query.Where(p => p.Amount, filter.OpportunityAmount);
            }
            if (filter.OpportunityItemId != null)
            {
                query = query.Where(p => p.Amount, filter.OpportunityAmount);
            }
            if (filter.OpportunityCompanyId != null)
            {
                query = query.Where(p => p.CompanyId, filter.OpportunityCompanyId);
            }
            if (filter.OpportunitySaleStageId != null)
            {
                query = query.Where(p => p.SaleStageId, filter.OpportunitySaleStageId);
            }
            if (filter.OpportunityProbabilityId != null)
            {
                query = query.Where(p => p.ProbabilityId, filter.OpportunityProbabilityId);
            }
            if (filter.OpportunityClosingDate != null)
            {
                query = query.Where(p => p.ClosingDate, filter.OpportunityClosingDate);
            }
            if (filter.OpportunityCreatedAt != null)
            {
                query = query.Where(p => p.CreatedAt, filter.OpportunityCreatedAt);
            }
            if (filter.AppUserId != null)
            {
                query = query.Where(p => p.AppUserId, filter.AppUserId);
            }
            //Tìm kiếm theo OpportunityItemId
            if (filter.OpportunityItemId != null)
            {
                if (filter.OpportunityItemId.Equal.HasValue)
                {
                    query = from q in query
                            join ar in DataContext.OpportunityItemMapping on q.Id equals ar.OpportunityId
                            where ar.ItemId == filter.OpportunityItemId.Equal.Value
                            select q;
                }
            }
            var data = query.Distinct().OrderBy(p => p.ClosingDate).ThenBy(p => p.Name).Skip(filter.Skip).Take(filter.Take).Select(p => new OpportunityReport_OpportunityDTO
            {
                Id = p.Id,
                Name = p.Name,
                ProbabilityName = p.Probability == null ? "" : p.Probability.Name,
                Amount = p.Amount == null ? "" : p.Amount.Value.ToString(),
                ClosingDate = p.ClosingDate,
                SaleStageName = p.SaleStage == null ? "" : p.SaleStage.Name,
                CompanyName = p.Company == null ? "" : p.Company.Name,
                CountItem = p.OpportunityItemMappings == null ? "" : p.OpportunityItemMappings.Count().ToString(),
                TotalRevenueOfItem = p.OpportunityItemMappings == null ? "" : p.OpportunityItemMappings.Sum(p => (p.RequestQuantity * p.SalePrice)).ToString(),
                AppUserName = p.AppUser == null ? "" : p.AppUser.DisplayName,
            }).ToList();
            OpportunityReport_OpportunityReportDTO OpportunityReport_OpportunityReportDTO = new OpportunityReport_OpportunityReportDTO();
            OpportunityReport_OpportunityReportDTO.Opportunities = data;

            return OpportunityReport_OpportunityReportDTO;
        }
        [Route(OpportunityReportRoute.Total), HttpPost]
        public async Task<ActionResult<OpportunityReport_OpportunityTotalDTO>> Total([FromBody] OpportunityReport_OpportunityReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            List<long> ItemIds = new List<long>();

            // Lấy danh sách Opportunity
            var query = from t1 in DataContext.Opportunity select t1;
            if (filter.OpportunityName != null)
            {
                query = query.Where(p => p.Name, filter.OpportunityName);
            }
            if (filter.OpportunityAmount != null)
            {
                query = query.Where(p => p.Amount, filter.OpportunityAmount);
            }
            if (filter.OpportunityItemId != null)
            {
                query = query.Where(p => p.Amount, filter.OpportunityAmount);
            }
            if (filter.OpportunityCompanyId != null)
            {
                query = query.Where(p => p.CompanyId, filter.OpportunityCompanyId);
            }
            if (filter.OpportunitySaleStageId != null)
            {
                query = query.Where(p => p.SaleStageId, filter.OpportunitySaleStageId);
            }
            if (filter.OpportunityProbabilityId != null)
            {
                query = query.Where(p => p.ProbabilityId, filter.OpportunityProbabilityId);
            }
            if (filter.OpportunityClosingDate != null)
            {
                query = query.Where(p => p.ClosingDate, filter.OpportunityClosingDate);
            }
            if (filter.OpportunityCreatedAt != null)
            {
                query = query.Where(p => p.CreatedAt, filter.OpportunityCreatedAt);
            }
            if (filter.AppUserId != null)
            {
                query = query.Where(p => p.AppUserId, filter.AppUserId);
            }
            //Tìm kiếm theo OpportunityItemId
            if (filter.OpportunityItemId != null)
            {
                if (filter.OpportunityItemId.Equal.HasValue)
                {
                    query = from q in query
                            join ar in DataContext.OpportunityItemMapping on q.Id equals ar.OpportunityId
                            where ar.ItemId == filter.OpportunityItemId.Equal.Value
                            select q;
                }
            }
            var data = query.Distinct().OrderBy(p => p.ClosingDate).ThenBy(p => p.Name)
                //.Skip(filter.Skip)
                //.Take(filter.Take)
                .Select(p => new OpportunityReport_OpportunityDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    ProbabilityName = p.Probability == null ? "" : p.Probability.Name,
                    Amount = p.Amount == null ? "" : p.Amount.Value.ToString(),
                    ClosingDate = p.ClosingDate,
                    SaleStageName = p.SaleStage == null ? "" : p.SaleStage.Name,
                    CompanyName = p.Company == null ? "" : p.Company.Name,
                    CountItem = p.OpportunityItemMappings == null ? "" : p.OpportunityItemMappings.Count().ToString(),
                    TotalRevenueOfItem = p.OpportunityItemMappings == null ? "" : p.OpportunityItemMappings.Sum(p => (p.RequestQuantity * p.SalePrice)).ToString(),
                    OpportunityItemMappings = p.OpportunityItemMappings == null ? null : p.OpportunityItemMappings.Select(p => new OpportunityReport_OpportunityItemMappingDTO
                    {
                        OpportunityId = p.OpportunityId,
                        ItemId = p.ItemId,
                        UnitOfMeasureId = p.UnitOfMeasureId,
                        Quantity = p.Quantity,
                        DiscountPercentage = p.DiscountPercentage,
                        RequestQuantity = p.RequestQuantity,
                        PrimaryPrice = p.PrimaryPrice,
                        SalePrice = p.SalePrice,
                        Discount = p.Discount,
                        VAT = p.VAT,
                        VATOther = p.VATOther,
                        Amount = p.Amount,
                        Factor = p.Factor,
                        Item = p.Item == null ? null : new OpportunityReport_ItemDTO
                        {
                            Id = p.Item.Id,
                            Name = p.Item.Name
                        },
                        UnitOfMeasure = p.UnitOfMeasure == null ? null : new OpportunityReport_UnitOfMeasureDTO
                        {
                            Name = p.UnitOfMeasure.Name
                        }
                    }).ToList()
                }).ToList();

            // Tính tổng số sản phẩm khác nhau
            foreach (var p in data)
            {
                var ids = p.OpportunityItemMappings.Select(p => p.ItemId);
                ItemIds.AddRange(ids);
            }
            OpportunityReport_OpportunityTotalDTO OpportunityReport_OpportunityTotalDTO = new OpportunityReport_OpportunityTotalDTO();
            OpportunityReport_OpportunityTotalDTO.TotalAmount = data.Sum(p => string.IsNullOrEmpty(p.Amount) ? 0 : decimal.Parse(p.Amount)).ToString();
            OpportunityReport_OpportunityTotalDTO.TotalItem = ItemIds.Distinct().Count().ToString();
            OpportunityReport_OpportunityTotalDTO.TotalRevenue = data.Sum(p => string.IsNullOrEmpty(p.TotalRevenueOfItem) ? 0 : decimal.Parse(p.TotalRevenueOfItem)).ToString();
            return OpportunityReport_OpportunityTotalDTO;
        }


        [Route(OpportunityReportRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] OpportunityReport_OpportunityReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            List<long> ItemIds = new List<long>();

            // Lấy danh sách Opportunity
            var query = from t1 in DataContext.Opportunity select t1;
            if (filter.OpportunityName != null)
            {
                query = query.Where(p => p.Name, filter.OpportunityName);
            }
            if (filter.OpportunityAmount != null)
            {
                query = query.Where(p => p.Amount, filter.OpportunityAmount);
            }
            if (filter.OpportunityItemId != null)
            {
                query = query.Where(p => p.Amount, filter.OpportunityAmount);
            }
            if (filter.OpportunityCompanyId != null)
            {
                query = query.Where(p => p.CompanyId, filter.OpportunityCompanyId);
            }
            if (filter.OpportunitySaleStageId != null)
            {
                query = query.Where(p => p.SaleStageId, filter.OpportunitySaleStageId);
            }
            if (filter.OpportunityProbabilityId != null)
            {
                query = query.Where(p => p.ProbabilityId, filter.OpportunityProbabilityId);
            }
            if (filter.OpportunityClosingDate != null)
            {
                query = query.Where(p => p.ClosingDate, filter.OpportunityClosingDate);
            }
            if (filter.OpportunityCreatedAt != null)
            {
                query = query.Where(p => p.CreatedAt, filter.OpportunityCreatedAt);
            }
            //Tìm kiếm theo OpportunityItemId
            if (filter.OpportunityItemId != null)
            {
                if (filter.OpportunityItemId.Equal.HasValue)
                {
                    query = from q in query
                            join ar in DataContext.OpportunityItemMapping on q.Id equals ar.OpportunityId
                            where ar.ItemId == filter.OpportunityItemId.Equal.Value
                            select q;
                }
            }
            var data = query.Distinct().OrderBy(p => p.ClosingDate).ThenBy(p => p.Name).Skip(filter.Skip).Take(filter.Take).Select(p => new OpportunityReport_OpportunityDTO
            {
                Id = p.Id,
                Name = p.Name,
                ProbabilityName = p.Probability == null ? "" : p.Probability.Name,
                Amount = p.Amount == null ? "" : p.Amount.Value.ToString(),
                ClosingDate = p.ClosingDate,
                SaleStageName = p.SaleStage == null ? "" : p.SaleStage.Name,
                CompanyName = p.Company == null ? "" : p.Company.Name,
                CountItem = p.OpportunityItemMappings == null ? "" : p.OpportunityItemMappings.Count().ToString(),
                TotalRevenueOfItem = p.OpportunityItemMappings == null ? "" : p.OpportunityItemMappings.Sum(p => (p.RequestQuantity * p.SalePrice)).ToString(),
                AppUserName = p.AppUser == null ? "" : p.AppUser.DisplayName,
            }).ToList();
            //OpportunityReport_OpportunityReportDTO OpportunityReport_OpportunityReportDTO = new OpportunityReport_OpportunityReportDTO();
            //OpportunityReport_OpportunityReportDTO.Opportunities = data;
            return data.Count();
        }

        [Route(OpportunityReportRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] OpportunityReport_OpportunityReportFilterDTO filter)
        {
            if (UnAuthorization) return Forbid();
            var info_vn = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            // Lấy danh sách Opportunity
            var query = from t1 in DataContext.Opportunity select t1;
            if (filter.OpportunityName != null)
            {
                query = query.Where(p => p.Name, filter.OpportunityName);
            }
            if (filter.OpportunityAmount != null)
            {
                query = query.Where(p => p.Amount, filter.OpportunityAmount);
            }
            if (filter.OpportunityItemId != null)
            {
                query = query.Where(p => p.Amount, filter.OpportunityAmount);
            }
            if (filter.OpportunityCompanyId != null)
            {
                query = query.Where(p => p.CompanyId, filter.OpportunityCompanyId);
            }
            if (filter.OpportunitySaleStageId != null)
            {
                query = query.Where(p => p.SaleStageId, filter.OpportunitySaleStageId);
            }
            if (filter.OpportunityProbabilityId != null)
            {
                query = query.Where(p => p.ProbabilityId, filter.OpportunityProbabilityId);
            }
            if (filter.OpportunityClosingDate != null)
            {
                query = query.Where(p => p.ClosingDate, filter.OpportunityClosingDate);
            }
            if (filter.OpportunityCreatedAt != null)
            {
                query = query.Where(p => p.CreatedAt, filter.OpportunityCreatedAt);
            }
            if (filter.AppUserId != null)
            {
                query = query.Where(p => p.AppUserId, filter.AppUserId);
            }
            //Tìm kiếm theo OpportunityItemId
            if (filter.OpportunityItemId != null)
            {
                if (filter.OpportunityItemId.Equal.HasValue)
                {
                    query = from q in query
                            join ar in DataContext.OpportunityItemMapping on q.Id equals ar.OpportunityId
                            where ar.ItemId == filter.OpportunityItemId.Equal.Value
                            select q;
                }
            }
            var data = query.Distinct().OrderBy(p => p.CreatedAt).ThenBy(p => p.Name).Skip(filter.Skip).Take(filter.Take).Select(p => new OpportunityReport_OpportunityDTO
            {
                Id = p.Id,
                Name = p.Name,
                ProbabilityName = p.Probability == null ? "" : p.Probability.Name,
                Amount = p.Amount == null ? "" : p.Amount.Value.ToString(),
                ClosingDate = p.ClosingDate,
                CreatedAt = p.CreatedAt,
                SaleStageName = p.SaleStage == null ? "" : p.SaleStage.Name,
                CompanyName = p.Company == null ? "" : p.Company.Name,
                CountItem = p.OpportunityItemMappings == null ? "" : p.OpportunityItemMappings.Count().ToString(),
                TotalRevenueOfItem = p.OpportunityItemMappings == null ? "" : p.OpportunityItemMappings.Sum(p => (p.RequestQuantity * p.SalePrice)).ToString(),
                AppUserName = p.AppUser == null ? "" : p.AppUser.DisplayName,
                ResultName = p.PotentialResult == null ? "" : p.PotentialResult.Name,
                ContactName = p.OpportunityContactMappings == null ? "" : string.Join(',', p.OpportunityContactMappings.Select(p => p.Contact.Name)),
                ForecastAmount = p.ForecastAmount.HasValue ? p.ForecastAmount.ToString() : "",
                LeadSourceName = p.LeadSource == null ? "" : p.LeadSource.Name,
                OpportunityItemMappings = p.OpportunityItemMappings == null ? null : p.OpportunityItemMappings.Select(p => new OpportunityReport_OpportunityItemMappingDTO
                {
                    OpportunityId = p.OpportunityId,
                    ItemId = p.ItemId,
                    UnitOfMeasureId = p.UnitOfMeasureId,
                    Quantity = p.Quantity,
                    DiscountPercentage = p.DiscountPercentage,
                    RequestQuantity = p.RequestQuantity,
                    PrimaryPrice = p.PrimaryPrice,
                    SalePrice = p.SalePrice,
                    Discount = p.Discount,
                    VAT = p.VAT,
                    VATOther = p.VATOther,
                    Amount = p.Amount,
                    Factor = p.Factor,
                    Item = p.Item == null ? null : new OpportunityReport_ItemDTO
                    {
                        Id = p.Item.Id,
                        Name = p.Item.Name
                    },
                    UnitOfMeasure = p.UnitOfMeasure == null ? null : new OpportunityReport_UnitOfMeasureDTO
                    {
                        Name = p.UnitOfMeasure.Name
                    }
                }).ToList()
            }).ToList();

            OpportunityReport_OpportunityTotalDTO Result = new OpportunityReport_OpportunityTotalDTO();
            Result.TotalAmount = data.Sum(p => string.IsNullOrEmpty(p.Amount) ? 0 : decimal.Parse(p.Amount)).ToString();
            Result.TotalItem = data.Sum(p => string.IsNullOrEmpty(p.CountItem) ? 0 : decimal.Parse(p.CountItem)).ToString();
            Result.TotalForecastAmount = data.Sum(p => string.IsNullOrEmpty(p.ForecastAmount) ? 0 : decimal.Parse(p.ForecastAmount)).ToString();

            var maxDate = data.Max(p => p.CreatedAt).HasValue ? data.Max(p => p.CreatedAt).Value : default(DateTime);
            var minDate = data.Min(p => p.CreatedAt).HasValue ? data.Min(p => p.CreatedAt).Value : default(DateTime);

            DateTime Start = filter.OpportunityCreatedAt?.GreaterEqual == null ?
                minDate :
                filter.OpportunityCreatedAt.GreaterEqual.Value;

            DateTime End = filter.OpportunityCreatedAt?.LessEqual == null ?
                maxDate :
                filter.OpportunityCreatedAt.LessEqual.Value;



            //Lấy danh sách Opportunity
            var LtsItemIds = new List<long>();

            var OpportunityReportExports = new List<OpportunityReportExport>();
            foreach (var opportunity in data.OrderByDescending(p => p.CreatedAt))
            {
                if (opportunity.OpportunityItemMappings.Any())
                {
                    foreach (var item in opportunity.OpportunityItemMappings)
                    {
                        OpportunityReportExport obj = new OpportunityReportExport();
                        obj.Name = opportunity.Name;
                        obj.CompanyName = opportunity.CompanyName;
                        obj.ContactName = opportunity.ContactName;
                        obj.ClosingDate = opportunity.ClosingDate.HasValue ? opportunity.ClosingDate.Value.ToString("dd/MM/yyyy") : "";
                        obj.ProbabilityName = opportunity.ProbabilityName;

                        obj.ItemName = item.Item == null ? null : item.Item.Name;
                        obj.UnitOfMeasureName = item.UnitOfMeasure == null ? null : item.UnitOfMeasure.Name;
                        obj.SalePrice = item.SalePrice.HasValue ? string.Format(info_vn, "{0:c}", item.SalePrice.Value) : "";
                        obj.RequestQuantity = item.RequestQuantity;
                        LtsItemIds.Add(item.ItemId);

                        obj.SaleStageName = opportunity.SaleStageName;

                        obj.Amount = string.Format(info_vn, "{0:c}",  string.IsNullOrEmpty(opportunity.Amount) ? (decimal)0.00 : decimal.Parse(opportunity.Amount));
                        obj.ForecastAmount = string.Format(info_vn, "{0:c}", string.IsNullOrEmpty(opportunity.ForecastAmount) ? (decimal)0.00 : decimal.Parse(opportunity.ForecastAmount));

                        obj.LeadSourceName = opportunity.LeadSourceName;
                        obj.AppUserName = opportunity.AppUserName;
                        obj.Result = opportunity.ResultName;
                        OpportunityReportExports.Add(obj);
                    }
                }
                else
                {
                    OpportunityReportExport obj = new OpportunityReportExport();
                    obj.Name = opportunity.Name;
                    obj.CompanyName = opportunity.CompanyName;
                    obj.ContactName = opportunity.ContactName;
                    obj.ClosingDate = opportunity.ClosingDate.HasValue ? opportunity.ClosingDate.Value.ToString("dd/MM/yyyy") : "";
                    obj.ProbabilityName = opportunity.ProbabilityName;

                    obj.SaleStageName = opportunity.SaleStageName;

                    obj.Amount = string.Format(info_vn, "{0:c}", string.IsNullOrEmpty(opportunity.Amount) ? (decimal)0.00 : decimal.Parse(opportunity.Amount));
                    obj.ForecastAmount = string.Format(info_vn, "{0:c}", string.IsNullOrEmpty(opportunity.ForecastAmount) ? (decimal)0.00 : decimal.Parse(opportunity.ForecastAmount));

                    obj.LeadSourceName = opportunity.LeadSourceName;
                    obj.AppUserName = opportunity.AppUserName;
                    obj.Result = opportunity.ResultName;
                    OpportunityReportExports.Add(obj);
                }
            }
            var STT = 1;
            foreach (var item in OpportunityReportExports)
            {
                item.STT = STT;
                STT++;
            }
            string path = "Templates/OpportunityReport.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();

            Data.Start = Start.AddHours(CurrentContext.TimeZone).ToString("dd-MM-yyyy");
            Data.End = End.AddHours(CurrentContext.TimeZone).ToString("dd-MM-yyyy");
            Data.OpportunityReportExports = OpportunityReportExports;
            Data.TotalAmount = string.Format(info_vn, "{0:c}", decimal.Parse(Result.TotalAmount));
            Data.TotalItem = LtsItemIds.Distinct().Count();
            Data.TotalForecastAmount = String.Format(info_vn, "{0:c}", decimal.Parse(Result.TotalForecastAmount));

            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "BaoCaoCoHoi.xlsx");
        }

        private OpportunityFilter ConvertFilterDTOToFilterEntity(OpportunityReport_OpportunityReportFilterDTO OpportunityReport_OpportunityReportFilterDTO)
        {
            OpportunityFilter OpportunityFilter = new OpportunityFilter();
            OpportunityFilter.Selects = OpportunitySelect.ALL;
            OpportunityFilter.Skip = 0;
            OpportunityFilter.Take = OpportunityReport_OpportunityReportFilterDTO.Take;
            OpportunityFilter.OrderType = OpportunityReport_OpportunityReportFilterDTO.OrderType;

            OpportunityFilter.Name = OpportunityReport_OpportunityReportFilterDTO.OpportunityName;
            OpportunityFilter.Amount = OpportunityReport_OpportunityReportFilterDTO.OpportunityAmount;
            OpportunityFilter.CompanyId = OpportunityReport_OpportunityReportFilterDTO.OpportunityCompanyId;
            OpportunityFilter.SaleStageId = OpportunityReport_OpportunityReportFilterDTO.OpportunitySaleStageId;
            OpportunityFilter.ProbabilityId = OpportunityReport_OpportunityReportFilterDTO.OpportunityProbabilityId;
            OpportunityFilter.ClosingDate = OpportunityReport_OpportunityReportFilterDTO.OpportunityClosingDate;
            OpportunityFilter.CreatedAt = OpportunityReport_OpportunityReportFilterDTO.OpportunityCreatedAt;

            return OpportunityFilter;
        }


    }
}

public class OpportunityReportExport
{
    public long STT { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string ClosingDate { get; set; }
    public string ProbabilityName { get; set; }
    public string ItemName { get; set; }
    public string UnitOfMeasureName { get; set; }
    public string SalePrice { get; set; }
    public decimal? RequestQuantity { get; set; }
    public string SaleStageName { get; set; }
    // Giá trị cơ hội
    public string Amount { get; set; }

    //Doanh số dự kiến
    public string ForecastAmount { get; set; }
    //Nguồn
    public string LeadSourceName { get; set; }
    // Người phụ trách
    public string AppUserName { get; set; }
    //Kết quả
    public string Result { get; set; }
}

