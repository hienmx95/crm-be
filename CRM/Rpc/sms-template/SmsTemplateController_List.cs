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
using CRM.Services.MSmsTemplate;
using CRM.Services.MStatus;

namespace CRM.Rpc.sms_template
{
    public partial class SmsTemplateController : RpcController
    {
        [Route(SmsTemplateRoute.FilterListStatus), HttpPost]
        public async Task<List<SmsTemplate_StatusDTO>> FilterListStatus([FromBody] SmsTemplate_StatusFilterDTO SmsTemplate_StatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;
            StatusFilter.Id = SmsTemplate_StatusFilterDTO.Id;
            StatusFilter.Code = SmsTemplate_StatusFilterDTO.Code;
            StatusFilter.Name = SmsTemplate_StatusFilterDTO.Name;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<SmsTemplate_StatusDTO> SmsTemplate_StatusDTOs = Statuses
                .Select(x => new SmsTemplate_StatusDTO(x)).ToList();
            return SmsTemplate_StatusDTOs;
        }

        [Route(SmsTemplateRoute.SingleListStatus), HttpPost]
        public async Task<List<SmsTemplate_StatusDTO>> SingleListStatus([FromBody] SmsTemplate_StatusFilterDTO SmsTemplate_StatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;
            StatusFilter.Id = SmsTemplate_StatusFilterDTO.Id;
            StatusFilter.Code = SmsTemplate_StatusFilterDTO.Code;
            StatusFilter.Name = SmsTemplate_StatusFilterDTO.Name;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<SmsTemplate_StatusDTO> SmsTemplate_StatusDTOs = Statuses
                .Select(x => new SmsTemplate_StatusDTO(x)).ToList();
            return SmsTemplate_StatusDTOs;
        }
    }
}

