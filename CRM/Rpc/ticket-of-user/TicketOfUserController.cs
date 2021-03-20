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
using CRM.Services.MTicketOfUser;
using CRM.Services.MTicket;
using CRM.Services.MTicketStatus;
using CRM.Services.MAppUser;
using CRM.Services.MOrganization;
using CRM.Models;

namespace CRM.Rpc.ticket_of_user
{
    public partial class TicketOfUserController : RpcController
    {
        private ITicketService TicketService;
        private ITicketStatusService TicketStatusService;
        private IAppUserService AppUserService;
        private IOrganizationService OrganizationService;
        private ITicketOfUserService TicketOfUserService;
        private ICurrentContext CurrentContext;
        public TicketOfUserController(
            ITicketService TicketService,
            ITicketStatusService TicketStatusService,
            IAppUserService AppUserService,
            IOrganizationService OrganizationService,
            ITicketOfUserService TicketOfUserService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.TicketService = TicketService;
            this.TicketStatusService = TicketStatusService;
            this.AppUserService = AppUserService;
            this.OrganizationService = OrganizationService;
            this.TicketOfUserService = TicketOfUserService;
            this.CurrentContext = CurrentContext;
        }

        [Route(TicketOfUserRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] TicketOfUser_TicketOfUserFilterDTO TicketOfUser_TicketOfUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketOfUserFilter TicketOfUserFilter = ConvertFilterDTOToFilterEntity(TicketOfUser_TicketOfUserFilterDTO);
            TicketOfUserFilter = TicketOfUserService.ToFilter(TicketOfUserFilter);
            int count = await TicketOfUserService.Count(TicketOfUserFilter);
            return count;
        }

        [Route(TicketOfUserRoute.List), HttpPost]
        public async Task<ActionResult<List<TicketOfUser_TicketOfUserDTO>>> List([FromBody] TicketOfUser_TicketOfUserFilterDTO TicketOfUser_TicketOfUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketOfUserFilter TicketOfUserFilter = ConvertFilterDTOToFilterEntity(TicketOfUser_TicketOfUserFilterDTO);
            TicketOfUserFilter = TicketOfUserService.ToFilter(TicketOfUserFilter);
            List<TicketOfUser> TicketOfUsers = await TicketOfUserService.List(TicketOfUserFilter);
            List<TicketOfUser_TicketOfUserDTO> TicketOfUser_TicketOfUserDTOs = TicketOfUsers
                .Select(c => new TicketOfUser_TicketOfUserDTO(c)).ToList();
            return TicketOfUser_TicketOfUserDTOs;
        }

        [Route(TicketOfUserRoute.Get), HttpPost]
        public async Task<ActionResult<TicketOfUser_TicketOfUserDTO>> Get([FromBody]TicketOfUser_TicketOfUserDTO TicketOfUser_TicketOfUserDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketOfUser_TicketOfUserDTO.Id))
                return Forbid();

