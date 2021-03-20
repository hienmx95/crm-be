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
using CRM.Services.MCustomerGrouping;
using CRM.Services.MCustomerType;
using CRM.Services.MStatus;

namespace CRM.Rpc.customer_grouping
{
    public partial class CustomerGroupingController : RpcController
    {
        [Route(CustomerGroupingRoute.FilterListCustomerType), HttpPost]
        public async Task<ActionResult<List<CustomerGrouping_CustomerTypeDTO>>> FilterListCustomerType([FromBody] CustomerGrouping_CustomerTypeFilterDTO CustomerGrouping_CustomerTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerTypeFilter CustomerTypeFilter = new CustomerTypeFilter();
            CustomerTypeFilter.Skip = 0;
            CustomerTypeFilter.Take = 20;
            CustomerTypeFilter.OrderBy = CustomerTypeOrder.Id;
            CustomerTypeFilter.OrderType = OrderType.ASC;
            CustomerTypeFilter.Selects = CustomerTypeSelect.ALL;
            CustomerTypeFilter.Id = CustomerGrouping_CustomerTypeFilterDTO.Id;
            CustomerTypeFilter.Code = CustomerGrouping_CustomerTypeFilterDTO.Code;
            CustomerTypeFilter.Name = CustomerGrouping_CustomerTypeFilterDTO.Name;

            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            List<CustomerGrouping_CustomerTypeDTO> CustomerGrouping_CustomerTypeDTOs = CustomerTypes
                .Select(x => new CustomerGrouping_CustomerTypeDTO(x)).ToList();
            return CustomerGrouping_CustomerTypeDTOs;
        }
        [Route(CustomerGroupingRoute.FilterListCustomerGroupinging), HttpPost]
        public async Task<ActionResult<List<CustomerGrouping_CustomerGroupingDTO>>> FilterListCustomerGroupinging([FromBody] CustomerGrouping_CustomerGroupingFilterDTO CustomerGrouping_CustomerGroupingFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerGroupingFilter CustomerGroupingFilter = new CustomerGroupingFilter();
            CustomerGroupingFilter.Skip = 0;
            CustomerGroupingFilter.Take = int.MaxValue;
            CustomerGroupingFilter.OrderBy = CustomerGroupingOrder.Id;
            CustomerGroupingFilter.OrderType = OrderType.ASC;
            CustomerGroupingFilter.Selects = CustomerGroupingSelect.ALL;
            CustomerGroupingFilter.Id = CustomerGrouping_CustomerGroupingFilterDTO.Id;
            CustomerGroupingFilter.Code = CustomerGrouping_CustomerGroupingFilterDTO.Code;
            CustomerGroupingFilter.Name = CustomerGrouping_CustomerGroupingFilterDTO.Name;
            CustomerGroupingFilter.CustomerTypeId = CustomerGrouping_CustomerGroupingFilterDTO.CustomerTypeId;
            CustomerGroupingFilter.ParentId = CustomerGrouping_CustomerGroupingFilterDTO.ParentId;
            CustomerGroupingFilter.Path = CustomerGrouping_CustomerGroupingFilterDTO.Path;
            CustomerGroupingFilter.Level = CustomerGrouping_CustomerGroupingFilterDTO.Level;
            CustomerGroupingFilter.StatusId = CustomerGrouping_CustomerGroupingFilterDTO.StatusId;
            CustomerGroupingFilter.Description = CustomerGrouping_CustomerGroupingFilterDTO.Description;

            List<CustomerGrouping> CustomerGroupings = await CustomerGroupingService.List(CustomerGroupingFilter);
            List<CustomerGrouping_CustomerGroupingDTO> CustomerGrouping_CustomerGroupingDTOs = CustomerGroupings
                .Select(x => new CustomerGrouping_CustomerGroupingDTO(x)).ToList();
            return CustomerGrouping_CustomerGroupingDTOs;
        }
        [Route(CustomerGroupingRoute.FilterListStatus), HttpPost]
        public async Task<ActionResult<List<CustomerGrouping_StatusDTO>>> FilterListStatus([FromBody] CustomerGrouping_StatusFilterDTO CustomerGrouping_StatusFilterDTO)
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
            List<CustomerGrouping_StatusDTO> CustomerGrouping_StatusDTOs = Statuses
                .Select(x => new CustomerGrouping_StatusDTO(x)).ToList();
            return CustomerGrouping_StatusDTOs;
        }

