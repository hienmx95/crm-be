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
using CRM.Services.MTicket;
using CRM.Services.MCustomer;
using CRM.Services.MOrganization;
using CRM.Services.MProduct;
using CRM.Services.MCallLog;
using CRM.Services.MStatus;
using CRM.Services.MTicketIssueLevel;
using CRM.Services.MTicketPriority;
using CRM.Services.MTicketSource;
using CRM.Services.MTicketStatus;
using CRM.Services.MAppUser;
using CRM.Enums;

namespace CRM.Rpc.ticket
{
    public partial class TicketController : RpcController
    {
        [Route(TicketRoute.FilterListCustomer), HttpPost]
        public async Task<List<Ticket_CustomerDTO>> FilterListCustomer([FromBody] Ticket_CustomerFilterDTO Ticket_CustomerFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            CustomerFilter.Id = Ticket_CustomerFilterDTO.Id;
            CustomerFilter.Code = Ticket_CustomerFilterDTO.Code;

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<Ticket_CustomerDTO> Ticket_CustomerDTOs = Customers
                .Select(x => new Ticket_CustomerDTO(x)).ToList();
            return Ticket_CustomerDTOs;
        }
        [Route(TicketRoute.FilterListOrganization), HttpPost]
        public async Task<List<Ticket_OrganizationDTO>> FilterListOrganization([FromBody] Ticket_OrganizationFilterDTO Ticket_OrganizationFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = 20;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = Ticket_OrganizationFilterDTO.Id;
            OrganizationFilter.Name = Ticket_OrganizationFilterDTO.Name;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<Ticket_OrganizationDTO> Ticket_OrganizationDTOs = Organizations
                .Select(x => new Ticket_OrganizationDTO(x)).ToList();
            return Ticket_OrganizationDTOs;
        }
        [Route(TicketRoute.FilterListProduct), HttpPost]
        public async Task<List<Ticket_ProductDTO>> FilterListProduct([FromBody] Ticket_ProductFilterDTO Ticket_ProductFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProductFilter ProductFilter = new ProductFilter();
            ProductFilter.Skip = 0;
            ProductFilter.Take = 20;
            ProductFilter.OrderBy = ProductOrder.Id;
            ProductFilter.OrderType = OrderType.ASC;
            ProductFilter.Selects = ProductSelect.ALL;
            ProductFilter.Id = Ticket_ProductFilterDTO.Id;
            ProductFilter.Name = Ticket_ProductFilterDTO.Name;

            List<Product> Products = await ProductService.List(ProductFilter);
            List<Ticket_ProductDTO> Ticket_ProductDTOs = Products
                .Select(x => new Ticket_ProductDTO(x)).ToList();
            return Ticket_ProductDTOs;
        }
        [Route(TicketRoute.FilterListCallLog), HttpPost]
        public async Task<List<Ticket_CallLogDTO>> FilterListCallLog([FromBody] Ticket_CallLogFilterDTO Ticket_CallLogFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = new CallLogFilter();
            CallLogFilter.Skip = 0;
            CallLogFilter.Take = 20;
            CallLogFilter.OrderBy = CallLogOrder.Id;
            CallLogFilter.OrderType = OrderType.ASC;
            CallLogFilter.Selects = CallLogSelect.ALL;
            CallLogFilter.Id = Ticket_CallLogFilterDTO.Id;
            CallLogFilter.EntityReferenceId = Ticket_CallLogFilterDTO.EntityReferenceId;
            CallLogFilter.CallTypeId = Ticket_CallLogFilterDTO.CallTypeId;
            CallLogFilter.CallEmotionId = Ticket_CallLogFilterDTO.CallEmotionId;
            CallLogFilter.AppUserId = Ticket_CallLogFilterDTO.AppUserId;
            CallLogFilter.Title = Ticket_CallLogFilterDTO.Title;
            CallLogFilter.Content = Ticket_CallLogFilterDTO.Content;
            CallLogFilter.Phone = Ticket_CallLogFilterDTO.Phone;
            CallLogFilter.CallTime = Ticket_CallLogFilterDTO.CallTime;

            List<CallLog> CallLogs = await CallLogService.List(CallLogFilter);
            List<Ticket_CallLogDTO> Ticket_CallLogDTOs = CallLogs
                .Select(x => new Ticket_CallLogDTO(x)).ToList();
            return Ticket_CallLogDTOs;
        }
        [Route(TicketRoute.FilterListTicket), HttpPost]
        public async Task<List<Ticket_TicketDTO>> FilterListTicket([FromBody] Ticket_TicketFilterDTO Ticket_TicketFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketFilter TicketFilter = new TicketFilter();
            TicketFilter.Skip = 0;
            TicketFilter.Take = 20;
            TicketFilter.OrderBy = TicketOrder.Id;
            TicketFilter.OrderType = OrderType.ASC;
            TicketFilter.Selects = TicketSelect.ALL;
            TicketFilter.Id = Ticket_TicketFilterDTO.Id;
            TicketFilter.Name = Ticket_TicketFilterDTO.Name;
            TicketFilter.Phone = Ticket_TicketFilterDTO.Phone;
            TicketFilter.CustomerId = Ticket_TicketFilterDTO.CustomerId;
            TicketFilter.UserId = Ticket_TicketFilterDTO.UserId;
            TicketFilter.ProductId = Ticket_TicketFilterDTO.ProductId;
            TicketFilter.ReceiveDate = Ticket_TicketFilterDTO.ReceiveDate;
            TicketFilter.ProcessDate = Ticket_TicketFilterDTO.ProcessDate;
            TicketFilter.FinishDate = Ticket_TicketFilterDTO.FinishDate;
            TicketFilter.Subject = Ticket_TicketFilterDTO.Subject;
            TicketFilter.Content = Ticket_TicketFilterDTO.Content;
            TicketFilter.TicketIssueLevelId = Ticket_TicketFilterDTO.TicketIssueLevelId;
            TicketFilter.TicketPriorityId = Ticket_TicketFilterDTO.TicketPriorityId;
            TicketFilter.TicketStatusId = Ticket_TicketFilterDTO.TicketStatusId;
            TicketFilter.TicketSourceId = Ticket_TicketFilterDTO.TicketSourceId;
            TicketFilter.TicketNumber = Ticket_TicketFilterDTO.TicketNumber;
            TicketFilter.DepartmentId = Ticket_TicketFilterDTO.DepartmentId;
            TicketFilter.RelatedTicketId = Ticket_TicketFilterDTO.RelatedTicketId;
            TicketFilter.SLA = Ticket_TicketFilterDTO.SLA;
            TicketFilter.RelatedCallLogId = Ticket_TicketFilterDTO.RelatedCallLogId;
            TicketFilter.ResponseMethodId = Ticket_TicketFilterDTO.ResponseMethodId;
            TicketFilter.StatusId = Ticket_TicketFilterDTO.StatusId;

            List<Ticket> Tickets = await TicketService.List(TicketFilter);
            List<Ticket_TicketDTO> Ticket_TicketDTOs = Tickets
                .Select(x => new Ticket_TicketDTO(x)).ToList();
            return Ticket_TicketDTOs;
        }
        [Route(TicketRoute.FilterListStatus), HttpPost]
        public async Task<List<Ticket_StatusDTO>> FilterListStatus([FromBody] Ticket_StatusFilterDTO Ticket_StatusFilterDTO)
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
            List<Ticket_StatusDTO> Ticket_StatusDTOs = Statuses
                .Select(x => new Ticket_StatusDTO(x)).ToList();
            return Ticket_StatusDTOs;
        }
        [Route(TicketRoute.FilterListTicketIssueLevel), HttpPost]
        public async Task<List<Ticket_TicketIssueLevelDTO>> FilterListTicketIssueLevel([FromBody] Ticket_TicketIssueLevelFilterDTO Ticket_TicketIssueLevelFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketIssueLevelFilter TicketIssueLevelFilter = new TicketIssueLevelFilter();
            TicketIssueLevelFilter.Skip = 0;
            TicketIssueLevelFilter.Take = 20;
            TicketIssueLevelFilter.OrderBy = TicketIssueLevelOrder.Id;
            TicketIssueLevelFilter.OrderType = OrderType.ASC;
            TicketIssueLevelFilter.Selects = TicketIssueLevelSelect.ALL;
            TicketIssueLevelFilter.Id = Ticket_TicketIssueLevelFilterDTO.Id;
            TicketIssueLevelFilter.Name = Ticket_TicketIssueLevelFilterDTO.Name;
            TicketIssueLevelFilter.OrderNumber = Ticket_TicketIssueLevelFilterDTO.OrderNumber;
            TicketIssueLevelFilter.TicketGroupId = Ticket_TicketIssueLevelFilterDTO.TicketGroupId;
            TicketIssueLevelFilter.StatusId = Ticket_TicketIssueLevelFilterDTO.StatusId;
            TicketIssueLevelFilter.SLA = Ticket_TicketIssueLevelFilterDTO.SLA;

            List<TicketIssueLevel> TicketIssueLevels = await TicketIssueLevelService.List(TicketIssueLevelFilter);
            List<Ticket_TicketIssueLevelDTO> Ticket_TicketIssueLevelDTOs = TicketIssueLevels
                .Select(x => new Ticket_TicketIssueLevelDTO(x)).ToList();
            return Ticket_TicketIssueLevelDTOs;
        }
        [Route(TicketRoute.FilterListTicketPriority), HttpPost]
        public async Task<List<Ticket_TicketPriorityDTO>> FilterListTicketPriority([FromBody] Ticket_TicketPriorityFilterDTO Ticket_TicketPriorityFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketPriorityFilter TicketPriorityFilter = new TicketPriorityFilter();
            TicketPriorityFilter.Skip = 0;
            TicketPriorityFilter.Take = 20;
            TicketPriorityFilter.OrderBy = TicketPriorityOrder.Id;
            TicketPriorityFilter.OrderType = OrderType.ASC;
            TicketPriorityFilter.Selects = TicketPrioritySelect.ALL;
            TicketPriorityFilter.Id = Ticket_TicketPriorityFilterDTO.Id;
            TicketPriorityFilter.Name = Ticket_TicketPriorityFilterDTO.Name;
            TicketPriorityFilter.OrderNumber = Ticket_TicketPriorityFilterDTO.OrderNumber;
            TicketPriorityFilter.ColorCode = Ticket_TicketPriorityFilterDTO.ColorCode;
            TicketPriorityFilter.StatusId = Ticket_TicketPriorityFilterDTO.StatusId;

            List<TicketPriority> TicketPriorities = await TicketPriorityService.List(TicketPriorityFilter);
            List<Ticket_TicketPriorityDTO> Ticket_TicketPriorityDTOs = TicketPriorities
                .Select(x => new Ticket_TicketPriorityDTO(x)).ToList();
            return Ticket_TicketPriorityDTOs;
        }
        [Route(TicketRoute.FilterListTicketSource), HttpPost]
        public async Task<List<Ticket_TicketSourceDTO>> FilterListTicketSource([FromBody] Ticket_TicketSourceFilterDTO Ticket_TicketSourceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketSourceFilter TicketSourceFilter = new TicketSourceFilter();
            TicketSourceFilter.Skip = 0;
            TicketSourceFilter.Take = 20;
            TicketSourceFilter.OrderBy = TicketSourceOrder.Id;
            TicketSourceFilter.OrderType = OrderType.ASC;
            TicketSourceFilter.Selects = TicketSourceSelect.ALL;
            TicketSourceFilter.Id = Ticket_TicketSourceFilterDTO.Id;
            TicketSourceFilter.Name = Ticket_TicketSourceFilterDTO.Name;
            TicketSourceFilter.OrderNumber = Ticket_TicketSourceFilterDTO.OrderNumber;
            TicketSourceFilter.StatusId = Ticket_TicketSourceFilterDTO.StatusId;

            List<TicketSource> TicketSources = await TicketSourceService.List(TicketSourceFilter);
            List<Ticket_TicketSourceDTO> Ticket_TicketSourceDTOs = TicketSources
                .Select(x => new Ticket_TicketSourceDTO(x)).ToList();
            return Ticket_TicketSourceDTOs;
        }
        [Route(TicketRoute.FilterListTicketStatus), HttpPost]
        public async Task<List<Ticket_TicketStatusDTO>> FilterListTicketStatus([FromBody] Ticket_TicketStatusFilterDTO Ticket_TicketStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketStatusFilter TicketStatusFilter = new TicketStatusFilter();
            TicketStatusFilter.Skip = 0;
            TicketStatusFilter.Take = 20;
            TicketStatusFilter.OrderBy = TicketStatusOrder.Id;
            TicketStatusFilter.OrderType = OrderType.ASC;
            TicketStatusFilter.Selects = TicketStatusSelect.ALL;
            TicketStatusFilter.Id = Ticket_TicketStatusFilterDTO.Id;
            TicketStatusFilter.Name = Ticket_TicketStatusFilterDTO.Name;
            TicketStatusFilter.OrderNumber = Ticket_TicketStatusFilterDTO.OrderNumber;
            TicketStatusFilter.ColorCode = Ticket_TicketStatusFilterDTO.ColorCode;
            TicketStatusFilter.StatusId = Ticket_TicketStatusFilterDTO.StatusId;

            List<TicketStatus> TicketStatuses = await TicketStatusService.List(TicketStatusFilter);
            List<Ticket_TicketStatusDTO> Ticket_TicketStatusDTOs = TicketStatuses
                .Select(x => new Ticket_TicketStatusDTO(x)).ToList();
            return Ticket_TicketStatusDTOs;
        }
        [Route(TicketRoute.FilterListAppUser), HttpPost]
        public async Task<List<Ticket_AppUserDTO>> FilterListAppUser([FromBody] Ticket_AppUserFilterDTO Ticket_AppUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = Ticket_AppUserFilterDTO.Id;
            AppUserFilter.Username = Ticket_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Ticket_AppUserFilterDTO.DisplayName;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Ticket_AppUserDTO> Ticket_AppUserDTOs = AppUsers
                .Select(x => new Ticket_AppUserDTO(x)).ToList();
            return Ticket_AppUserDTOs;
        }
        [Route(TicketRoute.FilterListTicketType), HttpPost]
        public async Task<List<Ticket_TicketTypeDTO>> FilterListTicketType([FromBody] Ticket_TicketTypeFilterDTO Ticket_TicketTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter();
            TicketTypeFilter.Skip = 0;
            TicketTypeFilter.Take = 20;
            TicketTypeFilter.OrderBy = TicketTypeOrder.Id;
            TicketTypeFilter.OrderType = OrderType.ASC;
            TicketTypeFilter.Selects = TicketTypeSelect.ALL;
            TicketTypeFilter.Id = Ticket_TicketTypeFilterDTO.Id;
            TicketTypeFilter.Name = Ticket_TicketTypeFilterDTO.Name;
            TicketTypeFilter.Code = Ticket_TicketTypeFilterDTO.Code;
            TicketTypeFilter.ColorCode = Ticket_TicketTypeFilterDTO.ColorCode;

            List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);
            List<Ticket_TicketTypeDTO> Ticket_TicketTypeDTOs = TicketTypes
                .Select(x => new Ticket_TicketTypeDTO(x)).ToList();
            return Ticket_TicketTypeDTOs;
        }
        [Route(TicketRoute.FilterListCustomerType), HttpPost]
        public async Task<List<Ticket_CustomerTypeDTO>> FilterListCustomerType([FromBody] Ticket_CustomerTypeFilterDTO Ticket_CustomerTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerTypeFilter CustomerTypeFilter = new CustomerTypeFilter();
            CustomerTypeFilter.Skip = 0;
            CustomerTypeFilter.Take = 20;
            CustomerTypeFilter.OrderBy = CustomerTypeOrder.Id;
            CustomerTypeFilter.OrderType = OrderType.ASC;
            CustomerTypeFilter.Selects = CustomerTypeSelect.ALL;
            CustomerTypeFilter.Id = Ticket_CustomerTypeFilterDTO.Id;
            CustomerTypeFilter.Name = Ticket_CustomerTypeFilterDTO.Name;
            CustomerTypeFilter.Code = Ticket_CustomerTypeFilterDTO.Code;

            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            List<Ticket_CustomerTypeDTO> Ticket_CustomerTypeDTOs = CustomerTypes
                .Select(x => new Ticket_CustomerTypeDTO(x)).ToList();
            return Ticket_CustomerTypeDTOs;
        }
        [Route(TicketRoute.SingleListCustomer), HttpPost]
        public async Task<List<Ticket_CustomerDTO>> SingleListCustomer([FromBody] Ticket_CustomerFilterDTO Ticket_CustomerFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            CustomerFilter.Id = Ticket_CustomerFilterDTO.Id;
            CustomerFilter.Code = Ticket_CustomerFilterDTO.Code;
            CustomerFilter.Name = Ticket_CustomerFilterDTO.Name;
            CustomerFilter.Phone = Ticket_CustomerFilterDTO.Phone;
            CustomerFilter.Address = Ticket_CustomerFilterDTO.Address;
            CustomerFilter.NationId = Ticket_CustomerFilterDTO.NationId;
            CustomerFilter.ProvinceId = Ticket_CustomerFilterDTO.ProvinceId;
            CustomerFilter.DistrictId = Ticket_CustomerFilterDTO.DistrictId;
            CustomerFilter.WardId = Ticket_CustomerFilterDTO.WardId;
            CustomerFilter.CustomerTypeId = Ticket_CustomerFilterDTO.CustomerTypeId;
            CustomerFilter.Birthday = Ticket_CustomerFilterDTO.Birthday;
            CustomerFilter.Email = Ticket_CustomerFilterDTO.Email;
            CustomerFilter.ProfessionId = Ticket_CustomerFilterDTO.ProfessionId;
            CustomerFilter.CustomerResourceId = Ticket_CustomerFilterDTO.CustomerResourceId;
            CustomerFilter.SexId = Ticket_CustomerFilterDTO.SexId;
            CustomerFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            CustomerFilter.CompanyId = Ticket_CustomerFilterDTO.CompanyId;
            CustomerFilter.ParentCompanyId = Ticket_CustomerFilterDTO.ParentCompanyId;
            CustomerFilter.TaxCode = Ticket_CustomerFilterDTO.TaxCode;
            CustomerFilter.Fax = Ticket_CustomerFilterDTO.Fax;
            CustomerFilter.Website = Ticket_CustomerFilterDTO.Website;
            CustomerFilter.NumberOfEmployee = Ticket_CustomerFilterDTO.NumberOfEmployee;
            CustomerFilter.BusinessTypeId = Ticket_CustomerFilterDTO.BusinessTypeId;
            CustomerFilter.Investment = Ticket_CustomerFilterDTO.Investment;
            CustomerFilter.RevenueAnnual = Ticket_CustomerFilterDTO.RevenueAnnual;
            CustomerFilter.Descreption = Ticket_CustomerFilterDTO.Descreption;

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<Ticket_CustomerDTO> Ticket_CustomerDTOs = Customers
                .Select(x => new Ticket_CustomerDTO(x)).ToList();
            return Ticket_CustomerDTOs;
        }
        [Route(TicketRoute.SingleListOrganization), HttpPost]
        public async Task<List<Ticket_OrganizationDTO>> SingleListOrganization([FromBody] Ticket_OrganizationFilterDTO Ticket_OrganizationFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = 20;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = Ticket_OrganizationFilterDTO.Id;
            OrganizationFilter.Name = Ticket_OrganizationFilterDTO.Name;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<Ticket_OrganizationDTO> Ticket_OrganizationDTOs = Organizations
                .Select(x => new Ticket_OrganizationDTO(x)).ToList();
            return Ticket_OrganizationDTOs;
        }
        [Route(TicketRoute.SingleListProduct), HttpPost]
        public async Task<List<Ticket_ProductDTO>> SingleListProduct([FromBody] Ticket_ProductFilterDTO Ticket_ProductFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProductFilter ProductFilter = new ProductFilter();
            ProductFilter.Skip = 0;
            ProductFilter.Take = 20;
            ProductFilter.OrderBy = ProductOrder.Id;
            ProductFilter.OrderType = OrderType.ASC;
            ProductFilter.Selects = ProductSelect.ALL;
            ProductFilter.Id = Ticket_ProductFilterDTO.Id;
            ProductFilter.Name = Ticket_ProductFilterDTO.Name;

            List<Product> Products = await ProductService.List(ProductFilter);
            List<Ticket_ProductDTO> Ticket_ProductDTOs = Products
                .Select(x => new Ticket_ProductDTO(x)).ToList();
            return Ticket_ProductDTOs;
        }
        [Route(TicketRoute.SingleListCallLog), HttpPost]
        public async Task<List<Ticket_CallLogDTO>> SingleListCallLog([FromBody] Ticket_CallLogFilterDTO Ticket_CallLogFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = new CallLogFilter();
            CallLogFilter.Skip = 0;
            CallLogFilter.Take = 20;
            CallLogFilter.OrderBy = CallLogOrder.Id;
            CallLogFilter.OrderType = OrderType.ASC;
            CallLogFilter.Selects = CallLogSelect.ALL;
            CallLogFilter.Id = Ticket_CallLogFilterDTO.Id;
            CallLogFilter.EntityReferenceId = Ticket_CallLogFilterDTO.EntityReferenceId;
            CallLogFilter.CallTypeId = Ticket_CallLogFilterDTO.CallTypeId;
            CallLogFilter.CallEmotionId = Ticket_CallLogFilterDTO.CallEmotionId;
            CallLogFilter.AppUserId = Ticket_CallLogFilterDTO.AppUserId;
            CallLogFilter.Title = Ticket_CallLogFilterDTO.Title;
            CallLogFilter.Content = Ticket_CallLogFilterDTO.Content;
            CallLogFilter.Phone = Ticket_CallLogFilterDTO.Phone;
            CallLogFilter.CallTime = Ticket_CallLogFilterDTO.CallTime;

            List<CallLog> CallLogs = await CallLogService.List(CallLogFilter);
            List<Ticket_CallLogDTO> Ticket_CallLogDTOs = CallLogs
                .Select(x => new Ticket_CallLogDTO(x)).ToList();
            return Ticket_CallLogDTOs;
        }
        [Route(TicketRoute.SingleListTicket), HttpPost]
        public async Task<List<Ticket_TicketDTO>> SingleListTicket([FromBody] Ticket_TicketFilterDTO Ticket_TicketFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketFilter TicketFilter = new TicketFilter();
            TicketFilter.Skip = 0;
            TicketFilter.Take = 20;
            TicketFilter.OrderBy = TicketOrder.Id;
            TicketFilter.OrderType = OrderType.ASC;
            TicketFilter.Selects = TicketSelect.ALL;
            TicketFilter.Id = Ticket_TicketFilterDTO.Id;
            TicketFilter.Name = Ticket_TicketFilterDTO.Name;
            TicketFilter.Phone = Ticket_TicketFilterDTO.Phone;
            TicketFilter.CustomerId = Ticket_TicketFilterDTO.CustomerId;
            TicketFilter.UserId = Ticket_TicketFilterDTO.UserId;
            TicketFilter.ProductId = Ticket_TicketFilterDTO.ProductId;
            TicketFilter.ReceiveDate = Ticket_TicketFilterDTO.ReceiveDate;
            TicketFilter.ProcessDate = Ticket_TicketFilterDTO.ProcessDate;
            TicketFilter.FinishDate = Ticket_TicketFilterDTO.FinishDate;
            TicketFilter.Subject = Ticket_TicketFilterDTO.Subject;
            TicketFilter.Content = Ticket_TicketFilterDTO.Content;
            TicketFilter.TicketIssueLevelId = Ticket_TicketFilterDTO.TicketIssueLevelId;
            TicketFilter.TicketPriorityId = Ticket_TicketFilterDTO.TicketPriorityId;
            TicketFilter.TicketStatusId = Ticket_TicketFilterDTO.TicketStatusId;
            TicketFilter.TicketSourceId = Ticket_TicketFilterDTO.TicketSourceId;
            TicketFilter.TicketNumber = Ticket_TicketFilterDTO.TicketNumber;
            TicketFilter.DepartmentId = Ticket_TicketFilterDTO.DepartmentId;
            TicketFilter.RelatedTicketId = Ticket_TicketFilterDTO.RelatedTicketId;
            TicketFilter.SLA = Ticket_TicketFilterDTO.SLA;
            TicketFilter.RelatedCallLogId = Ticket_TicketFilterDTO.RelatedCallLogId;
            TicketFilter.ResponseMethodId = Ticket_TicketFilterDTO.ResponseMethodId;
            TicketFilter.StatusId = Ticket_TicketFilterDTO.StatusId;

            List<Ticket> Tickets = await TicketService.List(TicketFilter);
            List<Ticket_TicketDTO> Ticket_TicketDTOs = Tickets
                .Select(x => new Ticket_TicketDTO(x)).ToList();
            return Ticket_TicketDTOs;
        }
        [Route(TicketRoute.SingleListStatus), HttpPost]
        public async Task<List<Ticket_StatusDTO>> SingleListStatus([FromBody] Ticket_StatusFilterDTO Ticket_StatusFilterDTO)
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
            List<Ticket_StatusDTO> Ticket_StatusDTOs = Statuses
                .Select(x => new Ticket_StatusDTO(x)).ToList();
            return Ticket_StatusDTOs;
        }
        [Route(TicketRoute.SingleListTicketIssueLevel), HttpPost]
        public async Task<List<Ticket_TicketIssueLevelDTO>> SingleListTicketIssueLevel([FromBody] Ticket_TicketIssueLevelFilterDTO Ticket_TicketIssueLevelFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketIssueLevelFilter TicketIssueLevelFilter = new TicketIssueLevelFilter();
            TicketIssueLevelFilter.Skip = 0;
            TicketIssueLevelFilter.Take = 20;
            TicketIssueLevelFilter.OrderBy = TicketIssueLevelOrder.Id;
            TicketIssueLevelFilter.OrderType = OrderType.ASC;
            TicketIssueLevelFilter.Selects = TicketIssueLevelSelect.ALL;
            TicketIssueLevelFilter.Id = Ticket_TicketIssueLevelFilterDTO.Id;
            TicketIssueLevelFilter.Name = Ticket_TicketIssueLevelFilterDTO.Name;
            TicketIssueLevelFilter.OrderNumber = Ticket_TicketIssueLevelFilterDTO.OrderNumber;
            TicketIssueLevelFilter.TicketGroupId = Ticket_TicketIssueLevelFilterDTO.TicketGroupId;
            TicketIssueLevelFilter.StatusId = Ticket_TicketIssueLevelFilterDTO.StatusId;
            TicketIssueLevelFilter.SLA = Ticket_TicketIssueLevelFilterDTO.SLA;

            List<TicketIssueLevel> TicketIssueLevels = await TicketIssueLevelService.List(TicketIssueLevelFilter);
            List<Ticket_TicketIssueLevelDTO> Ticket_TicketIssueLevelDTOs = TicketIssueLevels
                .Select(x => new Ticket_TicketIssueLevelDTO(x)).ToList();
            return Ticket_TicketIssueLevelDTOs;
        }
        [Route(TicketRoute.SingleListTicketPriority), HttpPost]
        public async Task<List<Ticket_TicketPriorityDTO>> SingleListTicketPriority([FromBody] Ticket_TicketPriorityFilterDTO Ticket_TicketPriorityFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketPriorityFilter TicketPriorityFilter = new TicketPriorityFilter();
            TicketPriorityFilter.Skip = 0;
            TicketPriorityFilter.Take = 20;
            TicketPriorityFilter.OrderBy = TicketPriorityOrder.OrderNumber;
            TicketPriorityFilter.OrderType = OrderType.ASC;
            TicketPriorityFilter.Selects = TicketPrioritySelect.ALL;
            TicketPriorityFilter.Id = Ticket_TicketPriorityFilterDTO.Id;
            TicketPriorityFilter.Name = Ticket_TicketPriorityFilterDTO.Name;
            TicketPriorityFilter.OrderNumber = Ticket_TicketPriorityFilterDTO.OrderNumber;
            TicketPriorityFilter.ColorCode = Ticket_TicketPriorityFilterDTO.ColorCode;
            TicketPriorityFilter.StatusId = Ticket_TicketPriorityFilterDTO.StatusId;

            List<TicketPriority> TicketPriorities = await TicketPriorityService.List(TicketPriorityFilter);
            List<Ticket_TicketPriorityDTO> Ticket_TicketPriorityDTOs = TicketPriorities
                .Select(x => new Ticket_TicketPriorityDTO(x)).ToList();
            return Ticket_TicketPriorityDTOs;
        }
        [Route(TicketRoute.SingleListTicketSource), HttpPost]
        public async Task<List<Ticket_TicketSourceDTO>> SingleListTicketSource([FromBody] Ticket_TicketSourceFilterDTO Ticket_TicketSourceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketSourceFilter TicketSourceFilter = new TicketSourceFilter();
            TicketSourceFilter.Skip = 0;
            TicketSourceFilter.Take = 20;
            TicketSourceFilter.OrderBy = TicketSourceOrder.Id;
            TicketSourceFilter.OrderType = OrderType.ASC;
            TicketSourceFilter.Selects = TicketSourceSelect.ALL;
            TicketSourceFilter.Id = Ticket_TicketSourceFilterDTO.Id;
            TicketSourceFilter.Name = Ticket_TicketSourceFilterDTO.Name;
            TicketSourceFilter.OrderNumber = Ticket_TicketSourceFilterDTO.OrderNumber;
            TicketSourceFilter.StatusId = Ticket_TicketSourceFilterDTO.StatusId;

            List<TicketSource> TicketSources = await TicketSourceService.List(TicketSourceFilter);
            List<Ticket_TicketSourceDTO> Ticket_TicketSourceDTOs = TicketSources
                .Select(x => new Ticket_TicketSourceDTO(x)).ToList();
            return Ticket_TicketSourceDTOs;
        }
        [Route(TicketRoute.SingleListTicketStatus), HttpPost]
        public async Task<List<Ticket_TicketStatusDTO>> SingleListTicketStatus([FromBody] Ticket_TicketStatusFilterDTO Ticket_TicketStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketStatusFilter TicketStatusFilter = new TicketStatusFilter();
            TicketStatusFilter.Skip = 0;
            TicketStatusFilter.Take = 20;
            TicketStatusFilter.OrderBy = TicketStatusOrder.Id;
            TicketStatusFilter.OrderType = OrderType.ASC;
            TicketStatusFilter.Selects = TicketStatusSelect.ALL;
            TicketStatusFilter.Id = Ticket_TicketStatusFilterDTO.Id;
            TicketStatusFilter.Name = Ticket_TicketStatusFilterDTO.Name;
            TicketStatusFilter.OrderNumber = Ticket_TicketStatusFilterDTO.OrderNumber;
            TicketStatusFilter.ColorCode = Ticket_TicketStatusFilterDTO.ColorCode;
            TicketStatusFilter.StatusId = Ticket_TicketStatusFilterDTO.StatusId;
            TicketStatusFilter.StatusId.Equal = StatusEnum.ACTIVE.Id;

            List<TicketStatus> TicketStatuses = await TicketStatusService.List(TicketStatusFilter);
            List<Ticket_TicketStatusDTO> Ticket_TicketStatusDTOs = TicketStatuses
                .Select(x => new Ticket_TicketStatusDTO(x)).ToList();
            return Ticket_TicketStatusDTOs;
        }
       
        [Route(TicketRoute.SingleListTicketGroup), HttpPost]
        public async Task<List<Ticket_TicketGroupDTO>> SingleListTicketGroup([FromBody] Ticket_TicketGroupFilterDTO Ticket_TicketGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketGroupFilter TicketGroupFilter = new TicketGroupFilter();
            TicketGroupFilter.Skip = 0;
            TicketGroupFilter.Take = 20;
            TicketGroupFilter.OrderBy = TicketGroupOrder.Id;
            TicketGroupFilter.OrderType = OrderType.ASC;
            TicketGroupFilter.Selects = TicketGroupSelect.ALL;
            TicketGroupFilter.Id = Ticket_TicketGroupFilterDTO.Id;
            TicketGroupFilter.Name = Ticket_TicketGroupFilterDTO.Name;
            TicketGroupFilter.OrderNumber = Ticket_TicketGroupFilterDTO.OrderNumber;
            TicketGroupFilter.StatusId = Ticket_TicketGroupFilterDTO.StatusId;
            TicketGroupFilter.TicketTypeId = Ticket_TicketGroupFilterDTO.TicketTypeId;

            List<TicketGroup> TicketGroups = await TicketGroupService.List(TicketGroupFilter);
            List<Ticket_TicketGroupDTO> TicketIssueLevel_TicketGroupDTOs = TicketGroups
                .Select(x => new Ticket_TicketGroupDTO(x)).ToList();
            return TicketIssueLevel_TicketGroupDTOs;
        }
        [Route(TicketRoute.SingleListTicketResolveType), HttpPost]
        public async Task<List<Ticket_TicketResolveTypeDTO>> SingleListTicketResolveType([FromBody] Ticket_TicketResolveTypeFilterDTO Ticket_TicketResolveTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketResolveTypeFilter TicketResolveTypeFilter = new TicketResolveTypeFilter();
            TicketResolveTypeFilter.Skip = 0;
            TicketResolveTypeFilter.Take = 20;
            TicketResolveTypeFilter.OrderBy = TicketResolveTypeOrder.Id;
            TicketResolveTypeFilter.OrderType = OrderType.ASC;
            TicketResolveTypeFilter.Selects = TicketResolveTypeSelect.ALL;
            TicketResolveTypeFilter.Id = Ticket_TicketResolveTypeFilterDTO.Id;
            TicketResolveTypeFilter.Code = Ticket_TicketResolveTypeFilterDTO.Code;
            TicketResolveTypeFilter.Name = Ticket_TicketResolveTypeFilterDTO.Name;

            List<TicketResolveType> TicketResolveTypes = await TicketResolveTypeService.List(TicketResolveTypeFilter);
            List<Ticket_TicketResolveTypeDTO> Ticket_TicketResolveTypeDTOs = TicketResolveTypes
                .Select(x => new Ticket_TicketResolveTypeDTO(x)).ToList();
            return Ticket_TicketResolveTypeDTOs;
        }
        [Route(TicketRoute.SingleListTicketType), HttpPost]
        public async Task<List<Ticket_TicketTypeDTO>> SingleListTicketType([FromBody] Ticket_TicketTypeFilterDTO Ticket_TicketTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter();
            TicketTypeFilter.Skip = 0;
            TicketTypeFilter.Take = 20;
            TicketTypeFilter.OrderBy = TicketTypeOrder.Id;
            TicketTypeFilter.OrderType = OrderType.ASC;
            TicketTypeFilter.Selects = TicketTypeSelect.ALL;
            TicketTypeFilter.Id = Ticket_TicketTypeFilterDTO.Id;
            TicketTypeFilter.Code = Ticket_TicketTypeFilterDTO.Code;
            TicketTypeFilter.Name = Ticket_TicketTypeFilterDTO.Name;
            TicketTypeFilter.ColorCode = Ticket_TicketTypeFilterDTO.ColorCode;
            TicketTypeFilter.StatusId = Ticket_TicketTypeFilterDTO.StatusId;

            List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);
            List<Ticket_TicketTypeDTO> Ticket_TicketTypeDTOs = TicketTypes
                .Select(x => new Ticket_TicketTypeDTO(x)).ToList();
            return Ticket_TicketTypeDTOs;
        }
        [Route(TicketRoute.SingleListAppUser), HttpPost]
        public async Task<List<Ticket_AppUserDTO>> SingleListAppUser([FromBody] Ticket_AppUserFilterDTO Ticket_AppUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = Ticket_AppUserFilterDTO.Id;
            AppUserFilter.Username = Ticket_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Ticket_AppUserFilterDTO.DisplayName;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Ticket_AppUserDTO> Ticket_AppUserDTOs = AppUsers
                .Select(x => new Ticket_AppUserDTO(x)).ToList();
            return Ticket_AppUserDTOs;
        }
        [Route(TicketRoute.SingleListCustomerType), HttpPost]
        public async Task<List<Ticket_CustomerTypeDTO>> SingleListCustomerType([FromBody] Ticket_CustomerTypeFilterDTO Ticket_CustomerTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerTypeFilter CustomerTypeFilter = new CustomerTypeFilter();
            CustomerTypeFilter.Skip = 0;
            CustomerTypeFilter.Take = 20;
            CustomerTypeFilter.OrderBy = CustomerTypeOrder.Id;
            CustomerTypeFilter.OrderType = OrderType.ASC;
            CustomerTypeFilter.Selects = CustomerTypeSelect.ALL;
            CustomerTypeFilter.Id = Ticket_CustomerTypeFilterDTO.Id;
            CustomerTypeFilter.Name = Ticket_CustomerTypeFilterDTO.Name;
            CustomerTypeFilter.Code = Ticket_CustomerTypeFilterDTO.Code;

            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            List<Ticket_CustomerTypeDTO> Ticket_CustomerTypeDTOs = CustomerTypes
                .Select(x => new Ticket_CustomerTypeDTO(x)).ToList();
            return Ticket_CustomerTypeDTOs;
        }
       
    }
}

