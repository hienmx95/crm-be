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
using CRM.Services.MMailTemplate;
using CRM.Services.MStatus;

namespace CRM.Rpc.mail_template
{
    public partial class MailTemplateController : RpcController
    {
        [Route(MailTemplateRoute.FilterListStatus), HttpPost]
        public async Task<ActionResult<List<MailTemplate_StatusDTO>>> FilterListStatus([FromBody] MailTemplate_StatusFilterDTO MailTemplate_StatusFilterDTO)
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
            List<MailTemplate_StatusDTO> MailTemplate_StatusDTOs = Statuses
                .Select(x => new MailTemplate_StatusDTO(x)).ToList();
            return MailTemplate_StatusDTOs;
        }

        [Route(MailTemplateRoute.SingleListStatus), HttpPost]
        public async Task<ActionResult<List<MailTemplate_StatusDTO>>> SingleListStatus([FromBody] MailTemplate_StatusFilterDTO MailTemplate_StatusFilterDTO)
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
            List<MailTemplate_StatusDTO> MailTemplate_StatusDTOs = Statuses
                .Select(x => new MailTemplate_StatusDTO(x)).ToList();
            return MailTemplate_StatusDTOs;
        }
    }
}

