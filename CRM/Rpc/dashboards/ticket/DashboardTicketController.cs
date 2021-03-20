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

namespace CRM.Rpc.dashboards.ticket
{
    public class DashboardTicketController : SimpleController
    {
        private const long TODAY = 0;
        private const long THIS_WEEK = 1;
        private const long THIS_MONTH = 2;
        private const long LAST_MONTH = 3;
        private const long THIS_QUARTER = 4;
        private const long LAST_QUATER = 5;
        private const long YEAR = 6;
        private DataContext DataContext;
        private ITicketStatusService TicketStatusService;
        private ITicketTypeService TicketTypeService;


        public DashboardTicketController(
            ITicketStatusService TicketStatusService,
            ITicketTypeService TicketTypeService,
            DataContext DataContext)
        {
            this.DataContext = DataContext;
            this.TicketStatusService = TicketStatusService;
            this.TicketTypeService = TicketTypeService;
        }

        [Route(DashboardTicketRoute.FilterListTime1), HttpPost]
        public List<DashboardTicket_EnumList> FilterListTime1()
        {
            List<DashboardTicket_EnumList> lts_enum = new List<DashboardTicket_EnumList>();
            lts_enum.Add(new DashboardTicket_EnumList { Id = TODAY, Name = "Hôm nay" });
            lts_enum.Add(new DashboardTicket_EnumList { Id = THIS_WEEK, Name = "Tuần này" });
            lts_enum.Add(new DashboardTicket_EnumList { Id = THIS_MONTH, Name = "Tháng này" });
            lts_enum.Add(new DashboardTicket_EnumList { Id = LAST_MONTH, Name = "Tháng trước" });
            return lts_enum;
        }

        [Route(DashboardTicketRoute.FilterListTime2), HttpPost]
        public List<DashboardTicket_EnumList> FilterListTime2()
        {
            List<DashboardTicket_EnumList> lts_enum = new List<DashboardTicket_EnumList>();
            lts_enum.Add(new DashboardTicket_EnumList { Id = THIS_MONTH, Name = "Tháng này" });
            lts_enum.Add(new DashboardTicket_EnumList { Id = LAST_MONTH, Name = "Tháng trước" });
            lts_enum.Add(new DashboardTicket_EnumList { Id = THIS_QUARTER, Name = "Quý này" });
            lts_enum.Add(new DashboardTicket_EnumList { Id = LAST_QUATER, Name = "Quý trước" });
            lts_enum.Add(new DashboardTicket_EnumList { Id = YEAR, Name = "Năm" });
            return lts_enum;
        }

        [Route(DashboardTicketRoute.SingleListTicketStatus), HttpPost]
        public async Task<List<Dashboard_TicketStatusDTO>> SingleListTicketStatus([FromBody] Dashboard_TicketStatusFilterDTO Dashboard_TicketStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketStatusFilter TicketStatusFilter = new TicketStatusFilter();
            TicketStatusFilter.Skip = 0;
            TicketStatusFilter.Take = 20;
            TicketStatusFilter.OrderBy = TicketStatusOrder.Id;
            TicketStatusFilter.OrderType = OrderType.ASC;
            TicketStatusFilter.Selects = TicketStatusSelect.ALL;
            TicketStatusFilter.Id = Dashboard_TicketStatusFilterDTO.Id;
            TicketStatusFilter.Name = Dashboard_TicketStatusFilterDTO.Name;
            TicketStatusFilter.OrderNumber = Dashboard_TicketStatusFilterDTO.OrderNumber;
            TicketStatusFilter.ColorCode = Dashboard_TicketStatusFilterDTO.ColorCode;
            TicketStatusFilter.StatusId = Dashboard_TicketStatusFilterDTO.StatusId;

            List<TicketStatus> TicketStatuses = await TicketStatusService.List(TicketStatusFilter);
            List<Dashboard_TicketStatusDTO> Dashboard_TicketStatusDTOs = TicketStatuses
                .Select(x => new Dashboard_TicketStatusDTO(x)).ToList();
            return Dashboard_TicketStatusDTOs;
        }

