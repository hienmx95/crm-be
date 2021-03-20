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
using CRM.Services.MPosition;
using CRM.Services.MStatus;

namespace CRM.Rpc.position
{
    public partial class PositionController : RpcController
    {
        [Route(PositionRoute.FilterListStatus), HttpPost]
        public async Task<ActionResult<List<Position_StatusDTO>>> FilterListStatus([FromBody] Position_StatusFilterDTO Position_StatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;
            StatusFilter.Id = Position_StatusFilterDTO.Id;
            StatusFilter.Code = Position_StatusFilterDTO.Code;
            StatusFilter.Name = Position_StatusFilterDTO.Name;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<Position_StatusDTO> Position_StatusDTOs = Statuses
                .Select(x => new Position_StatusDTO(x)).ToList();
            return Position_StatusDTOs;
        }

        [Route(PositionRoute.SingleListStatus), HttpPost]
        public async Task<ActionResult<List<Position_StatusDTO>>> SingleListStatus([FromBody] Position_StatusFilterDTO Position_StatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;
            StatusFilter.Id = Position_StatusFilterDTO.Id;
            StatusFilter.Code = Position_StatusFilterDTO.Code;
            StatusFilter.Name = Position_StatusFilterDTO.Name;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<Position_StatusDTO> Position_StatusDTOs = Statuses
                .Select(x => new Position_StatusDTO(x)).ToList();
            return Position_StatusDTOs;
        }

    }
}

