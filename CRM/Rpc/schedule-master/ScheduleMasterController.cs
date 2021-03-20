using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using CRM.Entities;
using CRM.Services.MScheduleMaster;
using CRM.Services.MAppUser;
using CRM.Services.MStatus;
using CRM.Services.MOrganization;
using CRM.Models;

namespace CRM.Rpc.schedule_master
{
    public class ScheduleMasterController : RpcController
    {
        private IAppUserService AppUserService;
        private IOrganizationService OrganizationService;
        private IStatusService StatusService;
        private IScheduleMasterService ScheduleMasterService;
        private ICurrentContext CurrentContext;
        public ScheduleMasterController(
            IAppUserService AppUserService,
            IOrganizationService OrganizationService,
            IStatusService StatusService,
            IScheduleMasterService ScheduleMasterService,
            ICurrentContext CurrentContext
        ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.AppUserService = AppUserService;
            this.OrganizationService = OrganizationService;
            this.StatusService = StatusService;
            this.ScheduleMasterService = ScheduleMasterService;
            this.CurrentContext = CurrentContext;
        }

        [Route(ScheduleMasterRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] ScheduleMaster_ScheduleMasterFilterDTO ScheduleMaster_ScheduleMasterFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ScheduleMasterFilter ScheduleMasterFilter = ConvertFilterDTOToFilterEntity(ScheduleMaster_ScheduleMasterFilterDTO);
            ScheduleMasterFilter = ScheduleMasterService.ToFilter(ScheduleMasterFilter);
            int count = await ScheduleMasterService.Count(ScheduleMasterFilter);
            return count;
        }

        [Route(ScheduleMasterRoute.List), HttpPost]
        public async Task<ActionResult<List<ScheduleMaster_ScheduleMasterDTO>>> List([FromBody] ScheduleMaster_ScheduleMasterFilterDTO ScheduleMaster_ScheduleMasterFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ScheduleMasterFilter ScheduleMasterFilter = ConvertFilterDTOToFilterEntity(ScheduleMaster_ScheduleMasterFilterDTO);
            ScheduleMasterFilter = ScheduleMasterService.ToFilter(ScheduleMasterFilter);
            List<ScheduleMaster> ScheduleMasters = await ScheduleMasterService.List(ScheduleMasterFilter);
            List<ScheduleMaster_ScheduleMasterDTO> ScheduleMaster_ScheduleMasterDTOs = ScheduleMasters
                .Select(c => new ScheduleMaster_ScheduleMasterDTO(c)).ToList();
            return ScheduleMaster_ScheduleMasterDTOs;
        }

        [Route(ScheduleMasterRoute.Get), HttpPost]
        public async Task<ActionResult<ScheduleMaster_ScheduleMasterDTO>> Get([FromBody]ScheduleMaster_ScheduleMasterDTO ScheduleMaster_ScheduleMasterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(ScheduleMaster_ScheduleMasterDTO.Id))
                return Forbid();

            ScheduleMaster ScheduleMaster = await ScheduleMasterService.Get(ScheduleMaster_ScheduleMasterDTO.Id);
            return new ScheduleMaster_ScheduleMasterDTO(ScheduleMaster);
        }

        [Route(ScheduleMasterRoute.Create), HttpPost]
        public async Task<ActionResult<ScheduleMaster_ScheduleMasterDTO>> Create([FromBody] ScheduleMaster_ScheduleMasterDTO ScheduleMaster_ScheduleMasterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(ScheduleMaster_ScheduleMasterDTO.Id))
                return Forbid();

