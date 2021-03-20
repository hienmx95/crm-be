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

namespace CRM.Rpc.ticket_of_user
{
    public partial class TicketOfUserController : RpcController
    {
        [Route(TicketOfUserRoute.FilterListTicket), HttpPost]
        public async Task<List<TicketOfUser_TicketDTO>> FilterListTicket([FromBody] TicketOfUser_TicketFilterDTO TicketOfUser_TicketFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketFilter TicketFilter = new TicketFilter();
            TicketFilter.Skip = 0;
            TicketFilter.Take = 20;
            TicketFilter.OrderBy = TicketOrder.Id;
            TicketFilter.OrderType = OrderType.ASC;
            TicketFilter.Selects = TicketSelect.ALL;
            TicketFilter.Id = TicketOfUser_TicketFilterDTO.Id;
            TicketFilter.Name = TicketOfUser_TicketFilterDTO.Name;
            TicketFilter.Phone = TicketOfUser_TicketFilterDTO.Phone;
            TicketFilter.CustomerId = TicketOfUser_TicketFilterDTO.CustomerId;
            TicketFilter.UserId = TicketOfUser_TicketFilterDTO.UserId;
            TicketFilter.ProductId = TicketOfUser_TicketFilterDTO.ProductId;
            TicketFilter.ReceiveDate = TicketOfUser_TicketFilterDTO.ReceiveDate;
            TicketFilter.ProcessDate = TicketOfUser_TicketFilterDTO.ProcessDate;
            TicketFilter.FinishDate = TicketOfUser_TicketFilterDTO.FinishDate;
            TicketFilter.Subject = TicketOfUser_TicketFilterDTO.Subject;
            TicketFilter.Content = TicketOfUser_TicketFilterDTO.Content;
            TicketFilter.TicketIssueLevelId = TicketOfUser_TicketFilterDTO.TicketIssueLevelId;
            TicketFilter.TicketPriorityId = TicketOfUser_TicketFilterDTO.TicketPriorityId;
            TicketFilter.TicketStatusId = TicketOfUser_TicketFilterDTO.TicketStatusId;
            TicketFilter.TicketSourceId = TicketOfUser_TicketFilterDTO.TicketSourceId;
            TicketFilter.TicketNumber = TicketOfUser_TicketFilterDTO.TicketNumber;
            TicketFilter.DepartmentId = TicketOfUser_TicketFilterDTO.DepartmentId;
            TicketFilter.RelatedTicketId = TicketOfUser_TicketFilterDTO.RelatedTicketId;
            TicketFilter.SLA = TicketOfUser_TicketFilterDTO.SLA;
            TicketFilter.RelatedCallLogId = TicketOfUser_TicketFilterDTO.RelatedCallLogId;
            TicketFilter.ResponseMethodId = TicketOfUser_TicketFilterDTO.ResponseMethodId;
            TicketFilter.StatusId = TicketOfUser_TicketFilterDTO.StatusId;

            List<Ticket> Tickets = await TicketService.List(TicketFilter);
            List<TicketOfUser_TicketDTO> TicketOfUser_TicketDTOs = Tickets
                .Select(x => new TicketOfUser_TicketDTO(x)).ToList();
            return TicketOfUser_TicketDTOs;
        }
        [Route(TicketOfUserRoute.FilterListTicketStatus), HttpPost]
        public async Task<List<TicketOfUser_TicketStatusDTO>> FilterListTicketStatus([FromBody] TicketOfUser_TicketStatusFilterDTO TicketOfUser_TicketStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketStatusFilter TicketStatusFilter = new TicketStatusFilter();
            TicketStatusFilter.Skip = 0;
            TicketStatusFilter.Take = 20;
            TicketStatusFilter.OrderBy = TicketStatusOrder.Id;
            TicketStatusFilter.OrderType = OrderType.ASC;
            TicketStatusFilter.Selects = TicketStatusSelect.ALL;
            TicketStatusFilter.Id = TicketOfUser_TicketStatusFilterDTO.Id;
            TicketStatusFilter.Name = TicketOfUser_TicketStatusFilterDTO.Name;
            TicketStatusFilter.OrderNumber = TicketOfUser_TicketStatusFilterDTO.OrderNumber;
            TicketStatusFilter.ColorCode = TicketOfUser_TicketStatusFilterDTO.ColorCode;
            TicketStatusFilter.StatusId = TicketOfUser_TicketStatusFilterDTO.StatusId;

            List<TicketStatus> TicketStatuses = await TicketStatusService.List(TicketStatusFilter);
            List<TicketOfUser_TicketStatusDTO> TicketOfUser_TicketStatusDTOs = TicketStatuses
                .Select(x => new TicketOfUser_TicketStatusDTO(x)).ToList();
            return TicketOfUser_TicketStatusDTOs;
        }
        [Route(TicketOfUserRoute.FilterListAppUser), HttpPost]
        public async Task<List<TicketOfUser_AppUserDTO>> FilterListAppUser([FromBody] TicketOfUser_AppUserFilterDTO TicketOfUser_AppUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = TicketOfUser_AppUserFilterDTO.Id;
            AppUserFilter.Username = TicketOfUser_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = TicketOfUser_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = TicketOfUser_AppUserFilterDTO.Address;
            AppUserFilter.Email = TicketOfUser_AppUserFilterDTO.Email;
            AppUserFilter.Phone = TicketOfUser_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = TicketOfUser_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = TicketOfUser_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = TicketOfUser_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = TicketOfUser_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = TicketOfUser_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = TicketOfUser_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = TicketOfUser_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = TicketOfUser_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = TicketOfUser_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = TicketOfUser_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<TicketOfUser_AppUserDTO> TicketOfUser_AppUserDTOs = AppUsers
                .Select(x => new TicketOfUser_AppUserDTO(x)).ToList();
            return TicketOfUser_AppUserDTOs;
        }