        [Route(DashboardTicketRoute.SingleListTicketType), HttpPost]
        public async Task<List<Dashboard_TicketTypeDTO>> SingleListTicketType([FromBody] Dashboard_TicketTypeFilterDTO Dashboard_TicketTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter();
            TicketTypeFilter.Skip = 0;
            TicketTypeFilter.Take = 20;
            TicketTypeFilter.OrderBy = TicketTypeOrder.Id;
            TicketTypeFilter.OrderType = OrderType.ASC;
            TicketTypeFilter.Selects = TicketTypeSelect.ALL;
            TicketTypeFilter.Id = Dashboard_TicketTypeFilterDTO.Id;
            TicketTypeFilter.Name = Dashboard_TicketTypeFilterDTO.Name;
            TicketTypeFilter.ColorCode = Dashboard_TicketTypeFilterDTO.ColorCode;
            TicketTypeFilter.StatusId = Dashboard_TicketTypeFilterDTO.StatusId;

            List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);
            List<Dashboard_TicketTypeDTO> Dashboard_TicketTypeDTOs = TicketTypes
                .Select(x => new Dashboard_TicketTypeDTO(x)).ToList();
            return Dashboard_TicketTypeDTOs;
        }

        [Route(DashboardTicketRoute.CountTicket), HttpPost]
        public async Task<List<DashboardTicket_TicketDTO>> CountTicket([FromBody] DashboardTicket_TicketFilterDTO DashboardTicket_TicketFilterDTO)
        {
            DateTime Now = StaticParams.DateTimeNow.Date;
            DateTime Start = new DateTime(Now.Year, Now.Month, Now.Day);
            DateTime End = new DateTime(Now.Year, Now.Month, Now.Day);
            List<DashboardTicket_TicketDTO> ltsResult = new List<DashboardTicket_TicketDTO>();

            // danh sách loại ticket
            var ltsTicketType = DataContext.TicketType.Where(p => p.DeletedAt == null);
            // foreach ltsTicketType
            // Nếu không filter thì sẽ lấy hết
            if (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal == null)
            {
                foreach (var type in ltsTicketType)
                {
                    DashboardTicket_TicketDTO DashboardTicket_TicketDTO = new DashboardTicket_TicketDTO();
                    DashboardTicket_TicketFilterDTO.TicketTypeId.Equal = type.Id;
                    DashboardTicket_TicketDTO.colorCode = type.ColorCode;
                    DashboardTicket_TicketDTO.TicketType = type.Name;
                    #region Tháng hiện tại
                    if (DashboardTicket_TicketFilterDTO.Time.Equal.HasValue == false
                        || DashboardTicket_TicketFilterDTO.Time.Equal.Value == THIS_MONTH)
                    {
                        Start = new DateTime(Now.Year, Now.Month, 1);
                        End = Start.AddMonths(1).AddSeconds(-1);
                        // Lấy theo thời gian, loại,trạng thái
                        var query = from t in DataContext.Ticket
                                    .Where(p =>
                                    p.CreatedAt >= Start
                                    && p.CreatedAt <= End
                                    && (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal == null || p.TicketStatusId == (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal))
                                    && (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal == null
                                        || p.TicketIssueLevel == null
                                            || p.TicketIssueLevel.TicketGroup == null
                                                || p.TicketIssueLevel.TicketGroup.TicketTypeId == (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal))
                                    )
                                    group t by t.CreatedAt.Day
                                    into x
                                    select new DashboardTicket_TicketByMonthDTO
                                    {
                                        Day = x.Key.ToString(),
                                        Count = x.Count()
                                    };

                        var TicketByMonths = await query.ToListAsync();
                        foreach (var item in TicketByMonths)
                        {
                            item.Day = (item.Day + "/" + Start.Month + "/" + Start.Year).ToString();
                        }
                        DashboardTicket_TicketDTO.TicketByMonths = new List<DashboardTicket_TicketByMonthDTO>();
                        var number_of_day_in_this_month = DateTime.DaysInMonth(Start.Year, Start.Month);
                        for (int i = 1; i < number_of_day_in_this_month + 1; i++)
                        {
                            DashboardTicket_TicketByMonthDTO DashboardTicket_TicketByMonthDTO = new DashboardTicket_TicketByMonthDTO
                            {
                                Day = (i + "/" + Start.Month + "/" + Start.Year).ToString(),
                                Count = 0
                            };
                            DashboardTicket_TicketDTO.TicketByMonths.Add(DashboardTicket_TicketByMonthDTO);
                        }
                        foreach (var p in DashboardTicket_TicketDTO.TicketByMonths)
                        {
                            var data = TicketByMonths.Where(x => x.Day == p.Day).FirstOrDefault();
                            if (data != null)
                            {
                                p.Count = data.Count;
                            }
                        }
                        ltsResult.Add(DashboardTicket_TicketDTO);
                    }
                    #endregion

                    #region Tháng trước
                    else if (DashboardTicket_TicketFilterDTO.Time.Equal.Value == LAST_MONTH)
                    {
                        Start = new DateTime(Now.Year, Now.Month, 1).AddMonths(-1);
                        End = Start.AddMonths(1).AddSeconds(-1);

                        var query = from t in DataContext.Ticket
                                    .Where(p =>
                                    p.CreatedAt >= Start
                                    && p.CreatedAt <= End
                                    && (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal == null || p.TicketStatusId == (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal))
                                    && (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal == null
                                        || p.TicketIssueLevel == null
                                            || p.TicketIssueLevel.TicketGroup == null
                                                || p.TicketIssueLevel.TicketGroup.TicketTypeId == (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal))
                                    )
                                    group t by t.CreatedAt.Day
                                    into x
                                    select new DashboardTicket_TicketByMonthDTO
                                    {
                                        Day = x.Key.ToString(),
                                        Count = x.Count()
                                    };

                        var TicketByMonths = await query.ToListAsync();
                        foreach (var item in TicketByMonths)
                        {
                            item.Day = (item.Day + "/" + Start.Month + "/" + Start.Year).ToString();
                        }
                        DashboardTicket_TicketDTO.TicketByMonths = new List<DashboardTicket_TicketByMonthDTO>();
                        var number_of_day_in_this_month = DateTime.DaysInMonth(Start.Year, Start.Month);
                        for (int i = 1; i < number_of_day_in_this_month + 1; i++)
                        {
                            DashboardTicket_TicketByMonthDTO DashboardTicket_TicketByMonthDTO = new DashboardTicket_TicketByMonthDTO
                            {
                                Day = (i + "/" + Start.Month + "/" + Start.Year).ToString(),
                                Count = 0
                            };
                            DashboardTicket_TicketDTO.TicketByMonths.Add(DashboardTicket_TicketByMonthDTO);
                        }
                        foreach (var p in DashboardTicket_TicketDTO.TicketByMonths)
                        {
                            var data = TicketByMonths.Where(x => x.Day == p.Day).FirstOrDefault();
                            if (data != null)
                            {
                                p.Count = data.Count;
                            }
                        }
                        ltsResult.Add(DashboardTicket_TicketDTO);
                    }
                    #endregion

                    #region Quý này
                    else if (DashboardTicket_TicketFilterDTO.Time.Equal.Value == THIS_QUARTER)
                    {
                        var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                        Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1);
                        End = Start.AddMonths(3).AddSeconds(-1);
                        var query = from t in DataContext.Ticket
                                   .Where(p =>
                                   p.CreatedAt >= Start
                                   && p.CreatedAt <= End
                                   && (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal == null || p.TicketStatusId == (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal))
                                   && (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal == null
                                       || p.TicketIssueLevel == null
                                           || p.TicketIssueLevel.TicketGroup == null
                                               || p.TicketIssueLevel.TicketGroup.TicketTypeId == (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal))
                                   )
                                    group t by t.CreatedAt.Month
                                   into x
                                    select new DashboardTicket_TicketByQuarterDTO
                                    {
                                        Month = x.Key.ToString(),
                                        Count = x.Count()
                                    };
                        var TicketByQuarters = await query.ToListAsync();
                        foreach (var item in TicketByQuarters)
                        {
                            item.Month = (item.Month + "/" + Start.Year).ToString();
                        }
                        int month_start = 3 * (this_quarter - 1) + 1;
                        int month_end = month_start + 3;
                        DashboardTicket_TicketDTO.TicketByQuaters = new List<DashboardTicket_TicketByQuarterDTO>();
                        for (int i = month_start; i < month_end; i++)
                        {
                            DashboardTicket_TicketByQuarterDTO DashboardTicket_TicketByQuarterDTO = new DashboardTicket_TicketByQuarterDTO
                            {
                                Month = (i + "/" + Start.Year).ToString(),
                                Count = 0
                            };
                            DashboardTicket_TicketDTO.TicketByQuaters.Add(DashboardTicket_TicketByQuarterDTO);
                        }
                        foreach (var p in DashboardTicket_TicketDTO.TicketByQuaters)
                        {
                            var data = TicketByQuarters.Where(x => x.Month == p.Month).FirstOrDefault();
                            if (data != null)
                                p.Count = data.Count;
                        }
                        ltsResult.Add(DashboardTicket_TicketDTO);

                    }
                    #endregion

                    #region Qúy trước
                    else if (DashboardTicket_TicketFilterDTO.Time.Equal.Value == LAST_QUATER)
                    {
                        var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                        Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddMonths(-3);
                        End = Start.AddMonths(3).AddSeconds(-1);

                        //Nếu quý hiện tại là quý 1 => quý trước sẽ là quý 4 của năm - 1
                        int month_start = 3 * (this_quarter - 2) + 1;
                        int month_end = month_start + 3;

                        if (this_quarter == 1)
                        {
                            this_quarter = 4;
                            var last_year = DateTime.Now.AddYears(-1);
                            Start = new DateTime(last_year.Year, 10, 1);
                            End = new DateTime(last_year.Year, 12, DateTime.DaysInMonth(last_year.Year, 12)).AddSeconds(-1);
                            month_start = 10;
                            month_end = 13;
                        }
                        var query = from t in DataContext.Ticket
                                   .Where(p =>
                                   p.CreatedAt >= Start
                                   && p.CreatedAt <= End
                                   && (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal == null || p.TicketStatusId == (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal))
                                   && (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal == null
                                       || p.TicketIssueLevel == null
                                           || p.TicketIssueLevel.TicketGroup == null
                                               || p.TicketIssueLevel.TicketGroup.TicketTypeId == (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal))
                                   )
                                    group t by t.CreatedAt.Month
                                   into x
                                    select new DashboardTicket_TicketByQuarterDTO
                                    {
                                        Month = x.Key.ToString(),
                                        Count = x.Count()
                                    };
                        var TicketByQuarters = await query.ToListAsync();
                        foreach (var item in TicketByQuarters)
                        {
                            item.Month = (item.Month + "/" + Start.Year).ToString();
                        }
                        DashboardTicket_TicketDTO.TicketByQuaters = new List<DashboardTicket_TicketByQuarterDTO>();
                        for (int i = month_start; i < month_end; i++)
                        {
                            DashboardTicket_TicketByQuarterDTO DashboardTicket_TicketByQuarterDTO = new DashboardTicket_TicketByQuarterDTO
                            {
                                Month = (i + "/" + Start.Year).ToString(),
                                Count = 0
                            };
                            DashboardTicket_TicketDTO.TicketByQuaters.Add(DashboardTicket_TicketByQuarterDTO);
                        }
                        foreach (var p in DashboardTicket_TicketDTO.TicketByQuaters)
                        {
                            var data = TicketByQuarters.Where(x => x.Month == p.Month).FirstOrDefault();
                            if (data != null)
                                p.Count = data.Count;
                        }
                        ltsResult.Add(DashboardTicket_TicketDTO);
                    }

                    #endregion

                    #region Năm
                    else if (DashboardTicket_TicketFilterDTO.Time.Equal.Value == YEAR)
                    {
                        Start = new DateTime(Now.Year, 1, 1);
                        End = Start.AddYears(1).AddSeconds(-1);

                        var query = from t in DataContext.Ticket
                                   .Where(p =>
                                   p.CreatedAt >= Start
                                   && p.CreatedAt <= End
                                   && (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal == null || p.TicketStatusId == (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal))
                                   && (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal == null
                                       || p.TicketIssueLevel == null
                                           || p.TicketIssueLevel.TicketGroup == null
                                               || p.TicketIssueLevel.TicketGroup.TicketTypeId == (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal))
                                   )
                                    group t by t.CreatedAt.Month
                                   into x
                                    select new DashboardTicket_TicketByYearDTO
                                    {
                                        Month = x.Key.ToString(),
                                        Count = x.Count()
                                    };
                        var TicketByYears = await query.ToListAsync();
                        foreach (var item in TicketByYears)
                        {
                            item.Month = (item.Month + "/" + Start.Year).ToString();
                        }
                        DashboardTicket_TicketDTO.TicketByQuaters = new List<DashboardTicket_TicketByQuarterDTO>();
                        for (int i = 1; i <= 12; i++)
                        {
                            DashboardTicket_TicketByQuarterDTO DashboardTicket_TicketByQuarterDTO = new DashboardTicket_TicketByQuarterDTO
                            {
                                Month = (i + "/" + Start.Year).ToString(),
                                Count = 0
                            };
                            DashboardTicket_TicketDTO.TicketByQuaters.Add(DashboardTicket_TicketByQuarterDTO);
                        }
                        foreach (var p in DashboardTicket_TicketDTO.TicketByQuaters)
                        {
                            var data = TicketByYears.Where(x => x.Month == p.Month).FirstOrDefault();
                            if (data != null)
                                p.Count = data.Count;
                        }
                        ltsResult.Add(DashboardTicket_TicketDTO);
                    }

                    #endregion
                }
            }
            // Nếu filter thì sẽ lấy hết 1 cái
            else
            {
                var type = DataContext.TicketType.Where(p => p.Id == DashboardTicket_TicketFilterDTO.TicketTypeId.Equal).FirstOrDefault();
                DashboardTicket_TicketDTO DashboardTicket_TicketDTO = new DashboardTicket_TicketDTO();
                DashboardTicket_TicketFilterDTO.TicketTypeId.Equal = type.Id;
                DashboardTicket_TicketDTO.colorCode = type.ColorCode;
                DashboardTicket_TicketDTO.TicketType = type.Name;

                #region Tháng hiện tại
                if (DashboardTicket_TicketFilterDTO.Time.Equal.HasValue == false
                    || DashboardTicket_TicketFilterDTO.Time.Equal.Value == THIS_MONTH)
                {
                    Start = new DateTime(Now.Year, Now.Month, 1);
                    End = Start.AddMonths(1).AddSeconds(-1);
                    // Lấy theo thời gian, loại,trạng thái
                    var query = from t in DataContext.Ticket
                                .Where(p =>
                                p.CreatedAt >= Start
                                && p.CreatedAt <= End
                                && (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal == null || p.TicketStatusId == (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal))
                                && (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal == null
                                    || p.TicketIssueLevel == null
                                        || p.TicketIssueLevel.TicketGroup == null
                                            || p.TicketIssueLevel.TicketGroup.TicketTypeId == (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal))
                                )
                                group t by t.CreatedAt.Day
                                into x
                                select new DashboardTicket_TicketByMonthDTO
                                {
                                    Day = x.Key.ToString(),
                                    Count = x.Count()
                                };

                    var TicketByMonths = await query.ToListAsync();
                    foreach (var item in TicketByMonths)
                    {
                        item.Day = (item.Day + "/" + Start.Month + "/" + Start.Year).ToString();
                    }
                    DashboardTicket_TicketDTO.TicketByMonths = new List<DashboardTicket_TicketByMonthDTO>();
                    var number_of_day_in_this_month = DateTime.DaysInMonth(Start.Year, Start.Month);
                    for (int i = 1; i < number_of_day_in_this_month + 1; i++)
                    {
                        DashboardTicket_TicketByMonthDTO DashboardTicket_TicketByMonthDTO = new DashboardTicket_TicketByMonthDTO
                        {
                            Day = (i + "/" + Start.Month + "/" + Start.Year).ToString(),
                            Count = 0
                        };
                        DashboardTicket_TicketDTO.TicketByMonths.Add(DashboardTicket_TicketByMonthDTO);
                    }
                    foreach (var p in DashboardTicket_TicketDTO.TicketByMonths)
                    {
                        var data = TicketByMonths.Where(x => x.Day == p.Day).FirstOrDefault();
                        if (data != null)
                        {
                            p.Count = data.Count;
                        }
                    }
                    ltsResult.Add(DashboardTicket_TicketDTO);
                }
                #endregion

                #region Tháng trước
                else if (DashboardTicket_TicketFilterDTO.Time.Equal.Value == LAST_MONTH)
                {
                    Start = new DateTime(Now.Year, Now.Month, 1).AddMonths(-1);
                    End = Start.AddMonths(1).AddSeconds(-1);

                    var query = from t in DataContext.Ticket
                                .Where(p =>
                                p.CreatedAt >= Start
                                && p.CreatedAt <= End
                                && (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal == null || p.TicketStatusId == (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal))
                                && (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal == null
                                    || p.TicketIssueLevel == null
                                        || p.TicketIssueLevel.TicketGroup == null
                                            || p.TicketIssueLevel.TicketGroup.TicketTypeId == (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal))
                                )
                                group t by t.CreatedAt.Day
                                into x
                                select new DashboardTicket_TicketByMonthDTO
                                {
                                    Day = x.Key.ToString(),
                                    Count = x.Count()
                                };

                    var TicketByMonths = await query.ToListAsync();
                    foreach (var item in TicketByMonths)
                    {
                        item.Day = (item.Day + "/" + Start.Month + "/" + Start.Year).ToString();
                    }
                    DashboardTicket_TicketDTO.TicketByMonths = new List<DashboardTicket_TicketByMonthDTO>();
                    var number_of_day_in_this_month = DateTime.DaysInMonth(Start.Year, Start.Month);
                    for (int i = 1; i < number_of_day_in_this_month + 1; i++)
                    {
                        DashboardTicket_TicketByMonthDTO DashboardTicket_TicketByMonthDTO = new DashboardTicket_TicketByMonthDTO
                        {
                            Day = (i + "/" + Start.Month + "/" + Start.Year).ToString(),
                            Count = 0
                        };
                        DashboardTicket_TicketDTO.TicketByMonths.Add(DashboardTicket_TicketByMonthDTO);
                    }
                    foreach (var p in DashboardTicket_TicketDTO.TicketByMonths)
                    {
                        var data = TicketByMonths.Where(x => x.Day == p.Day).FirstOrDefault();
                        if (data != null)
                        {
                            p.Count = data.Count;
                        }
                    }
                    ltsResult.Add(DashboardTicket_TicketDTO);
                }
                #endregion

                #region Quý này
                else if (DashboardTicket_TicketFilterDTO.Time.Equal.Value == THIS_QUARTER)
                {
                    var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                    Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1);
                    End = Start.AddMonths(3).AddSeconds(-1);
                    var query = from t in DataContext.Ticket
                               .Where(p =>
                               p.CreatedAt >= Start
                               && p.CreatedAt <= End
                               && (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal == null || p.TicketStatusId == (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal))
                               && (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal == null
                                   || p.TicketIssueLevel == null
                                       || p.TicketIssueLevel.TicketGroup == null
                                           || p.TicketIssueLevel.TicketGroup.TicketTypeId == (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal))
                               )
                                group t by t.CreatedAt.Month
                               into x
                                select new DashboardTicket_TicketByQuarterDTO
                                {
                                    Month = x.Key.ToString(),
                                    Count = x.Count()
                                };
                    var TicketByQuarters = await query.ToListAsync();
                    foreach (var item in TicketByQuarters)
                    {
                        item.Month = (item.Month + "/" + Start.Year).ToString();
                    }
                    int month_start = 3 * (this_quarter - 1) + 1;
                    int month_end = month_start + 3;
                    DashboardTicket_TicketDTO.TicketByQuaters = new List<DashboardTicket_TicketByQuarterDTO>();
                    for (int i = month_start; i < month_end; i++)
                    {
                        DashboardTicket_TicketByQuarterDTO DashboardTicket_TicketByQuarterDTO = new DashboardTicket_TicketByQuarterDTO
                        {
                            Month = (i + "/" + Start.Year).ToString(),
                            Count = 0
                        };
                        DashboardTicket_TicketDTO.TicketByQuaters.Add(DashboardTicket_TicketByQuarterDTO);
                    }
                    foreach (var p in DashboardTicket_TicketDTO.TicketByQuaters)
                    {
                        var data = TicketByQuarters.Where(x => x.Month == p.Month).FirstOrDefault();
                        if (data != null)
                            p.Count = data.Count;
                    }
                    ltsResult.Add(DashboardTicket_TicketDTO);

                }
                #endregion

