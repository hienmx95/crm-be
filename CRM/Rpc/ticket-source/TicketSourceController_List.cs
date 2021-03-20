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
using CRM.Services.MTicketSource;
using CRM.Services.MStatus;

namespace CRM.Rpc.ticket_source
{
    public partial class TicketSourceController : RpcController
    {
        [Route(TicketSourceRoute.FilterListStatus), HttpPost]
        public async Task<List<TicketSource_StatusDTO>> FilterListStatus([FromBody] TicketSource_StatusFilterDTO TicketSource_StatusFilterDTO)
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
            List<TicketSource_StatusDTO> TicketSource_StatusDTOs = Statuses
                .Select(x => new TicketSource_StatusDTO(x)).ToList();
            return TicketSource_StatusDTOs;
        }

        [Route(TicketSourceRoute.SingleListStatus), HttpPost]
        public async Task<List<TicketSource_StatusDTO>> SingleListStatus([FromBody] TicketSource_StatusFilterDTO TicketSource_StatusFilterDTO)
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
            List<TicketSource_StatusDTO> TicketSource_StatusDTOs = Statuses
                .Select(x => new TicketSource_StatusDTO(x)).ToList();
            return TicketSource_StatusDTOs;
        }

    }
}

