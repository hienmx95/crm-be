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
using CRM.Services.MCustomerLevel;
using CRM.Services.MStatus;

namespace CRM.Rpc.customer_level
{
    public partial class CustomerLevelController : RpcController
    {
        [Route(CustomerLevelRoute.FilterListStatus), HttpPost]
        public async Task<ActionResult<List<CustomerLevel_StatusDTO>>> FilterListStatus([FromBody] CustomerLevel_StatusFilterDTO CustomerLevel_StatusFilterDTO)
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
            List<CustomerLevel_StatusDTO> CustomerLevel_StatusDTOs = Statuses
                .Select(x => new CustomerLevel_StatusDTO(x)).ToList();
            return CustomerLevel_StatusDTOs;
        }

        [Route(CustomerLevelRoute.SingleListStatus), HttpPost]
        public async Task<ActionResult<List<CustomerLevel_StatusDTO>>> SingleListStatus([FromBody] CustomerLevel_StatusFilterDTO CustomerLevel_StatusFilterDTO)
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
            List<CustomerLevel_StatusDTO> CustomerLevel_StatusDTOs = Statuses
                .Select(x => new CustomerLevel_StatusDTO(x)).ToList();
            return CustomerLevel_StatusDTOs;
        }

    }
}