                #region Qúy trước
                else if (DashboardTicket_TicketFilterDTO.Time.Equal.Value == LAST_QUATER)
                {
                    var this_quarter = Convert.ToInt32(Math.Ceiling(Now.Month / 3m));
                    Start = new DateTime(Now.Year, (this_quarter - 1) * 3 + 1, 1).AddMonths(-3);
                    End = Start.AddMonths(3).AddSeconds(-1);

                    //Nếu quý hiện tại là quý 1 => quý trước sẽ là quý 4 của năm - 1
                    int month_start = 3 * (this_quarter - 2) + 1;
                    int month_end = month_start + 3;

                    if (this_quarter == 1)
                    {
                        this_quarter = 4;
                        var last_year = DateTime.Now.AddYears(-1);
                        Start = new DateTime(last_year.Year, 10, 1);
                        End = new DateTime(last_year.Year, 12, DateTime.DaysInMonth(last_year.Year, 12)).AddSeconds(-1);
                        month_start = 10;
                        month_end = 13;
                    }
                    var query = from t in DataContext.Ticket
                               .Where(p =>
                               p.CreatedAt >= Start
                               && p.CreatedAt <= End
                               && (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal == null || p.TicketStatusId == (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal))
                               && (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal == null
                                   || p.TicketIssueLevel == null
                                       || p.TicketIssueLevel.TicketGroup == null
                                           || p.TicketIssueLevel.TicketGroup.TicketTypeId == (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal))
                               )
                                group t by t.CreatedAt.Month
                               into x
                                select new DashboardTicket_TicketByQuarterDTO
                                {
                                    Month = x.Key.ToString(),
                                    Count = x.Count()
                                };
                    var TicketByQuarters = await query.ToListAsync();
                    foreach (var item in TicketByQuarters)
                    {
                        item.Month = (item.Month + "/" + Start.Year).ToString();
                    }
                    DashboardTicket_TicketDTO.TicketByQuaters = new List<DashboardTicket_TicketByQuarterDTO>();
                    for (int i = month_start; i < month_end; i++)
                    {
                        DashboardTicket_TicketByQuarterDTO DashboardTicket_TicketByQuarterDTO = new DashboardTicket_TicketByQuarterDTO
                        {
                            Month = (i + "/" + Start.Year).ToString(),
                            Count = 0
                        };
                        DashboardTicket_TicketDTO.TicketByQuaters.Add(DashboardTicket_TicketByQuarterDTO);
                    }
                    foreach (var p in DashboardTicket_TicketDTO.TicketByQuaters)
                    {
                        var data = TicketByQuarters.Where(x => x.Month == p.Month).FirstOrDefault();
                        if (data != null)
                            p.Count = data.Count;
                    }
                    ltsResult.Add(DashboardTicket_TicketDTO);
                }

