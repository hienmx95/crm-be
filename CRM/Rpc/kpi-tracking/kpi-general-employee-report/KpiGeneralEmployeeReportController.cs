using CRM.Common;
using CRM.Entities;
using CRM.Enums;
using CRM.Models;
using CRM.Services.MAppUser;
using CRM.Services.MKpiGeneral;
using CRM.Services.MKpiPeriod;
using CRM.Services.MKpiYear;
using CRM.Services.MOrganization;
using Hangfire.Annotations;
using CRM.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NGS.Templater;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using Microsoft.AspNetCore.Http;

namespace CRM.Rpc.kpi_tracking.kpi_general_employee_report
{
    public class KpiGeneralEmployeeReportController : RpcController
    {
        private DataContext DataContext;
        private IOrganizationService OrganizationService;
        private IAppUserService AppUserService;
        private IKpiYearService KpiYearService;
        private IKpiGeneralService KpiGeneralService;
        private IKpiPeriodService KpiPeriodService;
        private ICurrentContext CurrentContext;
        public KpiGeneralEmployeeReportController(DataContext DataContext,
            IOrganizationService OrganizationService,
            IAppUserService AppUserService,
            IKpiGeneralService KpiGeneralService,
            IKpiYearService KpiYearService,
            IKpiPeriodService KpiPeriodService,
            ICurrentContext CurrentContext,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.DataContext = DataContext;
            this.OrganizationService = OrganizationService;
            this.AppUserService = AppUserService;
            this.KpiGeneralService = KpiGeneralService;
            this.KpiPeriodService = KpiPeriodService;
            this.KpiYearService = KpiYearService;
            this.CurrentContext = CurrentContext;
        }

        [Route(KpiGeneralEmployeeReportRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<KpiGeneralEmployeeReport_AppUserDTO>>> FilterListAppUser([FromBody] KpiGeneralEmployeeReport_AppUserFilterDTO KpiGeneralEmployeeReport_AppUserFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.Id | AppUserSelect.Username | AppUserSelect.DisplayName;
            AppUserFilter.Id = KpiGeneralEmployeeReport_AppUserFilterDTO.Id;
            AppUserFilter.OrganizationId = KpiGeneralEmployeeReport_AppUserFilterDTO.OrganizationId;
            AppUserFilter.Username = KpiGeneralEmployeeReport_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = KpiGeneralEmployeeReport_AppUserFilterDTO.DisplayName;
            AppUserFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<KpiGeneralEmployeeReport_AppUserDTO> KpiGeneralEmployeeReport_AppUserDTOs = AppUsers
                .Select(x => new KpiGeneralEmployeeReport_AppUserDTO(x)).ToList();
            return KpiGeneralEmployeeReport_AppUserDTOs;
        }

        [Route(KpiGeneralEmployeeReportRoute.FilterListOrganization), HttpPost]
        public async Task<ActionResult<List<KpiGeneralEmployeeReport_OrganizationDTO>>> FilterListOrganization([FromBody] KpiGeneralEmployeeReport_OrganizationFilterDTO KpiGeneralEmployeeReport_OrganizationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = int.MaxValue;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<KpiGeneralEmployeeReport_OrganizationDTO> KpiGeneralEmployeeReport_OrganizationDTOs = Organizations
                .Select(x => new KpiGeneralEmployeeReport_OrganizationDTO(x)).ToList();
            return KpiGeneralEmployeeReport_OrganizationDTOs;
        }
        [Route(KpiGeneralEmployeeReportRoute.FilterListKpiPeriod), HttpPost]
        public async Task<ActionResult<List<KpiGeneralEmployeeReport_KpiPeriodDTO>>> FilterListKpiPeriod([FromBody] KpiGeneralEmployeeReport_KpiPeriodFilterDTO KpiGeneralEmployeeReport_KpiPeriodFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KpiPeriodFilter KpiPeriodFilter = new KpiPeriodFilter();
            KpiPeriodFilter.Skip = 0;
            KpiPeriodFilter.Take = 20;
            KpiPeriodFilter.OrderBy = KpiPeriodOrder.Id;
            KpiPeriodFilter.OrderType = OrderType.ASC;
            KpiPeriodFilter.Selects = KpiPeriodSelect.ALL;
            KpiPeriodFilter.Id = KpiGeneralEmployeeReport_KpiPeriodFilterDTO.Id;
            KpiPeriodFilter.Code = KpiGeneralEmployeeReport_KpiPeriodFilterDTO.Code;
            KpiPeriodFilter.Name = KpiGeneralEmployeeReport_KpiPeriodFilterDTO.Name;

            List<KpiPeriod> KpiPeriods = await KpiPeriodService.List(KpiPeriodFilter);
            List<KpiGeneralEmployeeReport_KpiPeriodDTO> KpiGeneralEmployeeReport_KpiPeriodDTOs = KpiPeriods
                .Select(x => new KpiGeneralEmployeeReport_KpiPeriodDTO(x)).ToList();
            return KpiGeneralEmployeeReport_KpiPeriodDTOs;
        }

        [Route(KpiGeneralEmployeeReportRoute.FilterListKpiYear), HttpPost]
        public async Task<ActionResult<List<KpiGeneralEmployeeReport_KpiYearDTO>>> FilterListKpiYear([FromBody] KpiGeneralEmployeeReport_KpiYearFilterDTO KpiGeneralEmployeeReport_KpiYearFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KpiYearFilter KpiYearFilter = new KpiYearFilter();
            KpiYearFilter.Skip = 0;
            KpiYearFilter.Take = 20;
            KpiYearFilter.OrderBy = KpiYearOrder.Id;
            KpiYearFilter.OrderType = OrderType.ASC;
            KpiYearFilter.Selects = KpiYearSelect.ALL;
            KpiYearFilter.Id = KpiGeneralEmployeeReport_KpiYearFilterDTO.Id;
            KpiYearFilter.Code = KpiGeneralEmployeeReport_KpiYearFilterDTO.Code;
            KpiYearFilter.Name = KpiGeneralEmployeeReport_KpiYearFilterDTO.Name;

            List<KpiYear> KpiYears = await KpiYearService.List(KpiYearFilter);
            List<KpiGeneralEmployeeReport_KpiYearDTO> KpiGeneralEmployeeReport_KpiYearDTOs = KpiYears
                .Select(x => new KpiGeneralEmployeeReport_KpiYearDTO(x)).ToList();
            return KpiGeneralEmployeeReport_KpiYearDTOs;
        }

        [Route(KpiGeneralEmployeeReportRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DateTime StartDate, EndDate;
            long? SaleEmployeeId = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.AppUserId.Equal;
            if (SaleEmployeeId == null)
                return 0;
            long? KpiPeriodId = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.KpiPeriodId?.Equal;
            long? KpiYearId = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.KpiYearId?.Equal;
            (StartDate, EndDate) = DateTimeConvert(KpiPeriodId, KpiYearId);

            List<long> AppUserIds, OrganizationIds;
            (AppUserIds, OrganizationIds) = await FilterOrganizationAndUser(KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.OrganizationId,
                AppUserService, OrganizationService, CurrentContext, DataContext);

            var query_detail = from a in DataContext.KpiGeneralContentKpiPeriodMapping
                               join b in DataContext.KpiGeneralContent on a.KpiGeneralContentId equals b.Id
                               join c in DataContext.KpiGeneral on b.KpiGeneralId equals c.Id
                               where OrganizationIds.Contains(c.OrganizationId) &&
                               c.EmployeeId == SaleEmployeeId.Value &&
                               (KpiYearId == null || c.KpiYearId == KpiYearId) &&
                               (KpiPeriodId == null || a.KpiPeriodId == KpiPeriodId) &&
                               c.StatusId == StatusEnum.ACTIVE.Id &&
                               c.DeletedAt == null
                               select new
                               {
                                   SaleEmployeeId = c.EmployeeId,
                                   KpiYearId = c.KpiYearId,
                                   KpiPeriodId = a.KpiPeriodId,
                               };
            return await query_detail.Distinct().CountAsync();
        }

        [Route(KpiGeneralEmployeeReportRoute.List), HttpPost]
        public async Task<ActionResult<List<KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO>>> List([FromBody] KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            if (KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.AppUserId == null)
                return BadRequest(new { message = "Chưa chọn nhân viên" });

            DateTime StartDate, EndDate;
            long? SaleEmployeeId = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.AppUserId.Equal.Value;
            long? KpiPeriodId = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.KpiPeriodId?.Equal;
            long? KpiYearId = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.KpiYearId?.Equal;
            (StartDate, EndDate) = DateTimeConvert(KpiPeriodId, KpiYearId);

            var KpiGeneralId = await DataContext.KpiGeneral
                .Where(x => x.EmployeeId == SaleEmployeeId.Value &&
                (KpiYearId.HasValue == false || x.KpiYearId == KpiYearId.Value) &&
                x.StatusId == StatusEnum.ACTIVE.Id &&
                (KpiYearId == null || x.KpiYearId == KpiYearId) &&
                x.DeletedAt == null)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
            var KpiGeneral = await KpiGeneralService.Get(KpiGeneralId);

            if (KpiGeneral == null)
                return new List<KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO>();
            var KpiPeriodIds = KpiGeneral.KpiGeneralContents
                .SelectMany(x => x.KpiGeneralContentKpiPeriodMappings)
                .Where(x => KpiPeriodId.HasValue == false || x.KpiPeriodId == KpiPeriodId.Value)
                .Select(x => x.KpiPeriodId)
                .Distinct()
                .Skip(KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.Skip)
                .Take(KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.Take)
                .ToList();
            var KpiPeriods = await KpiPeriodService.List(new KpiPeriodFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = KpiPeriodSelect.ALL,
                Id = new IdFilter { In = KpiPeriodIds },
                OrderBy = KpiPeriodOrder.Id,
                OrderType = OrderType.ASC
            });

            var KpiGeneralContentKpiPeriodMappings = KpiGeneral.KpiGeneralContents
                .SelectMany(x => x.KpiGeneralContentKpiPeriodMappings)
                .Where(x => KpiPeriodIds.Contains(x.KpiPeriodId))
                .ToList();
            List<KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO>
                KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTOs = new List<KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO>();
            Parallel.ForEach(KpiPeriods, KpiPeriod =>
            {
                KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO
                    = new KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO();
                KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.KpiPeriodId = KpiPeriod.Id;
                KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.KpiPeriodName = KpiPeriod.Name;
                KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.KpiYearId = KpiGeneral.KpiYearId;
                KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.KpiYearName = KpiGeneral.KpiYear.Name;
                KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.SaleEmployeeId = SaleEmployeeId.Value;
                KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTOs.Add(KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO);
            });

            //#region Danh sách Lead
            //var CustomerLeadDAOs = await DataContext.CustomerLead
            //    .Where(x => x.UserId == SaleEmployeeId &&
            //    x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
            //    !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion

            //#region Danh sách cơ hội thành công
            //var OpportunityDAOs = await DataContext.Opportunity
            //    .Where(x => x.AppUserId == SaleEmployeeId &&
            //    x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
            //    x.SaleStageId == Enums.SaleStageEnum.CLOSED.Id &&
            //    !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion

            //#region Danh sách khách hàng
            //#region Khách hàng lẻ
            //var CustomerRetailDAOs = await DataContext.CustomerRetail
            //    .Where(x => x.AssignedAppUserId == SaleEmployeeId &&
            //    x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
            //    !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#region Khách hàng đại lý
            //var CustomerAgentDAOs = await DataContext.CustomerAgent
            //    .Where(x => x.AssignedAppUserId == SaleEmployeeId &&
            //    x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
            //    !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#region Khách hàng dự án
            //var CustomerProjectDAOs = await DataContext.CustomerProject
            //    .Where(x => x.AppUserAssignedId == SaleEmployeeId &&
            //    x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
            //    !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#region Khách hàng xuất khẩu
            //var CustomerExportDAOs = await DataContext.CustomerExport
            //    .Where(x => x.AppUserAssignedId == SaleEmployeeId &&
            //    x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
            //    !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#endregion

            //#region Danh sách đơn hàng
            //#region Đơn hàng lẻ
            //var OrderRetailDAOs = await DataContext.OrderRetail
            //    .Where(x => x.AppUserAssignedId == SaleEmployeeId &&
            //    x.OrderDate >= StartDate && x.OrderDate <= EndDate
            //    && !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#region Đơn hàng đại lý
            //var OrderAgentDAOs = await DataContext.DirectSalesOrder
            //    .Where(x => x.SaleEmployeeId == SaleEmployeeId &&
            //    x.OrderDate >= StartDate && x.OrderDate <= EndDate
            //    && !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#region Đơn hàng dự án
            //var OrderProjectDAOs = await DataContext.OrderProject
            //    .Where(x => x.AppUserAssignedId == SaleEmployeeId &&
            //    x.OrderDate >= StartDate && x.OrderDate <= EndDate
            //    && !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#region Đơn hàng xuất khẩu
            //var OrderExportDAOs = await DataContext.OrderExport
            //    .Where(x => x.AppUserAssignedId == SaleEmployeeId &&
            //    x.OrderDate >= StartDate && x.OrderDate <= EndDate
            //    && !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion

            //#endregion

            #region Danh sách hợp đồng đã hoàn thành theo nhân viên
            var ContractDAOs = await DataContext.Contract
                .Where(x => x.AppUserId == SaleEmployeeId &&
                x.ValidityDate >= StartDate && x.ValidityDate <= EndDate &&
                !x.DeletedAt.HasValue
                ).ToListAsync();
            #endregion

            #region Danh sách ticket thành công
            var TicketDAOs = await DataContext.Ticket
                .Where(x => x.UserId == SaleEmployeeId &&
                x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
                x.SLAStatusId == Enums.SLAStatusEnum.Success.Id &&
                !x.DeletedAt.HasValue
                ).ToListAsync();
            #endregion

            //Parallel.ForEach(KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTOs, Period =>
            //{
            //    foreach (var KpiPeriod in KpiPeriodEnum.KpiPeriodEnumList)
            //    {
            //        if (KpiPeriod.Id == Period.KpiPeriodId)
            //            Period.KpiPeriodName = KpiPeriod.Name;
            //    }
            //    DateTime Start, End;
            //    (Start, End) = DateTimeConvert(Period.KpiPeriodId, Period.KpiYearId);
            //    #region Số Lead
            //    //kế hoạch
            //    Period.TotalLeadPLanned = KpiGeneralContentKpiPeriodMappings
            //            .Where(x => x.KpiPeriodId == Period.KpiPeriodId &&
            //            x.KpiGeneralContent.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.TOTAL_LEAD.Id)
            //            .Select(x => x.Value)
            //            .FirstOrDefault();
            //    if (Period.TotalLeadPLanned.HasValue)
            //    {
            //        //thực hiện
            //        Period.TotalLead = Period.TotalLeadPLanned == null ? null : (decimal?)CustomerLeadDAOs.Count();
            //        //tỉ lệ
            //        Period.TotalLeadRatio = Period.TotalLeadPLanned == null || Period.TotalLead == null || Period.TotalLeadPLanned == 0 ? null :
            //            (decimal?)
            //            Math.Round((Period.TotalLead.Value / Period.TotalLeadPLanned.Value) * 100, 2);
            //    }
            //    #endregion

            //    #region Số cơ hội thành công
            //    //kế hoạch
            //    Period.TotalOpportunityPlanned = KpiGeneralContentKpiPeriodMappings
            //            .Where(x => x.KpiPeriodId == Period.KpiPeriodId &&
            //            x.KpiGeneralContent.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.TOTAL_OPPORTUNITY.Id)
            //            .Select(x => x.Value)
            //            .FirstOrDefault();
            //    if (Period.TotalOpportunityPlanned.HasValue)
            //    {
            //        //thực hiện
            //        Period.TotalOpportunity = Period.TotalOpportunityPlanned == null ? null : (decimal?)OpportunityDAOs.Count();
            //        //tỉ lệ
            //        Period.TotalOpportunityRatio = Period.TotalOpportunityPlanned == null || Period.TotalOpportunity == null || Period.TotalOpportunityPlanned == 0 ? null :
            //            (decimal?)
            //            Math.Round((Period.TotalOpportunity.Value / Period.TotalOpportunityPlanned.Value) * 100, 2);
            //    }
            //    #endregion

            //    #region Số khách hàng
            //    //kế hoạch
            //    Period.TotalCustomerPlanned = KpiGeneralContentKpiPeriodMappings
            //            .Where(x => x.KpiPeriodId == Period.KpiPeriodId &&
            //            x.KpiGeneralContent.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.TOTAL_CUSTOMER.Id)
            //            .Select(x => x.Value)
            //            .FirstOrDefault();
            //    if (Period.TotalCustomerPlanned.HasValue)
            //    {
            //        //thực hiện
            //        Period.TotalCustomer = Period.TotalCustomerPlanned == null ? null : (decimal?)(CustomerRetailDAOs.Count + CustomerAgentDAOs.Count() + CustomerProjectDAOs.Count() + CustomerExportDAOs.Count());
            //        //tỉ lệ
            //        Period.TotalCustomerRatio = Period.TotalCustomerPlanned == null || Period.TotalCustomer == null || Period.TotalCustomerPlanned == 0 ? null :
            //            (decimal?)
            //            Math.Round((Period.TotalCustomer.Value / Period.TotalCustomerPlanned.Value) * 100, 2);
            //    }
            //    #endregion

            //    #region Số đơn hàng 
            //    //kế hoạch
            //    Period.TotalOrderPlanned = KpiGeneralContentKpiPeriodMappings
            //            .Where(x => x.KpiPeriodId == Period.KpiPeriodId &&
            //            x.KpiGeneralContent.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.TOTAl_ORDER.Id)
            //            .Select(x => x.Value)
            //            .FirstOrDefault();
            //    if (Period.TotalOrderPlanned.HasValue)
            //    {
            //        //thực hiện
            //        Period.TotalOrder = Period.TotalOrderPlanned == null ? null : (decimal?)(OrderRetailDAOs.Count() + OrderAgentDAOs.Count() + OrderProjectDAOs.Count() + OrderExportDAOs.Count());
            //        //tỉ lệ
            //        Period.TotalOrderRatio = Period.TotalOrderPlanned == null || Period.TotalOrder == null || Period.TotalOrderPlanned == 0 ? null :
            //            (decimal?)
            //            Math.Round((Period.TotalOrder.Value / Period.TotalOrderPlanned.Value) * 100, 2);
            //    }
            //    #endregion

            //    #region Doanh số theo đơn hàng
            //    //kế hoạch
            //    Period.TotalSalesOfOrderPlanned = KpiGeneralContentKpiPeriodMappings
            //            .Where(x => x.KpiPeriodId == Period.KpiPeriodId &&
            //            x.KpiGeneralContent.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.SALES_BY_ORDER.Id)
            //            .Select(x => x.Value)
            //            .FirstOrDefault();
            //    if (Period.TotalSalesOfOrderPlanned.HasValue)
            //    {
            //        //thực hiện
            //        Period.TotalSalesOfOrder = Period.TotalSalesOfOrderPlanned == null ? null : (decimal?)(OrderRetailDAOs.Sum(p => p.Total) + OrderAgentDAOs.Sum(p => p.Total) + OrderProjectDAOs.Sum(p => p.Total) + OrderExportDAOs.Sum(p => p.Total));
            //        //tỉ lệ
            //        Period.TotalSalesOfOrderRatio = Period.TotalSalesOfOrderPlanned == null || Period.TotalSalesOfOrder == null || Period.TotalSalesOfOrderPlanned == 0 ? null :
            //            (decimal?)
            //            Math.Round((Period.TotalSalesOfOrder.Value / Period.TotalSalesOfOrderPlanned.Value) * 100, 2);
            //    }
            //    #endregion

            //    #region Số hợp đồng 
            //    //kế hoạch
            //    Period.TotalContractPLanned = KpiGeneralContentKpiPeriodMappings
            //            .Where(x => x.KpiPeriodId == Period.KpiPeriodId &&
            //            x.KpiGeneralContent.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.TOTAL_CONTRACT.Id)
            //            .Select(x => x.Value)
            //            .FirstOrDefault();
            //    if (Period.TotalContractPLanned.HasValue)
            //    {
            //        //thực hiện
            //        Period.TotalContract = Period.TotalContractPLanned == null ? null : (decimal?)ContractDAOs.Count();
            //        //tỉ lệ
            //        Period.TotalContractRatio = Period.TotalContractPLanned == null || Period.TotalContract == null || Period.TotalContractPLanned == 0 ? null :
            //            (decimal?)
            //            Math.Round((Period.TotalContract.Value / Period.TotalContractPLanned.Value) * 100, 2);
            //    }
            //    #endregion

            //    #region Doanh số theo hợp đồng 
            //    //kế hoạch
            //    Period.TotalSalesOfContractPlanned = KpiGeneralContentKpiPeriodMappings
            //            .Where(x => x.KpiPeriodId == Period.KpiPeriodId &&
            //            x.KpiGeneralContent.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.SALES_BY_CONTRACT.Id)
            //            .Select(x => x.Value)
            //            .FirstOrDefault();
            //    if (Period.TotalSalesOfContractPlanned.HasValue)
            //    {
            //        //thực hiện
            //        Period.TotalSalesOfContract = Period.TotalSalesOfContractPlanned == null ? null : (decimal?)ContractDAOs.Sum(p=>p.TotalValue);
            //        //tỉ lệ
            //        Period.TotalSalesOfContractRatio = Period.TotalSalesOfContractPlanned == null || Period.TotalSalesOfContract == null || Period.TotalSalesOfContractPlanned == 0 ? null :
            //            (decimal?)
            //            Math.Round((Period.TotalSalesOfContract.Value / Period.TotalSalesOfContractPlanned.Value) * 100, 2);
            //    }
            //    #endregion

            //    #region Số ticket hoàn thành
            //    //kế hoạch
            //    Period.TotalTicketCompletedPlanned = KpiGeneralContentKpiPeriodMappings
            //            .Where(x => x.KpiPeriodId == Period.KpiPeriodId &&
            //            x.KpiGeneralContent.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.TOTAL_TICKET_COMPLETE.Id)
            //            .Select(x => x.Value)
            //            .FirstOrDefault();
            //    if (Period.TotalTicketCompletedPlanned.HasValue)
            //    {
            //        //thực hiện
            //        Period.TotalTicketCompleted = Period.TotalTicketCompletedPlanned == null ? null : (decimal?)TicketDAOs.Count();
            //        //tỉ lệ
            //        Period.TotalTicketCompletedRatio = Period.TotalTicketCompletedPlanned == null || Period.TotalTicketCompleted == null || Period.TotalTicketCompletedPlanned == 0 ? null :
            //            (decimal?)
            //            Math.Round((Period.TotalTicketCompleted.Value / Period.TotalTicketCompletedPlanned.Value) * 100, 2);
            //    }
            //    #endregion

            //});

            return KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTOs.OrderBy(x => x.KpiPeriodId).ThenBy(x => x.KpiYearId).ToList();
        }

        [Route(KpiGeneralEmployeeReportRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            if (KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.AppUserId == null)
                return BadRequest(new { message = "Chưa chọn nhân viên" });

            KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.Skip = 0;
            KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.Take = int.MaxValue;
            List<KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO> KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTOs = (await List(KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO)).Value;
            var SaleEmployee = await AppUserService.Get(KpiGeneralEmployeeReport_KpiGeneralEmployeeReportFilterDTO.AppUserId.Equal.Value);
            long stt = 1;
            foreach (KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO in KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTOs)
            {
                KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTO.STT = stt;
                stt++;
            }

            List<KpiGeneralEmployeeReport_ExportDTO> KpiGeneralEmployeeReport_ExportDTOs = KpiGeneralEmployeeReport_KpiGeneralEmployeeReportDTOs?.Select(x => new KpiGeneralEmployeeReport_ExportDTO(x)).ToList();
            string path = "Templates/KpiGeneral_Employee_Report.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            Data.Username = SaleEmployee.Username;
            Data.DisplayName = SaleEmployee.DisplayName;
            Data.KpiGeneralEmployeeReports = KpiGeneralEmployeeReport_ExportDTOs;
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };

            return File(output.ToArray(), "application/octet-stream", "KpiGeneralEmployeeReport.xlsx");
        }

        private Tuple<DateTime, DateTime> DateTimeConvert(long? KpiPeriodId, long? KpiYearId)
        {
            DateTime startDate = StaticParams.DateTimeNow;
            DateTime endDate = StaticParams.DateTimeNow;
            if (KpiYearId == null) KpiYearId = startDate.Year;
            if (KpiPeriodId == null)
            {
                startDate = new DateTime(2019, 1, 1);
                endDate = new DateTime(2040, 12, 12);
            }
            else
            if (KpiPeriodId <= Enums.KpiPeriodEnum.PERIOD_MONTH12.Id)
            {
                startDate = new DateTime((int)KpiYearId, (int)(KpiPeriodId % 100), 1);
                endDate = startDate.AddMonths(1).AddSeconds(-1);
            }
            else
            {
                if (KpiPeriodId == Enums.KpiPeriodEnum.PERIOD_QUATER01.Id)
                {
                    startDate = new DateTime((int)KpiYearId, 1, 1);
                    endDate = startDate.AddMonths(3).AddSeconds(-1);
                }
                if (KpiPeriodId == Enums.KpiPeriodEnum.PERIOD_QUATER02.Id)
                {
                    startDate = new DateTime((int)KpiYearId, 4, 1);
                    endDate = startDate.AddMonths(3).AddSeconds(-1);
                }
                if (KpiPeriodId == Enums.KpiPeriodEnum.PERIOD_QUATER03.Id)
                {
                    startDate = new DateTime((int)KpiYearId, 7, 1);
                    endDate = startDate.AddMonths(3).AddSeconds(-1);
                }
                if (KpiPeriodId == Enums.KpiPeriodEnum.PERIOD_QUATER04.Id)
                {
                    startDate = new DateTime((int)KpiYearId, 10, 1);
                    endDate = startDate.AddMonths(3).AddSeconds(-1);
                }
                if (KpiPeriodId == Enums.KpiPeriodEnum.PERIOD_YEAR01.Id)
                {
                    startDate = new DateTime((int)KpiYearId, 1, 1);
                    endDate = startDate.AddYears(1).AddSeconds(-1);
                }
            }

            return Tuple.Create(startDate, endDate);
        }
    }
}
