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
using CRM.Services.MPhoneType;
using CRM.Services.MStatus;

namespace CRM.Rpc.phone_type
{
    public partial class PhoneTypeController : RpcController
    {
        [Route(PhoneTypeRoute.FilterListStatus), HttpPost]
        public async Task<ActionResult<List<PhoneType_StatusDTO>>> FilterListStatus([FromBody] PhoneType_StatusFilterDTO PhoneType_StatusFilterDTO)
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
            List<PhoneType_StatusDTO> PhoneType_StatusDTOs = Statuses
                .Select(x => new PhoneType_StatusDTO(x)).ToList();
            return PhoneType_StatusDTOs;
        }

        [Route(PhoneTypeRoute.SingleListStatus), HttpPost]
        public async Task<ActionResult<List<PhoneType_StatusDTO>>> SingleListStatus([FromBody] PhoneType_StatusFilterDTO PhoneType_StatusFilterDTO)
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
            List<PhoneType_StatusDTO> PhoneType_StatusDTOs = Statuses
                .Select(x => new PhoneType_StatusDTO(x)).ToList();
            return PhoneType_StatusDTOs;
        }

    }
}