                #endregion

                #region Năm
                else if (DashboardTicket_TicketFilterDTO.Time.Equal.Value == YEAR)
                {
                    Start = new DateTime(Now.Year, 1, 1);
                    End = Start.AddYears(1).AddSeconds(-1);

                    var query = from t in DataContext.Ticket
                               .Where(p =>
                               p.CreatedAt >= Start
                               && p.CreatedAt <= End
                               && (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal == null || p.TicketStatusId == (DashboardTicket_TicketFilterDTO.TicketStatusId.Equal))
                               && (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal == null
                                   || p.TicketIssueLevel == null
                                       || p.TicketIssueLevel.TicketGroup == null
                                           || p.TicketIssueLevel.TicketGroup.TicketTypeId == (DashboardTicket_TicketFilterDTO.TicketTypeId.Equal))
                               )
                                group t by t.CreatedAt.Month
                               into x
                                select new DashboardTicket_TicketByYearDTO
                                {
                                    Month = x.Key.ToString(),
                                    Count = x.Count()
                                };
                    var TicketByYears = await query.ToListAsync();
                    foreach (var item in TicketByYears)
                    {
                        item.Month = (item.Month + "/" + Start.Year).ToString();
                    }
                    DashboardTicket_TicketDTO.TicketByQuaters = new List<DashboardTicket_TicketByQuarterDTO>();
                    for (int i = 1; i <= 12; i++)
                    {
                        DashboardTicket_TicketByQuarterDTO DashboardTicket_TicketByQuarterDTO = new DashboardTicket_TicketByQuarterDTO
                        {
                            Month = (i + "/" + Start.Year).ToString(),
                            Count = 0
                        };
                        DashboardTicket_TicketDTO.TicketByQuaters.Add(DashboardTicket_TicketByQuarterDTO);
                    }
                    foreach (var p in DashboardTicket_TicketDTO.TicketByQuaters)
                    {
                        var data = TicketByYears.Where(x => x.Month == p.Month).FirstOrDefault();
                        if (data != null)
                            p.Count = data.Count;
                    }
                    ltsResult.Add(DashboardTicket_TicketDTO);

                }
                #endregion
            }

            return ltsResult;
        }



    }
}
