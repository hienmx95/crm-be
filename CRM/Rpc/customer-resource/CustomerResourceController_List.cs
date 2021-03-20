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
using CRM.Services.MCustomerResource;
using CRM.Services.MStatus;

namespace CRM.Rpc.customer_resource
{
    public partial class CustomerResourceController : RpcController
    {
        [Route(CustomerResourceRoute.FilterListStatus), HttpPost]
        public async Task<ActionResult<List<CustomerResource_StatusDTO>>> FilterListStatus([FromBody] CustomerResource_StatusFilterDTO CustomerResource_StatusFilterDTO)
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
            List<CustomerResource_StatusDTO> CustomerResource_StatusDTOs = Statuses
                .Select(x => new CustomerResource_StatusDTO(x)).ToList();
            return CustomerResource_StatusDTOs;
        }

        [Route(CustomerResourceRoute.SingleListStatus), HttpPost]
        public async Task<ActionResult<List<CustomerResource_StatusDTO>>> SingleListStatus([FromBody] CustomerResource_StatusFilterDTO CustomerResource_StatusFilterDTO)
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
            List<CustomerResource_StatusDTO> CustomerResource_StatusDTOs = Statuses
                .Select(x => new CustomerResource_StatusDTO(x)).ToList();
            return CustomerResource_StatusDTOs;
        }

    }
}

