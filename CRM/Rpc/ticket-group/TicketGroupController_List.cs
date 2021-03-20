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
using CRM.Services.MTicketGroup;
using CRM.Services.MStatus;
using CRM.Services.MTicketType;

namespace CRM.Rpc.ticket_group
{
    public partial class TicketGroupController : RpcController
    {
        [Route(TicketGroupRoute.FilterListStatus), HttpPost]
        public async Task<List<TicketGroup_StatusDTO>> FilterListStatus([FromBody] TicketGroup_StatusFilterDTO TicketGroup_StatusFilterDTO)
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
            List<TicketGroup_StatusDTO> TicketGroup_StatusDTOs = Statuses
                .Select(x => new TicketGroup_StatusDTO(x)).ToList();
            return TicketGroup_StatusDTOs;
        }
        [Route(TicketGroupRoute.FilterListTicketType), HttpPost]
        public async Task<List<TicketGroup_TicketTypeDTO>> FilterListTicketType([FromBody] TicketGroup_TicketTypeFilterDTO TicketGroup_TicketTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter();
            TicketTypeFilter.Skip = 0;
            TicketTypeFilter.Take = 20;
            TicketTypeFilter.OrderBy = TicketTypeOrder.Id;
            TicketTypeFilter.OrderType = OrderType.ASC;
            TicketTypeFilter.Selects = TicketTypeSelect.ALL;
            TicketTypeFilter.Id = TicketGroup_TicketTypeFilterDTO.Id;
            TicketTypeFilter.Code = TicketGroup_TicketTypeFilterDTO.Code;
            TicketTypeFilter.Name = TicketGroup_TicketTypeFilterDTO.Name;
            TicketTypeFilter.ColorCode = TicketGroup_TicketTypeFilterDTO.ColorCode;
            TicketTypeFilter.StatusId = TicketGroup_TicketTypeFilterDTO.StatusId;

            List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);
            List<TicketGroup_TicketTypeDTO> TicketGroup_TicketTypeDTOs = TicketTypes
                .Select(x => new TicketGroup_TicketTypeDTO(x)).ToList();
            return TicketGroup_TicketTypeDTOs;
        }

        [Route(TicketGroupRoute.SingleListStatus), HttpPost]
        public async Task<List<TicketGroup_StatusDTO>> SingleListStatus([FromBody] TicketGroup_StatusFilterDTO TicketGroup_StatusFilterDTO)
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
            List<TicketGroup_StatusDTO> TicketGroup_StatusDTOs = Statuses
                .Select(x => new TicketGroup_StatusDTO(x)).ToList();
            return TicketGroup_StatusDTOs;
        }
        [Route(TicketGroupRoute.SingleListTicketType), HttpPost]
        public async Task<List<TicketGroup_TicketTypeDTO>> SingleListTicketType([FromBody] TicketGroup_TicketTypeFilterDTO TicketGroup_TicketTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter();
            TicketTypeFilter.Skip = 0;
            TicketTypeFilter.Take = 20;
            TicketTypeFilter.OrderBy = TicketTypeOrder.Id;
            TicketTypeFilter.OrderType = OrderType.ASC;
            TicketTypeFilter.Selects = TicketTypeSelect.ALL;
            TicketTypeFilter.Id = TicketGroup_TicketTypeFilterDTO.Id;
            TicketTypeFilter.Code = TicketGroup_TicketTypeFilterDTO.Code;
            TicketTypeFilter.Name = TicketGroup_TicketTypeFilterDTO.Name;
            TicketTypeFilter.ColorCode = TicketGroup_TicketTypeFilterDTO.ColorCode;
            TicketTypeFilter.StatusId = TicketGroup_TicketTypeFilterDTO.StatusId;

            List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);
            List<TicketGroup_TicketTypeDTO> TicketGroup_TicketTypeDTOs = TicketTypes
                .Select(x => new TicketGroup_TicketTypeDTO(x)).ToList();
            return TicketGroup_TicketTypeDTOs;
        }

    }
}

