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
using CRM.Services.MProfession;
using CRM.Services.MStatus;

namespace CRM.Rpc.profession
{
    public partial class ProfessionController : RpcController
    {
        [Route(ProfessionRoute.FilterListStatus), HttpPost]
        public async Task<ActionResult<List<Profession_StatusDTO>>> FilterListStatus([FromBody] Profession_StatusFilterDTO Profession_StatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
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
            List<Profession_StatusDTO> Profession_StatusDTOs = Statuses
                .Select(x => new Profession_StatusDTO(x)).ToList();
            return Profession_StatusDTOs;
        }

        [Route(ProfessionRoute.SingleListStatus), HttpPost]
        public async Task<ActionResult<List<Profession_StatusDTO>>> SingleListStatus([FromBody] Profession_StatusFilterDTO Profession_StatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
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
            List<Profession_StatusDTO> Profession_StatusDTOs = Statuses
                .Select(x => new Profession_StatusDTO(x)).ToList();
            return Profession_StatusDTOs;
        }

    }
}