        [Route(CustomerGroupingRoute.SingleListCustomerType), HttpPost]
        public async Task<ActionResult<List<CustomerGrouping_CustomerTypeDTO>>> SingleListCustomerType([FromBody] CustomerGrouping_CustomerTypeFilterDTO CustomerGrouping_CustomerTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerTypeFilter CustomerTypeFilter = new CustomerTypeFilter();
            CustomerTypeFilter.Skip = 0;
            CustomerTypeFilter.Take = 20;
            CustomerTypeFilter.OrderBy = CustomerTypeOrder.Id;
            CustomerTypeFilter.OrderType = OrderType.ASC;
            CustomerTypeFilter.Selects = CustomerTypeSelect.ALL;
            CustomerTypeFilter.Id = CustomerGrouping_CustomerTypeFilterDTO.Id;
            CustomerTypeFilter.Code = CustomerGrouping_CustomerTypeFilterDTO.Code;
            CustomerTypeFilter.Name = CustomerGrouping_CustomerTypeFilterDTO.Name;

            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            List<CustomerGrouping_CustomerTypeDTO> CustomerGrouping_CustomerTypeDTOs = CustomerTypes
                .Select(x => new CustomerGrouping_CustomerTypeDTO(x)).ToList();
            return CustomerGrouping_CustomerTypeDTOs;
        }
        [Route(CustomerGroupingRoute.SingleListCustomerGroupinging), HttpPost]
        public async Task<ActionResult<List<CustomerGrouping_CustomerGroupingDTO>>> SingleListCustomerGroupinging([FromBody] CustomerGrouping_CustomerGroupingFilterDTO CustomerGrouping_CustomerGroupingFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerGroupingFilter CustomerGroupingFilter = new CustomerGroupingFilter();
            CustomerGroupingFilter.Skip = 0;
            CustomerGroupingFilter.Take = int.MaxValue;
            CustomerGroupingFilter.OrderBy = CustomerGroupingOrder.Id;
            CustomerGroupingFilter.OrderType = OrderType.ASC;
            CustomerGroupingFilter.Selects = CustomerGroupingSelect.ALL;
            CustomerGroupingFilter.Id = CustomerGrouping_CustomerGroupingFilterDTO.Id;
            CustomerGroupingFilter.Code = CustomerGrouping_CustomerGroupingFilterDTO.Code;
            CustomerGroupingFilter.Name = CustomerGrouping_CustomerGroupingFilterDTO.Name;
            CustomerGroupingFilter.CustomerTypeId = CustomerGrouping_CustomerGroupingFilterDTO.CustomerTypeId;
            CustomerGroupingFilter.ParentId = CustomerGrouping_CustomerGroupingFilterDTO.ParentId;
            CustomerGroupingFilter.Path = CustomerGrouping_CustomerGroupingFilterDTO.Path;
            CustomerGroupingFilter.Level = CustomerGrouping_CustomerGroupingFilterDTO.Level;
            CustomerGroupingFilter.StatusId = CustomerGrouping_CustomerGroupingFilterDTO.StatusId;
            CustomerGroupingFilter.Description = CustomerGrouping_CustomerGroupingFilterDTO.Description;

            List<CustomerGrouping> CustomerGroupings = await CustomerGroupingService.List(CustomerGroupingFilter);
            List<CustomerGrouping_CustomerGroupingDTO> CustomerGrouping_CustomerGroupingDTOs = CustomerGroupings
                .Select(x => new CustomerGrouping_CustomerGroupingDTO(x)).ToList();
            return CustomerGrouping_CustomerGroupingDTOs;
        }
        [Route(CustomerGroupingRoute.SingleListStatus), HttpPost]
        public async Task<ActionResult<List<CustomerGrouping_StatusDTO>>> SingleListStatus([FromBody] CustomerGrouping_StatusFilterDTO CustomerGrouping_StatusFilterDTO)
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
            List<CustomerGrouping_StatusDTO> CustomerGrouping_StatusDTOs = Statuses
                .Select(x => new CustomerGrouping_StatusDTO(x)).ToList();
            return CustomerGrouping_StatusDTOs;
        }

    }
}

