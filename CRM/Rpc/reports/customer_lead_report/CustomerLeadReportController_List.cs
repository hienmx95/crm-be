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
namespace CRM.Rpc.reports.customer_lead_report
{
    public partial class CustomerLeadReportController : RpcController
    {
        [Route(CustomerLeadReportRoute.FilterListCustomerLeadSource), HttpPost]
        public async Task<ActionResult<List<CustomerLeadReport_CustomerLeadSourceDTO>>> FilterListCustomerLeadSource([FromBody] CustomerLeadReport_CustomerLeadSourceFilterDTO CustomerLeadReport_CustomerLeadSourceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadSourceFilter CustomerLeadSourceFilter = new CustomerLeadSourceFilter();
            CustomerLeadSourceFilter.Skip = 0;
            CustomerLeadSourceFilter.Take = 20;
            CustomerLeadSourceFilter.OrderBy = CustomerLeadSourceOrder.Id;
            CustomerLeadSourceFilter.OrderType = OrderType.ASC;
            CustomerLeadSourceFilter.Selects = CustomerLeadSourceSelect.ALL;
            CustomerLeadSourceFilter.Id = CustomerLeadReport_CustomerLeadSourceFilterDTO.Id;
            CustomerLeadSourceFilter.Code = CustomerLeadReport_CustomerLeadSourceFilterDTO.Code;
            CustomerLeadSourceFilter.Name = CustomerLeadReport_CustomerLeadSourceFilterDTO.Name;

            List<CustomerLeadSource> CustomerLeadSources = await CustomerLeadSourceService.List(CustomerLeadSourceFilter);
            List<CustomerLeadReport_CustomerLeadSourceDTO> CustomerLeadReport_CustomerLeadSourceDTOs = CustomerLeadSources
                .Select(x => new CustomerLeadReport_CustomerLeadSourceDTO(x)).ToList();
            return CustomerLeadReport_CustomerLeadSourceDTOs;
        }
        [Route(CustomerLeadReportRoute.FilterListCustomerLeadStatus), HttpPost]
        public async Task<ActionResult<List<CustomerLeadReport_CustomerLeadStatusDTO>>> FilterListCustomerLeadStatus([FromBody] CustomerLeadReport_CustomerLeadStatusFilterDTO CustomerLeadReport_CustomerLeadStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadStatusFilter CustomerLeadStatusFilter = new CustomerLeadStatusFilter();
            CustomerLeadStatusFilter.Skip = 0;
            CustomerLeadStatusFilter.Take = 20;
            CustomerLeadStatusFilter.OrderBy = CustomerLeadStatusOrder.Id;
            CustomerLeadStatusFilter.OrderType = OrderType.ASC;
            CustomerLeadStatusFilter.Selects = CustomerLeadStatusSelect.ALL;
            CustomerLeadStatusFilter.Id = CustomerLeadReport_CustomerLeadStatusFilterDTO.Id;
            CustomerLeadStatusFilter.Code = CustomerLeadReport_CustomerLeadStatusFilterDTO.Code;
            CustomerLeadStatusFilter.Name = CustomerLeadReport_CustomerLeadStatusFilterDTO.Name;

            List<CustomerLeadStatus> CustomerLeadStatuses = await CustomerLeadStatusService.List(CustomerLeadStatusFilter);
            List<CustomerLeadReport_CustomerLeadStatusDTO> CustomerLeadReport_CustomerLeadStatusDTOs = CustomerLeadStatuses
                .Select(x => new CustomerLeadReport_CustomerLeadStatusDTO(x)).ToList();
            return CustomerLeadReport_CustomerLeadStatusDTOs;
        }
        [Route(CustomerLeadReportRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<CustomerLeadReport_AppUserDTO>>> FilterListAppUser([FromBody] CustomerLeadReport_AppUserFilterDTO CustomerLeadReport_AppUserFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = CustomerLeadReport_AppUserFilterDTO.Id;
            AppUserFilter.Username = CustomerLeadReport_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = CustomerLeadReport_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = CustomerLeadReport_AppUserFilterDTO.Address;
            AppUserFilter.Email = CustomerLeadReport_AppUserFilterDTO.Email;
            AppUserFilter.Phone = CustomerLeadReport_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = CustomerLeadReport_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = CustomerLeadReport_AppUserFilterDTO.Birthday;
            AppUserFilter.PositionId = CustomerLeadReport_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = CustomerLeadReport_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = CustomerLeadReport_AppUserFilterDTO.OrganizationId;
            AppUserFilter.StatusId = CustomerLeadReport_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<CustomerLeadReport_AppUserDTO> CustomerLeadReport_AppUserDTOs = AppUsers
                .Select(x => new CustomerLeadReport_AppUserDTO(x)).ToList();
            return CustomerLeadReport_AppUserDTOs;
        }



    }
}

