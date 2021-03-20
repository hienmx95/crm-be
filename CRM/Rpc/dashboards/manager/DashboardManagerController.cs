using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Models;
using CRM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM.Entities;
using CRM.Services.MTicketStatus;
using CRM.Services.MTicketType;
using CRM.Services.MOrganization;
using CRM.Services.MAppUser;
using CRM.Enums;

namespace CRM.Rpc.dashboards.manager
{
    public class DashboardManagerController : RpcController
    {
        private const long THIS_QUARTER = 0;
        private const long LAST_QUATER = 1;
        private const long HALF_YEAR = 2;
        private const long YEAR = 3;
        private DataContext DataContext;
        private IAppUserService AppUserService;
        private ICurrentContext CurrentContext;
        private IOrganizationService OrganizationService;

        public DashboardManagerController(
            IAppUserService AppUserService,
            ICurrentContext CurrentContext,
            IOrganizationService OrganizationService,
            DataContext DataContext, IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ) : base(httpContextAccessor, _DataContext)
        {
            this.DataContext = DataContext;
            this.AppUserService = AppUserService;
            this.CurrentContext = CurrentContext;
            this.OrganizationService = OrganizationService;
        }

        [Route(DashboardManagerRoute.Top5RevenueByCompany), HttpPost]
        public async Task<ActionResult<List<DashboardManager_Top5RevenueByCompanyDTO>>> Top5RevenueByCompany([FromBody] DashboardManager_Top5RevenueByCompanyFilterDTO DashboardManager_Top5RevenueByCompanyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            AppUser CurrentUser = await AppUserService.Get(CurrentContext.UserId);
            DateTime Now = StaticParams.DateTimeNow.Date;
            DateTime Start = new DateTime(Now.Year, Now.Month, Now.Day);
            DateTime End = new DateTime(Now.Year, Now.Month, Now.Day);

            if (DashboardManager_Top5RevenueByCompanyFilterDTO.Time.Equal.HasValue == false)
            {
                DashboardManager_Top5RevenueByCompanyFilterDTO.Time.Equal = 0;
                Start = LocalStartDay(CurrentContext);
                End = Start.AddDays(1).AddSeconds(-1);
            }
            else if (DashboardManager_Top5RevenueByCompanyFilterDTO.Time.Equal.Value == THIS_QUARTER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);
            }
            else if (DashboardManager_Top5RevenueByCompanyFilterDTO.Time.Equal.Value == LAST_QUATER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddMonths(-3).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);
            }

            var query = from o in DataContext.Opportunity
                        join a in DataContext.Company on o.CompanyId equals a.Id
                        where o.CreatedAt >= Start && o.CreatedAt <= End
                        group o by a.Name into x
                        select new DashboardManager_Top5RevenueByCompanyDTO
                        {
                            CompanyName = x.Key,
                            Revenue = x.Sum(y => y.Amount ?? 0)
                        };

            List<DashboardManager_Top5RevenueByCompanyDTO>
                DashboardManager_Top5RevenueByCompanyDTOs = await query.OrderByDescending(x => x.Revenue)
                .Skip(0).Take(5).ToListAsync();

