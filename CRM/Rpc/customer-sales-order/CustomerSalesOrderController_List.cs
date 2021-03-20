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
using CRM.Services.MCustomerSalesOrder;
using CRM.Services.MContract;
using CRM.Services.MAppUser;
using CRM.Services.MCustomer;
using CRM.Services.MCustomerType;
using CRM.Services.MDistrict;
using CRM.Services.MNation;
using CRM.Services.MProvince;
using CRM.Services.MWard;
using CRM.Services.MEditedPriceStatus;
using CRM.Services.MOpportunity;
using CRM.Services.MOrderPaymentStatus;
using CRM.Services.MOrganization;
using CRM.Services.MRequestState;
using CRM.Services.MCustomerSalesOrderContent;
using CRM.Services.MProduct;
using CRM.Services.MUnitOfMeasure;
using CRM.Services.MCustomerSalesOrderPaymentHistory;
using CRM.Services.MCustomerSalesOrderPromotion;
using CRM.Enums;

namespace CRM.Rpc.customer_sales_order
{
    public partial class CustomerSalesOrderController : RpcController
    {
        [Route(CustomerSalesOrderRoute.FilterListContract), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_ContractDTO>>> FilterListContract([FromBody] CustomerSalesOrder_ContractFilterDTO CustomerSalesOrder_ContractFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractFilter ContractFilter = new ContractFilter();
            ContractFilter.Skip = 0;
            ContractFilter.Take = 20;
            ContractFilter.OrderBy = ContractOrder.Id;
            ContractFilter.OrderType = OrderType.ASC;
            ContractFilter.Selects = ContractSelect.ALL;
            ContractFilter.Id = CustomerSalesOrder_ContractFilterDTO.Id;
            ContractFilter.Code = CustomerSalesOrder_ContractFilterDTO.Code;
            ContractFilter.Name = CustomerSalesOrder_ContractFilterDTO.Name;
            ContractFilter.TotalValue = CustomerSalesOrder_ContractFilterDTO.TotalValue;
            ContractFilter.ValidityDate = CustomerSalesOrder_ContractFilterDTO.ValidityDate;
            ContractFilter.ExpirationDate = CustomerSalesOrder_ContractFilterDTO.ExpirationDate;
            ContractFilter.DeliveryUnit = CustomerSalesOrder_ContractFilterDTO.DeliveryUnit;
            ContractFilter.InvoiceAddress = CustomerSalesOrder_ContractFilterDTO.InvoiceAddress;
            ContractFilter.InvoiceZipCode = CustomerSalesOrder_ContractFilterDTO.InvoiceZipCode;
            ContractFilter.ReceiveAddress = CustomerSalesOrder_ContractFilterDTO.ReceiveAddress;
            ContractFilter.ReceiveZipCode = CustomerSalesOrder_ContractFilterDTO.ReceiveZipCode;
            ContractFilter.TermAndCondition = CustomerSalesOrder_ContractFilterDTO.TermAndCondition;
            ContractFilter.InvoiceNationId = CustomerSalesOrder_ContractFilterDTO.InvoiceNationId;
            ContractFilter.InvoiceProvinceId = CustomerSalesOrder_ContractFilterDTO.InvoiceProvinceId;
            ContractFilter.InvoiceDistrictId = CustomerSalesOrder_ContractFilterDTO.InvoiceDistrictId;
            ContractFilter.ReceiveNationId = CustomerSalesOrder_ContractFilterDTO.ReceiveNationId;
            ContractFilter.ReceiveProvinceId = CustomerSalesOrder_ContractFilterDTO.ReceiveProvinceId;
            ContractFilter.ReceiveDistrictId = CustomerSalesOrder_ContractFilterDTO.ReceiveDistrictId;
            ContractFilter.ContractTypeId = CustomerSalesOrder_ContractFilterDTO.ContractTypeId;
            ContractFilter.CompanyId = CustomerSalesOrder_ContractFilterDTO.CompanyId;
            ContractFilter.OpportunityId = CustomerSalesOrder_ContractFilterDTO.OpportunityId;
            ContractFilter.OrganizationId = CustomerSalesOrder_ContractFilterDTO.OrganizationId;
            ContractFilter.AppUserId = CustomerSalesOrder_ContractFilterDTO.AppUserId;
            ContractFilter.ContractStatusId = CustomerSalesOrder_ContractFilterDTO.ContractStatusId;
            ContractFilter.CreatorId = CustomerSalesOrder_ContractFilterDTO.CreatorId;
            ContractFilter.CustomerId = CustomerSalesOrder_ContractFilterDTO.CustomerId;
            ContractFilter.CurrencyId = CustomerSalesOrder_ContractFilterDTO.CurrencyId;
            ContractFilter.PaymentStatusId = CustomerSalesOrder_ContractFilterDTO.PaymentStatusId;

            List<Contract> Contracts = await ContractService.List(ContractFilter);
            List<CustomerSalesOrder_ContractDTO> CustomerSalesOrder_ContractDTOs = Contracts
                .Select(x => new CustomerSalesOrder_ContractDTO(x)).ToList();
            return CustomerSalesOrder_ContractDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_AppUserDTO>>> FilterListAppUser([FromBody] CustomerSalesOrder_AppUserFilterDTO CustomerSalesOrder_AppUserFilterDTO)
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
            AppUserFilter.Id = CustomerSalesOrder_AppUserFilterDTO.Id;
            AppUserFilter.Username = CustomerSalesOrder_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = CustomerSalesOrder_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = CustomerSalesOrder_AppUserFilterDTO.Address;
            AppUserFilter.Email = CustomerSalesOrder_AppUserFilterDTO.Email;
            AppUserFilter.Phone = CustomerSalesOrder_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = CustomerSalesOrder_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = CustomerSalesOrder_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = CustomerSalesOrder_AppUserFilterDTO.Avatar;
            AppUserFilter.Department = CustomerSalesOrder_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = CustomerSalesOrder_AppUserFilterDTO.OrganizationId;
            AppUserFilter.Longitude = CustomerSalesOrder_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = CustomerSalesOrder_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = CustomerSalesOrder_AppUserFilterDTO.StatusId;

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<CustomerSalesOrder_AppUserDTO> CustomerSalesOrder_AppUserDTOs = AppUsers
                .Select(x => new CustomerSalesOrder_AppUserDTO(x)).ToList();
            return CustomerSalesOrder_AppUserDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListCustomer), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_CustomerDTO>>> FilterListCustomer([FromBody] CustomerSalesOrder_CustomerFilterDTO CustomerSalesOrder_CustomerFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            CustomerFilter.Id = CustomerSalesOrder_CustomerFilterDTO.Id;
            CustomerFilter.Code = CustomerSalesOrder_CustomerFilterDTO.Code;
            CustomerFilter.Name = CustomerSalesOrder_CustomerFilterDTO.Name;
            CustomerFilter.Phone = CustomerSalesOrder_CustomerFilterDTO.Phone;
            CustomerFilter.Address = CustomerSalesOrder_CustomerFilterDTO.Address;
            CustomerFilter.NationId = CustomerSalesOrder_CustomerFilterDTO.NationId;
            CustomerFilter.ProvinceId = CustomerSalesOrder_CustomerFilterDTO.ProvinceId;
            CustomerFilter.DistrictId = CustomerSalesOrder_CustomerFilterDTO.DistrictId;
            CustomerFilter.WardId = CustomerSalesOrder_CustomerFilterDTO.WardId;
            CustomerFilter.CustomerTypeId = CustomerSalesOrder_CustomerFilterDTO.CustomerTypeId;
            CustomerFilter.Birthday = CustomerSalesOrder_CustomerFilterDTO.Birthday;
            CustomerFilter.Email = CustomerSalesOrder_CustomerFilterDTO.Email;
            CustomerFilter.ProfessionId = CustomerSalesOrder_CustomerFilterDTO.ProfessionId;
            CustomerFilter.CustomerResourceId = CustomerSalesOrder_CustomerFilterDTO.CustomerResourceId;
            CustomerFilter.SexId = CustomerSalesOrder_CustomerFilterDTO.SexId;
            CustomerFilter.StatusId = CustomerSalesOrder_CustomerFilterDTO.StatusId;
            CustomerFilter.CompanyId = CustomerSalesOrder_CustomerFilterDTO.CompanyId;
            CustomerFilter.ParentCompanyId = CustomerSalesOrder_CustomerFilterDTO.ParentCompanyId;
            CustomerFilter.TaxCode = CustomerSalesOrder_CustomerFilterDTO.TaxCode;
            CustomerFilter.Fax = CustomerSalesOrder_CustomerFilterDTO.Fax;
            CustomerFilter.Website = CustomerSalesOrder_CustomerFilterDTO.Website;
            CustomerFilter.NumberOfEmployee = CustomerSalesOrder_CustomerFilterDTO.NumberOfEmployee;
            CustomerFilter.BusinessTypeId = CustomerSalesOrder_CustomerFilterDTO.BusinessTypeId;
            CustomerFilter.Investment = CustomerSalesOrder_CustomerFilterDTO.Investment;
            CustomerFilter.RevenueAnnual = CustomerSalesOrder_CustomerFilterDTO.RevenueAnnual;
            CustomerFilter.Descreption = CustomerSalesOrder_CustomerFilterDTO.Descreption;

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<CustomerSalesOrder_CustomerDTO> CustomerSalesOrder_CustomerDTOs = Customers
                .Select(x => new CustomerSalesOrder_CustomerDTO(x)).ToList();
            return CustomerSalesOrder_CustomerDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListCustomerType), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_CustomerTypeDTO>>> FilterListCustomerType([FromBody] CustomerSalesOrder_CustomerTypeFilterDTO CustomerSalesOrder_CustomerTypeFilterDTO)
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
            CustomerTypeFilter.Id = CustomerSalesOrder_CustomerTypeFilterDTO.Id;
            CustomerTypeFilter.Code = CustomerSalesOrder_CustomerTypeFilterDTO.Code;
            CustomerTypeFilter.Name = CustomerSalesOrder_CustomerTypeFilterDTO.Name;

            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            List<CustomerSalesOrder_CustomerTypeDTO> CustomerSalesOrder_CustomerTypeDTOs = CustomerTypes
                .Select(x => new CustomerSalesOrder_CustomerTypeDTO(x)).ToList();
            return CustomerSalesOrder_CustomerTypeDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListDistrict), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_DistrictDTO>>> FilterListDistrict([FromBody] CustomerSalesOrder_DistrictFilterDTO CustomerSalesOrder_DistrictFilterDTO)
        {
            if (UnAuthorization) return Forbid(); if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DistrictFilter DistrictFilter = new DistrictFilter();
            DistrictFilter.Skip = 0;
            DistrictFilter.Take = 20;
            DistrictFilter.OrderBy = DistrictOrder.Id;
            DistrictFilter.OrderType = OrderType.ASC;
            DistrictFilter.Selects = DistrictSelect.ALL;
            DistrictFilter.Id = CustomerSalesOrder_DistrictFilterDTO.Id;
            DistrictFilter.Code = CustomerSalesOrder_DistrictFilterDTO.Code;
            DistrictFilter.Name = CustomerSalesOrder_DistrictFilterDTO.Name;
            DistrictFilter.Priority = CustomerSalesOrder_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = CustomerSalesOrder_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = CustomerSalesOrder_DistrictFilterDTO.StatusId;

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<CustomerSalesOrder_DistrictDTO> CustomerSalesOrder_DistrictDTOs = Districts
                .Select(x => new CustomerSalesOrder_DistrictDTO(x)).ToList();
            return CustomerSalesOrder_DistrictDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListNation), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_NationDTO>>> FilterListNation([FromBody] CustomerSalesOrder_NationFilterDTO CustomerSalesOrder_NationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            NationFilter NationFilter = new NationFilter();
            NationFilter.Skip = 0;
            NationFilter.Take = 20;
            NationFilter.OrderBy = NationOrder.Id;
            NationFilter.OrderType = OrderType.ASC;
            NationFilter.Selects = NationSelect.ALL;
            NationFilter.Id = CustomerSalesOrder_NationFilterDTO.Id;
            NationFilter.Code = CustomerSalesOrder_NationFilterDTO.Code;
            NationFilter.Name = CustomerSalesOrder_NationFilterDTO.Name;
            NationFilter.Priority = CustomerSalesOrder_NationFilterDTO.Priority;
            NationFilter.StatusId = CustomerSalesOrder_NationFilterDTO.StatusId;

            List<Nation> Nations = await NationService.List(NationFilter);
            List<CustomerSalesOrder_NationDTO> CustomerSalesOrder_NationDTOs = Nations
                .Select(x => new CustomerSalesOrder_NationDTO(x)).ToList();
            return CustomerSalesOrder_NationDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListProvince), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_ProvinceDTO>>> FilterListProvince([FromBody] CustomerSalesOrder_ProvinceFilterDTO CustomerSalesOrder_ProvinceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter.Skip = 0;
            ProvinceFilter.Take = 20;
            ProvinceFilter.OrderBy = ProvinceOrder.Id;
            ProvinceFilter.OrderType = OrderType.ASC;
            ProvinceFilter.Selects = ProvinceSelect.ALL;
            ProvinceFilter.Id = CustomerSalesOrder_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = CustomerSalesOrder_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = CustomerSalesOrder_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = CustomerSalesOrder_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = CustomerSalesOrder_ProvinceFilterDTO.StatusId;

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<CustomerSalesOrder_ProvinceDTO> CustomerSalesOrder_ProvinceDTOs = Provinces
                .Select(x => new CustomerSalesOrder_ProvinceDTO(x)).ToList();
            return CustomerSalesOrder_ProvinceDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListWard), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_WardDTO>>> FilterListWard([FromBody] CustomerSalesOrder_WardFilterDTO CustomerSalesOrder_WardFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            WardFilter WardFilter = new WardFilter();
            WardFilter.Skip = 0;
            WardFilter.Take = 20;
            WardFilter.OrderBy = WardOrder.Id;
            WardFilter.OrderType = OrderType.ASC;
            WardFilter.Selects = WardSelect.ALL;
            WardFilter.Id = CustomerSalesOrder_WardFilterDTO.Id;
            WardFilter.Code = CustomerSalesOrder_WardFilterDTO.Code;
            WardFilter.Name = CustomerSalesOrder_WardFilterDTO.Name;
            WardFilter.Priority = CustomerSalesOrder_WardFilterDTO.Priority;
            WardFilter.DistrictId = CustomerSalesOrder_WardFilterDTO.DistrictId;
            WardFilter.StatusId = CustomerSalesOrder_WardFilterDTO.StatusId;

            List<Ward> Wards = await WardService.List(WardFilter);
            List<CustomerSalesOrder_WardDTO> CustomerSalesOrder_WardDTOs = Wards
                .Select(x => new CustomerSalesOrder_WardDTO(x)).ToList();
            return CustomerSalesOrder_WardDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListEditedPriceStatus), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_EditedPriceStatusDTO>>> FilterListEditedPriceStatus([FromBody] CustomerSalesOrder_EditedPriceStatusFilterDTO CustomerSalesOrder_EditedPriceStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            EditedPriceStatusFilter EditedPriceStatusFilter = new EditedPriceStatusFilter();
            EditedPriceStatusFilter.Skip = 0;
            EditedPriceStatusFilter.Take = int.MaxValue;
            EditedPriceStatusFilter.Take = 20;
            EditedPriceStatusFilter.OrderBy = EditedPriceStatusOrder.Id;
            EditedPriceStatusFilter.OrderType = OrderType.ASC;
            EditedPriceStatusFilter.Selects = EditedPriceStatusSelect.ALL;

            List<EditedPriceStatus> EditedPriceStatuses = await EditedPriceStatusService.List(EditedPriceStatusFilter);
            List<CustomerSalesOrder_EditedPriceStatusDTO> CustomerSalesOrder_EditedPriceStatusDTOs = EditedPriceStatuses
                .Select(x => new CustomerSalesOrder_EditedPriceStatusDTO(x)).ToList();
            return CustomerSalesOrder_EditedPriceStatusDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListOpportunity), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_OpportunityDTO>>> FilterListOpportunity([FromBody] CustomerSalesOrder_OpportunityFilterDTO CustomerSalesOrder_OpportunityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityFilter OpportunityFilter = new OpportunityFilter();
            OpportunityFilter.Skip = 0;
            OpportunityFilter.Take = 20;
            OpportunityFilter.OrderBy = OpportunityOrder.Id;
            OpportunityFilter.OrderType = OrderType.ASC;
            OpportunityFilter.Selects = OpportunitySelect.ALL;
            OpportunityFilter.Id = CustomerSalesOrder_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = CustomerSalesOrder_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = CustomerSalesOrder_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.CustomerLeadId = CustomerSalesOrder_OpportunityFilterDTO.CustomerLeadId;
            OpportunityFilter.ClosingDate = CustomerSalesOrder_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = CustomerSalesOrder_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = CustomerSalesOrder_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = CustomerSalesOrder_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = CustomerSalesOrder_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.AppUserId = CustomerSalesOrder_OpportunityFilterDTO.AppUserId;
            OpportunityFilter.CurrencyId = CustomerSalesOrder_OpportunityFilterDTO.CurrencyId;
            OpportunityFilter.Amount = CustomerSalesOrder_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = CustomerSalesOrder_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = CustomerSalesOrder_OpportunityFilterDTO.Description;
            OpportunityFilter.OpportunityResultTypeId = CustomerSalesOrder_OpportunityFilterDTO.OpportunityResultTypeId;
            OpportunityFilter.CreatorId = CustomerSalesOrder_OpportunityFilterDTO.CreatorId;

            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<CustomerSalesOrder_OpportunityDTO> CustomerSalesOrder_OpportunityDTOs = Opportunities
                .Select(x => new CustomerSalesOrder_OpportunityDTO(x)).ToList();
            return CustomerSalesOrder_OpportunityDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListOrderPaymentStatus), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_OrderPaymentStatusDTO>>> FilterListOrderPaymentStatus([FromBody] CustomerSalesOrder_OrderPaymentStatusFilterDTO CustomerSalesOrder_OrderPaymentStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderPaymentStatusFilter OrderPaymentStatusFilter = new OrderPaymentStatusFilter();
            OrderPaymentStatusFilter.Skip = 0;
            OrderPaymentStatusFilter.Take = int.MaxValue;
            OrderPaymentStatusFilter.Take = 20;
            OrderPaymentStatusFilter.OrderBy = OrderPaymentStatusOrder.Id;
            OrderPaymentStatusFilter.OrderType = OrderType.ASC;
            OrderPaymentStatusFilter.Selects = OrderPaymentStatusSelect.ALL;

            List<OrderPaymentStatus> OrderPaymentStatuses = await OrderPaymentStatusService.List(OrderPaymentStatusFilter);
            List<CustomerSalesOrder_OrderPaymentStatusDTO> CustomerSalesOrder_OrderPaymentStatusDTOs = OrderPaymentStatuses
                .Select(x => new CustomerSalesOrder_OrderPaymentStatusDTO(x)).ToList();
            return CustomerSalesOrder_OrderPaymentStatusDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListOrganization), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_OrganizationDTO>>> FilterListOrganization([FromBody] CustomerSalesOrder_OrganizationFilterDTO CustomerSalesOrder_OrganizationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = int.MaxValue;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = CustomerSalesOrder_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = CustomerSalesOrder_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = CustomerSalesOrder_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = CustomerSalesOrder_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = CustomerSalesOrder_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = CustomerSalesOrder_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = CustomerSalesOrder_OrganizationFilterDTO.StatusId;
            OrganizationFilter.Phone = CustomerSalesOrder_OrganizationFilterDTO.Phone;
            OrganizationFilter.Email = CustomerSalesOrder_OrganizationFilterDTO.Email;
            OrganizationFilter.Address = CustomerSalesOrder_OrganizationFilterDTO.Address;

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<CustomerSalesOrder_OrganizationDTO> CustomerSalesOrder_OrganizationDTOs = Organizations
                .Select(x => new CustomerSalesOrder_OrganizationDTO(x)).ToList();
            return CustomerSalesOrder_OrganizationDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListRequestState), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_RequestStateDTO>>> FilterListRequestState([FromBody] CustomerSalesOrder_RequestStateFilterDTO CustomerSalesOrder_RequestStateFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RequestStateFilter RequestStateFilter = new RequestStateFilter();
            RequestStateFilter.Skip = 0;
            RequestStateFilter.Take = 20;
            RequestStateFilter.OrderBy = RequestStateOrder.Id;
            RequestStateFilter.OrderType = OrderType.ASC;
            RequestStateFilter.Selects = RequestStateSelect.ALL;
            RequestStateFilter.Id = CustomerSalesOrder_RequestStateFilterDTO.Id;
            RequestStateFilter.Code = CustomerSalesOrder_RequestStateFilterDTO.Code;
            RequestStateFilter.Name = CustomerSalesOrder_RequestStateFilterDTO.Name;

            List<RequestState> RequestStates = await RequestStateService.List(RequestStateFilter);
            List<CustomerSalesOrder_RequestStateDTO> CustomerSalesOrder_RequestStateDTOs = RequestStates
                .Select(x => new CustomerSalesOrder_RequestStateDTO(x)).ToList();
            return CustomerSalesOrder_RequestStateDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListItem), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_ItemDTO>>> FilterListItem([FromBody] CustomerSalesOrder_ItemFilterDTO CustomerSalesOrder_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.Id = CustomerSalesOrder_ItemFilterDTO.Id;
            ItemFilter.ProductId = CustomerSalesOrder_ItemFilterDTO.ProductId;
            ItemFilter.Code = CustomerSalesOrder_ItemFilterDTO.Code;
            ItemFilter.Name = CustomerSalesOrder_ItemFilterDTO.Name;
            ItemFilter.ScanCode = CustomerSalesOrder_ItemFilterDTO.ScanCode;
            ItemFilter.SalePrice = CustomerSalesOrder_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = CustomerSalesOrder_ItemFilterDTO.RetailPrice;
            ItemFilter.StatusId = CustomerSalesOrder_ItemFilterDTO.StatusId;

            List<Item> Items = await ItemService.List(ItemFilter);
            List<CustomerSalesOrder_ItemDTO> CustomerSalesOrder_ItemDTOs = Items
                .Select(x => new CustomerSalesOrder_ItemDTO(x)).ToList();
            return CustomerSalesOrder_ItemDTOs;
        }
        [Route(CustomerSalesOrderRoute.FilterListUnitOfMeasure), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_UnitOfMeasureDTO>>> FilterListUnitOfMeasure([FromBody] CustomerSalesOrder_UnitOfMeasureFilterDTO CustomerSalesOrder_UnitOfMeasureFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            UnitOfMeasureFilter UnitOfMeasureFilter = new UnitOfMeasureFilter();
            UnitOfMeasureFilter.Skip = 0;
            UnitOfMeasureFilter.Take = 20;
            UnitOfMeasureFilter.OrderBy = UnitOfMeasureOrder.Id;
            UnitOfMeasureFilter.OrderType = OrderType.ASC;
            UnitOfMeasureFilter.Selects = UnitOfMeasureSelect.ALL;
            UnitOfMeasureFilter.Id = CustomerSalesOrder_UnitOfMeasureFilterDTO.Id;
            UnitOfMeasureFilter.Code = CustomerSalesOrder_UnitOfMeasureFilterDTO.Code;
            UnitOfMeasureFilter.Name = CustomerSalesOrder_UnitOfMeasureFilterDTO.Name;
            UnitOfMeasureFilter.Description = CustomerSalesOrder_UnitOfMeasureFilterDTO.Description;
            UnitOfMeasureFilter.StatusId = CustomerSalesOrder_UnitOfMeasureFilterDTO.StatusId;

            List<UnitOfMeasure> UnitOfMeasures = await UnitOfMeasureService.List(UnitOfMeasureFilter);
            List<CustomerSalesOrder_UnitOfMeasureDTO> CustomerSalesOrder_UnitOfMeasureDTOs = UnitOfMeasures
                .Select(x => new CustomerSalesOrder_UnitOfMeasureDTO(x)).ToList();
            return CustomerSalesOrder_UnitOfMeasureDTOs;
        }

        [Route(CustomerSalesOrderRoute.SingleListContract), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_ContractDTO>>> SingleListContract([FromBody] CustomerSalesOrder_ContractFilterDTO CustomerSalesOrder_ContractFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractFilter ContractFilter = new ContractFilter();
            ContractFilter.Skip = 0;
            ContractFilter.Take = 20;
            ContractFilter.OrderBy = ContractOrder.Id;
            ContractFilter.OrderType = OrderType.ASC;
            ContractFilter.Selects = ContractSelect.ALL;
            ContractFilter.Id = CustomerSalesOrder_ContractFilterDTO.Id;
            ContractFilter.Code = CustomerSalesOrder_ContractFilterDTO.Code;
            ContractFilter.Name = CustomerSalesOrder_ContractFilterDTO.Name;
            ContractFilter.TotalValue = CustomerSalesOrder_ContractFilterDTO.TotalValue;
            ContractFilter.ValidityDate = CustomerSalesOrder_ContractFilterDTO.ValidityDate;
            ContractFilter.ExpirationDate = CustomerSalesOrder_ContractFilterDTO.ExpirationDate;
            ContractFilter.DeliveryUnit = CustomerSalesOrder_ContractFilterDTO.DeliveryUnit;
            ContractFilter.InvoiceAddress = CustomerSalesOrder_ContractFilterDTO.InvoiceAddress;
            ContractFilter.InvoiceZipCode = CustomerSalesOrder_ContractFilterDTO.InvoiceZipCode;
            ContractFilter.ReceiveAddress = CustomerSalesOrder_ContractFilterDTO.ReceiveAddress;
            ContractFilter.ReceiveZipCode = CustomerSalesOrder_ContractFilterDTO.ReceiveZipCode;
            ContractFilter.TermAndCondition = CustomerSalesOrder_ContractFilterDTO.TermAndCondition;
            ContractFilter.InvoiceNationId = CustomerSalesOrder_ContractFilterDTO.InvoiceNationId;
            ContractFilter.InvoiceProvinceId = CustomerSalesOrder_ContractFilterDTO.InvoiceProvinceId;
            ContractFilter.InvoiceDistrictId = CustomerSalesOrder_ContractFilterDTO.InvoiceDistrictId;
            ContractFilter.ReceiveNationId = CustomerSalesOrder_ContractFilterDTO.ReceiveNationId;
            ContractFilter.ReceiveProvinceId = CustomerSalesOrder_ContractFilterDTO.ReceiveProvinceId;
            ContractFilter.ReceiveDistrictId = CustomerSalesOrder_ContractFilterDTO.ReceiveDistrictId;
            ContractFilter.ContractTypeId = CustomerSalesOrder_ContractFilterDTO.ContractTypeId;
            ContractFilter.CompanyId = CustomerSalesOrder_ContractFilterDTO.CompanyId;
            ContractFilter.OpportunityId = CustomerSalesOrder_ContractFilterDTO.OpportunityId;
            ContractFilter.OrganizationId = CustomerSalesOrder_ContractFilterDTO.OrganizationId;
            ContractFilter.AppUserId = CustomerSalesOrder_ContractFilterDTO.AppUserId;
            ContractFilter.ContractStatusId = CustomerSalesOrder_ContractFilterDTO.ContractStatusId;
            ContractFilter.CreatorId = CustomerSalesOrder_ContractFilterDTO.CreatorId;
            ContractFilter.CustomerId = CustomerSalesOrder_ContractFilterDTO.CustomerId;
            ContractFilter.CurrencyId = CustomerSalesOrder_ContractFilterDTO.CurrencyId;
            ContractFilter.PaymentStatusId = CustomerSalesOrder_ContractFilterDTO.PaymentStatusId;

            List<Contract> Contracts = await ContractService.List(ContractFilter);
            List<CustomerSalesOrder_ContractDTO> CustomerSalesOrder_ContractDTOs = Contracts
                .Select(x => new CustomerSalesOrder_ContractDTO(x)).ToList();
            return CustomerSalesOrder_ContractDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListAppUser), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_AppUserDTO>>> SingleListAppUser([FromBody] CustomerSalesOrder_AppUserFilterDTO CustomerSalesOrder_AppUserFilterDTO)
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
            AppUserFilter.Id = CustomerSalesOrder_AppUserFilterDTO.Id;
            AppUserFilter.Username = CustomerSalesOrder_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = CustomerSalesOrder_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = CustomerSalesOrder_AppUserFilterDTO.Address;
            AppUserFilter.Email = CustomerSalesOrder_AppUserFilterDTO.Email;
            AppUserFilter.Phone = CustomerSalesOrder_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = CustomerSalesOrder_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = CustomerSalesOrder_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = CustomerSalesOrder_AppUserFilterDTO.Avatar;
            AppUserFilter.Department = CustomerSalesOrder_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = CustomerSalesOrder_AppUserFilterDTO.OrganizationId;
            AppUserFilter.Longitude = CustomerSalesOrder_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = CustomerSalesOrder_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<CustomerSalesOrder_AppUserDTO> CustomerSalesOrder_AppUserDTOs = AppUsers
                .Select(x => new CustomerSalesOrder_AppUserDTO(x)).ToList();
            return CustomerSalesOrder_AppUserDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListCustomer), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_CustomerDTO>>> SingleListCustomer([FromBody] CustomerSalesOrder_CustomerFilterDTO CustomerSalesOrder_CustomerFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            CustomerFilter.Id = CustomerSalesOrder_CustomerFilterDTO.Id;
            CustomerFilter.Code = CustomerSalesOrder_CustomerFilterDTO.Code;
            CustomerFilter.Name = CustomerSalesOrder_CustomerFilterDTO.Name;
            CustomerFilter.Phone = CustomerSalesOrder_CustomerFilterDTO.Phone;
            CustomerFilter.Address = CustomerSalesOrder_CustomerFilterDTO.Address;
            CustomerFilter.NationId = CustomerSalesOrder_CustomerFilterDTO.NationId;
            CustomerFilter.ProvinceId = CustomerSalesOrder_CustomerFilterDTO.ProvinceId;
            CustomerFilter.DistrictId = CustomerSalesOrder_CustomerFilterDTO.DistrictId;
            CustomerFilter.WardId = CustomerSalesOrder_CustomerFilterDTO.WardId;
            CustomerFilter.CustomerTypeId = CustomerSalesOrder_CustomerFilterDTO.CustomerTypeId;
            CustomerFilter.Birthday = CustomerSalesOrder_CustomerFilterDTO.Birthday;
            CustomerFilter.Email = CustomerSalesOrder_CustomerFilterDTO.Email;
            CustomerFilter.ProfessionId = CustomerSalesOrder_CustomerFilterDTO.ProfessionId;
            CustomerFilter.CustomerResourceId = CustomerSalesOrder_CustomerFilterDTO.CustomerResourceId;
            CustomerFilter.SexId = CustomerSalesOrder_CustomerFilterDTO.SexId;
            CustomerFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            CustomerFilter.CompanyId = CustomerSalesOrder_CustomerFilterDTO.CompanyId;
            CustomerFilter.ParentCompanyId = CustomerSalesOrder_CustomerFilterDTO.ParentCompanyId;
            CustomerFilter.TaxCode = CustomerSalesOrder_CustomerFilterDTO.TaxCode;
            CustomerFilter.Fax = CustomerSalesOrder_CustomerFilterDTO.Fax;
            CustomerFilter.Website = CustomerSalesOrder_CustomerFilterDTO.Website;
            CustomerFilter.NumberOfEmployee = CustomerSalesOrder_CustomerFilterDTO.NumberOfEmployee;
            CustomerFilter.BusinessTypeId = CustomerSalesOrder_CustomerFilterDTO.BusinessTypeId;
            CustomerFilter.Investment = CustomerSalesOrder_CustomerFilterDTO.Investment;
            CustomerFilter.RevenueAnnual = CustomerSalesOrder_CustomerFilterDTO.RevenueAnnual;
            CustomerFilter.Descreption = CustomerSalesOrder_CustomerFilterDTO.Descreption;

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<CustomerSalesOrder_CustomerDTO> CustomerSalesOrder_CustomerDTOs = Customers
                .Select(x => new CustomerSalesOrder_CustomerDTO(x)).ToList();
            return CustomerSalesOrder_CustomerDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListCustomerType), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_CustomerTypeDTO>>> SingleListCustomerType([FromBody] CustomerSalesOrder_CustomerTypeFilterDTO CustomerSalesOrder_CustomerTypeFilterDTO)
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
            CustomerTypeFilter.Id = CustomerSalesOrder_CustomerTypeFilterDTO.Id;
            CustomerTypeFilter.Code = CustomerSalesOrder_CustomerTypeFilterDTO.Code;
            CustomerTypeFilter.Name = CustomerSalesOrder_CustomerTypeFilterDTO.Name;

            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            List<CustomerSalesOrder_CustomerTypeDTO> CustomerSalesOrder_CustomerTypeDTOs = CustomerTypes
                .Select(x => new CustomerSalesOrder_CustomerTypeDTO(x)).ToList();
            return CustomerSalesOrder_CustomerTypeDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListDistrict), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_DistrictDTO>>> SingleListDistrict([FromBody] CustomerSalesOrder_DistrictFilterDTO CustomerSalesOrder_DistrictFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DistrictFilter DistrictFilter = new DistrictFilter();
            DistrictFilter.Skip = 0;
            DistrictFilter.Take = 20;
            DistrictFilter.OrderBy = DistrictOrder.Id;
            DistrictFilter.OrderType = OrderType.ASC;
            DistrictFilter.Selects = DistrictSelect.ALL;
            DistrictFilter.Id = CustomerSalesOrder_DistrictFilterDTO.Id;
            DistrictFilter.Code = CustomerSalesOrder_DistrictFilterDTO.Code;
            DistrictFilter.Name = CustomerSalesOrder_DistrictFilterDTO.Name;
            DistrictFilter.Priority = CustomerSalesOrder_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = CustomerSalesOrder_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<CustomerSalesOrder_DistrictDTO> CustomerSalesOrder_DistrictDTOs = Districts
                .Select(x => new CustomerSalesOrder_DistrictDTO(x)).ToList();
            return CustomerSalesOrder_DistrictDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListNation), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_NationDTO>>> SingleListNation([FromBody] CustomerSalesOrder_NationFilterDTO CustomerSalesOrder_NationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            NationFilter NationFilter = new NationFilter();
            NationFilter.Skip = 0;
            NationFilter.Take = 20;
            NationFilter.OrderBy = NationOrder.Id;
            NationFilter.OrderType = OrderType.ASC;
            NationFilter.Selects = NationSelect.ALL;
            NationFilter.Id = CustomerSalesOrder_NationFilterDTO.Id;
            NationFilter.Code = CustomerSalesOrder_NationFilterDTO.Code;
            NationFilter.Name = CustomerSalesOrder_NationFilterDTO.Name;
            NationFilter.Priority = CustomerSalesOrder_NationFilterDTO.Priority;
            NationFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Nation> Nations = await NationService.List(NationFilter);
            List<CustomerSalesOrder_NationDTO> CustomerSalesOrder_NationDTOs = Nations
                .Select(x => new CustomerSalesOrder_NationDTO(x)).ToList();
            return CustomerSalesOrder_NationDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListProvince), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_ProvinceDTO>>> SingleListProvince([FromBody] CustomerSalesOrder_ProvinceFilterDTO CustomerSalesOrder_ProvinceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter.Skip = 0;
            ProvinceFilter.Take = 20;
            ProvinceFilter.OrderBy = ProvinceOrder.Id;
            ProvinceFilter.OrderType = OrderType.ASC;
            ProvinceFilter.Selects = ProvinceSelect.ALL;
            ProvinceFilter.Id = CustomerSalesOrder_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = CustomerSalesOrder_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = CustomerSalesOrder_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = CustomerSalesOrder_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<CustomerSalesOrder_ProvinceDTO> CustomerSalesOrder_ProvinceDTOs = Provinces
                .Select(x => new CustomerSalesOrder_ProvinceDTO(x)).ToList();
            return CustomerSalesOrder_ProvinceDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListWard), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_WardDTO>>> SingleListWard([FromBody] CustomerSalesOrder_WardFilterDTO CustomerSalesOrder_WardFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            WardFilter WardFilter = new WardFilter();
            WardFilter.Skip = 0;
            WardFilter.Take = 20;
            WardFilter.OrderBy = WardOrder.Id;
            WardFilter.OrderType = OrderType.ASC;
            WardFilter.Selects = WardSelect.ALL;
            WardFilter.Id = CustomerSalesOrder_WardFilterDTO.Id;
            WardFilter.Code = CustomerSalesOrder_WardFilterDTO.Code;
            WardFilter.Name = CustomerSalesOrder_WardFilterDTO.Name;
            WardFilter.Priority = CustomerSalesOrder_WardFilterDTO.Priority;
            WardFilter.DistrictId = CustomerSalesOrder_WardFilterDTO.DistrictId;
            WardFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Ward> Wards = await WardService.List(WardFilter);
            List<CustomerSalesOrder_WardDTO> CustomerSalesOrder_WardDTOs = Wards
                .Select(x => new CustomerSalesOrder_WardDTO(x)).ToList();
            return CustomerSalesOrder_WardDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListEditedPriceStatus), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_EditedPriceStatusDTO>>> SingleListEditedPriceStatus([FromBody] CustomerSalesOrder_EditedPriceStatusFilterDTO CustomerSalesOrder_EditedPriceStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            EditedPriceStatusFilter EditedPriceStatusFilter = new EditedPriceStatusFilter();
            EditedPriceStatusFilter.Skip = 0;
            EditedPriceStatusFilter.Take = int.MaxValue;
            EditedPriceStatusFilter.Take = 20;
            EditedPriceStatusFilter.OrderBy = EditedPriceStatusOrder.Id;
            EditedPriceStatusFilter.OrderType = OrderType.ASC;
            EditedPriceStatusFilter.Selects = EditedPriceStatusSelect.ALL;

            List<EditedPriceStatus> EditedPriceStatuses = await EditedPriceStatusService.List(EditedPriceStatusFilter);
            List<CustomerSalesOrder_EditedPriceStatusDTO> CustomerSalesOrder_EditedPriceStatusDTOs = EditedPriceStatuses
                .Select(x => new CustomerSalesOrder_EditedPriceStatusDTO(x)).ToList();
            return CustomerSalesOrder_EditedPriceStatusDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListOpportunity), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_OpportunityDTO>>> SingleListOpportunity([FromBody] CustomerSalesOrder_OpportunityFilterDTO CustomerSalesOrder_OpportunityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityFilter OpportunityFilter = new OpportunityFilter();
            OpportunityFilter.Skip = 0;
            OpportunityFilter.Take = 20;
            OpportunityFilter.OrderBy = OpportunityOrder.Id;
            OpportunityFilter.OrderType = OrderType.ASC;
            OpportunityFilter.Selects = OpportunitySelect.ALL;
            OpportunityFilter.Id = CustomerSalesOrder_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = CustomerSalesOrder_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = CustomerSalesOrder_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.CustomerLeadId = CustomerSalesOrder_OpportunityFilterDTO.CustomerLeadId;
            OpportunityFilter.ClosingDate = CustomerSalesOrder_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = CustomerSalesOrder_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = CustomerSalesOrder_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = CustomerSalesOrder_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = CustomerSalesOrder_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.AppUserId = CustomerSalesOrder_OpportunityFilterDTO.AppUserId;
            OpportunityFilter.CurrencyId = CustomerSalesOrder_OpportunityFilterDTO.CurrencyId;
            OpportunityFilter.Amount = CustomerSalesOrder_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = CustomerSalesOrder_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = CustomerSalesOrder_OpportunityFilterDTO.Description;
            OpportunityFilter.OpportunityResultTypeId = CustomerSalesOrder_OpportunityFilterDTO.OpportunityResultTypeId;
            OpportunityFilter.CreatorId = CustomerSalesOrder_OpportunityFilterDTO.CreatorId;

            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<CustomerSalesOrder_OpportunityDTO> CustomerSalesOrder_OpportunityDTOs = Opportunities
                .Select(x => new CustomerSalesOrder_OpportunityDTO(x)).ToList();
            return CustomerSalesOrder_OpportunityDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListOrderPaymentStatus), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_OrderPaymentStatusDTO>>> SingleListOrderPaymentStatus([FromBody] CustomerSalesOrder_OrderPaymentStatusFilterDTO CustomerSalesOrder_OrderPaymentStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderPaymentStatusFilter OrderPaymentStatusFilter = new OrderPaymentStatusFilter();
            OrderPaymentStatusFilter.Skip = 0;
            OrderPaymentStatusFilter.Take = int.MaxValue;
            OrderPaymentStatusFilter.Take = 20;
            OrderPaymentStatusFilter.OrderBy = OrderPaymentStatusOrder.Id;
            OrderPaymentStatusFilter.OrderType = OrderType.ASC;
            OrderPaymentStatusFilter.Selects = OrderPaymentStatusSelect.ALL;

            List<OrderPaymentStatus> OrderPaymentStatuses = await OrderPaymentStatusService.List(OrderPaymentStatusFilter);
            List<CustomerSalesOrder_OrderPaymentStatusDTO> CustomerSalesOrder_OrderPaymentStatusDTOs = OrderPaymentStatuses
                .Select(x => new CustomerSalesOrder_OrderPaymentStatusDTO(x)).ToList();
            return CustomerSalesOrder_OrderPaymentStatusDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListOrganization), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_OrganizationDTO>>> SingleListOrganization([FromBody] CustomerSalesOrder_OrganizationFilterDTO CustomerSalesOrder_OrganizationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = int.MaxValue;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = CustomerSalesOrder_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = CustomerSalesOrder_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = CustomerSalesOrder_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = CustomerSalesOrder_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = CustomerSalesOrder_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = CustomerSalesOrder_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            OrganizationFilter.Phone = CustomerSalesOrder_OrganizationFilterDTO.Phone;
            OrganizationFilter.Email = CustomerSalesOrder_OrganizationFilterDTO.Email;
            OrganizationFilter.Address = CustomerSalesOrder_OrganizationFilterDTO.Address;

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<CustomerSalesOrder_OrganizationDTO> CustomerSalesOrder_OrganizationDTOs = Organizations
                .Select(x => new CustomerSalesOrder_OrganizationDTO(x)).ToList();
            return CustomerSalesOrder_OrganizationDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListRequestState), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_RequestStateDTO>>> SingleListRequestState([FromBody] CustomerSalesOrder_RequestStateFilterDTO CustomerSalesOrder_RequestStateFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RequestStateFilter RequestStateFilter = new RequestStateFilter();
            RequestStateFilter.Skip = 0;
            RequestStateFilter.Take = 20;
            RequestStateFilter.OrderBy = RequestStateOrder.Id;
            RequestStateFilter.OrderType = OrderType.ASC;
            RequestStateFilter.Selects = RequestStateSelect.ALL;
            RequestStateFilter.Id = CustomerSalesOrder_RequestStateFilterDTO.Id;
            RequestStateFilter.Code = CustomerSalesOrder_RequestStateFilterDTO.Code;
            RequestStateFilter.Name = CustomerSalesOrder_RequestStateFilterDTO.Name;

            List<RequestState> RequestStates = await RequestStateService.List(RequestStateFilter);
            List<CustomerSalesOrder_RequestStateDTO> CustomerSalesOrder_RequestStateDTOs = RequestStates
                .Select(x => new CustomerSalesOrder_RequestStateDTO(x)).ToList();
            return CustomerSalesOrder_RequestStateDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListItem), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_ItemDTO>>> SingleListItem([FromBody] CustomerSalesOrder_ItemFilterDTO CustomerSalesOrder_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.Id = CustomerSalesOrder_ItemFilterDTO.Id;
            ItemFilter.ProductId = CustomerSalesOrder_ItemFilterDTO.ProductId;
            ItemFilter.Code = CustomerSalesOrder_ItemFilterDTO.Code;
            ItemFilter.Name = CustomerSalesOrder_ItemFilterDTO.Name;
            ItemFilter.ScanCode = CustomerSalesOrder_ItemFilterDTO.ScanCode;
            ItemFilter.SalePrice = CustomerSalesOrder_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = CustomerSalesOrder_ItemFilterDTO.RetailPrice;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<CustomerSalesOrder_ItemDTO> CustomerSalesOrder_ItemDTOs = Items
                .Select(x => new CustomerSalesOrder_ItemDTO(x)).ToList();
            return CustomerSalesOrder_ItemDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListUnitOfMeasure), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_UnitOfMeasureDTO>>> SingleListUnitOfMeasure([FromBody] CustomerSalesOrder_UnitOfMeasureFilterDTO CustomerSalesOrder_UnitOfMeasureFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            List<Product> Products = await ProductService.List(new ProductFilter
            {
                Id = CustomerSalesOrder_UnitOfMeasureFilterDTO.ProductId,
                Selects = ProductSelect.Id,
            });
            long ProductId = Products.Select(p => p.Id).FirstOrDefault();
            Product Product = await ProductService.Get(ProductId);

            List<CustomerSalesOrder_UnitOfMeasureDTO> CustomerSalesOrder_UnitOfMeasureDTOs = new List<CustomerSalesOrder_UnitOfMeasureDTO>();
            if (Product.UnitOfMeasureGrouping != null)
            {
                CustomerSalesOrder_UnitOfMeasureDTOs = Product.UnitOfMeasureGrouping.UnitOfMeasureGroupingContents.Select(x => new CustomerSalesOrder_UnitOfMeasureDTO(x)).ToList();
            }
            CustomerSalesOrder_UnitOfMeasureDTO CustomerSalesOrder_UnitOfMeasureDTO = new CustomerSalesOrder_UnitOfMeasureDTO
            {
                Id = Product.UnitOfMeasure.Id,
                Code = Product.UnitOfMeasure.Code,
                Name = Product.UnitOfMeasure.Name,
                Description = Product.UnitOfMeasure.Description,
                StatusId = StatusEnum.ACTIVE.Id,
                Factor = 1,
            };
            CustomerSalesOrder_UnitOfMeasureDTOs.Add(CustomerSalesOrder_UnitOfMeasureDTO);
            CustomerSalesOrder_UnitOfMeasureDTOs = CustomerSalesOrder_UnitOfMeasureDTOs.Distinct().ToList();
            return CustomerSalesOrder_UnitOfMeasureDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListProductGrouping), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_ProductGroupingDTO>>> SingleListProductGrouping([FromBody] CustomerSalesOrder_ProductGroupingFilterDTO CustomerSalesOrder_ProductGroupingFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ProductGroupingFilter ProductGroupingFilter = new ProductGroupingFilter();
            ProductGroupingFilter.Skip = 0;
            ProductGroupingFilter.Take = int.MaxValue;
            ProductGroupingFilter.OrderBy = ProductGroupingOrder.Id;
            ProductGroupingFilter.OrderType = OrderType.ASC;
            ProductGroupingFilter.Selects = ProductGroupingSelect.Id | ProductGroupingSelect.Code
                | ProductGroupingSelect.Name | ProductGroupingSelect.Parent;

            if (ProductGroupingFilter.Id == null) ProductGroupingFilter.Id = new IdFilter();
            ProductGroupingFilter.Id.In = await FilterProductGrouping(ProductGroupingService, CurrentContext);
            List<ProductGrouping> ProductGroupings = await ProductGroupingService.List(ProductGroupingFilter);
            List<CustomerSalesOrder_ProductGroupingDTO> CustomerSalesOrder_ProductGroupingDTOs = ProductGroupings
                .Select(x => new CustomerSalesOrder_ProductGroupingDTO(x)).ToList();
            return CustomerSalesOrder_ProductGroupingDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListProductType), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_ProductTypeDTO>>> SingleListProductType([FromBody] CustomerSalesOrder_ProductTypeFilterDTO CustomerSalesOrder_ProductTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProductTypeFilter ProductTypeFilter = new ProductTypeFilter();
            ProductTypeFilter.Skip = 0;
            ProductTypeFilter.Take = 20;
            ProductTypeFilter.OrderBy = ProductTypeOrder.Id;
            ProductTypeFilter.OrderType = OrderType.ASC;
            ProductTypeFilter.Selects = ProductTypeSelect.ALL;
            ProductTypeFilter.Id = CustomerSalesOrder_ProductTypeFilterDTO.Id;
            ProductTypeFilter.Code = CustomerSalesOrder_ProductTypeFilterDTO.Code;
            ProductTypeFilter.Name = CustomerSalesOrder_ProductTypeFilterDTO.Name;
            ProductTypeFilter.Description = CustomerSalesOrder_ProductTypeFilterDTO.Description;
            ProductTypeFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            if (ProductTypeFilter.Id == null) ProductTypeFilter.Id = new IdFilter();
            ProductTypeFilter.Id.In = await FilterProductType(ProductTypeService, CurrentContext);

            List<ProductType> ProductTypes = await ProductTypeService.List(ProductTypeFilter);
            List<CustomerSalesOrder_ProductTypeDTO> CustomerSalesOrder_ProductTypeDTOs = ProductTypes
                .Select(x => new CustomerSalesOrder_ProductTypeDTO(x)).ToList();
            return CustomerSalesOrder_ProductTypeDTOs;
        }
        [Route(CustomerSalesOrderRoute.SingleListSupplier), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_SupplierDTO>>> SingleListSupplier([FromBody] CustomerSalesOrder_SupplierFilterDTO CustomerSalesOrder_SupplierFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SupplierFilter SupplierFilter = new SupplierFilter();
            SupplierFilter.Skip = 0;
            SupplierFilter.Take = 20;
            SupplierFilter.OrderBy = SupplierOrder.Id;
            SupplierFilter.OrderType = OrderType.ASC;
            SupplierFilter.Selects = SupplierSelect.ALL;
            SupplierFilter.Id = CustomerSalesOrder_SupplierFilterDTO.Id;
            SupplierFilter.Code = CustomerSalesOrder_SupplierFilterDTO.Code;
            SupplierFilter.Name = CustomerSalesOrder_SupplierFilterDTO.Name;
            SupplierFilter.TaxCode = CustomerSalesOrder_SupplierFilterDTO.TaxCode;
            SupplierFilter.Phone = CustomerSalesOrder_SupplierFilterDTO.Phone;
            SupplierFilter.Email = CustomerSalesOrder_SupplierFilterDTO.Email;
            SupplierFilter.Address = CustomerSalesOrder_SupplierFilterDTO.Address;
            SupplierFilter.ProvinceId = CustomerSalesOrder_SupplierFilterDTO.ProvinceId;
            SupplierFilter.DistrictId = CustomerSalesOrder_SupplierFilterDTO.DistrictId;
            SupplierFilter.WardId = CustomerSalesOrder_SupplierFilterDTO.WardId;
            SupplierFilter.OwnerName = CustomerSalesOrder_SupplierFilterDTO.OwnerName;
            SupplierFilter.PersonInChargeId = CustomerSalesOrder_SupplierFilterDTO.PersonInChargeId;
            SupplierFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            SupplierFilter.Description = CustomerSalesOrder_SupplierFilterDTO.Description;

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<CustomerSalesOrder_SupplierDTO> CustomerSalesOrder_SupplierDTOs = Suppliers
                .Select(x => new CustomerSalesOrder_SupplierDTO(x)).ToList();
            return CustomerSalesOrder_SupplierDTOs;
        }

        [Route(CustomerSalesOrderRoute.SingleListTaxType), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_TaxTypeDTO>>> SingleListTaxType([FromBody] CustomerSalesOrder_TaxTypeFilterDTO CustomerSalesOrder_TaxTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TaxTypeFilter TaxTypeFilter = new TaxTypeFilter();
            TaxTypeFilter.Skip = 0;
            TaxTypeFilter.Take = 20;
            TaxTypeFilter.OrderBy = TaxTypeOrder.Id;
            TaxTypeFilter.OrderType = OrderType.ASC;
            TaxTypeFilter.Selects = TaxTypeSelect.ALL;
            TaxTypeFilter.Id = CustomerSalesOrder_TaxTypeFilterDTO.Id;
            TaxTypeFilter.Code = CustomerSalesOrder_TaxTypeFilterDTO.Code;
            TaxTypeFilter.Name = CustomerSalesOrder_TaxTypeFilterDTO.Name;
            TaxTypeFilter.StatusId = new IdFilter { Equal = Enums.StatusEnum.ACTIVE.Id };

            List<TaxType> TaxTypes = await TaxTypeService.List(TaxTypeFilter);
            List<CustomerSalesOrder_TaxTypeDTO> CustomerSalesOrder_TaxTypeDTOs = TaxTypes
                .Select(x => new CustomerSalesOrder_TaxTypeDTO(x)).ToList();
            return CustomerSalesOrder_TaxTypeDTOs;
        }
        #region Item
        [Route(CustomerSalesOrderRoute.ListItem), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_ItemDTO>>> ListItem([FromBody] CustomerSalesOrder_ItemFilterDTO CustomerSalesOrder_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = CustomerSalesOrder_ItemFilterDTO.Skip;
            ItemFilter.Take = CustomerSalesOrder_ItemFilterDTO.Take;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.Id = CustomerSalesOrder_ItemFilterDTO.Id;
            ItemFilter.Code = CustomerSalesOrder_ItemFilterDTO.Code;
            ItemFilter.Name = CustomerSalesOrder_ItemFilterDTO.Name;
            ItemFilter.OtherName = CustomerSalesOrder_ItemFilterDTO.OtherName;
            ItemFilter.ProductGroupingId = CustomerSalesOrder_ItemFilterDTO.ProductGroupingId;
            ItemFilter.ProductId = CustomerSalesOrder_ItemFilterDTO.ProductId;
            ItemFilter.ProductTypeId = CustomerSalesOrder_ItemFilterDTO.ProductTypeId;
            ItemFilter.RetailPrice = CustomerSalesOrder_ItemFilterDTO.RetailPrice;
            ItemFilter.SalePrice = CustomerSalesOrder_ItemFilterDTO.SalePrice;
            ItemFilter.ScanCode = CustomerSalesOrder_ItemFilterDTO.ScanCode;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            ItemFilter = ItemService.ToFilter(ItemFilter);

            List<Item> Items = await ContractService.ListItem(ItemFilter);
            List<CustomerSalesOrder_ItemDTO> CustomerSalesOrder_ItemDTOs = Items
                .Select(x => new CustomerSalesOrder_ItemDTO(x)).ToList();
            return CustomerSalesOrder_ItemDTOs;
        }
        [Route(CustomerSalesOrderRoute.CountItem), HttpPost]
        public async Task<ActionResult<long>> CountItem([FromBody] CustomerSalesOrder_ItemFilterDTO CustomerSalesOrder_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Id = CustomerSalesOrder_ItemFilterDTO.Id;
            ItemFilter.Code = CustomerSalesOrder_ItemFilterDTO.Code;
            ItemFilter.Name = CustomerSalesOrder_ItemFilterDTO.Name;
            ItemFilter.OtherName = CustomerSalesOrder_ItemFilterDTO.OtherName;
            ItemFilter.ProductGroupingId = CustomerSalesOrder_ItemFilterDTO.ProductGroupingId;
            ItemFilter.ProductId = CustomerSalesOrder_ItemFilterDTO.ProductId;
            ItemFilter.ProductTypeId = CustomerSalesOrder_ItemFilterDTO.ProductTypeId;
            ItemFilter.RetailPrice = CustomerSalesOrder_ItemFilterDTO.RetailPrice;
            ItemFilter.SalePrice = CustomerSalesOrder_ItemFilterDTO.SalePrice;
            ItemFilter.ScanCode = CustomerSalesOrder_ItemFilterDTO.ScanCode;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            ItemFilter = ItemService.ToFilter(ItemFilter);

            return await ItemService.Count(ItemFilter);
        }
        #endregion
    }
}

