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
using CRM.Services.MContract;
using CRM.Services.MCompany;
using CRM.Services.MAppUser;
using CRM.Services.MContact;
using CRM.Services.MContractStatus;
using CRM.Services.MContractType;
using CRM.Services.MCurrency;
using CRM.Services.MCustomer;
using CRM.Services.MDistrict;
using CRM.Services.MNation;
using CRM.Services.MProvince;
using CRM.Services.MOpportunity;
using CRM.Services.MOrganization;
using CRM.Services.MPaymentStatus;
using CRM.Enums;

namespace CRM.Rpc.contract
{
    public partial class ContractController : RpcController
    {
        [Route(ContractRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<Contract_AppUserDTO>>> FilterListAppUser([FromBody] Contract_AppUserFilterDTO Contract_AppUserFilterDTO)
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
            AppUserFilter.Id = Contract_AppUserFilterDTO.Id;
            AppUserFilter.Username = Contract_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Contract_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = Contract_AppUserFilterDTO.Address;
            AppUserFilter.Email = Contract_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Contract_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = Contract_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = Contract_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = Contract_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = Contract_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = Contract_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = Contract_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = Contract_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = Contract_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = Contract_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = Contract_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Contract_AppUserDTO> Contract_AppUserDTOs = AppUsers
                .Select(x => new Contract_AppUserDTO(x)).ToList();
            return Contract_AppUserDTOs;
        }
        [Route(ContractRoute.FilterListCustomer), HttpPost]
        public async Task<ActionResult<List<Contract_CustomerDTO>>> FilterListCustomer([FromBody] Contract_CustomerFilterDTO Contract_CustomerFilterDTO)
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
            CustomerFilter.Id = Contract_CustomerFilterDTO.Id;
            CustomerFilter.Code = Contract_CustomerFilterDTO.Code;

            CustomerFilter.StatusId = Contract_CustomerFilterDTO.StatusId;

            CustomerFilter.Phone = Contract_CustomerFilterDTO.Phone;
            CustomerFilter.Address = Contract_CustomerFilterDTO.Address;
            CustomerFilter.Name = Contract_CustomerFilterDTO.Name;
            CustomerFilter.Email = Contract_CustomerFilterDTO.Email;
            CustomerFilter.CustomerTypeId = Contract_CustomerFilterDTO.CustomerTypeId;

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<Contract_CustomerDTO> Contract_CustomerDTOs = Customers
                .Select(x => new Contract_CustomerDTO(x)).ToList();
            return Contract_CustomerDTOs;
        }
        [Route(ContractRoute.FilterListContractStatus), HttpPost]
        public async Task<ActionResult<List<Contract_ContractStatusDTO>>> FilterListContractStatus([FromBody] Contract_ContractStatusFilterDTO Contract_ContractStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractStatusFilter ContractStatusFilter = new ContractStatusFilter();
            ContractStatusFilter.Skip = 0;
            ContractStatusFilter.Take = 20;
            ContractStatusFilter.OrderBy = ContractStatusOrder.Id;
            ContractStatusFilter.OrderType = OrderType.ASC;
            ContractStatusFilter.Selects = ContractStatusSelect.ALL;
            ContractStatusFilter.Id = Contract_ContractStatusFilterDTO.Id;
            ContractStatusFilter.Name = Contract_ContractStatusFilterDTO.Name;
            ContractStatusFilter.Code = Contract_ContractStatusFilterDTO.Code;

            List<ContractStatus> ContractStatuses = await ContractStatusService.List(ContractStatusFilter);
            List<Contract_ContractStatusDTO> Contract_ContractStatusDTOs = ContractStatuses
                .Select(x => new Contract_ContractStatusDTO(x)).ToList();
            return Contract_ContractStatusDTOs;
        }
        [Route(ContractRoute.FilterListContractType), HttpPost]
        public async Task<ActionResult<List<Contract_ContractTypeDTO>>> FilterListContractType([FromBody] Contract_ContractTypeFilterDTO Contract_ContractTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractTypeFilter ContractTypeFilter = new ContractTypeFilter();
            ContractTypeFilter.Skip = 0;
            ContractTypeFilter.Take = 20;
            ContractTypeFilter.OrderBy = ContractTypeOrder.Id;
            ContractTypeFilter.OrderType = OrderType.ASC;
            ContractTypeFilter.Selects = ContractTypeSelect.ALL;
            ContractTypeFilter.Id = Contract_ContractTypeFilterDTO.Id;
            ContractTypeFilter.Name = Contract_ContractTypeFilterDTO.Name;
            ContractTypeFilter.Code = Contract_ContractTypeFilterDTO.Code;

            List<ContractType> ContractTypes = await ContractTypeService.List(ContractTypeFilter);
            List<Contract_ContractTypeDTO> Contract_ContractTypeDTOs = ContractTypes
                .Select(x => new Contract_ContractTypeDTO(x)).ToList();
            return Contract_ContractTypeDTOs;
        }
        [Route(ContractRoute.FilterListCurrency), HttpPost]
        public async Task<ActionResult<List<Contract_CurrencyDTO>>> FilterListCurrency([FromBody] Contract_CurrencyFilterDTO Contract_CurrencyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CurrencyFilter CurrencyFilter = new CurrencyFilter();
            CurrencyFilter.Skip = 0;
            CurrencyFilter.Take = 20;
            CurrencyFilter.OrderBy = CurrencyOrder.Id;
            CurrencyFilter.OrderType = OrderType.ASC;
            CurrencyFilter.Selects = CurrencySelect.ALL;
            CurrencyFilter.Id = Contract_CurrencyFilterDTO.Id;
            CurrencyFilter.Code = Contract_CurrencyFilterDTO.Code;
            CurrencyFilter.Name = Contract_CurrencyFilterDTO.Name; 

            List<Currency> Currencies = await CurrencyService.List(CurrencyFilter);
            List<Contract_CurrencyDTO> Contract_CurrencyDTOs = Currencies
                .Select(x => new Contract_CurrencyDTO(x)).ToList();
            return Contract_CurrencyDTOs;
        }
        [Route(ContractRoute.FilterListPaymentStatus), HttpPost]
        public async Task<ActionResult<List<Contract_PaymentStatusDTO>>> FilterListPaymentStatus([FromBody] Contract_PaymentStatusFilterDTO Contract_PaymentStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PaymentStatusFilter PaymentStatusFilter = new PaymentStatusFilter();
            PaymentStatusFilter.Skip = 0;
            PaymentStatusFilter.Take = 20;
            PaymentStatusFilter.OrderBy = PaymentStatusOrder.Id;
            PaymentStatusFilter.OrderType = OrderType.ASC;
            PaymentStatusFilter.Selects = PaymentStatusSelect.ALL;
            PaymentStatusFilter.Id = Contract_PaymentStatusFilterDTO.Id;
            PaymentStatusFilter.Name = Contract_PaymentStatusFilterDTO.Name;
            PaymentStatusFilter.Code = Contract_PaymentStatusFilterDTO.Code;

            List<PaymentStatus> PaymentStatuses = await PaymentStatusService.List(PaymentStatusFilter);
            List<Contract_PaymentStatusDTO> Contract_PaymentStatusDTOs = PaymentStatuses
                .Select(x => new Contract_PaymentStatusDTO(x)).ToList();
            return Contract_PaymentStatusDTOs;
        }
       
