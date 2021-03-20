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
using CRM.Services.MTicketIssueLevel;
using CRM.Services.MStatus;
using CRM.Services.MTicketGroup;
using CRM.Services.MTicketType;
using CRM.Enums;

namespace CRM.Rpc.ticket_issue_level
{
    public partial class TicketIssueLevelController : RpcController
    {
        [Route(TicketIssueLevelRoute.FilterListStatus), HttpPost]
        public async Task<List<TicketIssueLevel_StatusDTO>> FilterListStatus([FromBody] TicketIssueLevel_StatusFilterDTO TicketIssueLevel_StatusFilterDTO)
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
            List<TicketIssueLevel_StatusDTO> TicketIssueLevel_StatusDTOs = Statuses
                .Select(x => new TicketIssueLevel_StatusDTO(x)).ToList();
            return TicketIssueLevel_StatusDTOs;
        }
        [Route(TicketIssueLevelRoute.FilterListTicketGroup), HttpPost]
        public async Task<List<TicketIssueLevel_TicketGroupDTO>> FilterListTicketGroup([FromBody] TicketIssueLevel_TicketGroupFilterDTO TicketIssueLevel_TicketGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketGroupFilter TicketGroupFilter = new TicketGroupFilter();
            TicketGroupFilter.Skip = 0;
            TicketGroupFilter.Take = 20;
            TicketGroupFilter.OrderBy = TicketGroupOrder.Id;
            TicketGroupFilter.OrderType = OrderType.ASC;
            TicketGroupFilter.Selects = TicketGroupSelect.ALL;
            TicketGroupFilter.Id = TicketIssueLevel_TicketGroupFilterDTO.Id;
            TicketGroupFilter.Name = TicketIssueLevel_TicketGroupFilterDTO.Name;
            TicketGroupFilter.OrderNumber = TicketIssueLevel_TicketGroupFilterDTO.OrderNumber;
            TicketGroupFilter.StatusId = TicketIssueLevel_TicketGroupFilterDTO.StatusId;
            TicketGroupFilter.TicketTypeId = TicketIssueLevel_TicketGroupFilterDTO.TicketTypeId;
            TicketGroupFilter.StatusId.Equal = StatusEnum.ACTIVE.Id;

            List<TicketGroup> TicketGroups = await TicketGroupService.List(TicketGroupFilter);
            List<TicketIssueLevel_TicketGroupDTO> TicketIssueLevel_TicketGroupDTOs = TicketGroups
                .Select(x => new TicketIssueLevel_TicketGroupDTO(x)).ToList();
            return TicketIssueLevel_TicketGroupDTOs;
        }
        [Route(TicketIssueLevelRoute.FilterListTicketType), HttpPost]
        public async Task<List<TicketIssueLevel_TicketTypeDTO>> FilterListTicketType([FromBody] TicketIssueLevel_TicketTypeFilterDTO TicketIssueLevel_TicketTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter();
            TicketTypeFilter.Skip = 0;
            TicketTypeFilter.Take = 20;
            TicketTypeFilter.OrderBy = TicketTypeOrder.Id;
            TicketTypeFilter.OrderType = OrderType.ASC;
            TicketTypeFilter.Selects = TicketTypeSelect.ALL;
            TicketTypeFilter.Id = TicketIssueLevel_TicketTypeFilterDTO.Id;
            TicketTypeFilter.Name = TicketIssueLevel_TicketTypeFilterDTO.Name;
            TicketTypeFilter.Code = TicketIssueLevel_TicketTypeFilterDTO.Code;
            TicketTypeFilter.StatusId = TicketIssueLevel_TicketTypeFilterDTO.StatusId;
            TicketTypeFilter.StatusId.Equal = StatusEnum.ACTIVE.Id;

            List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);
            List<TicketIssueLevel_TicketTypeDTO> TicketIssueLevel_TicketTypeDTOs = TicketTypes
                .Select(x => new TicketIssueLevel_TicketTypeDTO(x)).ToList();
            return TicketIssueLevel_TicketTypeDTOs;
        }
        [Route(TicketIssueLevelRoute.FilterListSLATimeUnit), HttpPost]
        public async Task<List<TicketIssueLevel_SLATimeUnitDTO>> FilterListSLATimeUnit([FromBody] TicketIssueLevel_SLATimeUnitFilterDTO TicketIssueLevel_SLATimeUnitFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SLATimeUnitFilter SLATimeUnitFilter = new SLATimeUnitFilter();
            SLATimeUnitFilter.Skip = 0;
            SLATimeUnitFilter.Take = 20;
            SLATimeUnitFilter.OrderBy = SLATimeUnitOrder.Id;
            SLATimeUnitFilter.OrderType = OrderType.ASC;
            SLATimeUnitFilter.Selects = SLATimeUnitSelect.ALL;
            SLATimeUnitFilter.Id = TicketIssueLevel_SLATimeUnitFilterDTO.Id;
            SLATimeUnitFilter.Code = TicketIssueLevel_SLATimeUnitFilterDTO.Code;
            SLATimeUnitFilter.Name = TicketIssueLevel_SLATimeUnitFilterDTO.Name;

            List<SLATimeUnit> SLATimeUnits = await SLATimeUnitService.List(SLATimeUnitFilter);
            List<TicketIssueLevel_SLATimeUnitDTO> TicketIssueLevel_SLATimeUnitDTOs = SLATimeUnits
                .Select(x => new TicketIssueLevel_SLATimeUnitDTO(x)).ToList();
            return TicketIssueLevel_SLATimeUnitDTOs;
        }
        [Route(TicketIssueLevelRoute.SingleListStatus), HttpPost]
        public async Task<List<TicketIssueLevel_StatusDTO>> SingleListStatus([FromBody] TicketIssueLevel_StatusFilterDTO TicketIssueLevel_StatusFilterDTO)
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
            List<TicketIssueLevel_StatusDTO> TicketIssueLevel_StatusDTOs = Statuses
                .Select(x => new TicketIssueLevel_StatusDTO(x)).ToList();
            return TicketIssueLevel_StatusDTOs;
        }
        [Route(TicketIssueLevelRoute.SingleListTicketGroup), HttpPost]
        public async Task<List<TicketIssueLevel_TicketGroupDTO>> SingleListTicketGroup([FromBody] TicketIssueLevel_TicketGroupFilterDTO TicketIssueLevel_TicketGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketGroupFilter TicketGroupFilter = new TicketGroupFilter();
            TicketGroupFilter.Skip = 0;
            TicketGroupFilter.Take = 20;
            TicketGroupFilter.OrderBy = TicketGroupOrder.Id;
            TicketGroupFilter.OrderType = OrderType.ASC;
            TicketGroupFilter.Selects = TicketGroupSelect.ALL;
            TicketGroupFilter.Id = TicketIssueLevel_TicketGroupFilterDTO.Id;
            TicketGroupFilter.Name = TicketIssueLevel_TicketGroupFilterDTO.Name;
            TicketGroupFilter.OrderNumber = TicketIssueLevel_TicketGroupFilterDTO.OrderNumber;
            TicketGroupFilter.StatusId = TicketIssueLevel_TicketGroupFilterDTO.StatusId;
            TicketGroupFilter.TicketTypeId = TicketIssueLevel_TicketGroupFilterDTO.TicketTypeId;
            TicketGroupFilter.StatusId.Equal = StatusEnum.ACTIVE.Id;

            List<TicketGroup> TicketGroups = await TicketGroupService.List(TicketGroupFilter);
            List<TicketIssueLevel_TicketGroupDTO> TicketIssueLevel_TicketGroupDTOs = TicketGroups
                .Select(x => new TicketIssueLevel_TicketGroupDTO(x)).ToList();
            return TicketIssueLevel_TicketGroupDTOs;
        }
        [Route(TicketIssueLevelRoute.SingleListTicketType), HttpPost]
        public async Task<List<TicketIssueLevel_TicketTypeDTO>> SingleListTicketType([FromBody] TicketIssueLevel_TicketTypeFilterDTO TicketIssueLevel_TicketTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter();
            TicketTypeFilter.Skip = 0;
            TicketTypeFilter.Take = 20;
            TicketTypeFilter.OrderBy = TicketTypeOrder.Id;
            TicketTypeFilter.OrderType = OrderType.ASC;
            TicketTypeFilter.Selects = TicketTypeSelect.ALL;
            TicketTypeFilter.Id = TicketIssueLevel_TicketTypeFilterDTO.Id;
            TicketTypeFilter.Code = TicketIssueLevel_TicketTypeFilterDTO.Code;
            TicketTypeFilter.Name = TicketIssueLevel_TicketTypeFilterDTO.Name;
            TicketTypeFilter.ColorCode = TicketIssueLevel_TicketTypeFilterDTO.ColorCode;
            TicketTypeFilter.StatusId = TicketIssueLevel_TicketTypeFilterDTO.StatusId;
            TicketTypeFilter.StatusId.Equal = StatusEnum.ACTIVE.Id;

            List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);
            List<TicketIssueLevel_TicketTypeDTO> TicketGroup_TicketTypeDTOs = TicketTypes
                .Select(x => new TicketIssueLevel_TicketTypeDTO(x)).ToList();
            return TicketGroup_TicketTypeDTOs;
        }
        [Route(TicketIssueLevelRoute.SingleListSLATimeUnit), HttpPost]
        public async Task<List<TicketIssueLevel_SLATimeUnitDTO>> SingleListSLATimeUnit([FromBody] TicketIssueLevel_SLATimeUnitFilterDTO TicketIssueLevel_SLATimeUnitFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SLATimeUnitFilter SLATimeUnitFilter = new SLATimeUnitFilter();
            SLATimeUnitFilter.Skip = 0;
            SLATimeUnitFilter.Take = 20;
            SLATimeUnitFilter.OrderBy = SLATimeUnitOrder.Id;
            SLATimeUnitFilter.OrderType = OrderType.ASC;
            SLATimeUnitFilter.Selects = SLATimeUnitSelect.ALL;
            SLATimeUnitFilter.Id = TicketIssueLevel_SLATimeUnitFilterDTO.Id;
            SLATimeUnitFilter.Code = TicketIssueLevel_SLATimeUnitFilterDTO.Code;
            SLATimeUnitFilter.Name = TicketIssueLevel_SLATimeUnitFilterDTO.Name;

            List<SLATimeUnit> SLATimeUnits = await SLATimeUnitService.List(SLATimeUnitFilter);
            List<TicketIssueLevel_SLATimeUnitDTO> TicketIssueLevel_SLATimeUnitDTOs = SLATimeUnits
                .Select(x => new TicketIssueLevel_SLATimeUnitDTO(x)).ToList();
            return TicketIssueLevel_SLATimeUnitDTOs;
        }
        [Route(TicketIssueLevelRoute.SingleListSmsTemplate), HttpPost]
        public async Task<List<TicketIssueLevel_SmsTemplateDTO>> SingleListSmsTemplate([FromBody] TicketIssueLevel_SmsTemplateFilterDTO TicketIssueLevel_SmsTemplateFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SmsTemplateFilter SmsTemplateFilter = new SmsTemplateFilter();
            SmsTemplateFilter.Skip = 0;
            SmsTemplateFilter.Take = 20;
            SmsTemplateFilter.OrderBy = SmsTemplateOrder.Id;
            SmsTemplateFilter.OrderType = OrderType.ASC;
            SmsTemplateFilter.Selects = SmsTemplateSelect.ALL;
            SmsTemplateFilter.Id = TicketIssueLevel_SmsTemplateFilterDTO.Id;
            SmsTemplateFilter.Code = TicketIssueLevel_SmsTemplateFilterDTO.Code;
            SmsTemplateFilter.Name = TicketIssueLevel_SmsTemplateFilterDTO.Name;
            SmsTemplateFilter.Content = TicketIssueLevel_SmsTemplateFilterDTO.Content;

            List<SmsTemplate> SmsTemplates = await SmsTemplateService.List(SmsTemplateFilter);
            List<TicketIssueLevel_SmsTemplateDTO> TicketIssueLevel_SmsTemplateDTOs = SmsTemplates
                .Select(x => new TicketIssueLevel_SmsTemplateDTO(x)).ToList();
            return TicketIssueLevel_SmsTemplateDTOs;
        }

        [Route(TicketIssueLevelRoute.SingleListMailTemplate), HttpPost]
        public async Task<List<TicketIssueLevel_MailTemplateDTO>> SingleListMailTemplate([FromBody] TicketIssueLevel_MailTemplateFilterDTO TicketIssueLevel_MailTemplateFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MailTemplateFilter MailTemplateFilter = new MailTemplateFilter();
            MailTemplateFilter.Skip = 0;
            MailTemplateFilter.Take = 20;
            MailTemplateFilter.OrderBy = MailTemplateOrder.Id;
            MailTemplateFilter.OrderType = OrderType.ASC;
            MailTemplateFilter.Selects = MailTemplateSelect.ALL;
            MailTemplateFilter.Id = TicketIssueLevel_MailTemplateFilterDTO.Id;
            MailTemplateFilter.Code = TicketIssueLevel_MailTemplateFilterDTO.Code;
            MailTemplateFilter.Name = TicketIssueLevel_MailTemplateFilterDTO.Name;
            MailTemplateFilter.Content = TicketIssueLevel_MailTemplateFilterDTO.Content;

            List<MailTemplate> MailTemplates = await MailTemplateService.List(MailTemplateFilter);
            List<TicketIssueLevel_MailTemplateDTO> TicketIssueLevel_MailTemplateDTOs = MailTemplates
                .Select(x => new TicketIssueLevel_MailTemplateDTO(x)).ToList();
            return TicketIssueLevel_MailTemplateDTOs;
        }
        [Route(TicketIssueLevelRoute.SingleListAppUser), HttpPost]
        public async Task<List<TicketIssueLevel_AppUserDTO>> SingleListAppUser([FromBody] TicketIssueLevel_AppUserFilterDTO TicketIssueLevel_AppUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = TicketIssueLevel_AppUserFilterDTO.Id;
            AppUserFilter.Username = TicketIssueLevel_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = TicketIssueLevel_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = TicketIssueLevel_AppUserFilterDTO.Address;
            AppUserFilter.Email = TicketIssueLevel_AppUserFilterDTO.Email;
            AppUserFilter.Phone = TicketIssueLevel_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = TicketIssueLevel_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = TicketIssueLevel_AppUserFilterDTO.Birthday;
            AppUserFilter.PositionId = TicketIssueLevel_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = TicketIssueLevel_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = TicketIssueLevel_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = TicketIssueLevel_AppUserFilterDTO.ProvinceId;
            AppUserFilter.StatusId = TicketIssueLevel_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<TicketIssueLevel_AppUserDTO> TicketIssueLevel_AppUserDTOs = AppUsers
                .Select(x => new TicketIssueLevel_AppUserDTO(x)).ToList();
            return TicketIssueLevel_AppUserDTOs;
        }
    }
}

