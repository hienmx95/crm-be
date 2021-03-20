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
namespace CRM.Rpc.reports.ticket_report
{
    public partial class TicketReportController : RpcController
    {
        //public const string FilterListTicketType = Default + "/filter-list-ticket-type";
        [Route(TicketReportRoute.FilterListTicketType), HttpPost]
        public async Task<List<TicketReport_TicketTypeDTO>> FilterListTicketType([FromBody] TicketReport_TicketTypeFilterDTO TicketReport_TicketTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter();
            TicketTypeFilter.Skip = 0;
            TicketTypeFilter.Take = 20;
            TicketTypeFilter.OrderBy = TicketTypeOrder.Id;
            TicketTypeFilter.OrderType = OrderType.ASC;
            TicketTypeFilter.Selects = TicketTypeSelect.ALL;
            TicketTypeFilter.Id = TicketReport_TicketTypeFilterDTO.Id;
            TicketTypeFilter.Name = TicketReport_TicketTypeFilterDTO.Name;
            TicketTypeFilter.Code = TicketReport_TicketTypeFilterDTO.Code;
            TicketTypeFilter.ColorCode = TicketReport_TicketTypeFilterDTO.ColorCode;

            List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);
            List<TicketReport_TicketTypeDTO> TicketReport_TicketTypeDTOs = TicketTypes
                .Select(x => new TicketReport_TicketTypeDTO(x)).ToList();
            return TicketReport_TicketTypeDTOs;
        }
        //public const string FilterListTicketGroup = Default + "/filter-list-ticket-group";
        [Route(TicketReportRoute.FilterListTicketGroup), HttpPost]
        public async Task<List<TicketReport_TicketGroupDTO>> FilterListTicketGroup([FromBody] TicketReport_TicketGroupFilterDTO TicketReport_TicketGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketGroupFilter TicketGroupFilter = new TicketGroupFilter();
            TicketGroupFilter.Skip = 0;
            TicketGroupFilter.Take = 20;
            TicketGroupFilter.OrderBy = TicketGroupOrder.Id;
            TicketGroupFilter.OrderType = OrderType.ASC;
            TicketGroupFilter.Selects = TicketGroupSelect.ALL;
            TicketGroupFilter.Id = TicketReport_TicketGroupFilterDTO.Id;
            TicketGroupFilter.Name = TicketReport_TicketGroupFilterDTO.Name;

            List<TicketGroup> TicketGroups = await TicketGroupService.List(TicketGroupFilter);
            List<TicketReport_TicketGroupDTO> TicketReport_TicketGroupDTOs = TicketGroups
                .Select(x => new TicketReport_TicketGroupDTO(x)).ToList();
            return TicketReport_TicketGroupDTOs;
        }
        //public const string FilterListTicketStatus = Default + "/filter-list-ticket-status";
        [Route(TicketReportRoute.FilterListTicketStatus), HttpPost]
        public async Task<List<TicketReport_TicketStatusDTO>> FilterListTicketStatus([FromBody] TicketReport_TicketStatusFilterDTO TicketReport_TicketStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketStatusFilter TicketStatusFilter = new TicketStatusFilter();
            TicketStatusFilter.Skip = 0;
            TicketStatusFilter.Take = 20;
            TicketStatusFilter.OrderBy = TicketStatusOrder.Id;
            TicketStatusFilter.OrderType = OrderType.ASC;
            TicketStatusFilter.Selects = TicketStatusSelect.ALL;
            TicketStatusFilter.Id = TicketReport_TicketStatusFilterDTO.Id;
            TicketStatusFilter.Name = TicketReport_TicketStatusFilterDTO.Name;

            List<TicketStatus> TicketStatuss = await TicketStatusService.List(TicketStatusFilter);
            List<TicketReport_TicketStatusDTO> TicketReport_TicketStatusDTOs = TicketStatuss
                .Select(x => new TicketReport_TicketStatusDTO(x)).ToList();
            return TicketReport_TicketStatusDTOs;
        }
        //public const string FilterListSLAStatus = Default + "/filter-list-s-l-a-status";
        [Route(TicketReportRoute.FilterListSLAStatus), HttpPost]
        public async Task<List<TicketReport_SLAStatusDTO>> FilterListSLAStatus([FromBody] TicketReport_SLAStatusFilterDTO TicketReport_SLAStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SLAStatusFilter SLAStatusFilter = new SLAStatusFilter();
            SLAStatusFilter.Skip = 0;
            SLAStatusFilter.Take = 20;
            SLAStatusFilter.OrderBy = SLAStatusOrder.Id;
            SLAStatusFilter.OrderType = OrderType.ASC;
            SLAStatusFilter.Selects = SLAStatusSelect.ALL;
            SLAStatusFilter.Id = TicketReport_SLAStatusFilterDTO.Id;
            SLAStatusFilter.Name = TicketReport_SLAStatusFilterDTO.Name;

            List<SLAStatus> SLAStatuss = await SLAStatusService.List(SLAStatusFilter);
            List<TicketReport_SLAStatusDTO> TicketReport_SLAStatusDTOs = SLAStatuss
                .Select(x => new TicketReport_SLAStatusDTO(x)).ToList();
            return TicketReport_SLAStatusDTOs;
        }
        //public const string FilterListTicketPriority = Default + "/filter-list-ticket-priority";
        [Route(TicketReportRoute.FilterListTicketPriority), HttpPost]
        public async Task<List<TicketReport_TicketPriorityDTO>> FilterListTicketPriority([FromBody] TicketReport_TicketPriorityFilterDTO TicketReport_TicketPriorityFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketPriorityFilter TicketPriorityFilter = new TicketPriorityFilter();
            TicketPriorityFilter.Skip = 0;
            TicketPriorityFilter.Take = 20;
            TicketPriorityFilter.OrderBy = TicketPriorityOrder.Id;
            TicketPriorityFilter.OrderType = OrderType.ASC;
            TicketPriorityFilter.Selects = TicketPrioritySelect.ALL;
            TicketPriorityFilter.Id = TicketReport_TicketPriorityFilterDTO.Id;
            TicketPriorityFilter.Name = TicketReport_TicketPriorityFilterDTO.Name;

            List<TicketPriority> TicketPrioritys = await TicketPriorityService.List(TicketPriorityFilter);
            List<TicketReport_TicketPriorityDTO> TicketReport_TicketPriorityDTOs = TicketPrioritys
                .Select(x => new TicketReport_TicketPriorityDTO(x)).ToList();
            return TicketReport_TicketPriorityDTOs;
        }
        //public const string FilterListCustomer = Default + "/filter-list-customer";
        [Route(TicketReportRoute.FilterListCustomer), HttpPost]
        public async Task<List<TicketReport_CustomerDTO>> FilterListCustomer([FromBody] TicketReport_CustomerFilterDTO TicketReport_CustomerFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            CustomerFilter.Id = TicketReport_CustomerFilterDTO.Id;
            CustomerFilter.Code = TicketReport_CustomerFilterDTO.Code;
            CustomerFilter.Phone = TicketReport_CustomerFilterDTO.Phone;
            CustomerFilter.Address = TicketReport_CustomerFilterDTO.Address;
            CustomerFilter.Name = TicketReport_CustomerFilterDTO.FullName;
            CustomerFilter.Email = TicketReport_CustomerFilterDTO.Email;


            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<TicketReport_CustomerDTO> TicketReport_CustomerDTOs = Customers
                .Select(x => new TicketReport_CustomerDTO(x)).ToList();
            return TicketReport_CustomerDTOs;
        }
        //public const string FilterListAppUser = Default + "/filter-list-app-user";
        [Route(TicketReportRoute.FilterListAppUser), HttpPost]
        public async Task<List<TicketReport_AppUserDTO>> FilterListAppUser([FromBody] TicketReport_AppUserFilterDTO TicketReport_AppUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = TicketReport_AppUserFilterDTO.Id;
            AppUserFilter.Username = TicketReport_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = TicketReport_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = TicketReport_AppUserFilterDTO.Address;
            AppUserFilter.Email = TicketReport_AppUserFilterDTO.Email;
            AppUserFilter.Phone = TicketReport_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = TicketReport_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = TicketReport_AppUserFilterDTO.Birthday;
            AppUserFilter.PositionId = TicketReport_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = TicketReport_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = TicketReport_AppUserFilterDTO.OrganizationId;
            AppUserFilter.StatusId = TicketReport_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<TicketReport_AppUserDTO> TicketReport_AppUserDTOs = AppUsers
                .Select(x => new TicketReport_AppUserDTO(x)).ToList();
            return TicketReport_AppUserDTOs;
        }
        [Route(TicketReportRoute.FilterListCustomerType), HttpPost]
        public async Task<List<TicketReport_CustomerTypeDTO>> FilterListCustomerType([FromBody] TicketReport_CustomerTypeFilterDTO TicketReport_CustomerTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerTypeFilter CustomerTypeFilter = new CustomerTypeFilter();
            CustomerTypeFilter.Skip = 0;
            CustomerTypeFilter.Take = 20;
            CustomerTypeFilter.OrderBy = CustomerTypeOrder.Id;
            CustomerTypeFilter.OrderType = OrderType.ASC;
            CustomerTypeFilter.Selects = CustomerTypeSelect.ALL;
            CustomerTypeFilter.Id = TicketReport_CustomerTypeFilterDTO.Id;
            CustomerTypeFilter.Name = TicketReport_CustomerTypeFilterDTO.Name;
            CustomerTypeFilter.Code = TicketReport_CustomerTypeFilterDTO.Code;

            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            List<TicketReport_CustomerTypeDTO> TicketReport_CustomerTypeDTOs = CustomerTypes
                .Select(x => new TicketReport_CustomerTypeDTO(x)).ToList();
            return TicketReport_CustomerTypeDTOs;
        }
    }
}