            return DashboardManager_Top5RevenueByCompanyDTOs;
        }

        [Route(DashboardManagerRoute.Top5RevenueByCompanyWin), HttpPost]
        public async Task<ActionResult<List<DashboardManager_Top5RevenueByCompanyDTO>>> Top5RevenueByCompanyWin([FromBody] DashboardManager_Top5RevenueByCompanyFilterDTO DashboardManager_Top5RevenueByCompanyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            AppUser CurrentUser = await AppUserService.Get(CurrentContext.UserId);
            DateTime Now = StaticParams.DateTimeNow.Date;
            DateTime Start = new DateTime(Now.Year, Now.Month, Now.Day);
            DateTime End = new DateTime(Now.Year, Now.Month, Now.Day);

            if (DashboardManager_Top5RevenueByCompanyFilterDTO.Time.Equal.HasValue == false)
            {
                DashboardManager_Top5RevenueByCompanyFilterDTO.Time.Equal = 0;
                Start = LocalStartDay(CurrentContext);
                End = Start.AddDays(1).AddSeconds(-1);
            }
            else if (DashboardManager_Top5RevenueByCompanyFilterDTO.Time.Equal.Value == THIS_QUARTER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);
            }
            else if (DashboardManager_Top5RevenueByCompanyFilterDTO.Time.Equal.Value == LAST_QUATER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddMonths(-3).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);
            }

            var query = from o in DataContext.Opportunity
                        join a in DataContext.Company on o.CompanyId equals a.Id
                        where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.SUCCESS.Id
                        group o by a.Name into x
                        select new DashboardManager_Top5RevenueByCompanyDTO
                        {
                            CompanyName = x.Key,
                            Revenue = x.Sum(y => y.Amount ?? 0)
                        };

            List<DashboardManager_Top5RevenueByCompanyDTO>
                DashboardManager_Top5RevenueByCompanyDTOs = await query.OrderByDescending(x => x.Revenue)
                .Skip(0).Take(5).ToListAsync();

            return DashboardManager_Top5RevenueByCompanyDTOs;
        }

        [Route(DashboardManagerRoute.FilterListTime), HttpPost]
        public ActionResult<List<DashboardManager_EnumList>> FilterListTime()
        {
            if (UnAuthorization) return Forbid();
            List<DashboardManager_EnumList> Dashborad_EnumLists = new List<DashboardManager_EnumList>();
            Dashborad_EnumLists.Add(new DashboardManager_EnumList { Id = THIS_QUARTER, Name = "Quý này" });
            Dashborad_EnumLists.Add(new DashboardManager_EnumList { Id = LAST_QUATER, Name = "Quý trước" });
            Dashborad_EnumLists.Add(new DashboardManager_EnumList { Id = HALF_YEAR, Name = "6 Tháng" });
            Dashborad_EnumLists.Add(new DashboardManager_EnumList { Id = YEAR, Name = "Cả năm" });
            return Dashborad_EnumLists;
        }

        [Route(DashboardManagerRoute.RevenueByOpportunityWin), HttpPost]
        public async Task<ActionResult<decimal>> RevenueByOpportunityWin([FromBody] DashboardManager_RevenueByOpportunityWinFilterDTO DashboardManager_RevenueByOpportunityWinFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            AppUser CurrentUser = await AppUserService.Get(CurrentContext.UserId);
            DateTime Now = StaticParams.DateTimeNow.Date;
            DateTime Start = new DateTime(Now.Year, Now.Month, Now.Day);
            DateTime End = new DateTime(Now.Year, Now.Month, Now.Day);

            if (DashboardManager_RevenueByOpportunityWinFilterDTO.Time.Equal.HasValue == false)
            {
                DashboardManager_RevenueByOpportunityWinFilterDTO.Time.Equal = 0;
                Start = LocalStartDay(CurrentContext);
                End = Start.AddDays(1).AddSeconds(-1);
            }
            else if (DashboardManager_RevenueByOpportunityWinFilterDTO.Time.Equal.Value == THIS_QUARTER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);
            }
            else if (DashboardManager_RevenueByOpportunityWinFilterDTO.Time.Equal.Value == LAST_QUATER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddMonths(-3).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);
            }

            var query = from o in DataContext.Opportunity
                        where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.SUCCESS.Id
                        select o;

            var RevenueTotal = query.Select(x => x.Amount ?? 0).Sum();

            return RevenueTotal;
        }

        [Route(DashboardManagerRoute.RevenueOpportunityWinFluctuation), HttpPost]
        public async Task<ActionResult<DashboardManager_RevenueOpportunityWinFluctuationDTO>> RevenueOpportunityWinFluctuation([FromBody] DashboardManager_RevenueOpportunityWinFluctuationFilterDTO DashboardManager_RevenueOpportunityWinFluctuationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            AppUser CurrentUser = await AppUserService.Get(CurrentContext.UserId);
            DateTime Now = StaticParams.DateTimeNow.Date;
            DateTime Start = LocalStartDay(CurrentContext);
            DateTime End = new DateTime(Now.Year, Now.Month, Now.Day);

            if (DashboardManager_RevenueOpportunityWinFluctuationFilterDTO.Time.Equal.Value == THIS_QUARTER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);

                var query = from o in DataContext.Opportunity
                            where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.SUCCESS.Id
                            group o by o.CreatedAt.Month into x
                            select new DashboardManager_RevenueOpportunityWinByQuarterDTO
                            {
                                Month = x.Key,
                                Revenue = x.Sum(y => y.Amount ?? 0)
                            };
                var DashboardManager_RevenueOpportunityWinByQuarterDTOs = await query.ToListAsync();
                DashboardManager_RevenueOpportunityWinFluctuationDTO DashboardManager_RevenueOpportunityWinFluctuationDTO = new DashboardManager_RevenueOpportunityWinFluctuationDTO();
                DashboardManager_RevenueOpportunityWinFluctuationDTO.RevenueOpportunityWinFluctuationByThisQuaters = new List<DashboardManager_RevenueOpportunityWinByQuarterDTO>();
                int start = 3 * (this_quarter - 1) + 1;
                int end = start + 3;
                for (int i = start; i < end; i++)
                {
                    DashboardManager_RevenueOpportunityWinByQuarterDTO RevenueOpportunityWinFluctuationByThisQuater = new DashboardManager_RevenueOpportunityWinByQuarterDTO
                    {
                        Month = i,
                        Revenue = 0
                    };
                    DashboardManager_RevenueOpportunityWinFluctuationDTO.RevenueOpportunityWinFluctuationByThisQuaters.Add(RevenueOpportunityWinFluctuationByThisQuater);
                }
                foreach (var RevenueOpportunityWinFluctuationByThisQuater in DashboardManager_RevenueOpportunityWinFluctuationDTO.RevenueOpportunityWinFluctuationByThisQuaters)
                {
                    var data = DashboardManager_RevenueOpportunityWinByQuarterDTOs.Where(x => x.Month == RevenueOpportunityWinFluctuationByThisQuater.Month).FirstOrDefault();
                    if (data != null)
                        RevenueOpportunityWinFluctuationByThisQuater.Revenue = data.Revenue;
                }
                return DashboardManager_RevenueOpportunityWinFluctuationDTO;
            }
            else if (DashboardManager_RevenueOpportunityWinFluctuationFilterDTO.Time.Equal.Value == LAST_QUATER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddMonths(-3).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);

                var query = from o in DataContext.Opportunity
                            where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.SUCCESS.Id
                            group o by o.CreatedAt.Month into x
                            select new DashboardManager_RevenueOpportunityWinByQuarterDTO
                            {
                                Month = x.Key,
                                Revenue = x.Sum(y => y.Amount ?? 0)
                            };
                var DashboardManager_RevenueOpportunityWinByQuarterDTOs = await query.ToListAsync();
                DashboardManager_RevenueOpportunityWinFluctuationDTO DashboardManager_RevenueOpportunityWinFluctuationDTO = new DashboardManager_RevenueOpportunityWinFluctuationDTO();
                DashboardManager_RevenueOpportunityWinFluctuationDTO.RevenueOpportunityWinFluctuationByLastQuaters = new List<DashboardManager_RevenueOpportunityWinByQuarterDTO>();
                int start = 3 * (this_quarter - 1) + 1;
                int end = start + 3;
                for (int i = start; i < end; i++)
                {
                    DashboardManager_RevenueOpportunityWinByQuarterDTO RevenueOpportunityWinFluctuationByLastQuater = new DashboardManager_RevenueOpportunityWinByQuarterDTO
                    {
                        Month = i,
                        Revenue = 0
                    };
                    DashboardManager_RevenueOpportunityWinFluctuationDTO.RevenueOpportunityWinFluctuationByLastQuaters.Add(RevenueOpportunityWinFluctuationByLastQuater);
                }
                foreach (var RevenueOpportunityWinFluctuationByLastQuater in DashboardManager_RevenueOpportunityWinFluctuationDTO.RevenueOpportunityWinFluctuationByLastQuaters)
                {
                    var data = DashboardManager_RevenueOpportunityWinByQuarterDTOs.Where(x => x.Month == RevenueOpportunityWinFluctuationByLastQuater.Month).FirstOrDefault();
                    if (data != null)
                        RevenueOpportunityWinFluctuationByLastQuater.Revenue = data.Revenue;
                }
                return DashboardManager_RevenueOpportunityWinFluctuationDTO;
            }
            else if (DashboardManager_RevenueOpportunityWinFluctuationFilterDTO.Time.Equal.Value == YEAR)
            {
                Start = new DateTime(Now.Year, 1, 1).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddYears(1).AddSeconds(-1);

                var query = from o in DataContext.Opportunity
                            where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.SUCCESS.Id
                            group o by o.CreatedAt.Month into x
                            select new DashboardManager_RevenueOpportunityWinByQuarterDTO
                            {
                                Month = x.Key,
                                Revenue = x.Sum(y => y.Amount ?? 0)
                            };

                var DashboardManager_RevenueOpportunityWinByHalfYearDTOs = await query.ToListAsync();
                DashboardManager_RevenueOpportunityWinFluctuationDTO DashboardManager_RevenueOpportunityWinFluctuationDTO = new DashboardManager_RevenueOpportunityWinFluctuationDTO();
                DashboardManager_RevenueOpportunityWinFluctuationDTO.RevenueOpportunityWinFluctuationByYears = new List<DashboardManager_RevenueOpportunityWinByYearDTO>();
                for (int i = 1; i <= 12; i++)
                {
                    DashboardManager_RevenueOpportunityWinByYearDTO RevenueOpportunityWinFluctuationByYear = new DashboardManager_RevenueOpportunityWinByYearDTO
                    {
                        Month = i,
                        Revenue = 0
                    };
                    DashboardManager_RevenueOpportunityWinFluctuationDTO.RevenueOpportunityWinFluctuationByYears.Add(RevenueOpportunityWinFluctuationByYear);
                }
                foreach (var RevenueOpportunityWinFluctuationByYear in DashboardManager_RevenueOpportunityWinFluctuationDTO.RevenueOpportunityWinFluctuationByYears)
                {
                    var data = DashboardManager_RevenueOpportunityWinByHalfYearDTOs.Where(x => x.Month == RevenueOpportunityWinFluctuationByYear.Month).FirstOrDefault();
                    if (data != null)
                        RevenueOpportunityWinFluctuationByYear.Revenue = data.Revenue;
                }

                return DashboardManager_RevenueOpportunityWinFluctuationDTO;
            }

            return new DashboardManager_RevenueOpportunityWinFluctuationDTO();
        }

        [Route(DashboardManagerRoute.OpportunityLoseByReason), HttpPost]
        public async Task<ActionResult<DashboardManager_OpportunityLoseByReasonDTO>> OpportunityLoseByReason([FromBody] DashboardManager_OpportunityLoseByReasonFilterDTO DashboardManager_OpportunityLoseByReasonFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            AppUser CurrentUser = await AppUserService.Get(CurrentContext.UserId);
            DateTime Now = StaticParams.DateTimeNow.Date;
            DateTime Start = new DateTime(Now.Year, Now.Month, Now.Day);
            DateTime End = new DateTime(Now.Year, Now.Month, Now.Day);

            if (DashboardManager_OpportunityLoseByReasonFilterDTO.Time.Equal.HasValue == false)
            {
                DashboardManager_OpportunityLoseByReasonFilterDTO.Time.Equal = 0;
                Start = LocalStartDay(CurrentContext);
                End = Start.AddDays(1).AddSeconds(-1);
            }
            else if (DashboardManager_OpportunityLoseByReasonFilterDTO.Time.Equal.Value == THIS_QUARTER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);
            }
            else if (DashboardManager_OpportunityLoseByReasonFilterDTO.Time.Equal.Value == LAST_QUATER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddMonths(-3).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);
            }

            var queryRevenue = from o in DataContext.Opportunity
                               where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.FAILED.Id
                               select o;

            var RevenueTotal = queryRevenue.Select(x => x.Amount ?? 0).Sum();

            var queryResult = from or in DataContext.OpportunityResultType
                              select or;

            var OpportunityResultTypeDAOs = await queryResult.ToListAsync();

            var query = from o in DataContext.Opportunity
                        join or in DataContext.OpportunityResultType on o.OpportunityResultTypeId equals or.Id
                        where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.FAILED.Id
                        group o by or.Name into x
                        select new DashboardManager_OpportunityLoseRevenueByReasonDTO
                        {
                            Reason = x.Key,
                            Revenue = x.Sum(y => y.Amount ?? 0)
                        };

            DashboardManager_OpportunityLoseByReasonDTO DashboardManager_OpportunityLoseByReasonDTO = new DashboardManager_OpportunityLoseByReasonDTO();
            DashboardManager_OpportunityLoseByReasonDTO.OpportunityLoseRevenueByReasons = new List<DashboardManager_OpportunityLoseRevenueByReasonDTO>();

            foreach (var OpportunityResultTypeDAO in OpportunityResultTypeDAOs)
            {
                DashboardManager_OpportunityLoseRevenueByReasonDTO DashboardManager_OpportunityLoseRevenueByReasonDTO = new DashboardManager_OpportunityLoseRevenueByReasonDTO
                {
                    Reason = OpportunityResultTypeDAO.Name,
                    Revenue = 0
                };
                DashboardManager_OpportunityLoseByReasonDTO.OpportunityLoseRevenueByReasons.Add(DashboardManager_OpportunityLoseRevenueByReasonDTO);
            }

            List<DashboardManager_OpportunityLoseRevenueByReasonDTO> DashboardManager_OpportunityLoseRevenueByReasonDTOs =
                await query.OrderByDescending(x => x.Revenue).ToListAsync();

            foreach (var OpportunityLoseRevenueByReason in DashboardManager_OpportunityLoseByReasonDTO.OpportunityLoseRevenueByReasons)
            {
                var data = DashboardManager_OpportunityLoseRevenueByReasonDTOs.Where(x => x.Reason == OpportunityLoseRevenueByReason.Reason).FirstOrDefault();
                if (data != null)
                    OpportunityLoseRevenueByReason.Revenue = data.Revenue;
            }
            DashboardManager_OpportunityLoseByReasonDTO.Revenue = RevenueTotal;

            return DashboardManager_OpportunityLoseByReasonDTO;
        }

        [Route(DashboardManagerRoute.OpportunityWinBySaleStage), HttpPost]
        public async Task<ActionResult<DashboardManager_OpportunityWinBySaleStageDTO>> OpportunityWinBySaleStage([FromBody] DashboardManager_OpportunityWinBySaleStageFilterDTO DashboardManager_OpportunityWinBySaleStageFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            AppUser CurrentUser = await AppUserService.Get(CurrentContext.UserId);
            DateTime Now = StaticParams.DateTimeNow.Date;
            DateTime Start = new DateTime(Now.Year, Now.Month, Now.Day);
            DateTime End = new DateTime(Now.Year, Now.Month, Now.Day);

            if (DashboardManager_OpportunityWinBySaleStageFilterDTO.Time.Equal.HasValue == false)
            {
                DashboardManager_OpportunityWinBySaleStageFilterDTO.Time.Equal = 0;
                Start = LocalStartDay(CurrentContext);
                End = Start.AddDays(1).AddSeconds(-1);
            }
            else if (DashboardManager_OpportunityWinBySaleStageFilterDTO.Time.Equal.Value == THIS_QUARTER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);
            }
            else if (DashboardManager_OpportunityWinBySaleStageFilterDTO.Time.Equal.Value == LAST_QUATER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddMonths(-3).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);
            }

            var queryRevenue = from o in DataContext.Opportunity
                               where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.SUCCESS.Id
                               select o;

            var RevenueTotal = queryRevenue.Select(x => x.Amount ?? 0).Sum();


            var query = from o in DataContext.Opportunity
                        join s in DataContext.SaleStage on o.SaleStageId equals s.Id
                        where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.SUCCESS.Id
                        group o by s.Name into x
                        select new DashboardManager_OpportunityWinRevenueBySaleStageDTO
                        {
                            SaleStage = x.Key,
                            Revenue = x.Sum(y => y.Amount ?? 0)
                        };

            var querySaleStage = from s in DataContext.SaleStage
                                 select s;

            var SaleStageDAOs = await querySaleStage.ToListAsync();

            List<DashboardManager_OpportunityWinRevenueBySaleStageDTO> DashboardManager_OpportunityWinRevenueBySaleStageDTOs =
                await query.OrderByDescending(x => x.Revenue).ToListAsync();

            DashboardManager_OpportunityWinBySaleStageDTO DashboardManager_OpportunityWinBySaleStageDTO = new DashboardManager_OpportunityWinBySaleStageDTO();
            DashboardManager_OpportunityWinBySaleStageDTO.OpportunityWinRevenueBySaleStages = new List<DashboardManager_OpportunityWinRevenueBySaleStageDTO>();

            foreach (var SaleStageDAO in SaleStageDAOs)
            {
                DashboardManager_OpportunityWinRevenueBySaleStageDTO DashboardManager_OpportunityWinRevenueBySaleStageDTO = new DashboardManager_OpportunityWinRevenueBySaleStageDTO
                {
                    SaleStage = SaleStageDAO.Name,
                    Revenue = 0
                };
                DashboardManager_OpportunityWinBySaleStageDTO.OpportunityWinRevenueBySaleStages.Add(DashboardManager_OpportunityWinRevenueBySaleStageDTO);
            }
            foreach (var OpportunityWinRevenueBySaleStage in DashboardManager_OpportunityWinBySaleStageDTO.OpportunityWinRevenueBySaleStages)
            {
                var data = DashboardManager_OpportunityWinRevenueBySaleStageDTOs.Where(x => x.SaleStage == OpportunityWinRevenueBySaleStage.SaleStage).FirstOrDefault();
                if (data != null)
                    OpportunityWinRevenueBySaleStage.Revenue = data.Revenue;
            }
            DashboardManager_OpportunityWinBySaleStageDTO.Revenue = RevenueTotal;

            return DashboardManager_OpportunityWinBySaleStageDTO;
        }

        [Route(DashboardManagerRoute.OpportunityWinAndLoseFluctuation), HttpPost]
        public async Task<ActionResult<DashboardManager_OpportunityWinAndLoseFluctuationDTO>> OpportunityWinAndLoseFluctuation([FromBody] DashboardManager_OpportunityWinAndLoseFluctuationFilterDTO DashboardManager_OpportunityWinAndLoseFluctuationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            AppUser CurrentUser = await AppUserService.Get(CurrentContext.UserId);
            DateTime Now = StaticParams.DateTimeNow.Date;
            DateTime Start = LocalStartDay(CurrentContext);
            DateTime End = new DateTime(Now.Year, Now.Month, Now.Day);

            if (DashboardManager_OpportunityWinAndLoseFluctuationFilterDTO.Time.Equal.Value == THIS_QUARTER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);

                var queryWin = from o in DataContext.Opportunity
                               where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.SUCCESS.Id
                               group o by o.CreatedAt.Month into x
                               select new DashboardManager_OpportunityWinAndLoseByQuarterDTO
                               {
                                   Month = x.Key,
                                   Win = x.Sum(y => y.Amount ?? 0)
                               };

                var queryLose = from o in DataContext.Opportunity
                                where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.FAILED.Id
                                group o by o.CreatedAt.Month into x
                                select new DashboardManager_OpportunityWinAndLoseByQuarterDTO
                                {
                                    Month = x.Key,
                                    Win = x.Sum(y => y.Amount ?? 0)
                                };
                var DashboardManager_OpportunityWinByQuarterDTO = await queryWin.ToListAsync();
                var DashboardManager_OpportunityLoseByQuarterDTO = await queryLose.ToListAsync();
                DashboardManager_OpportunityWinAndLoseFluctuationDTO DashboardManager_OpportunityWinAndLoseFluctuationDTO = new DashboardManager_OpportunityWinAndLoseFluctuationDTO();
                DashboardManager_OpportunityWinAndLoseFluctuationDTO.OpportunityWinAndLoseFluctuationByThisQuaters = new List<DashboardManager_OpportunityWinAndLoseByQuarterDTO>();
                int start = 3 * (this_quarter - 1) + 1;
                int end = start + 3;
                for (int i = start; i < end; i++)
                {
                    DashboardManager_OpportunityWinAndLoseByQuarterDTO DashboardManager_OpportunityWinAndLoseByQuarterDTO = new DashboardManager_OpportunityWinAndLoseByQuarterDTO
                    {
                        Month = i,
                        Win = 0,
                        Lose = 0
                    };
                    DashboardManager_OpportunityWinAndLoseFluctuationDTO.OpportunityWinAndLoseFluctuationByThisQuaters.Add(DashboardManager_OpportunityWinAndLoseByQuarterDTO);
                }
                foreach (var OpportunityWinAndLoseFluctuationByThisQuater in DashboardManager_OpportunityWinAndLoseFluctuationDTO.OpportunityWinAndLoseFluctuationByThisQuaters)
                {
                    var winData = DashboardManager_OpportunityWinByQuarterDTO.Where(x => x.Month == OpportunityWinAndLoseFluctuationByThisQuater.Month).FirstOrDefault();
                    if (winData != null)
                        OpportunityWinAndLoseFluctuationByThisQuater.Win = winData.Win;
                    var loseData = DashboardManager_OpportunityLoseByQuarterDTO.Where(x => x.Month == OpportunityWinAndLoseFluctuationByThisQuater.Month).FirstOrDefault();
                    if (loseData != null)
                        OpportunityWinAndLoseFluctuationByThisQuater.Lose = loseData.Lose;
                }
                return DashboardManager_OpportunityWinAndLoseFluctuationDTO;
            }
            else if (DashboardManager_OpportunityWinAndLoseFluctuationFilterDTO.Time.Equal.Value == LAST_QUATER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddMonths(-3).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);

                var queryWin = from o in DataContext.Opportunity
                               where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.SUCCESS.Id
                               group o by o.CreatedAt.Month into x
                               select new DashboardManager_OpportunityWinAndLoseByQuarterDTO
                               {
                                   Month = x.Key,
                                   Win = x.Sum(y => y.Amount ?? 0)
                               };

                var queryLose = from o in DataContext.Opportunity
                                where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.FAILED.Id
                                group o by o.CreatedAt.Month into x
                                select new DashboardManager_OpportunityWinAndLoseByQuarterDTO
                                {
                                    Month = x.Key,
                                    Win = x.Sum(y => y.Amount ?? 0)
                                };
                var DashboardManager_OpportunityWinByQuarterDTO = await queryWin.ToListAsync();
                var DashboardManager_OpportunityLoseByQuarterDTO = await queryLose.ToListAsync();
                DashboardManager_OpportunityWinAndLoseFluctuationDTO DashboardManager_OpportunityWinAndLoseFluctuationDTO = new DashboardManager_OpportunityWinAndLoseFluctuationDTO();
                DashboardManager_OpportunityWinAndLoseFluctuationDTO.OpportunityWinAndLoseFluctuationByLastQuaters = new List<DashboardManager_OpportunityWinAndLoseByQuarterDTO>();

                int start = 3 * (this_quarter - 1) + 1;
                int end = start + 3;
                for (int i = start; i < end; i++)
                {
                    DashboardManager_OpportunityWinAndLoseByQuarterDTO DashboardManager_OpportunityWinAndLoseByLastQuarterDTO = new DashboardManager_OpportunityWinAndLoseByQuarterDTO
                    {
                        Month = i,
                        Win = 0,
                        Lose = 0
                    };
                    DashboardManager_OpportunityWinAndLoseFluctuationDTO.OpportunityWinAndLoseFluctuationByLastQuaters.Add(DashboardManager_OpportunityWinAndLoseByLastQuarterDTO);
                }
                foreach (var OpportunityWinAndLoseFluctuationByLastQuater in DashboardManager_OpportunityWinAndLoseFluctuationDTO.OpportunityWinAndLoseFluctuationByLastQuaters)
                {
                    var winData = DashboardManager_OpportunityWinByQuarterDTO.Where(x => x.Month == OpportunityWinAndLoseFluctuationByLastQuater.Month).FirstOrDefault();
                    if (winData != null)
                        OpportunityWinAndLoseFluctuationByLastQuater.Win = winData.Win;
                    var loseData = DashboardManager_OpportunityLoseByQuarterDTO.Where(x => x.Month == OpportunityWinAndLoseFluctuationByLastQuater.Month).FirstOrDefault();
                    if (loseData != null)
                        OpportunityWinAndLoseFluctuationByLastQuater.Lose = loseData.Lose;
                }
                return DashboardManager_OpportunityWinAndLoseFluctuationDTO;
            }
            else if (DashboardManager_OpportunityWinAndLoseFluctuationFilterDTO.Time.Equal.Value == YEAR)
            {
                Start = new DateTime(Now.Year, 1, 1).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddYears(1).AddSeconds(-1);

                var queryWin = from o in DataContext.Opportunity
                               where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.SUCCESS.Id
                               group o by o.CreatedAt.Month into x
                               select new DashboardManager_OpportunityWinAndLoseByYearDTO
                               {
                                   Month = x.Key,
                                   Win = x.Sum(y => y.Amount ?? 0)
                               };

                var queryLose = from o in DataContext.Opportunity
                                where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.FAILED.Id
                                group o by o.CreatedAt.Month into x
                                select new DashboardManager_OpportunityWinAndLoseByYearDTO
                                {
                                    Month = x.Key,
                                    Win = x.Sum(y => y.Amount ?? 0)
                                };
                var DashboardManager_OpportunityWinByYearDTO = await queryWin.ToListAsync();
                var DashboardManager_OpportunityLoseByYearDTO = await queryLose.ToListAsync();
                DashboardManager_OpportunityWinAndLoseFluctuationDTO DashboardManager_OpportunityWinAndLoseFluctuationDTO = new DashboardManager_OpportunityWinAndLoseFluctuationDTO();
                DashboardManager_OpportunityWinAndLoseFluctuationDTO.OpportunityWinAndLoseFluctuationByYears = new List<DashboardManager_OpportunityWinAndLoseByYearDTO>();
                for (int i = 1; i <= 12; i++)
                {
                    DashboardManager_OpportunityWinAndLoseByYearDTO DashboardManager_OpportunityWinAndLoseByYearDTO = new DashboardManager_OpportunityWinAndLoseByYearDTO
                    {
                        Month = i,
                        Win = 0,
                        Lose = 0
                    };
                    DashboardManager_OpportunityWinAndLoseFluctuationDTO.OpportunityWinAndLoseFluctuationByYears.Add(DashboardManager_OpportunityWinAndLoseByYearDTO);
                }
                foreach (var OpportunityWinAndLoseByYear in DashboardManager_OpportunityWinAndLoseFluctuationDTO.OpportunityWinAndLoseFluctuationByYears)
                {
                    var winData = DashboardManager_OpportunityWinByYearDTO.Where(x => x.Month == OpportunityWinAndLoseByYear.Month).FirstOrDefault();
                    if (winData != null)
                        OpportunityWinAndLoseByYear.Win = winData.Win;
                    var loseData = DashboardManager_OpportunityLoseByYearDTO.Where(x => x.Month == OpportunityWinAndLoseByYear.Month).FirstOrDefault();
                    if (loseData != null)
                        OpportunityWinAndLoseByYear.Lose = loseData.Lose;
                }

                return DashboardManager_OpportunityWinAndLoseFluctuationDTO;
            }

            return new DashboardManager_OpportunityWinAndLoseFluctuationDTO();
        }

        [Route(DashboardManagerRoute.Top20BusinessTracking), HttpPost]
        public async Task<ActionResult<List<DashboardManager_Top20BusinessTrackingDTO>>> Top20BusinessTracking([FromBody] DashboardManager_Top20BusinessTrackingFilterDTO DashboardManager_Top20BusinessTrackingFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            try
            {
                if (CurrentContext.UserId == 0)
                {
                    CurrentContext.UserId = 1;
                }
                AppUser CurrentUser = await AppUserService.Get(CurrentContext.UserId);
                DateTime Now = StaticParams.DateTimeNow.Date;
                DateTime Start = new DateTime(Now.Year, Now.Month, Now.Day);
                DateTime End = new DateTime(Now.Year, Now.Month, Now.Day);

                if (DashboardManager_Top20BusinessTrackingFilterDTO.Time.Equal.HasValue == false)
                {
                    DashboardManager_Top20BusinessTrackingFilterDTO.Time.Equal = 0;
                    Start = LocalStartDay(CurrentContext);
                    End = Start.AddDays(1).AddSeconds(-1);
                }
                else if (DashboardManager_Top20BusinessTrackingFilterDTO.Time.Equal.Value == THIS_QUARTER)
                {
                    var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                    Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddHours(0 - CurrentContext.TimeZone);
                    End = Start.AddMonths(3).AddSeconds(-1);
                }
                else if (DashboardManager_Top20BusinessTrackingFilterDTO.Time.Equal.Value == LAST_QUATER)
                {
                    var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                    Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddMonths(-3).AddHours(0 - CurrentContext.TimeZone);
                    End = Start.AddMonths(3).AddSeconds(-1);
                }

                var queryAppUser = from au in DataContext.AppUser
                                   join ou in DataContext.Organization on au.OrganizationId equals ou.Id
                                   where ou.Path.StartsWith(CurrentUser.Organization.Path)
                                   select au;

                var AppUsers = await queryAppUser.Select(x => new AppUser
                {
                    Id = x.Id,
                    Username = x.Username,
                    DisplayName = x.DisplayName,
                    Avatar = x.Avatar,
                    OrganizationId = x.OrganizationId
                }).Skip(0).Take(20).ToListAsync();

                List<DashboardManager_Top20BusinessTrackingDTO> DashboardManager_Top20BusinessTrackingDTOs = new List<DashboardManager_Top20BusinessTrackingDTO>();
                foreach (var item in AppUsers)
                {
                    var queryRevenue = from o in DataContext.Opportunity
                                       join au in DataContext.AppUser on o.AppUserId equals au.Id
                                       where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.SUCCESS.Id && au.Id == item.Id
                                       select o;

                    var queryOpportunityWin = from o in DataContext.Opportunity
                                              join au in DataContext.AppUser on o.AppUserId equals au.Id
                                              where o.CreatedAt >= Start && o.CreatedAt <= End && o.PotentialResultId == PotentialResultEnum.SUCCESS.Id && au.Id == item.Id
                                              select o;

                    var queryTicket = from t in DataContext.Ticket
                                      join au in DataContext.AppUser on t.UserId equals au.Id
                                      where t.CreatedAt >= Start && t.CreatedAt <= End && au.Id == item.Id
                                      select t;

                    var queryCall = from c in DataContext.CallLog
                                    join au in DataContext.AppUser on c.AppUserId equals au.Id
                                    where c.CreatedAt >= Start && c.CreatedAt <= End && au.Id == item.Id
                                    select c;

                    DashboardManager_Top20BusinessTrackingDTO DashboardManager_Top20BusinessTrackingDTO = new DashboardManager_Top20BusinessTrackingDTO
                    {
                        AppUser = new DashboardManager_AppUserDTO(item),
                        Revenue = queryRevenue.Select(x => x.Amount ?? 0).Sum(),
                        OpportunityWin = queryOpportunityWin.Count(),
                        Ticket = queryTicket.Count(),
                        Call = queryCall.Count()
                    };
                    DashboardManager_Top20BusinessTrackingDTOs.Add(DashboardManager_Top20BusinessTrackingDTO);
                }

                return DashboardManager_Top20BusinessTrackingDTOs;
            }
            catch(Exception e)
            {
                return new List<DashboardManager_Top20BusinessTrackingDTO>();
            }
            
        }


        [Route(DashboardManagerRoute.CustomerDistribute), HttpPost]
        public async Task<object
            //DashboardManager_CustomerDistributeDTO
            > CustomerDistribute([FromBody] DashboardManager_CustomerDistributeFilterDTO DashboardManager_CustomerDistributeFilterDTO)
        {if (UnAuthorization) return Forbid();
            AppUser CurrentUser = await AppUserService.Get(CurrentContext.UserId);
            DateTime Now = StaticParams.DateTimeNow.Date;
            DateTime Start = LocalStartDay(CurrentContext);
            DateTime End = new DateTime(Now.Year, Now.Month, Now.Day);
            //Lấy danh sách khách hàng mới : Tính đến thời điểm xét mà chưa phát sinh đơn hàng
            //Lấy danh sách khách hàng cũ : Tính thời điểm đang xét : Lấy tổng số khách hàng đang xét tại thời điểm đó - số khách hàng mới
            var queryCustomerSalesOrder = from t1 in DataContext.Customer
                                          join t2 in DataContext.CustomerSalesOrder on t1.Id equals t2.CustomerId
                                          where (t1.DeletedAt == null && t2.DeletedAt == null)
                                          select new
                                          {
                                              CustomerId = t1.Id,
                                              CreatedAtCustomer = t1.CreatedAt,
                                              CreatedAtOrder = t2.CreatedAt,
                                          };
            var queryAgent = from t1 in DataContext.Customer
                             join t2 in DataContext.DirectSalesOrder on t1.Id equals t2.BuyerStoreId
                             where (t1.DeletedAt == null && t2.DeletedAt == null)
                             select new
                             {
                                 CustomerId = t1.Id,
                                 CreatedAtCustomer = t1.CreatedAt,
                                 CreatedAtOrder = t2.CreatedAt,
                             };
            // Đây là những thằng phát sinh đơn hàng
            var query = queryCustomerSalesOrder.Union(queryAgent);

            //Tổng danh sách khách hàng tại thời điểm đang xét (cả cũ và mới tại thời điểm hiện tại)
            var queryCustomerCurrent = DataContext.Customer
                                        .Where(p =>
                                        !p.DeletedAt.HasValue
                                        );


            if (DashboardManager_CustomerDistributeFilterDTO.Time.Equal.HasValue == false
                || DashboardManager_CustomerDistributeFilterDTO.Time.Equal.Value == THIS_QUARTER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(3).AddSeconds(-1);

                DashboardManager_CustomerDistributeDTO DashboardManager_CustomerDistributeDTO = new DashboardManager_CustomerDistributeDTO();
                DashboardManager_CustomerDistributeDTO.CustomerDistributeByQuarters = new List<DashboardManager_CustomerDistributeByQuarterDTO>();
                int start = 3 * (this_quarter - 1) + 1;
                int end = start + 3;
                for (int i = start; i < end; i++)
                {
                    DashboardManager_CustomerDistributeByQuarterDTO CustomerDistributeByQuarter = new DashboardManager_CustomerDistributeByQuarterDTO
                    {
                        Month = i,
                        New = 0,
                        Old = 0,
                        TotalCurrent = 0,
                    };
                    DashboardManager_CustomerDistributeDTO.CustomerDistributeByQuarters.Add(CustomerDistributeByQuarter);
                }

                foreach (var CustomerDistributeByQuater in DashboardManager_CustomerDistributeDTO.CustomerDistributeByQuarters)
                {
                    var TimeCurrent = new DateTime(End.Year, (int)CustomerDistributeByQuater.Month, DateTime.DaysInMonth(End.Year, (int)CustomerDistributeByQuater.Month));
                    //Tổng số khách hàng tại thời điểm đang xét
                    CustomerDistributeByQuater.TotalCurrent = queryCustomerCurrent.Where(p => p.CreatedAt <= TimeCurrent).Count();
                    //Số khách hàng cũ(phát sinh đơn hàng)
                    CustomerDistributeByQuater.Old = query.Where(p => p.CreatedAtOrder <= TimeCurrent).Select(p => p.CustomerId).Distinct().Count();
                    // Số khách hàng mới(Không phát sinh đơn hàng)
                    CustomerDistributeByQuater.New = CustomerDistributeByQuater.TotalCurrent - CustomerDistributeByQuater.Old;
                }
                return DashboardManager_CustomerDistributeDTO;
            }
            else if (DashboardManager_CustomerDistributeFilterDTO.Time.Equal.Value == LAST_QUATER)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddMonths(-3).AddHours(0 - CurrentContext.TimeZone);
                if (this_quarter == 1)
                {
                    Start = new DateTime(Now.AddYears(-1).Year, 10, 1).AddHours(0 - CurrentContext.TimeZone);
                }
                End = Start.AddMonths(3).AddSeconds(-1);

                DashboardManager_CustomerDistributeDTO DashboardManager_CustomerDistributeDTO = new DashboardManager_CustomerDistributeDTO();
                DashboardManager_CustomerDistributeDTO.CustomerDistributeByQuarters = new List<DashboardManager_CustomerDistributeByQuarterDTO>();
                int start = 3 * (this_quarter - 1) + 1;
                if (this_quarter == 1)
                {
                    start = 10;
                }
                int end = start + 3;

                for (int i = start; i < end; i++)
                {
                    DashboardManager_CustomerDistributeByQuarterDTO CustomerDistributeByQuarter = new DashboardManager_CustomerDistributeByQuarterDTO
                    {
                        Month = i,
                        New = 0,
                        Old = 0,
                        TotalCurrent = 0,
                    };
                    DashboardManager_CustomerDistributeDTO.CustomerDistributeByQuarters.Add(CustomerDistributeByQuarter);
                }

                foreach (var CustomerDistributeByQuater in DashboardManager_CustomerDistributeDTO.CustomerDistributeByQuarters)
                {
                    var TimeCurrent = new DateTime(End.Year, (int)CustomerDistributeByQuater.Month, DateTime.DaysInMonth(End.Year, (int)CustomerDistributeByQuater.Month));
                    //Tổng số khách hàng tại thời điểm đang xét
                    CustomerDistributeByQuater.TotalCurrent = queryCustomerCurrent.Where(p => p.CreatedAt <= TimeCurrent).Count();
                    //Số khách hàng cũ(phát sinh đơn hàng)
                    CustomerDistributeByQuater.Old = query.Where(p => p.CreatedAtOrder <= TimeCurrent).Select(p => p.CustomerId).Distinct().Count();
                    // Số khách hàng mới(Không phát sinh đơn hàng)
                    CustomerDistributeByQuater.New = CustomerDistributeByQuater.TotalCurrent - CustomerDistributeByQuater.Old;
                }
                return DashboardManager_CustomerDistributeDTO;
            }
            else if (DashboardManager_CustomerDistributeFilterDTO.Time.Equal.Value == HALF_YEAR)
            {
                Start = new DateTime(Now.Year, 1, 1).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(6).AddSeconds(-1);

                DashboardManager_CustomerDistributeDTO DashboardManager_CustomerDistributeDTO = new DashboardManager_CustomerDistributeDTO();
                DashboardManager_CustomerDistributeDTO.CustomerDistributeByQuarters = new List<DashboardManager_CustomerDistributeByQuarterDTO>();

                for (int i = 1; i <= 6; i++)
                {
                    DashboardManager_CustomerDistributeByQuarterDTO CustomerDistributeByQuarter = new DashboardManager_CustomerDistributeByQuarterDTO
                    {
                        Month = i,
                        New = 0,
                        Old = 0,
                        TotalCurrent = 0,
                    };
                    DashboardManager_CustomerDistributeDTO.CustomerDistributeByQuarters.Add(CustomerDistributeByQuarter);
                }

                foreach (var CustomerDistributeByQuater in DashboardManager_CustomerDistributeDTO.CustomerDistributeByQuarters)
                {
                    var TimeCurrent = new DateTime(End.Year, (int)CustomerDistributeByQuater.Month, DateTime.DaysInMonth(End.Year, (int)CustomerDistributeByQuater.Month));
                    //Tổng số khách hàng tại thời điểm đang xét
                    CustomerDistributeByQuater.TotalCurrent = queryCustomerCurrent.Where(p => p.CreatedAt <= TimeCurrent).Count();
                    //Số khách hàng cũ(phát sinh đơn hàng)
                    CustomerDistributeByQuater.Old = query.Where(p => p.CreatedAtOrder <= TimeCurrent).Select(p => p.CustomerId).Distinct().Count();
                    // Số khách hàng mới(Không phát sinh đơn hàng)
                    CustomerDistributeByQuater.New = CustomerDistributeByQuater.TotalCurrent - CustomerDistributeByQuater.Old;
                }
                return DashboardManager_CustomerDistributeDTO;
            }
            else if (DashboardManager_CustomerDistributeFilterDTO.Time.Equal.Value == YEAR)
            {
                var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                Start = new DateTime(Now.Year, 1, 1).AddHours(0 - CurrentContext.TimeZone);
                End = Start.AddMonths(12).AddSeconds(-1);

                DashboardManager_CustomerDistributeDTO DashboardManager_CustomerDistributeDTO = new DashboardManager_CustomerDistributeDTO();
                DashboardManager_CustomerDistributeDTO.CustomerDistributeByYears = new List<DashboardManager_CustomerDistributeByYearDTO>();

                for (int i = 1; i <= 4; i++)
                {
                    DashboardManager_CustomerDistributeByYearDTO CustomerDistributeByYear = new DashboardManager_CustomerDistributeByYearDTO
                    {
                        Quarter = i,
                        New = 0,
                        Old = 0,
                        TotalCurrent = 0,
                    };
                    DashboardManager_CustomerDistributeDTO.CustomerDistributeByYears.Add(CustomerDistributeByYear);
                }

                foreach (var CustomerDistributeByQuater in DashboardManager_CustomerDistributeDTO.CustomerDistributeByYears)
                {
                    var TimeCurrent = new DateTime(End.Year, (int)(CustomerDistributeByQuater.Quarter * 3), DateTime.DaysInMonth(End.Year, (int)(CustomerDistributeByQuater.Quarter * 3)));
                    //Tổng số khách hàng tại thời điểm đang xét
                    CustomerDistributeByQuater.TotalCurrent = queryCustomerCurrent.Where(p => p.CreatedAt <= TimeCurrent).Count();
                    //Số khách hàng cũ(phát sinh đơn hàng)
                    CustomerDistributeByQuater.Old = query.Where(p => p.CreatedAtOrder <= TimeCurrent).Select(p => p.CustomerId).Distinct().Count();
                    // Số khách hàng mới(Không phát sinh đơn hàng)
                    CustomerDistributeByQuater.New = CustomerDistributeByQuater.TotalCurrent - CustomerDistributeByQuater.Old;
                }
                return DashboardManager_CustomerDistributeDTO;
            }
            return null;
        }



    }
}