        [Route(ContractRoute.SingleListCompany), HttpPost]
        public async Task<ActionResult<List<Contract_CompanyDTO>>> SingleListCompany([FromBody] Contract_CompanyFilterDTO Contract_CompanyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyFilter CompanyFilter = new CompanyFilter();
            CompanyFilter.Skip = 0;
            CompanyFilter.Take = int.MaxValue;
            CompanyFilter.OrderBy = CompanyOrder.Id;
            CompanyFilter.OrderType = OrderType.ASC;
            CompanyFilter.Selects = CompanySelect.ALL;
            CompanyFilter.Id = Contract_CompanyFilterDTO.Id;
            CompanyFilter.Name = Contract_CompanyFilterDTO.Name;
            CompanyFilter.Phone = Contract_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = Contract_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = Contract_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = Contract_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = Contract_CompanyFilterDTO.EmailOther;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<Contract_CompanyDTO> Contract_CompanyDTOs = Companys
                .Select(x => new Contract_CompanyDTO(x)).ToList();
            return Contract_CompanyDTOs;
        }
        [Route(ContractRoute.SingleListAppUser), HttpPost]
        public async Task<ActionResult<List<Contract_AppUserDTO>>> SingleListAppUser([FromBody] Contract_AppUserFilterDTO Contract_AppUserFilterDTO)
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
            AppUserFilter.Id = Contract_AppUserFilterDTO.Id;
            AppUserFilter.Username = Contract_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Contract_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = Contract_AppUserFilterDTO.Address;
            AppUserFilter.Email = Contract_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Contract_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = Contract_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = Contract_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = Contract_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = Contract_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = Contract_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = Contract_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = Contract_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = Contract_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = Contract_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Contract_AppUserDTO> Contract_AppUserDTOs = AppUsers
                .Select(x => new Contract_AppUserDTO(x)).ToList();
            return Contract_AppUserDTOs;
        }
        [Route(ContractRoute.SingleListContact), HttpPost]
        public async Task<ActionResult<List<Contract_ContactDTO>>> SingleListContact([FromBody] Contract_ContactFilterDTO Contract_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = new ContactFilter();
            ContactFilter.Skip = 0;
            ContactFilter.Take = 20;
            ContactFilter.OrderBy = ContactOrder.Id;
            ContactFilter.OrderType = OrderType.ASC;
            ContactFilter.Selects = ContactSelect.ALL;
            ContactFilter.Id = Contract_ContactFilterDTO.Id;
            ContactFilter.Name = Contract_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = Contract_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = Contract_ContactFilterDTO.CompanyId;

            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<Contract_ContactDTO> Contract_ContactDTOs = Contacts
                .Select(x => new Contract_ContactDTO(x)).ToList();
            return Contract_ContactDTOs;
        }
        [Route(ContractRoute.SingleListContractStatus), HttpPost]
        public async Task<ActionResult<List<Contract_ContractStatusDTO>>> SingleListContractStatus([FromBody] Contract_ContractStatusFilterDTO Contract_ContractStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractStatusFilter ContractStatusFilter = new ContractStatusFilter();
            ContractStatusFilter.Skip = 0;
            ContractStatusFilter.Take = 20;
            ContractStatusFilter.OrderBy = ContractStatusOrder.Id;
            ContractStatusFilter.OrderType = OrderType.ASC;
            ContractStatusFilter.Selects = ContractStatusSelect.ALL;
            ContractStatusFilter.Id = Contract_ContractStatusFilterDTO.Id;
            ContractStatusFilter.Name = Contract_ContractStatusFilterDTO.Name;
            ContractStatusFilter.Code = Contract_ContractStatusFilterDTO.Code;

            List<ContractStatus> ContractStatuses = await ContractStatusService.List(ContractStatusFilter);
            List<Contract_ContractStatusDTO> Contract_ContractStatusDTOs = ContractStatuses
                .Select(x => new Contract_ContractStatusDTO(x)).ToList();
            return Contract_ContractStatusDTOs;
        }
        [Route(ContractRoute.SingleListContractType), HttpPost]
        public async Task<ActionResult<List<Contract_ContractTypeDTO>>> SingleListContractType([FromBody] Contract_ContractTypeFilterDTO Contract_ContractTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractTypeFilter ContractTypeFilter = new ContractTypeFilter();
            ContractTypeFilter.Skip = 0;
            ContractTypeFilter.Take = 20;
            ContractTypeFilter.OrderBy = ContractTypeOrder.Id;
            ContractTypeFilter.OrderType = OrderType.ASC;
            ContractTypeFilter.Selects = ContractTypeSelect.ALL;
            ContractTypeFilter.Id = Contract_ContractTypeFilterDTO.Id;
            ContractTypeFilter.Name = Contract_ContractTypeFilterDTO.Name;
            ContractTypeFilter.Code = Contract_ContractTypeFilterDTO.Code;

            List<ContractType> ContractTypes = await ContractTypeService.List(ContractTypeFilter);
            List<Contract_ContractTypeDTO> Contract_ContractTypeDTOs = ContractTypes
                .Select(x => new Contract_ContractTypeDTO(x)).ToList();
            return Contract_ContractTypeDTOs;
        }
        [Route(ContractRoute.SingleListCurrency), HttpPost]
        public async Task<ActionResult<List<Contract_CurrencyDTO>>> SingleListCurrency([FromBody] Contract_CurrencyFilterDTO Contract_CurrencyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CurrencyFilter CurrencyFilter = new CurrencyFilter();
            CurrencyFilter.Skip = 0;
            CurrencyFilter.Take = 20;
            CurrencyFilter.OrderBy = CurrencyOrder.Id;
            CurrencyFilter.OrderType = OrderType.ASC;
            CurrencyFilter.Selects = CurrencySelect.ALL;
            CurrencyFilter.Id = Contract_CurrencyFilterDTO.Id;
            CurrencyFilter.Code = Contract_CurrencyFilterDTO.Code;
            CurrencyFilter.Name = Contract_CurrencyFilterDTO.Name;

            List<Currency> Currencies = await CurrencyService.List(CurrencyFilter);
            List<Contract_CurrencyDTO> Contract_CurrencyDTOs = Currencies
                .Select(x => new Contract_CurrencyDTO(x)).ToList();
            return Contract_CurrencyDTOs;
        }
        [Route(ContractRoute.SingleListCustomer), HttpPost]
        public async Task<ActionResult<List<Contract_CustomerDTO>>> SingleListCustomer([FromBody] Contract_CustomerFilterDTO Contract_CustomerFilterDTO)
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
            CustomerFilter.Id = Contract_CustomerFilterDTO.Id;
            CustomerFilter.Code = Contract_CustomerFilterDTO.Code;

            CustomerFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            CustomerFilter.Phone = Contract_CustomerFilterDTO.Phone;
            CustomerFilter.Address = Contract_CustomerFilterDTO.Address;
            CustomerFilter.Name = Contract_CustomerFilterDTO.Name;
            CustomerFilter.Email = Contract_CustomerFilterDTO.Email;
            CustomerFilter.CustomerTypeId = Contract_CustomerFilterDTO.CustomerTypeId;

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<Contract_CustomerDTO> Contract_CustomerDTOs = Customers
                .Select(x => new Contract_CustomerDTO(x)).ToList();
            return Contract_CustomerDTOs;
        }
        [Route(ContractRoute.SingleListDistrict), HttpPost]
        public async Task<ActionResult<List<Contract_DistrictDTO>>> SingleListDistrict([FromBody] Contract_DistrictFilterDTO Contract_DistrictFilterDTO)
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
            DistrictFilter.Id = Contract_DistrictFilterDTO.Id;
            DistrictFilter.Code = Contract_DistrictFilterDTO.Code;
            DistrictFilter.Name = Contract_DistrictFilterDTO.Name;
            DistrictFilter.Priority = Contract_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = Contract_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<Contract_DistrictDTO> Contract_DistrictDTOs = Districts
                .Select(x => new Contract_DistrictDTO(x)).ToList();
            return Contract_DistrictDTOs;
        }
        [Route(ContractRoute.SingleListNation), HttpPost]
        public async Task<ActionResult<List<Contract_NationDTO>>> SingleListNation([FromBody] Contract_NationFilterDTO Contract_NationFilterDTO)
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
            NationFilter.Id = Contract_NationFilterDTO.Id;
            NationFilter.Code = Contract_NationFilterDTO.Code;
            NationFilter.Name = Contract_NationFilterDTO.Name;
            NationFilter.Priority = Contract_NationFilterDTO.DisplayOrder;
            NationFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Nation> Nations = await NationService.List(NationFilter);
            List<Contract_NationDTO> Contract_NationDTOs = Nations
                .Select(x => new Contract_NationDTO(x)).ToList();
            return Contract_NationDTOs;
        }
        [Route(ContractRoute.SingleListProvince), HttpPost]
        public async Task<ActionResult<List<Contract_ProvinceDTO>>> SingleListProvince([FromBody] Contract_ProvinceFilterDTO Contract_ProvinceFilterDTO)
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
            ProvinceFilter.Id = Contract_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = Contract_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = Contract_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = Contract_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<Contract_ProvinceDTO> Contract_ProvinceDTOs = Provinces
                .Select(x => new Contract_ProvinceDTO(x)).ToList();
            return Contract_ProvinceDTOs;
        }
        [Route(ContractRoute.SingleListOpportunity), HttpPost]
        public async Task<ActionResult<List<Contract_OpportunityDTO>>> SingleListOpportunity([FromBody] Contract_OpportunityFilterDTO Contract_OpportunityFilterDTO)
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
            OpportunityFilter.Id = Contract_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = Contract_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = Contract_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.CustomerLeadId = Contract_OpportunityFilterDTO.CustomerLeadId;
            OpportunityFilter.ClosingDate = Contract_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = Contract_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = Contract_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = Contract_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = Contract_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.AppUserId = Contract_OpportunityFilterDTO.AppUserId;
            OpportunityFilter.Amount = Contract_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = Contract_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = Contract_OpportunityFilterDTO.Description;
            OpportunityFilter.CreatorId = Contract_OpportunityFilterDTO.CreatorId;

            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<Contract_OpportunityDTO> Contract_OpportunityDTOs = Opportunities
                .Select(x => new Contract_OpportunityDTO(x)).ToList();
            return Contract_OpportunityDTOs;
        }
        [Route(ContractRoute.SingleListOrganization), HttpPost]
        public async Task<ActionResult<List<Contract_OrganizationDTO>>> SingleListOrganization([FromBody] Contract_OrganizationFilterDTO Contract_OrganizationFilterDTO)
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
            OrganizationFilter.Id = Contract_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = Contract_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = Contract_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = Contract_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = Contract_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = Contract_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            OrganizationFilter.Phone = Contract_OrganizationFilterDTO.Phone;
            OrganizationFilter.Email = Contract_OrganizationFilterDTO.Email;
            OrganizationFilter.Address = Contract_OrganizationFilterDTO.Address;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<Contract_OrganizationDTO> Contract_OrganizationDTOs = Organizations
                .Select(x => new Contract_OrganizationDTO(x)).ToList();
            return Contract_OrganizationDTOs;
        }
        [Route(ContractRoute.SingleListPaymentStatus), HttpPost]
        public async Task<ActionResult<List<Contract_PaymentStatusDTO>>> SingleListPaymentStatus([FromBody] Contract_PaymentStatusFilterDTO Contract_PaymentStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PaymentStatusFilter PaymentStatusFilter = new PaymentStatusFilter();
            PaymentStatusFilter.Skip = 0;
            PaymentStatusFilter.Take = 20;
            PaymentStatusFilter.OrderBy = PaymentStatusOrder.Id;
            PaymentStatusFilter.OrderType = OrderType.ASC;
            PaymentStatusFilter.Selects = PaymentStatusSelect.ALL;
            PaymentStatusFilter.Id = Contract_PaymentStatusFilterDTO.Id;
            PaymentStatusFilter.Name = Contract_PaymentStatusFilterDTO.Name;
            PaymentStatusFilter.Code = Contract_PaymentStatusFilterDTO.Code;

            List<PaymentStatus> PaymentStatuses = await PaymentStatusService.List(PaymentStatusFilter);
            List<Contract_PaymentStatusDTO> Contract_PaymentStatusDTOs = PaymentStatuses
                .Select(x => new Contract_PaymentStatusDTO(x)).ToList();
            return Contract_PaymentStatusDTOs;
        }
        [Route(ContractRoute.SingleListProduct), HttpPost]
        public async Task<ActionResult<List<Contract_ProductDTO>>> SingleListProduct([FromBody] Contract_ProductFilterDTO Contract_ProductFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProductFilter ProductFilter = new ProductFilter();
            ProductFilter.Skip = 0;
            ProductFilter.Take = 20;
            ProductFilter.OrderBy = ProductOrder.Id;
            ProductFilter.OrderType = OrderType.ASC;
            ProductFilter.Selects = ProductSelect.ALL;
            ProductFilter.Id = Contract_ProductFilterDTO.Id;
            ProductFilter.Name = Contract_ProductFilterDTO.Name;
            ProductFilter.Code = Contract_ProductFilterDTO.Code;
            ProductFilter.SupplierId = Contract_ProductFilterDTO.SupplierId;
            ProductFilter.ProductTypeId = Contract_ProductFilterDTO.ProductTypeId;

            List<Product> Products = await ProductService.List(ProductFilter);
            List<Contract_ProductDTO> Contract_ProductDTOs = Products
                .Select(x => new Contract_ProductDTO(x)).ToList();
            return Contract_ProductDTOs;
        }
        [Route(ContractRoute.SingleListProductGrouping), HttpPost]
        public async Task<ActionResult<List<Contract_ProductGroupingDTO>>> SingleListProductGrouping([FromBody] Contract_ProductGroupingFilterDTO Contract_ProductGroupingFilterDTO)
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
            List<Contract_ProductGroupingDTO> Contract_ProductGroupingDTOs = ProductGroupings
                .Select(x => new Contract_ProductGroupingDTO(x)).ToList();
            return Contract_ProductGroupingDTOs;
        }
        [Route(ContractRoute.SingleListProductType), HttpPost]
        public async Task<ActionResult<List<Contract_ProductTypeDTO>>> SingleListProductType([FromBody] Contract_ProductTypeFilterDTO Contract_ProductTypeFilterDTO)
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
            ProductTypeFilter.Id = Contract_ProductTypeFilterDTO.Id;
            ProductTypeFilter.Code = Contract_ProductTypeFilterDTO.Code;
            ProductTypeFilter.Name = Contract_ProductTypeFilterDTO.Name;
            ProductTypeFilter.Description = Contract_ProductTypeFilterDTO.Description;
            ProductTypeFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            if (ProductTypeFilter.Id == null) ProductTypeFilter.Id = new IdFilter();
            ProductTypeFilter.Id.In = await FilterProductType(ProductTypeService, CurrentContext);

            List<ProductType> ProductTypes = await ProductTypeService.List(ProductTypeFilter);
            List<Contract_ProductTypeDTO> Contract_ProductTypeDTOs = ProductTypes
                .Select(x => new Contract_ProductTypeDTO(x)).ToList();
            return Contract_ProductTypeDTOs;
        }
        [Route(ContractRoute.SingleListSupplier), HttpPost]
        public async Task<ActionResult<List<Contract_SupplierDTO>>> SingleListSupplier([FromBody] Contract_SupplierFilterDTO Contract_SupplierFilterDTO)
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
            SupplierFilter.Id = Contract_SupplierFilterDTO.Id;
            SupplierFilter.Code = Contract_SupplierFilterDTO.Code;
            SupplierFilter.Name = Contract_SupplierFilterDTO.Name;
            SupplierFilter.TaxCode = Contract_SupplierFilterDTO.TaxCode;
            SupplierFilter.Phone = Contract_SupplierFilterDTO.Phone;
            SupplierFilter.Email = Contract_SupplierFilterDTO.Email;
            SupplierFilter.Address = Contract_SupplierFilterDTO.Address;
            SupplierFilter.ProvinceId = Contract_SupplierFilterDTO.ProvinceId;
            SupplierFilter.DistrictId = Contract_SupplierFilterDTO.DistrictId;
            SupplierFilter.WardId = Contract_SupplierFilterDTO.WardId;
            SupplierFilter.OwnerName = Contract_SupplierFilterDTO.OwnerName;
            SupplierFilter.PersonInChargeId = Contract_SupplierFilterDTO.PersonInChargeId;
            SupplierFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            SupplierFilter.Description = Contract_SupplierFilterDTO.Description;

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<Contract_SupplierDTO> Contract_SupplierDTOs = Suppliers
                .Select(x => new Contract_SupplierDTO(x)).ToList();
            return Contract_SupplierDTOs;
        }
        [Route(ContractRoute.SingleListUnitOfMeasure), HttpPost]
        public async Task<ActionResult<List<Contract_UnitOfMeasureDTO>>> SingleListUnitOfMeasure([FromBody] Contract_UnitOfMeasureFilterDTO Contract_UnitOfMeasureFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            //TODO cần optimize lại phần này, sử dụng itemId thay vì productId

            List<Product> Products = await ProductService.List(new ProductFilter
            {
                Id = Contract_UnitOfMeasureFilterDTO.ProductId,
                Selects = ProductSelect.Id,
            });
            long ProductId = Products.Select(p => p.Id).FirstOrDefault();
            Product Product = await ProductService.Get(ProductId);

            List<Contract_UnitOfMeasureDTO> Contract_UnitOfMeasureDTOs = new List<Contract_UnitOfMeasureDTO>();
            if (Product.UnitOfMeasureGrouping != null)
            {
                Contract_UnitOfMeasureDTOs = Product.UnitOfMeasureGrouping.UnitOfMeasureGroupingContents.Select(x => new Contract_UnitOfMeasureDTO(x)).ToList();
            }
            Contract_UnitOfMeasureDTO Contract_UnitOfMeasureDTO = new Contract_UnitOfMeasureDTO
            {
                Id = Product.UnitOfMeasure.Id,
                Code = Product.UnitOfMeasure.Code,
                Name = Product.UnitOfMeasure.Name,
                Description = Product.UnitOfMeasure.Description,
                StatusId = StatusEnum.ACTIVE.Id,
                Factor = 1,
            };
            Contract_UnitOfMeasureDTOs.Add(Contract_UnitOfMeasureDTO);
            Contract_UnitOfMeasureDTOs = Contract_UnitOfMeasureDTOs.Distinct().ToList();
            return Contract_UnitOfMeasureDTOs;
        }
        [Route(ContractRoute.SingleListTaxType), HttpPost]
        public async Task<ActionResult<List<Contract_TaxTypeDTO>>> SingleListTaxType([FromBody] Contract_TaxTypeFilterDTO Contract_TaxTypeFilterDTO)
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
            TaxTypeFilter.Id = Contract_TaxTypeFilterDTO.Id;
            TaxTypeFilter.Code = Contract_TaxTypeFilterDTO.Code;
            TaxTypeFilter.Name = Contract_TaxTypeFilterDTO.Name;
            TaxTypeFilter.StatusId = new IdFilter { Equal = Enums.StatusEnum.ACTIVE.Id };

            List<TaxType> TaxTypes = await TaxTypeService.List(TaxTypeFilter);
            List<Contract_TaxTypeDTO> Contract_TaxTypeDTOs = TaxTypes
                .Select(x => new Contract_TaxTypeDTO(x)).ToList();
            return Contract_TaxTypeDTOs;
        }

        [Route(ContractRoute.SingleListFileType), HttpPost]
        public async Task<ActionResult<List<Contract_FileTypeDTO>>> SingleListFileType([FromBody] Contract_FileTypeFilterDTO Contract_FileTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            FileTypeFilter FileTypeFilter = new FileTypeFilter();
            FileTypeFilter.Skip = 0;
            FileTypeFilter.Take = 20;
            FileTypeFilter.OrderBy = FileTypeOrder.Id;
            FileTypeFilter.OrderType = OrderType.ASC;
            FileTypeFilter.Selects = FileTypeSelect.ALL;
            FileTypeFilter.Id = Contract_FileTypeFilterDTO.Id;
            FileTypeFilter.Code = Contract_FileTypeFilterDTO.Code;
            FileTypeFilter.Name = Contract_FileTypeFilterDTO.Name;

            List<FileType> FileTypes = await FileTypeService.List(FileTypeFilter);
            List<Contract_FileTypeDTO> Contract_FileTypeDTOs = FileTypes
                .Select(x => new Contract_FileTypeDTO(x)).ToList();
            return Contract_FileTypeDTOs;
        }
    }
}