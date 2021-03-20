using CRM.Common;
using CRM.Entities;
using CRM.Enums;
using CRM.Models;
using CRM.Services.MAppUser;
using CRM.Services.MKpiPeriod;
using CRM.Services.MKpiYear;
using CRM.Services.MOrganization;
using Hangfire.Annotations;
using CRM.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CRM.Rpc.kpi_tracking.kpi_general_period_report
{
    public class KpiGeneralPeriodReportController : RpcController
    {
        private DataContext DataContext;
        private IOrganizationService OrganizationService;
        private IAppUserService AppUserService;
        private IKpiYearService KpiYearService;
        private IKpiPeriodService KpiPeriodService;
        private ICurrentContext CurrentContext;
        public KpiGeneralPeriodReportController(DataContext DataContext,
            IOrganizationService OrganizationService,
            IAppUserService AppUserService,
            IKpiYearService KpiYearService,
            IKpiPeriodService KpiPeriodService,
            ICurrentContext CurrentContext,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.DataContext = DataContext;
            this.OrganizationService = OrganizationService;
            this.AppUserService = AppUserService;
            this.KpiPeriodService = KpiPeriodService;
            this.KpiYearService = KpiYearService;
            this.CurrentContext = CurrentContext;
        }

        [Route(KpiGeneralPeriodReportRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<KpiGeneralPeriodReport_AppUserDTO>>> FilterListAppUser([FromBody] KpiGeneralPeriodReport_AppUserFilterDTO KpiGeneralPeriodReport_AppUserFilterDTO)
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
            AppUserFilter.Id = KpiGeneralPeriodReport_AppUserFilterDTO.Id;
            AppUserFilter.OrganizationId = KpiGeneralPeriodReport_AppUserFilterDTO.OrganizationId;
            AppUserFilter.Username = KpiGeneralPeriodReport_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = KpiGeneralPeriodReport_AppUserFilterDTO.DisplayName;
            AppUserFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<KpiGeneralPeriodReport_AppUserDTO> KpiGeneralPeriodReport_AppUserDTOs = AppUsers
                .Select(x => new KpiGeneralPeriodReport_AppUserDTO(x)).ToList();
            return KpiGeneralPeriodReport_AppUserDTOs;
        }

        [Route(KpiGeneralPeriodReportRoute.FilterListOrganization), HttpPost]
        public async Task<ActionResult<List<KpiGeneralPeriodReport_OrganizationDTO>>> FilterListOrganization([FromBody] KpiGeneralPeriodReport_OrganizationFilterDTO KpiGeneralPeriodReport_OrganizationFilterDTO)
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
            List<KpiGeneralPeriodReport_OrganizationDTO> KpiGeneralPeriodReport_OrganizationDTOs = Organizations
                .Select(x => new KpiGeneralPeriodReport_OrganizationDTO(x)).ToList();
            return KpiGeneralPeriodReport_OrganizationDTOs;
        }
        [Route(KpiGeneralPeriodReportRoute.FilterListKpiPeriod), HttpPost]
        public async Task<ActionResult<List<KpiGeneralPeriodReport_KpiPeriodDTO>>> FilterListKpiPeriod([FromBody] KpiGeneralPeriodReport_KpiPeriodFilterDTO KpiGeneralPeriodReport_KpiPeriodFilterDTO)
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
            KpiPeriodFilter.Id = KpiGeneralPeriodReport_KpiPeriodFilterDTO.Id;
            KpiPeriodFilter.Code = KpiGeneralPeriodReport_KpiPeriodFilterDTO.Code;
            KpiPeriodFilter.Name = KpiGeneralPeriodReport_KpiPeriodFilterDTO.Name;

            List<KpiPeriod> KpiPeriods = await KpiPeriodService.List(KpiPeriodFilter);
            List<KpiGeneralPeriodReport_KpiPeriodDTO> KpiGeneralPeriodReport_KpiPeriodDTOs = KpiPeriods
                .Select(x => new KpiGeneralPeriodReport_KpiPeriodDTO(x)).ToList();
            return KpiGeneralPeriodReport_KpiPeriodDTOs;
        }

        [Route(KpiGeneralPeriodReportRoute.FilterListKpiYear), HttpPost]
        public async Task<ActionResult<List<KpiGeneralPeriodReport_KpiYearDTO>>> FilterListKpiYear([FromBody] KpiGeneralPeriodReport_KpiYearFilterDTO KpiGeneralPeriodReport_KpiYearFilterDTO)
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
            KpiYearFilter.Id = KpiGeneralPeriodReport_KpiYearFilterDTO.Id;
            KpiYearFilter.Code = KpiGeneralPeriodReport_KpiYearFilterDTO.Code;
            KpiYearFilter.Name = KpiGeneralPeriodReport_KpiYearFilterDTO.Name;

            List<KpiYear> KpiYears = await KpiYearService.List(KpiYearFilter);
            List<KpiGeneralPeriodReport_KpiYearDTO> KpiGeneralPeriodReport_KpiYearDTOs = KpiYears
                .Select(x => new KpiGeneralPeriodReport_KpiYearDTO(x)).ToList();
            return KpiGeneralPeriodReport_KpiYearDTOs;
        }

        [Route(KpiGeneralPeriodReportRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState); // to do kpi year and period
            long? SaleEmployeeId = KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.AppUserId?.Equal;
            long KpiPeriodId = KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.KpiPeriodId?.Equal ?? 100 + StaticParams.DateTimeNow.AddHours(CurrentContext.TimeZone).Month;
            long KpiYearId = KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.KpiYearId?.Equal ?? StaticParams.DateTimeNow.AddHours(CurrentContext.TimeZone).Year;

            List<long> AppUserIds, OrganizationIds;
            (AppUserIds, OrganizationIds) = await FilterOrganizationAndUser(KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.OrganizationId,
                AppUserService, OrganizationService, CurrentContext, DataContext);

            var query = from k in DataContext.KpiGeneral
                        join kc in DataContext.KpiGeneralContent on k.Id equals kc.KpiGeneralId
                        join km in DataContext.KpiGeneralContentKpiPeriodMapping on kc.Id equals km.KpiGeneralContentId
                        where OrganizationIds.Contains(k.OrganizationId) &&
                        AppUserIds.Contains(k.EmployeeId) &&
                        (SaleEmployeeId == null || k.EmployeeId == SaleEmployeeId.Value) &&
                        k.KpiYearId == KpiYearId &&
                        km.KpiPeriodId == KpiPeriodId &&
                        km.Value.HasValue &&
                        k.StatusId == StatusEnum.ACTIVE.Id &&
                        k.DeletedAt == null
                        select k.Id;
            return await query.Distinct().CountAsync();
        }

        [Route(KpiGeneralPeriodReportRoute.List), HttpPost]
        public async Task<ActionResult<List<KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO>>> List([FromBody] KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            if (KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.KpiPeriodId == null)
                return BadRequest(new { message = "Chưa chọn kì KPI" });
            if (KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.KpiYearId == null)
                return BadRequest(new { message = "Chưa chọn năm KPI" });

            DateTime StartDate, EndDate;
            long? SaleEmployeeId = KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.AppUserId?.Equal;
            long KpiPeriodId = KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.KpiPeriodId?.Equal ?? 100 + StaticParams.DateTimeNow.AddHours(CurrentContext.TimeZone).Month;
            long KpiYearId = KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.KpiYearId?.Equal ?? StaticParams.DateTimeNow.AddHours(CurrentContext.TimeZone).Year;
            (StartDate, EndDate) = DateTimeConvert(KpiPeriodId, KpiYearId);

            List<long> AppUserIds, OrganizationIds;
            (AppUserIds, OrganizationIds) = await FilterOrganizationAndUser(KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.OrganizationId,
                AppUserService, OrganizationService, CurrentContext, DataContext);

            #region Danh sách bo nhan vien trong organization co kpi general
            var query = from k in DataContext.KpiGeneral
                        join kc in DataContext.KpiGeneralContent on k.Id equals kc.KpiGeneralId
                        join km in DataContext.KpiGeneralContentKpiPeriodMapping on kc.Id equals km.KpiGeneralContentId
                        where OrganizationIds.Contains(k.OrganizationId) &&
                        AppUserIds.Contains(k.EmployeeId) &&
                        (SaleEmployeeId == null || k.EmployeeId == SaleEmployeeId.Value) &&
                        k.KpiYearId == KpiYearId &&
                        km.KpiPeriodId == KpiPeriodId &&
                        km.Value.HasValue &&
                        k.StatusId == StatusEnum.ACTIVE.Id &&
                        k.DeletedAt == null
                        select new
                        {
                            EmployeeId = k.EmployeeId,
                            OrganizationId = k.OrganizationId
                        };
            var Ids = await query
                .Distinct()
                .OrderBy(x => x.OrganizationId)
                .Skip(KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.Skip)
                .Take(KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.Take)
                .ToListAsync();
            AppUserIds = Ids.Select(x => x.EmployeeId).Distinct().ToList();

            List<AppUserDAO> AppUserDAOs = await DataContext.AppUser
                .Where(x => x.DeletedAt == null)
                .Where(au => AppUserIds.Contains(au.Id))
                .OrderBy(su => su.OrganizationId).ThenBy(x => x.DisplayName)
                .Select(x => new AppUserDAO
                {
                    Id = x.Id,
                    Username = x.Username,
                    DisplayName = x.DisplayName,
                    OrganizationId = x.OrganizationId
                })
                .ToListAsync();
            #endregion

            #region Danh sách phòng ban chứa nhân viên trên
            OrganizationIds = Ids.Select(x => x.OrganizationId).Distinct().ToList();
            var Organizations = await DataContext.Organization
                .Where(x => OrganizationIds.Contains(x.Id))
                .OrderBy(x => x.Id)
                .Select(x => new OrganizationDAO
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
            #endregion


            List<KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO> KpiGeneralPeriodReport_KpiGeneralPeriodReportDTOs = new List<KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO>();
            Parallel.ForEach(Organizations, Organization =>
            {
                KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO = new KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO()
                {
                    OrganizationId = Organization.Id,
                    OrganizationName = Organization.Name,
                    SaleEmployees = new List<KpiGeneralPeriodReport_SaleEmployeeDTO>()
                };
                KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO.SaleEmployees = Ids.Where(x => x.OrganizationId == Organization.Id).Select(x => new KpiGeneralPeriodReport_SaleEmployeeDTO
                {
                    SaleEmployeeId = x.EmployeeId
                }).ToList();
                KpiGeneralPeriodReport_KpiGeneralPeriodReportDTOs.Add(KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO);

                foreach (var SaleEmployee in KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO.SaleEmployees)
                {
                    var Employee = AppUserDAOs.Where(x => x.Id == SaleEmployee.SaleEmployeeId).FirstOrDefault();
                    if (Employee != null)
                    {
                        SaleEmployee.Username = Employee.Username;
                        SaleEmployee.DisplayName = Employee.DisplayName;
                    }
                }
            });
            // list toan bo mapping value and criteria
            var query_detail = from kcm in DataContext.KpiGeneralContentKpiPeriodMapping
                               join kc in DataContext.KpiGeneralContent on kcm.KpiGeneralContentId equals kc.Id
                               join k in DataContext.KpiGeneral on kc.KpiGeneralId equals k.Id
                               where (AppUserIds.Contains(k.EmployeeId) &&
                               OrganizationIds.Contains(k.OrganizationId) &&
                               k.KpiYearId == KpiYearId &&
                               kcm.KpiPeriodId == KpiPeriodId &&
                               k.StatusId == StatusEnum.ACTIVE.Id &&
                               k.DeletedAt == null)
                               select new
                               {
                                   EmployeeId = k.EmployeeId,
                                   KpiCriteriaGeneralId = kc.KpiCriteriaGeneralId,
                                   Value = kcm.Value,
                               };
            List<KpiGeneralPeriodReport_SaleEmployeeDetailDTO> KpiGeneralPeriodReport_SaleEmployeeDetailDTOs = (await query_detail
                .Distinct()
                .ToListAsync())
                .Select(x => new KpiGeneralPeriodReport_SaleEmployeeDetailDTO
                {
                    SaleEmployeeId = x.EmployeeId,
                    KpiCriteriaGeneralId = x.KpiCriteriaGeneralId,
                    Value = x.Value,
                }).ToList();

            //#region Danh sách lead
            //var CustomerLeadDAOs = await DataContext.CustomerLead
            //    .Where(x => x.UserId.HasValue && AppUserIds.Contains(x.UserId.Value) &&
            //    x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
            //    !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion

            //#region Danh sách cơ hội thành công
            //var OpportunityDAOs = await DataContext.Opportunity
            //    .Where(x => x.AppUserId.HasValue && AppUserIds.Contains(x.AppUserId.Value) &&
            //    x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
            //    x.SaleStageId == Enums.SaleStageEnum.CLOSED.Id &&
            //    !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion

            //#region Danh sách khách hàng
            //#region Khách hàng lẻ
            //var CustomerRetailDAOs = await DataContext.CustomerRetail
            //    .Where(x => x.AssignedAppUserId.HasValue && AppUserIds.Contains(x.AssignedAppUserId.Value) &&
            //    x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
            //    !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#region Khách hàng đại lý
            //var CustomerAgentDAOs = await DataContext.CustomerAgent
            //    .Where(x => x.AssignedAppUserId.HasValue && AppUserIds.Contains(x.AssignedAppUserId.Value) &&
            //    x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
            //    !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#region Khách hàng dự án
            //var CustomerProjectDAOs = await DataContext.CustomerProject
            //    .Where(x => x.AppUserAssignedId.HasValue && AppUserIds.Contains(x.AppUserAssignedId.Value) &&
            //    x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
            //    !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#region Khách hàng xuất khẩu
            //var CustomerExportDAOs = await DataContext.CustomerExport
            //    .Where(x => x.AppUserAssignedId.HasValue && AppUserIds.Contains(x.AppUserAssignedId.Value) &&
            //    x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
            //    !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#endregion

            //#region Danh sách đơn hàng
            //#region Đơn hàng lẻ
            //var OrderRetailDAOs = await DataContext.OrderRetail
            //    .Where(x => AppUserIds.Contains(x.AppUserAssignedId) &&
            //    x.OrderDate >= StartDate && x.OrderDate <= EndDate
            //    && !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#region Đơn hàng đại lý
            //var OrderAgentDAOs = await DataContext.DirectSalesOrder
            //    .Where(x => AppUserIds.Contains(x.SaleEmployeeId) &&
            //    x.OrderDate >= StartDate && x.OrderDate <= EndDate
            //    && !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#region Đơn hàng dự án
            //var OrderProjectDAOs = await DataContext.OrderProject
            //    .Where(x => AppUserIds.Contains(x.AppUserAssignedId) &&
            //    x.OrderDate >= StartDate && x.OrderDate <= EndDate
            //    && !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion
            //#region Đơn hàng xuất khẩu
            //var OrderExportDAOs = await DataContext.OrderExport
            //    .Where(x => AppUserIds.Contains(x.AppUserAssignedId) &&
            //    x.OrderDate >= StartDate && x.OrderDate <= EndDate
            //    && !x.DeletedAt.HasValue
            //    ).ToListAsync();
            //#endregion

            //#endregion

            #region Danh sách hợp đồng đã hoàn thành theo nhân viên
            var ContractDAOs = await DataContext.Contract
                .Where(x => AppUserIds.Contains(x.AppUserId) &&
                x.ValidityDate >= StartDate && x.ValidityDate <= EndDate &&
                !x.DeletedAt.HasValue
                ).ToListAsync();
            #endregion

            #region Danh sách ticket thành công
            var TicketDAOs = await DataContext.Ticket
                .Where(x => AppUserIds.Contains(x.UserId) &&
                x.CreatedAt >= StartDate && x.CreatedAt <= EndDate &&
                x.SLAStatusId == Enums.SLAStatusEnum.Success.Id &&
                !x.DeletedAt.HasValue
                ).ToListAsync();
            #endregion

            //Parallel.ForEach(KpiGeneralPeriodReport_KpiGeneralPeriodReportDTOs, KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO =>
            //{
            //    foreach (var SaleEmployeeDTO in KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO.SaleEmployees)
            //    {
            //        SaleEmployeeDTO.OrganizationName = KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO.OrganizationName;

            //        #region Số Lead
            //        //lấy tất cả đơn hàng được thực hiện bởi nhân viên đang xét 
            //        var CustomerLeads = CustomerLeadDAOs
            //            .Where(x => x.UserId == SaleEmployeeDTO.SaleEmployeeId)
            //            .ToList();
            //        //kế hoạch
            //        SaleEmployeeDTO.TotalLeadPlanned = KpiGeneralPeriodReport_SaleEmployeeDetailDTOs
            //                .Where(x => x.SaleEmployeeId == SaleEmployeeDTO.SaleEmployeeId &&
            //                 x.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.TOTAL_LEAD.Id)
            //                .Select(x => x.Value).FirstOrDefault();
            //        if (SaleEmployeeDTO.TotalLeadPlanned.HasValue)
            //        {
            //            //thực hiện
            //            SaleEmployeeDTO.TotalLead = SaleEmployeeDTO.TotalLeadPlanned == null ? null : (decimal?)CustomerLeads.Count();
            //            //tỉ lệ
            //            SaleEmployeeDTO.TotalLeadRatio = (SaleEmployeeDTO.TotalLeadPlanned == null || SaleEmployeeDTO.TotalLead == null || SaleEmployeeDTO.TotalLeadPlanned.Value == 0)
            //                ? null
            //                : (decimal?)Math.Round(SaleEmployeeDTO.TotalLead.Value / SaleEmployeeDTO.TotalLeadPlanned.Value * 100, 2);
            //        }
            //        #endregion

            //        #region Số cơ hội
            //        //lấy tất cả đơn hàng được thực hiện bởi nhân viên đang xét 
            //        var Opportunitys = OpportunityDAOs
            //            .Where(x => x.AppUserId == SaleEmployeeDTO.SaleEmployeeId)
            //            .ToList();
            //        //kế hoạch
            //        SaleEmployeeDTO.TotalOpportunityPlanned = KpiGeneralPeriodReport_SaleEmployeeDetailDTOs
            //                .Where(x => x.SaleEmployeeId == SaleEmployeeDTO.SaleEmployeeId &&
            //                 x.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.TOTAL_OPPORTUNITY.Id)
            //                .Select(x => x.Value).FirstOrDefault();
            //        if (SaleEmployeeDTO.TotalOpportunityPlanned.HasValue)
            //        {
            //            //thực hiện
            //            SaleEmployeeDTO.TotalOpportunity = SaleEmployeeDTO.TotalOpportunityPlanned == null ? null : (decimal?)Opportunitys.Count();
            //            //tỉ lệ
            //            SaleEmployeeDTO.TotalOpportunityRatio = (SaleEmployeeDTO.TotalOpportunityPlanned == null || SaleEmployeeDTO.TotalOpportunity == null || SaleEmployeeDTO.TotalOpportunityPlanned.Value == 0)
            //                ? null
            //                : (decimal?)Math.Round(SaleEmployeeDTO.TotalOpportunity.Value / SaleEmployeeDTO.TotalOpportunityPlanned.Value * 100, 2);
            //        }
            //        #endregion

            //        #region Số khách hàng
            //        //lấy tất cả đơn hàng được thực hiện bởi nhân viên đang xét 
            //        var CustomerRetails = CustomerRetailDAOs
            //            .Where(x => x.AssignedAppUserId == SaleEmployeeDTO.SaleEmployeeId)
            //            .ToList();
            //        var CustomerAgents = CustomerAgentDAOs
            //           .Where(x => x.AssignedAppUserId == SaleEmployeeDTO.SaleEmployeeId)
            //           .ToList();
            //        var CustomerProjects = CustomerProjectDAOs
            //           .Where(x => x.AppUserAssignedId == SaleEmployeeDTO.SaleEmployeeId)
            //           .ToList();
            //        var CustomerExports = CustomerExportDAOs
            //          .Where(x => x.AppUserAssignedId == SaleEmployeeDTO.SaleEmployeeId)
            //          .ToList();
            //        //kế hoạch
            //        SaleEmployeeDTO.TotalCustomerPlanned = KpiGeneralPeriodReport_SaleEmployeeDetailDTOs
            //                .Where(x => x.SaleEmployeeId == SaleEmployeeDTO.SaleEmployeeId &&
            //                 x.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.TOTAL_CUSTOMER.Id)
            //                .Select(x => x.Value).FirstOrDefault();
            //        if (SaleEmployeeDTO.TotalCustomerPlanned.HasValue)
            //        {
            //            //thực hiện
            //            SaleEmployeeDTO.TotalCustomer = SaleEmployeeDTO.TotalCustomerPlanned == null ? null : (decimal?)(CustomerRetails.Count() + CustomerAgents.Count() + CustomerProjects.Count() + CustomerExports.Count());
            //            //tỉ lệ
            //            SaleEmployeeDTO.TotalCustomerRatio = (SaleEmployeeDTO.TotalCustomerPlanned == null || SaleEmployeeDTO.TotalCustomer == null || SaleEmployeeDTO.TotalCustomerPlanned.Value == 0)
            //                ? null
            //                : (decimal?)Math.Round(SaleEmployeeDTO.TotalCustomer.Value / SaleEmployeeDTO.TotalCustomerPlanned.Value * 100, 2);
            //        }
            //        #endregion

            //        #region Số đơn hàng
            //        //lấy tất cả đơn hàng được thực hiện bởi nhân viên đang xét 
            //        var OrderRetails = OrderRetailDAOs
            //            .Where(x => x.AppUserAssignedId == SaleEmployeeDTO.SaleEmployeeId)
            //            .ToList();
            //        var OrderAgents = OrderAgentDAOs
            //           .Where(x => x.SaleEmployeeId == SaleEmployeeDTO.SaleEmployeeId)
            //           .ToList();
            //        var OrderProjects = OrderProjectDAOs
            //           .Where(x => x.AppUserAssignedId == SaleEmployeeDTO.SaleEmployeeId)
            //           .ToList();
            //        var OrderExports = OrderExportDAOs
            //          .Where(x => x.AppUserAssignedId == SaleEmployeeDTO.SaleEmployeeId)
            //          .ToList();
            //        //kế hoạch
            //        SaleEmployeeDTO.TotalOrderPlanned = KpiGeneralPeriodReport_SaleEmployeeDetailDTOs
            //                .Where(x => x.SaleEmployeeId == SaleEmployeeDTO.SaleEmployeeId &&
            //                 x.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.TOTAl_ORDER.Id)
            //                .Select(x => x.Value).FirstOrDefault();
            //        if (SaleEmployeeDTO.TotalOrderPlanned.HasValue)
            //        {
            //            //thực hiện
            //            SaleEmployeeDTO.TotalOrder = SaleEmployeeDTO.TotalOrderPlanned == null ? null : (decimal?)(OrderRetails.Count() + OrderAgents.Count() + OrderProjects.Count() + OrderExports.Count());
            //            //tỉ lệ
            //            SaleEmployeeDTO.TotalOrderRatio = (SaleEmployeeDTO.TotalOrderPlanned == null || SaleEmployeeDTO.TotalOrder == null || SaleEmployeeDTO.TotalOrderPlanned.Value == 0)
            //                ? null
            //                : (decimal?)Math.Round(SaleEmployeeDTO.TotalOrder.Value / SaleEmployeeDTO.TotalOrderPlanned.Value * 100, 2);
            //        }
            //        #endregion

            //        #region Doanh số theo đơn hàng 
            //        //kế hoạch
            //        SaleEmployeeDTO.TotalSalesOfOrderPlanned = KpiGeneralPeriodReport_SaleEmployeeDetailDTOs
            //                .Where(x => x.SaleEmployeeId == SaleEmployeeDTO.SaleEmployeeId &&
            //                 x.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.SALES_BY_ORDER.Id)
            //                .Select(x => x.Value).FirstOrDefault();
            //        if (SaleEmployeeDTO.TotalSalesOfOrderPlanned.HasValue)
            //        {
            //            //thực hiện
            //            SaleEmployeeDTO.TotalSalesOfOrder = SaleEmployeeDTO.TotalSalesOfOrderPlanned == null ? null : (decimal?)(OrderRetails.Sum(p => p.Total) + OrderAgents.Sum(p => p.Total) + OrderProjects.Sum(p => p.Total) + OrderExports.Sum(p => p.Total));
            //            //tỉ lệ
            //            SaleEmployeeDTO.TotalSalesOfOrderRatio = (SaleEmployeeDTO.TotalSalesOfOrderPlanned == null || SaleEmployeeDTO.TotalSalesOfOrder == null || SaleEmployeeDTO.TotalSalesOfOrderPlanned.Value == 0)
            //                ? null
            //                : (decimal?)Math.Round(SaleEmployeeDTO.TotalSalesOfOrder.Value / SaleEmployeeDTO.TotalSalesOfOrderPlanned.Value * 100, 2);
            //        }
            //        #endregion

            //        #region Số hợp đồng
            //        //lấy tất cả đơn hàng được thực hiện bởi nhân viên đang xét 
            //        var Contracts = ContractDAOs
            //            .Where(x => x.AppUserId == SaleEmployeeDTO.SaleEmployeeId)
            //            .ToList();
            //        //kế hoạch
            //        SaleEmployeeDTO.TotalContractPlanned = KpiGeneralPeriodReport_SaleEmployeeDetailDTOs
            //                .Where(x => x.SaleEmployeeId == SaleEmployeeDTO.SaleEmployeeId &&
            //                 x.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.TOTAL_CONTRACT.Id)
            //                .Select(x => x.Value).FirstOrDefault();
            //        if (SaleEmployeeDTO.TotalContractPlanned.HasValue)
            //        {
            //            //thực hiện
            //            SaleEmployeeDTO.TotalContract = SaleEmployeeDTO.TotalContractPlanned == null ? null : (decimal?)Contracts.Count();
            //            //tỉ lệ
            //            SaleEmployeeDTO.TotalContractRatio = (SaleEmployeeDTO.TotalContractPlanned == null || SaleEmployeeDTO.TotalContract == null || SaleEmployeeDTO.TotalContractPlanned.Value == 0)
            //                ? null
            //                : (decimal?)Math.Round(SaleEmployeeDTO.TotalContract.Value / SaleEmployeeDTO.TotalContractPlanned.Value * 100, 2);
            //        }
            //        #endregion

            //        #region Doanh số theo hợp đồng 
            //        //kế hoạch
            //        SaleEmployeeDTO.TotalSalesOfContractPlanned = KpiGeneralPeriodReport_SaleEmployeeDetailDTOs
            //                .Where(x => x.SaleEmployeeId == SaleEmployeeDTO.SaleEmployeeId &&
            //                 x.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.SALES_BY_CONTRACT.Id)
            //                .Select(x => x.Value).FirstOrDefault();
            //        if (SaleEmployeeDTO.TotalSalesOfContractPlanned.HasValue)
            //        {
            //            //thực hiện
            //            SaleEmployeeDTO.TotalSalesOfContract = SaleEmployeeDTO.TotalSalesOfContractPlanned == null ? null : (decimal?)Contracts.Sum(p => p.TotalValue);
            //            //tỉ lệ
            //            SaleEmployeeDTO.TotalSalesOfContractRatio = (SaleEmployeeDTO.TotalSalesOfContractPlanned == null || SaleEmployeeDTO.TotalSalesOfContract == null || SaleEmployeeDTO.TotalSalesOfContractPlanned.Value == 0)
            //                ? null
            //                : (decimal?)Math.Round(SaleEmployeeDTO.TotalSalesOfContract.Value / SaleEmployeeDTO.TotalSalesOfContractPlanned.Value * 100, 2);
            //        }
            //        #endregion

            //        #region Số ticket
            //        //lấy tất cả đơn hàng được thực hiện bởi nhân viên đang xét 
            //        var Tickets = TicketDAOs
            //            .Where(x => x.UserId == SaleEmployeeDTO.SaleEmployeeId)
            //            .ToList();
            //        //kế hoạch
            //        SaleEmployeeDTO.TotalTicketCompletedPlanned = KpiGeneralPeriodReport_SaleEmployeeDetailDTOs
            //                .Where(x => x.SaleEmployeeId == SaleEmployeeDTO.SaleEmployeeId &&
            //                 x.KpiCriteriaGeneralId == KpiCriteriaGeneralEnum.TOTAL_TICKET_COMPLETE.Id)
            //                .Select(x => x.Value).FirstOrDefault();
            //        if (SaleEmployeeDTO.TotalTicketCompletedPlanned.HasValue)
            //        {
            //            //thực hiện
            //            SaleEmployeeDTO.TotalTicketCompleted = SaleEmployeeDTO.TotalTicketCompletedPlanned == null ? null : (decimal?)Tickets.Count();
            //            //tỉ lệ
            //            SaleEmployeeDTO.TotalTicketCompletedRatio = (SaleEmployeeDTO.TotalTicketCompletedPlanned == null || SaleEmployeeDTO.TotalTicketCompleted == null || SaleEmployeeDTO.TotalTicketCompletedPlanned.Value == 0)
            //                ? null
            //                : (decimal?)Math.Round(SaleEmployeeDTO.TotalTicketCompleted.Value / SaleEmployeeDTO.TotalTicketCompletedPlanned.Value * 100, 2);
            //        }
            //        #endregion

            //    }
            //});

            return KpiGeneralPeriodReport_KpiGeneralPeriodReportDTOs;
        }

        [Route(KpiGeneralPeriodReportRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            if (KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.KpiPeriodId == null)
                return BadRequest(new { message = "Chưa chọn kì KPI" });
            if (KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.KpiYearId == null)
                return BadRequest(new { message = "Chưa chọn năm KPI" });

            var KpiPeriod = KpiPeriodEnum.KpiPeriodEnumList.Where(x => x.Id == KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.KpiPeriodId.Equal.Value).FirstOrDefault();
            var KpiYear = KpiYearEnum.KpiYearEnumList.Where(x => x.Id == KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.KpiYearId.Equal.Value).FirstOrDefault();

            KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.Skip = 0;
            KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO.Take = int.MaxValue;
            List<KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO> KpiGeneralPeriodReport_KpiGeneralPeriodReportDTOs = (await List(KpiGeneralPeriodReport_KpiGeneralPeriodReportFilterDTO)).Value;

            long stt = 1;
            foreach (KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO in KpiGeneralPeriodReport_KpiGeneralPeriodReportDTOs)
            {
                foreach (var SaleEmployee in KpiGeneralPeriodReport_KpiGeneralPeriodReportDTO.SaleEmployees)
                {
                    SaleEmployee.STT = stt;
                    stt++;
                }
            }

            List<KpiGeneralPeriodReport_ExportDTO> KpiGeneralPeriodReport_ExportDTOs = KpiGeneralPeriodReport_KpiGeneralPeriodReportDTOs?.Select(x => new KpiGeneralPeriodReport_ExportDTO(x)).ToList();
            string path = "Templates/KpiGeneral_Period_Report.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            Data.KpiPeriod = KpiPeriod.Name;
            Data.KpiYear = KpiYear.Name;
            Data.KpiGeneralPeriodReports = KpiGeneralPeriodReport_ExportDTOs;
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };

            return File(output.ToArray(), "application/octet-stream", "KpiGeneralPeriodReport.xlsx");
        }

        private Tuple<DateTime, DateTime> DateTimeConvert(long KpiPeriodId, long KpiYearId)
        {
            DateTime startDate = StaticParams.DateTimeNow;
            DateTime endDate = StaticParams.DateTimeNow;
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