            ScheduleMaster ScheduleMaster = ConvertDTOToEntity(ScheduleMaster_ScheduleMasterDTO);
            ScheduleMaster = await ScheduleMasterService.Create(ScheduleMaster);
            ScheduleMaster_ScheduleMasterDTO = new ScheduleMaster_ScheduleMasterDTO(ScheduleMaster);
            if (ScheduleMaster.IsValidated)
                return ScheduleMaster_ScheduleMasterDTO;
            else
                return BadRequest(ScheduleMaster_ScheduleMasterDTO);
        }

        [Route(ScheduleMasterRoute.Update), HttpPost]
        public async Task<ActionResult<ScheduleMaster_ScheduleMasterDTO>> Update([FromBody] ScheduleMaster_ScheduleMasterDTO ScheduleMaster_ScheduleMasterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(ScheduleMaster_ScheduleMasterDTO.Id))
                return Forbid();

            ScheduleMaster ScheduleMaster = ConvertDTOToEntity(ScheduleMaster_ScheduleMasterDTO);
            ScheduleMaster = await ScheduleMasterService.Update(ScheduleMaster);
            ScheduleMaster_ScheduleMasterDTO = new ScheduleMaster_ScheduleMasterDTO(ScheduleMaster);
            if (ScheduleMaster.IsValidated)
                return ScheduleMaster_ScheduleMasterDTO;
            else
                return BadRequest(ScheduleMaster_ScheduleMasterDTO);
        }

        [Route(ScheduleMasterRoute.Delete), HttpPost]
        public async Task<ActionResult<ScheduleMaster_ScheduleMasterDTO>> Delete([FromBody] ScheduleMaster_ScheduleMasterDTO ScheduleMaster_ScheduleMasterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(ScheduleMaster_ScheduleMasterDTO.Id))
                return Forbid();

            ScheduleMaster ScheduleMaster = ConvertDTOToEntity(ScheduleMaster_ScheduleMasterDTO);
            ScheduleMaster = await ScheduleMasterService.Delete(ScheduleMaster);
            ScheduleMaster_ScheduleMasterDTO = new ScheduleMaster_ScheduleMasterDTO(ScheduleMaster);
            if (ScheduleMaster.IsValidated)
                return ScheduleMaster_ScheduleMasterDTO;
            else
                return BadRequest(ScheduleMaster_ScheduleMasterDTO);
        }
        
        [Route(ScheduleMasterRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ScheduleMasterFilter ScheduleMasterFilter = new ScheduleMasterFilter();
            ScheduleMasterFilter = ScheduleMasterService.ToFilter(ScheduleMasterFilter);
            ScheduleMasterFilter.Id = new IdFilter { In = Ids };
            ScheduleMasterFilter.Selects = ScheduleMasterSelect.Id;
            ScheduleMasterFilter.Skip = 0;
            ScheduleMasterFilter.Take = int.MaxValue;

            List<ScheduleMaster> ScheduleMasters = await ScheduleMasterService.List(ScheduleMasterFilter);
            ScheduleMasters = await ScheduleMasterService.BulkDelete(ScheduleMasters);
            return true;
        }
        
        [Route(ScheduleMasterRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            AppUserFilter ManagerFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> Managers = await AppUserService.List(ManagerFilter);
            AppUserFilter SalerFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> Salers = await AppUserService.List(SalerFilter);
            StatusFilter StatusFilter = new StatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = StatusSelect.ALL
            };
            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<ScheduleMaster> ScheduleMasters = new List<ScheduleMaster>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(ScheduleMasters);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int ManagerIdColumn = 1 + StartColumn;
                int SalerIdColumn = 2 + StartColumn;
                int NameColumn = 3 + StartColumn;
                int CodeColumn = 4 + StartColumn;
                int StatusIdColumn = 5 + StartColumn;
                int RecurDaysColumn = 6 + StartColumn;
                int StartDateColumn = 7 + StartColumn;
                int EndDateColumn = 8 + StartColumn;
                int NoEndDateColumn = 9 + StartColumn;
                int StartDayOfWeekColumn = 10 + StartColumn;
                int DisplayOrderColumn = 11 + StartColumn;
                int DescriptionColumn = 12 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string ManagerIdValue = worksheet.Cells[i + StartRow, ManagerIdColumn].Value?.ToString();
                    string SalerIdValue = worksheet.Cells[i + StartRow, SalerIdColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string RecurDaysValue = worksheet.Cells[i + StartRow, RecurDaysColumn].Value?.ToString();
                    string StartDateValue = worksheet.Cells[i + StartRow, StartDateColumn].Value?.ToString();
                    string EndDateValue = worksheet.Cells[i + StartRow, EndDateColumn].Value?.ToString();
                    string NoEndDateValue = worksheet.Cells[i + StartRow, NoEndDateColumn].Value?.ToString();
                    string StartDayOfWeekValue = worksheet.Cells[i + StartRow, StartDayOfWeekColumn].Value?.ToString();
                    string DisplayOrderValue = worksheet.Cells[i + StartRow, DisplayOrderColumn].Value?.ToString();
                    string DescriptionValue = worksheet.Cells[i + StartRow, DescriptionColumn].Value?.ToString();
                    
                    ScheduleMaster ScheduleMaster = new ScheduleMaster();
                    ScheduleMaster.Name = NameValue;
                    ScheduleMaster.Code = CodeValue;
                    ScheduleMaster.RecurDays = DateTime.TryParse(RecurDaysValue, out DateTime RecurDays) ? RecurDays : DateTime.Now;
                    ScheduleMaster.StartDate = DateTime.TryParse(StartDateValue, out DateTime StartDate) ? StartDate : DateTime.Now;
                    ScheduleMaster.EndDate = DateTime.TryParse(EndDateValue, out DateTime EndDate) ? EndDate : DateTime.Now;
                    ScheduleMaster.StartDayOfWeek = DateTime.TryParse(StartDayOfWeekValue, out DateTime StartDayOfWeek) ? StartDayOfWeek : DateTime.Now;
                    ScheduleMaster.DisplayOrder = long.TryParse(DisplayOrderValue, out long DisplayOrder) ? DisplayOrder : 0;
                    ScheduleMaster.Description = DescriptionValue;
                    AppUser Manager = Managers.Where(x => x.Id.ToString() == ManagerIdValue).FirstOrDefault();
                    ScheduleMaster.ManagerId = Manager == null ? 0 : Manager.Id;
                    ScheduleMaster.Manager = Manager;
                    AppUser Saler = Salers.Where(x => x.Id.ToString() == SalerIdValue).FirstOrDefault();
                    ScheduleMaster.SalerId = Saler == null ? 0 : Saler.Id;
                    ScheduleMaster.Saler = Saler;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    ScheduleMaster.StatusId = Status == null ? 0 : Status.Id;
                    ScheduleMaster.Status = Status;
                    
                    ScheduleMasters.Add(ScheduleMaster);
                }
            }
            ScheduleMasters = await ScheduleMasterService.Import(ScheduleMasters);
            if (ScheduleMasters.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < ScheduleMasters.Count; i++)
                {
                    ScheduleMaster ScheduleMaster = ScheduleMasters[i];
                    if (!ScheduleMaster.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.Id)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.Id)];
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.ManagerId)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.ManagerId)];
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.SalerId)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.SalerId)];
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.Name)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.Name)];
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.Code)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.Code)];
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.StatusId)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.StatusId)];
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.RecurDays)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.RecurDays)];
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.StartDate)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.StartDate)];
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.EndDate)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.EndDate)];
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.NoEndDate)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.NoEndDate)];
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.StartDayOfWeek)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.StartDayOfWeek)];
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.DisplayOrder)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.DisplayOrder)];
                        if (ScheduleMaster.Errors.ContainsKey(nameof(ScheduleMaster.Description)))
                            Error += ScheduleMaster.Errors[nameof(ScheduleMaster.Description)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(ScheduleMasterRoute.Export), HttpPost]
        public async Task<FileResult> Export([FromBody] ScheduleMaster_ScheduleMasterFilterDTO ScheduleMaster_ScheduleMasterFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region ScheduleMaster
                var ScheduleMasterFilter = ConvertFilterDTOToFilterEntity(ScheduleMaster_ScheduleMasterFilterDTO);
                ScheduleMasterFilter.Skip = 0;
                ScheduleMasterFilter.Take = int.MaxValue;
                ScheduleMasterFilter = ScheduleMasterService.ToFilter(ScheduleMasterFilter);
                List<ScheduleMaster> ScheduleMasters = await ScheduleMasterService.List(ScheduleMasterFilter);

                var ScheduleMasterHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "ManagerId",
                        "SalerId",
                        "Name",
                        "Code",
                        "StatusId",
                        "RecurDays",
                        "StartDate",
                        "EndDate",
                        "NoEndDate",
                        "StartDayOfWeek",
                        "DisplayOrder",
                        "Description",
                    }
                };
                List<object[]> ScheduleMasterData = new List<object[]>();
                for (int i = 0; i < ScheduleMasters.Count; i++)
                {
                    var ScheduleMaster = ScheduleMasters[i];
                    ScheduleMasterData.Add(new Object[]
                    {
                        ScheduleMaster.Id,
                        ScheduleMaster.ManagerId,
                        ScheduleMaster.SalerId,
                        ScheduleMaster.Name,
                        ScheduleMaster.Code,
                        ScheduleMaster.StatusId,
                        ScheduleMaster.RecurDays,
                        ScheduleMaster.StartDate,
                        ScheduleMaster.EndDate,
                        ScheduleMaster.NoEndDate,
                        ScheduleMaster.StartDayOfWeek,
                        ScheduleMaster.DisplayOrder,
                        ScheduleMaster.Description,
                    });
                }
                excel.GenerateWorksheet("ScheduleMaster", ScheduleMasterHeaders, ScheduleMasterData);
                #endregion
                
                #region AppUser
                var AppUserFilter = new AppUserFilter();
                AppUserFilter.Selects = AppUserSelect.ALL;
                AppUserFilter.OrderBy = AppUserOrder.Id;
                AppUserFilter.OrderType = OrderType.ASC;
                AppUserFilter.Skip = 0;
                AppUserFilter.Take = int.MaxValue;
                List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);

                var AppUserHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Username",
                        "DisplayName",
                        "Address",
                        "Email",
                        "Phone",
                        "PositionId",
                        "Department",
                        "OrganizationId",
                        "StatusId",
                        "Avatar",
                        "ProvinceId",
                        "SexId",
                        "Birthday",
                    }
                };
                List<object[]> AppUserData = new List<object[]>();
                for (int i = 0; i < AppUsers.Count; i++)
                {
                    var AppUser = AppUsers[i];
                    AppUserData.Add(new Object[]
                    {
                        AppUser.Id,
                        AppUser.Username,
                        AppUser.DisplayName,
                        AppUser.Address,
                        AppUser.Email,
                        AppUser.Phone,
                        AppUser.PositionId,
                        AppUser.Department,
                        AppUser.OrganizationId,
                        AppUser.StatusId,
                        AppUser.Avatar,
                        AppUser.ProvinceId,
                        AppUser.SexId,
                        AppUser.Birthday,
                    });
                }
                excel.GenerateWorksheet("AppUser", AppUserHeaders, AppUserData);
                #endregion
                #region Status
                var StatusFilter = new StatusFilter();
                StatusFilter.Selects = StatusSelect.ALL;
                StatusFilter.OrderBy = StatusOrder.Id;
                StatusFilter.OrderType = OrderType.ASC;
                StatusFilter.Skip = 0;
                StatusFilter.Take = int.MaxValue;
                List<Status> Statuses = await StatusService.List(StatusFilter);

                var StatusHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> StatusData = new List<object[]>();
                for (int i = 0; i < Statuses.Count; i++)
                {
                    var Status = Statuses[i];
                    StatusData.Add(new Object[]
                    {
                        Status.Id,
                        Status.Code,
                        Status.Name,
                    });
                }
                excel.GenerateWorksheet("Status", StatusHeaders, StatusData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "ScheduleMaster.xlsx");
        }

        [Route(ScheduleMasterRoute.ExportTemplate), HttpPost]
        public async Task<FileResult> ExportTemplate([FromBody] ScheduleMaster_ScheduleMasterFilterDTO ScheduleMaster_ScheduleMasterFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region ScheduleMaster
                var ScheduleMasterHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "ManagerId",
                        "SalerId",
                        "Name",
                        "Code",
                        "StatusId",
                        "RecurDays",
                        "StartDate",
                        "EndDate",
                        "NoEndDate",
                        "StartDayOfWeek",
                        "DisplayOrder",
                        "Description",
                    }
                };
                List<object[]> ScheduleMasterData = new List<object[]>();
                excel.GenerateWorksheet("ScheduleMaster", ScheduleMasterHeaders, ScheduleMasterData);
                #endregion
                
                #region AppUser
                var AppUserFilter = new AppUserFilter();
                AppUserFilter.Selects = AppUserSelect.ALL;
                AppUserFilter.OrderBy = AppUserOrder.Id;
                AppUserFilter.OrderType = OrderType.ASC;
                AppUserFilter.Skip = 0;
                AppUserFilter.Take = int.MaxValue;
                List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);

                var AppUserHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Username",
                        "DisplayName",
                        "Address",
                        "Email",
                        "Phone",
                        "PositionId",
                        "Department",
                        "OrganizationId",
                        "StatusId",
                        "Avatar",
                        "ProvinceId",
                        "SexId",
                        "Birthday",
                    }
                };
                List<object[]> AppUserData = new List<object[]>();
                for (int i = 0; i < AppUsers.Count; i++)
                {
                    var AppUser = AppUsers[i];
                    AppUserData.Add(new Object[]
                    {
                        AppUser.Id,
                        AppUser.Username,
                        AppUser.DisplayName,
                        AppUser.Address,
                        AppUser.Email,
                        AppUser.Phone,
                        AppUser.PositionId,
                        AppUser.Department,
                        AppUser.OrganizationId,
                        AppUser.StatusId,
                        AppUser.Avatar,
                        AppUser.ProvinceId,
                        AppUser.SexId,
                        AppUser.Birthday,
                    });
                }
                excel.GenerateWorksheet("AppUser", AppUserHeaders, AppUserData);
                #endregion
                #region Status
                var StatusFilter = new StatusFilter();
                StatusFilter.Selects = StatusSelect.ALL;
                StatusFilter.OrderBy = StatusOrder.Id;
                StatusFilter.OrderType = OrderType.ASC;
                StatusFilter.Skip = 0;
                StatusFilter.Take = int.MaxValue;
                List<Status> Statuses = await StatusService.List(StatusFilter);

                var StatusHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> StatusData = new List<object[]>();
                for (int i = 0; i < Statuses.Count; i++)
                {
                    var Status = Statuses[i];
                    StatusData.Add(new Object[]
                    {
                        Status.Id,
                        Status.Code,
                        Status.Name,
                    });
                }
                excel.GenerateWorksheet("Status", StatusHeaders, StatusData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "ScheduleMaster.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            ScheduleMasterFilter ScheduleMasterFilter = new ScheduleMasterFilter();
            ScheduleMasterFilter = ScheduleMasterService.ToFilter(ScheduleMasterFilter);
            if (Id == 0)
            {

            }
            else
            {
                ScheduleMasterFilter.Id = new IdFilter { Equal = Id };
                int count = await ScheduleMasterService.Count(ScheduleMasterFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private ScheduleMaster ConvertDTOToEntity(ScheduleMaster_ScheduleMasterDTO ScheduleMaster_ScheduleMasterDTO)
        {
            ScheduleMaster ScheduleMaster = new ScheduleMaster();
            ScheduleMaster.Id = ScheduleMaster_ScheduleMasterDTO.Id;
            ScheduleMaster.ManagerId = ScheduleMaster_ScheduleMasterDTO.ManagerId;
            ScheduleMaster.SalerId = ScheduleMaster_ScheduleMasterDTO.SalerId;
            ScheduleMaster.Name = ScheduleMaster_ScheduleMasterDTO.Name;
            ScheduleMaster.Code = ScheduleMaster_ScheduleMasterDTO.Code;
            ScheduleMaster.StatusId = ScheduleMaster_ScheduleMasterDTO.StatusId;
            ScheduleMaster.RecurDays = ScheduleMaster_ScheduleMasterDTO.RecurDays;
            ScheduleMaster.StartDate = ScheduleMaster_ScheduleMasterDTO.StartDate;
            ScheduleMaster.EndDate = ScheduleMaster_ScheduleMasterDTO.EndDate;
            ScheduleMaster.NoEndDate = ScheduleMaster_ScheduleMasterDTO.NoEndDate;
            ScheduleMaster.StartDayOfWeek = ScheduleMaster_ScheduleMasterDTO.StartDayOfWeek;
            ScheduleMaster.DisplayOrder = ScheduleMaster_ScheduleMasterDTO.DisplayOrder;
            ScheduleMaster.Description = ScheduleMaster_ScheduleMasterDTO.Description;
            ScheduleMaster.Manager = ScheduleMaster_ScheduleMasterDTO.Manager == null ? null : new AppUser
            {
                Id = ScheduleMaster_ScheduleMasterDTO.Manager.Id,
                Username = ScheduleMaster_ScheduleMasterDTO.Manager.Username,
                DisplayName = ScheduleMaster_ScheduleMasterDTO.Manager.DisplayName,
                Address = ScheduleMaster_ScheduleMasterDTO.Manager.Address,
                Email = ScheduleMaster_ScheduleMasterDTO.Manager.Email,
                Phone = ScheduleMaster_ScheduleMasterDTO.Manager.Phone,
                PositionId = ScheduleMaster_ScheduleMasterDTO.Manager.PositionId,
                Department = ScheduleMaster_ScheduleMasterDTO.Manager.Department,
                OrganizationId = ScheduleMaster_ScheduleMasterDTO.Manager.OrganizationId,
                StatusId = ScheduleMaster_ScheduleMasterDTO.Manager.StatusId,
                Avatar = ScheduleMaster_ScheduleMasterDTO.Manager.Avatar,
                ProvinceId = ScheduleMaster_ScheduleMasterDTO.Manager.ProvinceId,
                SexId = ScheduleMaster_ScheduleMasterDTO.Manager.SexId,
                Birthday = ScheduleMaster_ScheduleMasterDTO.Manager.Birthday,
            };
            ScheduleMaster.Saler = ScheduleMaster_ScheduleMasterDTO.Saler == null ? null : new AppUser
            {
                Id = ScheduleMaster_ScheduleMasterDTO.Saler.Id,
                Username = ScheduleMaster_ScheduleMasterDTO.Saler.Username,
                DisplayName = ScheduleMaster_ScheduleMasterDTO.Saler.DisplayName,
                Address = ScheduleMaster_ScheduleMasterDTO.Saler.Address,
                Email = ScheduleMaster_ScheduleMasterDTO.Saler.Email,
                Phone = ScheduleMaster_ScheduleMasterDTO.Saler.Phone,
                PositionId = ScheduleMaster_ScheduleMasterDTO.Saler.PositionId,
                Department = ScheduleMaster_ScheduleMasterDTO.Saler.Department,
                OrganizationId = ScheduleMaster_ScheduleMasterDTO.Saler.OrganizationId,
                StatusId = ScheduleMaster_ScheduleMasterDTO.Saler.StatusId,
                Avatar = ScheduleMaster_ScheduleMasterDTO.Saler.Avatar,
                ProvinceId = ScheduleMaster_ScheduleMasterDTO.Saler.ProvinceId,
                SexId = ScheduleMaster_ScheduleMasterDTO.Saler.SexId,
                Birthday = ScheduleMaster_ScheduleMasterDTO.Saler.Birthday,
            };
            ScheduleMaster.Status = ScheduleMaster_ScheduleMasterDTO.Status == null ? null : new Status
            {
                Id = ScheduleMaster_ScheduleMasterDTO.Status.Id,
                Code = ScheduleMaster_ScheduleMasterDTO.Status.Code,
                Name = ScheduleMaster_ScheduleMasterDTO.Status.Name,
            };
            ScheduleMaster.BaseLanguage = CurrentContext.Language;
            return ScheduleMaster;
        }

        private ScheduleMasterFilter ConvertFilterDTOToFilterEntity(ScheduleMaster_ScheduleMasterFilterDTO ScheduleMaster_ScheduleMasterFilterDTO)
        {
            ScheduleMasterFilter ScheduleMasterFilter = new ScheduleMasterFilter();
            ScheduleMasterFilter.Selects = ScheduleMasterSelect.ALL;
            ScheduleMasterFilter.Skip = ScheduleMaster_ScheduleMasterFilterDTO.Skip;
            ScheduleMasterFilter.Take = ScheduleMaster_ScheduleMasterFilterDTO.Take;
            ScheduleMasterFilter.OrderBy = ScheduleMaster_ScheduleMasterFilterDTO.OrderBy;
            ScheduleMasterFilter.OrderType = ScheduleMaster_ScheduleMasterFilterDTO.OrderType;

            ScheduleMasterFilter.Id = ScheduleMaster_ScheduleMasterFilterDTO.Id;
            ScheduleMasterFilter.ManagerId = ScheduleMaster_ScheduleMasterFilterDTO.ManagerId;
            ScheduleMasterFilter.SalerId = ScheduleMaster_ScheduleMasterFilterDTO.SalerId;
            ScheduleMasterFilter.Name = ScheduleMaster_ScheduleMasterFilterDTO.Name;
            ScheduleMasterFilter.Code = ScheduleMaster_ScheduleMasterFilterDTO.Code;
            ScheduleMasterFilter.StatusId = ScheduleMaster_ScheduleMasterFilterDTO.StatusId;
            ScheduleMasterFilter.RecurDays = ScheduleMaster_ScheduleMasterFilterDTO.RecurDays;
            ScheduleMasterFilter.StartDate = ScheduleMaster_ScheduleMasterFilterDTO.StartDate;
            ScheduleMasterFilter.EndDate = ScheduleMaster_ScheduleMasterFilterDTO.EndDate;
            ScheduleMasterFilter.StartDayOfWeek = ScheduleMaster_ScheduleMasterFilterDTO.StartDayOfWeek;
            ScheduleMasterFilter.DisplayOrder = ScheduleMaster_ScheduleMasterFilterDTO.DisplayOrder;
            ScheduleMasterFilter.Description = ScheduleMaster_ScheduleMasterFilterDTO.Description;
            ScheduleMasterFilter.CreatedAt = ScheduleMaster_ScheduleMasterFilterDTO.CreatedAt;
            ScheduleMasterFilter.UpdatedAt = ScheduleMaster_ScheduleMasterFilterDTO.UpdatedAt;
            return ScheduleMasterFilter;
        }

        [Route(ScheduleMasterRoute.FilterListAppUser), HttpPost]
        public async Task<List<ScheduleMaster_AppUserDTO>> FilterListAppUser([FromBody] ScheduleMaster_AppUserFilterDTO ScheduleMaster_AppUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = ScheduleMaster_AppUserFilterDTO.Id;
            AppUserFilter.Username = ScheduleMaster_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = ScheduleMaster_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = ScheduleMaster_AppUserFilterDTO.Address;
            AppUserFilter.Email = ScheduleMaster_AppUserFilterDTO.Email;
            AppUserFilter.Phone = ScheduleMaster_AppUserFilterDTO.Phone;
            AppUserFilter.PositionId = ScheduleMaster_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = ScheduleMaster_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = ScheduleMaster_AppUserFilterDTO.OrganizationId;
            AppUserFilter.StatusId = ScheduleMaster_AppUserFilterDTO.StatusId; 
            AppUserFilter.ProvinceId = ScheduleMaster_AppUserFilterDTO.ProvinceId;
            AppUserFilter.SexId = ScheduleMaster_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = ScheduleMaster_AppUserFilterDTO.Birthday;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<ScheduleMaster_AppUserDTO> ScheduleMaster_AppUserDTOs = AppUsers
                .Select(x => new ScheduleMaster_AppUserDTO(x)).ToList();
            return ScheduleMaster_AppUserDTOs;
        }
        [Route(ScheduleMasterRoute.FilterListStatus), HttpPost]
        public async Task<List<ScheduleMaster_StatusDTO>> FilterListStatus([FromBody] ScheduleMaster_StatusFilterDTO ScheduleMaster_StatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = int.MaxValue;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<ScheduleMaster_StatusDTO> ScheduleMaster_StatusDTOs = Statuses
                .Select(x => new ScheduleMaster_StatusDTO(x)).ToList();
            return ScheduleMaster_StatusDTOs;
        }

        [Route(ScheduleMasterRoute.SingleListAppUser), HttpPost]
        public async Task<List<ScheduleMaster_AppUserDTO>> SingleListAppUser([FromBody] ScheduleMaster_AppUserFilterDTO ScheduleMaster_AppUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = ScheduleMaster_AppUserFilterDTO.Id;
            AppUserFilter.Username = ScheduleMaster_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = ScheduleMaster_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = ScheduleMaster_AppUserFilterDTO.Address;
            AppUserFilter.Email = ScheduleMaster_AppUserFilterDTO.Email;
            AppUserFilter.Phone = ScheduleMaster_AppUserFilterDTO.Phone;
            AppUserFilter.PositionId = ScheduleMaster_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = ScheduleMaster_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = ScheduleMaster_AppUserFilterDTO.OrganizationId;
            AppUserFilter.StatusId = ScheduleMaster_AppUserFilterDTO.StatusId; 
            AppUserFilter.ProvinceId = ScheduleMaster_AppUserFilterDTO.ProvinceId;
            AppUserFilter.SexId = ScheduleMaster_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = ScheduleMaster_AppUserFilterDTO.Birthday;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<ScheduleMaster_AppUserDTO> ScheduleMaster_AppUserDTOs = AppUsers
                .Select(x => new ScheduleMaster_AppUserDTO(x)).ToList();
            return ScheduleMaster_AppUserDTOs;
        }
        [Route(ScheduleMasterRoute.SingleListStatus), HttpPost]
        public async Task<List<ScheduleMaster_StatusDTO>> SingleListStatus([FromBody] ScheduleMaster_StatusFilterDTO ScheduleMaster_StatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = int.MaxValue;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<ScheduleMaster_StatusDTO> ScheduleMaster_StatusDTOs = Statuses
                .Select(x => new ScheduleMaster_StatusDTO(x)).ToList();
            return ScheduleMaster_StatusDTOs;
        }

    }
}