            TicketOfUser TicketOfUser = await TicketOfUserService.Get(TicketOfUser_TicketOfUserDTO.Id);
            return new TicketOfUser_TicketOfUserDTO(TicketOfUser);
        }

        [Route(TicketOfUserRoute.Create), HttpPost]
        public async Task<ActionResult<TicketOfUser_TicketOfUserDTO>> Create([FromBody] TicketOfUser_TicketOfUserDTO TicketOfUser_TicketOfUserDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(TicketOfUser_TicketOfUserDTO.Id))
                return Forbid();

            TicketOfUser TicketOfUser = ConvertDTOToEntity(TicketOfUser_TicketOfUserDTO);
            TicketOfUser = await TicketOfUserService.Create(TicketOfUser);
            TicketOfUser_TicketOfUserDTO = new TicketOfUser_TicketOfUserDTO(TicketOfUser);
            if (TicketOfUser.IsValidated)
                return TicketOfUser_TicketOfUserDTO;
            else
                return BadRequest(TicketOfUser_TicketOfUserDTO);
        }

        [Route(TicketOfUserRoute.Update), HttpPost]
        public async Task<ActionResult<TicketOfUser_TicketOfUserDTO>> Update([FromBody] TicketOfUser_TicketOfUserDTO TicketOfUser_TicketOfUserDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(TicketOfUser_TicketOfUserDTO.Id))
                return Forbid();

            TicketOfUser TicketOfUser = ConvertDTOToEntity(TicketOfUser_TicketOfUserDTO);
            TicketOfUser = await TicketOfUserService.Update(TicketOfUser);
            TicketOfUser_TicketOfUserDTO = new TicketOfUser_TicketOfUserDTO(TicketOfUser);
            if (TicketOfUser.IsValidated)
                return TicketOfUser_TicketOfUserDTO;
            else
                return BadRequest(TicketOfUser_TicketOfUserDTO);
        }

        [Route(TicketOfUserRoute.Delete), HttpPost]
        public async Task<ActionResult<TicketOfUser_TicketOfUserDTO>> Delete([FromBody] TicketOfUser_TicketOfUserDTO TicketOfUser_TicketOfUserDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketOfUser_TicketOfUserDTO.Id))
                return Forbid();

            TicketOfUser TicketOfUser = ConvertDTOToEntity(TicketOfUser_TicketOfUserDTO);
            TicketOfUser = await TicketOfUserService.Delete(TicketOfUser);
            TicketOfUser_TicketOfUserDTO = new TicketOfUser_TicketOfUserDTO(TicketOfUser);
            if (TicketOfUser.IsValidated)
                return TicketOfUser_TicketOfUserDTO;
            else
                return BadRequest(TicketOfUser_TicketOfUserDTO);
        }
        
        [Route(TicketOfUserRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketOfUserFilter TicketOfUserFilter = new TicketOfUserFilter();
            TicketOfUserFilter = TicketOfUserService.ToFilter(TicketOfUserFilter);
            TicketOfUserFilter.Id = new IdFilter { In = Ids };
            TicketOfUserFilter.Selects = TicketOfUserSelect.Id;
            TicketOfUserFilter.Skip = 0;
            TicketOfUserFilter.Take = int.MaxValue;

            List<TicketOfUser> TicketOfUsers = await TicketOfUserService.List(TicketOfUserFilter);
            TicketOfUsers = await TicketOfUserService.BulkDelete(TicketOfUsers);
            if (TicketOfUsers.Any(x => !x.IsValidated))
                return BadRequest(TicketOfUsers.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(TicketOfUserRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            TicketFilter TicketFilter = new TicketFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = TicketSelect.ALL
            };
            List<Ticket> Tickets = await TicketService.List(TicketFilter);
            TicketStatusFilter TicketStatusFilter = new TicketStatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = TicketStatusSelect.ALL
            };
            List<TicketStatus> TicketStatuses = await TicketStatusService.List(TicketStatusFilter);
            AppUserFilter UserFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> Users = await AppUserService.List(UserFilter);
            List<TicketOfUser> TicketOfUsers = new List<TicketOfUser>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(TicketOfUsers);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int NotesColumn = 1 + StartColumn;
                int UserIdColumn = 2 + StartColumn;
                int TicketIdColumn = 3 + StartColumn;
                int TicketStatusIdColumn = 4 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string NotesValue = worksheet.Cells[i + StartRow, NotesColumn].Value?.ToString();
                    string UserIdValue = worksheet.Cells[i + StartRow, UserIdColumn].Value?.ToString();
                    string TicketIdValue = worksheet.Cells[i + StartRow, TicketIdColumn].Value?.ToString();
                    string TicketStatusIdValue = worksheet.Cells[i + StartRow, TicketStatusIdColumn].Value?.ToString();
                    
                    TicketOfUser TicketOfUser = new TicketOfUser();
                    TicketOfUser.Notes = NotesValue;
                    Ticket Ticket = Tickets.Where(x => x.Id.ToString() == TicketIdValue).FirstOrDefault();
                    TicketOfUser.TicketId = Ticket == null ? 0 : Ticket.Id;
                    TicketOfUser.Ticket = Ticket;
                    TicketStatus TicketStatus = TicketStatuses.Where(x => x.Id.ToString() == TicketStatusIdValue).FirstOrDefault();
                    TicketOfUser.TicketStatusId = TicketStatus == null ? 0 : TicketStatus.Id;
                    TicketOfUser.TicketStatus = TicketStatus;
                    AppUser User = Users.Where(x => x.Id.ToString() == UserIdValue).FirstOrDefault();
                    TicketOfUser.UserId = User == null ? 0 : User.Id;
                    TicketOfUser.User = User;
                    
                    TicketOfUsers.Add(TicketOfUser);
                }
            }
            TicketOfUsers = await TicketOfUserService.Import(TicketOfUsers);
            if (TicketOfUsers.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < TicketOfUsers.Count; i++)
                {
                    TicketOfUser TicketOfUser = TicketOfUsers[i];
                    if (!TicketOfUser.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (TicketOfUser.Errors.ContainsKey(nameof(TicketOfUser.Id)))
                            Error += TicketOfUser.Errors[nameof(TicketOfUser.Id)];
                        if (TicketOfUser.Errors.ContainsKey(nameof(TicketOfUser.Notes)))
                            Error += TicketOfUser.Errors[nameof(TicketOfUser.Notes)];
                        if (TicketOfUser.Errors.ContainsKey(nameof(TicketOfUser.UserId)))
                            Error += TicketOfUser.Errors[nameof(TicketOfUser.UserId)];
                        if (TicketOfUser.Errors.ContainsKey(nameof(TicketOfUser.TicketId)))
                            Error += TicketOfUser.Errors[nameof(TicketOfUser.TicketId)];
                        if (TicketOfUser.Errors.ContainsKey(nameof(TicketOfUser.TicketStatusId)))
                            Error += TicketOfUser.Errors[nameof(TicketOfUser.TicketStatusId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(TicketOfUserRoute.Export), HttpPost]
        public async Task<FileResult> Export([FromBody] TicketOfUser_TicketOfUserFilterDTO TicketOfUser_TicketOfUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketOfUser
                var TicketOfUserFilter = ConvertFilterDTOToFilterEntity(TicketOfUser_TicketOfUserFilterDTO);
                TicketOfUserFilter.Skip = 0;
                TicketOfUserFilter.Take = int.MaxValue;
                TicketOfUserFilter = TicketOfUserService.ToFilter(TicketOfUserFilter);
                List<TicketOfUser> TicketOfUsers = await TicketOfUserService.List(TicketOfUserFilter);

                var TicketOfUserHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Notes",
                        "UserId",
                        "TicketId",
                        "TicketStatusId",
                    }
                };
                List<object[]> TicketOfUserData = new List<object[]>();
                for (int i = 0; i < TicketOfUsers.Count; i++)
                {
                    var TicketOfUser = TicketOfUsers[i];
                    TicketOfUserData.Add(new Object[]
                    {
                        TicketOfUser.Id,
                        TicketOfUser.Notes,
                        TicketOfUser.UserId,
                        TicketOfUser.TicketId,
                        TicketOfUser.TicketStatusId,
                    });
                }
                excel.GenerateWorksheet("TicketOfUser", TicketOfUserHeaders, TicketOfUserData);
                #endregion
                
                #region Ticket
                var TicketFilter = new TicketFilter();
                TicketFilter.Selects = TicketSelect.ALL;
                TicketFilter.OrderBy = TicketOrder.Id;
                TicketFilter.OrderType = OrderType.ASC;
                TicketFilter.Skip = 0;
                TicketFilter.Take = int.MaxValue;
                List<Ticket> Tickets = await TicketService.List(TicketFilter);

                var TicketHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Name",
                        "Phone",
                        "CustomerId",
                        "UserId",
                        "ProductId",
                        "ReceiveDate",
                        "ProcessDate",
                        "FinishDate",
                        "Subject",
                        "Content",
                        "TicketIssueLevelId",
                        "TicketPriorityId",
                        "TicketStatusId",
                        "TicketSourceId",
                        "TicketNumber",
                        "DepartmentId",
                        "RelatedTicketId",
                        "SLA",
                        "RelatedCallLogId",
                        "ResponseMethodId",
                        "StatusId",
                        "Used",
                    }
                };
                List<object[]> TicketData = new List<object[]>();
                for (int i = 0; i < Tickets.Count; i++)
                {
                    var Ticket = Tickets[i];
                    TicketData.Add(new Object[]
                    {
                        Ticket.Id,
                        Ticket.Name,
                        Ticket.Phone,
                        Ticket.CustomerId,
                        Ticket.UserId,
                        Ticket.ProductId,
                        Ticket.ReceiveDate,
                        Ticket.ProcessDate,
                        Ticket.FinishDate,
                        Ticket.Subject,
                        Ticket.Content,
                        Ticket.TicketIssueLevelId,
                        Ticket.TicketPriorityId,
                        Ticket.TicketStatusId,
                        Ticket.TicketSourceId,
                        Ticket.TicketNumber,
                        Ticket.DepartmentId,
                        Ticket.RelatedTicketId,
                        Ticket.SLA,
                        Ticket.RelatedCallLogId,
                        Ticket.ResponseMethodId,
                        Ticket.StatusId,
                        Ticket.Used,
                    });
                }
                excel.GenerateWorksheet("Ticket", TicketHeaders, TicketData);
                #endregion
                #region TicketStatus
                var TicketStatusFilter = new TicketStatusFilter();
                TicketStatusFilter.Selects = TicketStatusSelect.ALL;
                TicketStatusFilter.OrderBy = TicketStatusOrder.Id;
                TicketStatusFilter.OrderType = OrderType.ASC;
                TicketStatusFilter.Skip = 0;
                TicketStatusFilter.Take = int.MaxValue;
                List<TicketStatus> TicketStatuses = await TicketStatusService.List(TicketStatusFilter);

                var TicketStatusHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Name",
                        "OrderNumber",
                        "ColorCode",
                        "StatusId",
                        "Used",
                    }
                };
                List<object[]> TicketStatusData = new List<object[]>();
                for (int i = 0; i < TicketStatuses.Count; i++)
                {
                    var TicketStatus = TicketStatuses[i];
                    TicketStatusData.Add(new Object[]
                    {
                        TicketStatus.Id,
                        TicketStatus.Name,
                        TicketStatus.OrderNumber,
                        TicketStatus.ColorCode,
                        TicketStatus.StatusId,
                        TicketStatus.Used,
                    });
                }
                excel.GenerateWorksheet("TicketStatus", TicketStatusHeaders, TicketStatusData);
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
                        "SexId",
                        "Birthday",
                        "Avatar",
                        "PositionId",
                        "Department",
                        "OrganizationId",
                        "ProvinceId",
                        "Longitude",
                        "Latitude",
                        "StatusId",
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
                        AppUser.SexId,
                        AppUser.Birthday,
                        AppUser.Avatar,
                        AppUser.PositionId,
                        AppUser.Department,
                        AppUser.OrganizationId,
                        AppUser.ProvinceId,
                        AppUser.Longitude,
                        AppUser.Latitude,
                        AppUser.StatusId,
                    });
                }
                excel.GenerateWorksheet("AppUser", AppUserHeaders, AppUserData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketOfUser.xlsx");
        }

        [Route(TicketOfUserRoute.ExportTemplate), HttpPost]
        public async Task<FileResult> ExportTemplate([FromBody] TicketOfUser_TicketOfUserFilterDTO TicketOfUser_TicketOfUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketOfUser
                var TicketOfUserHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Notes",
                        "UserId",
                        "TicketId",
                        "TicketStatusId",
                    }
                };
                List<object[]> TicketOfUserData = new List<object[]>();
                excel.GenerateWorksheet("TicketOfUser", TicketOfUserHeaders, TicketOfUserData);
                #endregion
                
                #region Ticket
                var TicketFilter = new TicketFilter();
                TicketFilter.Selects = TicketSelect.ALL;
                TicketFilter.OrderBy = TicketOrder.Id;
                TicketFilter.OrderType = OrderType.ASC;
                TicketFilter.Skip = 0;
                TicketFilter.Take = int.MaxValue;
                List<Ticket> Tickets = await TicketService.List(TicketFilter);

                var TicketHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Name",
                        "Phone",
                        "CustomerId",
                        "UserId",
                        "ProductId",
                        "ReceiveDate",
                        "ProcessDate",
                        "FinishDate",
                        "Subject",
                        "Content",
                        "TicketIssueLevelId",
                        "TicketPriorityId",
                        "TicketStatusId",
                        "TicketSourceId",
                        "TicketNumber",
                        "DepartmentId",
                        "RelatedTicketId",
                        "SLA",
                        "RelatedCallLogId",
                        "ResponseMethodId",
                        "StatusId",
                        "Used",
                    }
                };
                List<object[]> TicketData = new List<object[]>();
                for (int i = 0; i < Tickets.Count; i++)
                {
                    var Ticket = Tickets[i];
                    TicketData.Add(new Object[]
                    {
                        Ticket.Id,
                        Ticket.Name,
                        Ticket.Phone,
                        Ticket.CustomerId,
                        Ticket.UserId,
                        Ticket.ProductId,
                        Ticket.ReceiveDate,
                        Ticket.ProcessDate,
                        Ticket.FinishDate,
                        Ticket.Subject,
                        Ticket.Content,
                        Ticket.TicketIssueLevelId,
                        Ticket.TicketPriorityId,
                        Ticket.TicketStatusId,
                        Ticket.TicketSourceId,
                        Ticket.TicketNumber,
                        Ticket.DepartmentId,
                        Ticket.RelatedTicketId,
                        Ticket.SLA,
                        Ticket.RelatedCallLogId,
                        Ticket.ResponseMethodId,
                        Ticket.StatusId,
                        Ticket.Used,
                    });
                }
                excel.GenerateWorksheet("Ticket", TicketHeaders, TicketData);
                #endregion
                #region TicketStatus
                var TicketStatusFilter = new TicketStatusFilter();
                TicketStatusFilter.Selects = TicketStatusSelect.ALL;
                TicketStatusFilter.OrderBy = TicketStatusOrder.Id;
                TicketStatusFilter.OrderType = OrderType.ASC;
                TicketStatusFilter.Skip = 0;
                TicketStatusFilter.Take = int.MaxValue;
                List<TicketStatus> TicketStatuses = await TicketStatusService.List(TicketStatusFilter);

                var TicketStatusHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Name",
                        "OrderNumber",
                        "ColorCode",
                        "StatusId",
                        "Used",
                    }
                };
                List<object[]> TicketStatusData = new List<object[]>();
                for (int i = 0; i < TicketStatuses.Count; i++)
                {
                    var TicketStatus = TicketStatuses[i];
                    TicketStatusData.Add(new Object[]
                    {
                        TicketStatus.Id,
                        TicketStatus.Name,
                        TicketStatus.OrderNumber,
                        TicketStatus.ColorCode,
                        TicketStatus.StatusId,
                        TicketStatus.Used,
                    });
                }
                excel.GenerateWorksheet("TicketStatus", TicketStatusHeaders, TicketStatusData);
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
                        "SexId",
                        "Birthday",
                        "Avatar",
                        "PositionId",
                        "Department",
                        "OrganizationId",
                        "ProvinceId",
                        "Longitude",
                        "Latitude",
                        "StatusId",
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
                        AppUser.SexId,
                        AppUser.Birthday,
                        AppUser.Avatar,
                        AppUser.PositionId,
                        AppUser.Department,
                        AppUser.OrganizationId,
                        AppUser.ProvinceId,
                        AppUser.Longitude,
                        AppUser.Latitude,
                        AppUser.StatusId,
                    });
                }
                excel.GenerateWorksheet("AppUser", AppUserHeaders, AppUserData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketOfUser.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            TicketOfUserFilter TicketOfUserFilter = new TicketOfUserFilter();
            TicketOfUserFilter = TicketOfUserService.ToFilter(TicketOfUserFilter);
            if (Id == 0)
            {

            }
            else
            {
                TicketOfUserFilter.Id = new IdFilter { Equal = Id };
                int count = await TicketOfUserService.Count(TicketOfUserFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private TicketOfUser ConvertDTOToEntity(TicketOfUser_TicketOfUserDTO TicketOfUser_TicketOfUserDTO)
        {
            TicketOfUser TicketOfUser = new TicketOfUser();
            TicketOfUser.Id = TicketOfUser_TicketOfUserDTO.Id;
            TicketOfUser.Notes = TicketOfUser_TicketOfUserDTO.Notes;
            TicketOfUser.UserId = TicketOfUser_TicketOfUserDTO.UserId;
            TicketOfUser.TicketId = TicketOfUser_TicketOfUserDTO.TicketId;
            TicketOfUser.TicketStatusId = TicketOfUser_TicketOfUserDTO.TicketStatusId;
            TicketOfUser.Ticket = TicketOfUser_TicketOfUserDTO.Ticket == null ? null : new Ticket
            {
                Id = TicketOfUser_TicketOfUserDTO.Ticket.Id,
                Name = TicketOfUser_TicketOfUserDTO.Ticket.Name,
                Phone = TicketOfUser_TicketOfUserDTO.Ticket.Phone,
                CustomerId = TicketOfUser_TicketOfUserDTO.Ticket.CustomerId,
                UserId = TicketOfUser_TicketOfUserDTO.Ticket.UserId,
                ProductId = TicketOfUser_TicketOfUserDTO.Ticket.ProductId,
                ReceiveDate = TicketOfUser_TicketOfUserDTO.Ticket.ReceiveDate,
                ProcessDate = TicketOfUser_TicketOfUserDTO.Ticket.ProcessDate,
                FinishDate = TicketOfUser_TicketOfUserDTO.Ticket.FinishDate,
                Subject = TicketOfUser_TicketOfUserDTO.Ticket.Subject,
                Content = TicketOfUser_TicketOfUserDTO.Ticket.Content,
                TicketIssueLevelId = TicketOfUser_TicketOfUserDTO.Ticket.TicketIssueLevelId,
                TicketPriorityId = TicketOfUser_TicketOfUserDTO.Ticket.TicketPriorityId,
                TicketStatusId = TicketOfUser_TicketOfUserDTO.Ticket.TicketStatusId,
                TicketSourceId = TicketOfUser_TicketOfUserDTO.Ticket.TicketSourceId,
                TicketNumber = TicketOfUser_TicketOfUserDTO.Ticket.TicketNumber,
                DepartmentId = TicketOfUser_TicketOfUserDTO.Ticket.DepartmentId,
                RelatedTicketId = TicketOfUser_TicketOfUserDTO.Ticket.RelatedTicketId,
                SLA = TicketOfUser_TicketOfUserDTO.Ticket.SLA,
                RelatedCallLogId = TicketOfUser_TicketOfUserDTO.Ticket.RelatedCallLogId,
                ResponseMethodId = TicketOfUser_TicketOfUserDTO.Ticket.ResponseMethodId,
                StatusId = TicketOfUser_TicketOfUserDTO.Ticket.StatusId,
                Used = TicketOfUser_TicketOfUserDTO.Ticket.Used,
            };
            TicketOfUser.TicketStatus = TicketOfUser_TicketOfUserDTO.TicketStatus == null ? null : new TicketStatus
            {
                Id = TicketOfUser_TicketOfUserDTO.TicketStatus.Id,
                Name = TicketOfUser_TicketOfUserDTO.TicketStatus.Name,
                OrderNumber = TicketOfUser_TicketOfUserDTO.TicketStatus.OrderNumber,
                ColorCode = TicketOfUser_TicketOfUserDTO.TicketStatus.ColorCode,
                StatusId = TicketOfUser_TicketOfUserDTO.TicketStatus.StatusId,
                Used = TicketOfUser_TicketOfUserDTO.TicketStatus.Used,
            };
            TicketOfUser.User = TicketOfUser_TicketOfUserDTO.User == null ? null : new AppUser
            {
                Id = TicketOfUser_TicketOfUserDTO.User.Id,
                Username = TicketOfUser_TicketOfUserDTO.User.Username,
                DisplayName = TicketOfUser_TicketOfUserDTO.User.DisplayName,
                Address = TicketOfUser_TicketOfUserDTO.User.Address,
                Email = TicketOfUser_TicketOfUserDTO.User.Email,
                Phone = TicketOfUser_TicketOfUserDTO.User.Phone,
                SexId = TicketOfUser_TicketOfUserDTO.User.SexId,
                Birthday = TicketOfUser_TicketOfUserDTO.User.Birthday,
                Avatar = TicketOfUser_TicketOfUserDTO.User.Avatar,
                PositionId = TicketOfUser_TicketOfUserDTO.User.PositionId,
                Department = TicketOfUser_TicketOfUserDTO.User.Department,
                OrganizationId = TicketOfUser_TicketOfUserDTO.User.OrganizationId,
                ProvinceId = TicketOfUser_TicketOfUserDTO.User.ProvinceId,
                Longitude = TicketOfUser_TicketOfUserDTO.User.Longitude,
                Latitude = TicketOfUser_TicketOfUserDTO.User.Latitude,
                StatusId = TicketOfUser_TicketOfUserDTO.User.StatusId,
            };
            TicketOfUser.BaseLanguage = CurrentContext.Language;
            return TicketOfUser;
        }

        private TicketOfUserFilter ConvertFilterDTOToFilterEntity(TicketOfUser_TicketOfUserFilterDTO TicketOfUser_TicketOfUserFilterDTO)
        {
            TicketOfUserFilter TicketOfUserFilter = new TicketOfUserFilter();
            TicketOfUserFilter.Selects = TicketOfUserSelect.ALL;
            TicketOfUserFilter.Skip = TicketOfUser_TicketOfUserFilterDTO.Skip;
            TicketOfUserFilter.Take = TicketOfUser_TicketOfUserFilterDTO.Take;
            TicketOfUserFilter.OrderBy = TicketOfUser_TicketOfUserFilterDTO.OrderBy;
            TicketOfUserFilter.OrderType = TicketOfUser_TicketOfUserFilterDTO.OrderType;

            TicketOfUserFilter.Id = TicketOfUser_TicketOfUserFilterDTO.Id;
            TicketOfUserFilter.Notes = TicketOfUser_TicketOfUserFilterDTO.Notes;
            TicketOfUserFilter.UserId = TicketOfUser_TicketOfUserFilterDTO.UserId;
            TicketOfUserFilter.TicketId = TicketOfUser_TicketOfUserFilterDTO.TicketId;
            TicketOfUserFilter.TicketStatusId = TicketOfUser_TicketOfUserFilterDTO.TicketStatusId;
            TicketOfUserFilter.CreatedAt = TicketOfUser_TicketOfUserFilterDTO.CreatedAt;
            TicketOfUserFilter.UpdatedAt = TicketOfUser_TicketOfUserFilterDTO.UpdatedAt;
            return TicketOfUserFilter;
        }
    }
}

