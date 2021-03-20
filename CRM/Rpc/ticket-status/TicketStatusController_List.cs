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
using CRM.Services.MTicketStatus;
using CRM.Services.MStatus;

namespace CRM.Rpc.ticket_status
{
    public partial class TicketStatusController : RpcController
    {
        [Route(TicketStatusRoute.FilterListStatus), HttpPost]
        public async Task<List<TicketStatus_StatusDTO>> FilterListStatus([FromBody] TicketStatus_StatusFilterDTO TicketStatus_StatusFilterDTO)
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
            List<TicketStatus_StatusDTO> TicketStatus_StatusDTOs = Statuses
                .Select(x => new TicketStatus_StatusDTO(x)).ToList();
            return TicketStatus_StatusDTOs;
        }

        [Route(TicketStatusRoute.SingleListStatus), HttpPost]
        public async Task<List<TicketStatus_StatusDTO>> SingleListStatus([FromBody] TicketStatus_StatusFilterDTO TicketStatus_StatusFilterDTO)
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
            List<TicketStatus_StatusDTO> TicketStatus_StatusDTOs = Statuses
                .Select(x => new TicketStatus_StatusDTO(x)).ToList();
            return TicketStatus_StatusDTOs;
        }

    }
}