        [Route(TicketOfUserRoute.SingleListTicket), HttpPost]
        public async Task<List<TicketOfUser_TicketDTO>> SingleListTicket([FromBody] TicketOfUser_TicketFilterDTO TicketOfUser_TicketFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketFilter TicketFilter = new TicketFilter();
            TicketFilter.Skip = 0;
            TicketFilter.Take = 20;
            TicketFilter.OrderBy = TicketOrder.Id;
            TicketFilter.OrderType = OrderType.ASC;
            TicketFilter.Selects = TicketSelect.ALL;
            TicketFilter.Id = TicketOfUser_TicketFilterDTO.Id;
            TicketFilter.Name = TicketOfUser_TicketFilterDTO.Name;
            TicketFilter.Phone = TicketOfUser_TicketFilterDTO.Phone;
            TicketFilter.CustomerId = TicketOfUser_TicketFilterDTO.CustomerId;
            TicketFilter.UserId = TicketOfUser_TicketFilterDTO.UserId;
            TicketFilter.ProductId = TicketOfUser_TicketFilterDTO.ProductId;
            TicketFilter.ReceiveDate = TicketOfUser_TicketFilterDTO.ReceiveDate;
            TicketFilter.ProcessDate = TicketOfUser_TicketFilterDTO.ProcessDate;
            TicketFilter.FinishDate = TicketOfUser_TicketFilterDTO.FinishDate;
            TicketFilter.Subject = TicketOfUser_TicketFilterDTO.Subject;
            TicketFilter.Content = TicketOfUser_TicketFilterDTO.Content;
            TicketFilter.TicketIssueLevelId = TicketOfUser_TicketFilterDTO.TicketIssueLevelId;
            TicketFilter.TicketPriorityId = TicketOfUser_TicketFilterDTO.TicketPriorityId;
            TicketFilter.TicketStatusId = TicketOfUser_TicketFilterDTO.TicketStatusId;
            TicketFilter.TicketSourceId = TicketOfUser_TicketFilterDTO.TicketSourceId;
            TicketFilter.TicketNumber = TicketOfUser_TicketFilterDTO.TicketNumber;
            TicketFilter.DepartmentId = TicketOfUser_TicketFilterDTO.DepartmentId;
            TicketFilter.RelatedTicketId = TicketOfUser_TicketFilterDTO.RelatedTicketId;
            TicketFilter.SLA = TicketOfUser_TicketFilterDTO.SLA;
            TicketFilter.RelatedCallLogId = TicketOfUser_TicketFilterDTO.RelatedCallLogId;
            TicketFilter.ResponseMethodId = TicketOfUser_TicketFilterDTO.ResponseMethodId;
            TicketFilter.StatusId = TicketOfUser_TicketFilterDTO.StatusId;

            List<Ticket> Tickets = await TicketService.List(TicketFilter);
            List<TicketOfUser_TicketDTO> TicketOfUser_TicketDTOs = Tickets
                .Select(x => new TicketOfUser_TicketDTO(x)).ToList();
            return TicketOfUser_TicketDTOs;
        }
        [Route(TicketOfUserRoute.SingleListTicketStatus), HttpPost]
        public async Task<List<TicketOfUser_TicketStatusDTO>> SingleListTicketStatus([FromBody] TicketOfUser_TicketStatusFilterDTO TicketOfUser_TicketStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketStatusFilter TicketStatusFilter = new TicketStatusFilter();
            TicketStatusFilter.Skip = 0;
            TicketStatusFilter.Take = 20;
            TicketStatusFilter.OrderBy = TicketStatusOrder.Id;
            TicketStatusFilter.OrderType = OrderType.ASC;
            TicketStatusFilter.Selects = TicketStatusSelect.ALL;
            TicketStatusFilter.Id = TicketOfUser_TicketStatusFilterDTO.Id;
            TicketStatusFilter.Name = TicketOfUser_TicketStatusFilterDTO.Name;
            TicketStatusFilter.OrderNumber = TicketOfUser_TicketStatusFilterDTO.OrderNumber;
            TicketStatusFilter.ColorCode = TicketOfUser_TicketStatusFilterDTO.ColorCode;
            TicketStatusFilter.StatusId = TicketOfUser_TicketStatusFilterDTO.StatusId;

            List<TicketStatus> TicketStatuses = await TicketStatusService.List(TicketStatusFilter);
            List<TicketOfUser_TicketStatusDTO> TicketOfUser_TicketStatusDTOs = TicketStatuses
                .Select(x => new TicketOfUser_TicketStatusDTO(x)).ToList();
            return TicketOfUser_TicketStatusDTOs;
        }
        [Route(TicketOfUserRoute.SingleListAppUser), HttpPost]
        public async Task<List<TicketOfUser_AppUserDTO>> SingleListAppUser([FromBody] TicketOfUser_AppUserFilterDTO TicketOfUser_AppUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = TicketOfUser_AppUserFilterDTO.Id;
            AppUserFilter.Username = TicketOfUser_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = TicketOfUser_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = TicketOfUser_AppUserFilterDTO.Address;
            AppUserFilter.Email = TicketOfUser_AppUserFilterDTO.Email;
            AppUserFilter.Phone = TicketOfUser_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = TicketOfUser_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = TicketOfUser_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = TicketOfUser_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = TicketOfUser_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = TicketOfUser_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = TicketOfUser_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = TicketOfUser_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = TicketOfUser_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = TicketOfUser_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = TicketOfUser_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<TicketOfUser_AppUserDTO> TicketOfUser_AppUserDTOs = AppUsers
                .Select(x => new TicketOfUser_AppUserDTO(x)).ToList();
            return TicketOfUser_AppUserDTOs;
        }

    }
}

