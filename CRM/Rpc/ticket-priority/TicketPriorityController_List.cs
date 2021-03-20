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
using CRM.Services.MTicketPriority;
using CRM.Services.MStatus;

namespace CRM.Rpc.ticket_priority
{
    public partial class TicketPriorityController : RpcController
    {
        [Route(TicketPriorityRoute.FilterListStatus), HttpPost]
        public async Task<List<TicketPriority_StatusDTO>> FilterListStatus([FromBody] TicketPriority_StatusFilterDTO TicketPriority_StatusFilterDTO)
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
            List<TicketPriority_StatusDTO> TicketPriority_StatusDTOs = Statuses
                .Select(x => new TicketPriority_StatusDTO(x)).ToList();
            return TicketPriority_StatusDTOs;
        }

        [Route(TicketPriorityRoute.SingleListStatus), HttpPost]
        public async Task<List<TicketPriority_StatusDTO>> SingleListStatus([FromBody] TicketPriority_StatusFilterDTO TicketPriority_StatusFilterDTO)
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
            List<TicketPriority_StatusDTO> TicketPriority_StatusDTOs = Statuses
                .Select(x => new TicketPriority_StatusDTO(x)).ToList();
            return TicketPriority_StatusDTOs;
        }

    }
}

