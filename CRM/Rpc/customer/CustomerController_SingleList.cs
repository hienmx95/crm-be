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
using CRM.Services.MCustomer;
using CRM.Services.MBusinessType;
using CRM.Services.MCompany;
using CRM.Services.MAppUser;
using CRM.Services.MCustomerResource;
using CRM.Services.MCustomerType;
using CRM.Services.MDistrict;
using CRM.Services.MNation;
using CRM.Services.MProfession;
using CRM.Services.MProvince;
using CRM.Services.MSex;
using CRM.Services.MStatus;
using CRM.Services.MWard;
using CRM.Services.MCustomerEmail;
using CRM.Services.MEmailType;
using CRM.Services.MCustomerFeedback;
using CRM.Services.MCustomerFeedbackType;
using CRM.Services.MCustomerPhone;
using CRM.Services.MPhoneType;
using CRM.Services.MCustomerPointHistory;
using CRM.Services.MRepairTicket;
using CRM.Services.MStore;
using CRM.Enums;

namespace CRM.Rpc.customer
{
    public partial class CustomerController : RpcController
    {
        [Route(CustomerRoute.FilterListBusinessType), HttpPost]
        public async Task<ActionResult<List<Customer_BusinessTypeDTO>>> FilterListBusinessType([FromBody] Customer_BusinessTypeFilterDTO Customer_BusinessTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            BusinessTypeFilter BusinessTypeFilter = new BusinessTypeFilter();
            BusinessTypeFilter.Skip = 0;
            BusinessTypeFilter.Take = int.MaxValue;
            BusinessTypeFilter.Take = 20;
            BusinessTypeFilter.OrderBy = BusinessTypeOrder.Id;
            BusinessTypeFilter.OrderType = OrderType.ASC;
            BusinessTypeFilter.Selects = BusinessTypeSelect.ALL;

            List<BusinessType> BusinessTypes = await BusinessTypeService.List(BusinessTypeFilter);
            List<Customer_BusinessTypeDTO> Customer_BusinessTypeDTOs = BusinessTypes
                .Select(x => new Customer_BusinessTypeDTO(x)).ToList();
            return Customer_BusinessTypeDTOs;
        }
        [Route(CustomerRoute.FilterListCallType), HttpPost]
        public async Task<ActionResult<List<Customer_CallTypeDTO>>> FilterListCallType([FromBody] Customer_CallTypeFilterDTO Customer_CallTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallTypeFilter CallTypeFilter = new CallTypeFilter();
            CallTypeFilter.Skip = 0;
            CallTypeFilter.Take = 20;
            CallTypeFilter.OrderBy = CallTypeOrder.Id;
            CallTypeFilter.OrderType = OrderType.ASC;
            CallTypeFilter.Selects = CallTypeSelect.ALL;
            CallTypeFilter.Id = Customer_CallTypeFilterDTO.Id;
            CallTypeFilter.Code = Customer_CallTypeFilterDTO.Code;
            CallTypeFilter.Name = Customer_CallTypeFilterDTO.Name;
            CallTypeFilter.ColorCode = Customer_CallTypeFilterDTO.ColorCode;
            CallTypeFilter.StatusId = Customer_CallTypeFilterDTO.StatusId;

            List<CallType> CallTypes = await CallTypeService.List(CallTypeFilter);
            List<Customer_CallTypeDTO> Customer_CallTypeDTOs = CallTypes
                .Select(x => new Customer_CallTypeDTO(x)).ToList();
            return Customer_CallTypeDTOs;
        }
        [Route(CustomerRoute.FilterListCallCategory), HttpPost]
        public async Task<ActionResult<List<Customer_CallCategoryDTO>>> FilterListCallCategory([FromBody] Customer_CallCategoryFilterDTO Customer_CallCategoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallCategoryFilter CallCategoryFilter = new CallCategoryFilter();
            CallCategoryFilter.Skip = 0;
            CallCategoryFilter.Take = int.MaxValue;
            CallCategoryFilter.Take = 20;
            CallCategoryFilter.OrderBy = CallCategoryOrder.Id;
            CallCategoryFilter.OrderType = OrderType.ASC;
            CallCategoryFilter.Selects = CallCategorySelect.ALL;

            List<CallCategory> CallCategories = await CallCategoryService.List(CallCategoryFilter);
            List<Customer_CallCategoryDTO> Customer_CallCategoryDTOs = CallCategories
                .Select(x => new Customer_CallCategoryDTO(x)).ToList();
            return Customer_CallCategoryDTOs;
        }
        [Route(CustomerRoute.FilterListCallStatus), HttpPost]
        public async Task<ActionResult<List<Customer_CallStatusDTO>>> FilterListCallStatus([FromBody] Customer_CallStatusFilterDTO Customer_CallStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallStatusFilter CallStatusFilter = new CallStatusFilter();
            CallStatusFilter.Skip = 0;
            CallStatusFilter.Take = int.MaxValue;
            CallStatusFilter.Take = 20;
            CallStatusFilter.OrderBy = CallStatusOrder.Id;
            CallStatusFilter.OrderType = OrderType.ASC;
            CallStatusFilter.Selects = CallStatusSelect.ALL;

            List<CallStatus> CallStatuses = await CallStatusService.List(CallStatusFilter);
            List<Customer_CallStatusDTO> Customer_CallStatusDTOs = CallStatuses
                .Select(x => new Customer_CallStatusDTO(x)).ToList();
            return Customer_CallStatusDTOs;
        }
        [Route(CustomerRoute.FilterListCompany), HttpPost]
        public async Task<ActionResult<List<Customer_CompanyDTO>>> FilterListCompany([FromBody] Customer_CompanyFilterDTO Customer_CompanyFilterDTO)
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
            CompanyFilter.Id = Customer_CompanyFilterDTO.Id;
            CompanyFilter.Name = Customer_CompanyFilterDTO.Name;
            CompanyFilter.Phone = Customer_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = Customer_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = Customer_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = Customer_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = Customer_CompanyFilterDTO.EmailOther;
            CompanyFilter.ZIPCode = Customer_CompanyFilterDTO.ZIPCode;
            CompanyFilter.Revenue = Customer_CompanyFilterDTO.Revenue;
            CompanyFilter.Website = Customer_CompanyFilterDTO.Website;
            CompanyFilter.Address = Customer_CompanyFilterDTO.Address;
            CompanyFilter.NationId = Customer_CompanyFilterDTO.NationId;
            CompanyFilter.ProvinceId = Customer_CompanyFilterDTO.ProvinceId;
            CompanyFilter.DistrictId = Customer_CompanyFilterDTO.DistrictId;
            CompanyFilter.NumberOfEmployee = Customer_CompanyFilterDTO.NumberOfEmployee;
            CompanyFilter.CustomerLeadId = Customer_CompanyFilterDTO.CustomerLeadId;
            CompanyFilter.ParentId = Customer_CompanyFilterDTO.ParentId;
            CompanyFilter.Path = Customer_CompanyFilterDTO.Path;
            CompanyFilter.Level = Customer_CompanyFilterDTO.Level;
            CompanyFilter.ProfessionId = Customer_CompanyFilterDTO.ProfessionId;
            CompanyFilter.AppUserId = Customer_CompanyFilterDTO.AppUserId;
            CompanyFilter.CreatorId = Customer_CompanyFilterDTO.CreatorId;
            CompanyFilter.CurrencyId = Customer_CompanyFilterDTO.CurrencyId;
            CompanyFilter.CompanyStatusId = Customer_CompanyFilterDTO.CompanyStatusId;
            CompanyFilter.Description = Customer_CompanyFilterDTO.Description;
            CompanyFilter.RowId = Customer_CompanyFilterDTO.RowId;

            List<Company> Companies = await CompanyService.List(CompanyFilter);
            List<Customer_CompanyDTO> Customer_CompanyDTOs = Companies
                .Select(x => new Customer_CompanyDTO(x)).ToList();
            return Customer_CompanyDTOs;
        }
        [Route(CustomerRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<Customer_AppUserDTO>>> FilterListAppUser([FromBody] Customer_AppUserFilterDTO Customer_AppUserFilterDTO)
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
            AppUserFilter.Id = Customer_AppUserFilterDTO.Id;
            AppUserFilter.Username = Customer_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Customer_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = Customer_AppUserFilterDTO.Address;
            AppUserFilter.Email = Customer_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Customer_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = Customer_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = Customer_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = Customer_AppUserFilterDTO.Avatar;
            AppUserFilter.Department = Customer_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = Customer_AppUserFilterDTO.OrganizationId;
            AppUserFilter.Longitude = Customer_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = Customer_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = Customer_AppUserFilterDTO.StatusId;

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Customer_AppUserDTO> Customer_AppUserDTOs = AppUsers
                .Select(x => new Customer_AppUserDTO(x)).ToList();
            return Customer_AppUserDTOs;
        }
        [Route(CustomerRoute.FilterListCustomerResource), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerResourceDTO>>> FilterListCustomerResource([FromBody] Customer_CustomerResourceFilterDTO Customer_CustomerResourceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerResourceFilter CustomerResourceFilter = new CustomerResourceFilter();
            CustomerResourceFilter.Skip = 0;
            CustomerResourceFilter.Take = 20;
            CustomerResourceFilter.OrderBy = CustomerResourceOrder.Id;
            CustomerResourceFilter.OrderType = OrderType.ASC;
            CustomerResourceFilter.Selects = CustomerResourceSelect.ALL;
            CustomerResourceFilter.Id = Customer_CustomerResourceFilterDTO.Id;
            CustomerResourceFilter.Code = Customer_CustomerResourceFilterDTO.Code;
            CustomerResourceFilter.Name = Customer_CustomerResourceFilterDTO.Name;
            CustomerResourceFilter.StatusId = Customer_CustomerResourceFilterDTO.StatusId;
            CustomerResourceFilter.Description = Customer_CustomerResourceFilterDTO.Description;
            CustomerResourceFilter.RowId = Customer_CustomerResourceFilterDTO.RowId;

            List<CustomerResource> CustomerResources = await CustomerResourceService.List(CustomerResourceFilter);
            List<Customer_CustomerResourceDTO> Customer_CustomerResourceDTOs = CustomerResources
                .Select(x => new Customer_CustomerResourceDTO(x)).ToList();
            return Customer_CustomerResourceDTOs;
        }
        [Route(CustomerRoute.FilterListCustomerType), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerTypeDTO>>> FilterListCustomerType([FromBody] Customer_CustomerTypeFilterDTO Customer_CustomerTypeFilterDTO)
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
            CustomerTypeFilter.Id = Customer_CustomerTypeFilterDTO.Id;
            CustomerTypeFilter.Code = Customer_CustomerTypeFilterDTO.Code;
            CustomerTypeFilter.Name = Customer_CustomerTypeFilterDTO.Name;

            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            List<Customer_CustomerTypeDTO> Customer_CustomerTypeDTOs = CustomerTypes
                .Select(x => new Customer_CustomerTypeDTO(x)).ToList();
            return Customer_CustomerTypeDTOs;
        }
        [Route(CustomerRoute.FilterListDistrict), HttpPost]
        public async Task<ActionResult<List<Customer_DistrictDTO>>> FilterListDistrict([FromBody] Customer_DistrictFilterDTO Customer_DistrictFilterDTO)
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
            DistrictFilter.Id = Customer_DistrictFilterDTO.Id;
            DistrictFilter.Code = Customer_DistrictFilterDTO.Code;
            DistrictFilter.Name = Customer_DistrictFilterDTO.Name;
            DistrictFilter.Priority = Customer_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = Customer_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = Customer_DistrictFilterDTO.StatusId;

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<Customer_DistrictDTO> Customer_DistrictDTOs = Districts
                .Select(x => new Customer_DistrictDTO(x)).ToList();
            return Customer_DistrictDTOs;
        }
        [Route(CustomerRoute.FilterListNation), HttpPost]
        public async Task<ActionResult<List<Customer_NationDTO>>> FilterListNation([FromBody] Customer_NationFilterDTO Customer_NationFilterDTO)
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
            NationFilter.Id = Customer_NationFilterDTO.Id;
            NationFilter.Code = Customer_NationFilterDTO.Code;
            NationFilter.Name = Customer_NationFilterDTO.Name;
            NationFilter.Priority = Customer_NationFilterDTO.Priority;
            NationFilter.StatusId = Customer_NationFilterDTO.StatusId;

            List<Nation> Nations = await NationService.List(NationFilter);
            List<Customer_NationDTO> Customer_NationDTOs = Nations
                .Select(x => new Customer_NationDTO(x)).ToList();
            return Customer_NationDTOs;
        }
        [Route(CustomerRoute.FilterListProfession), HttpPost]
        public async Task<ActionResult<List<Customer_ProfessionDTO>>> FilterListProfession([FromBody] Customer_ProfessionFilterDTO Customer_ProfessionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProfessionFilter ProfessionFilter = new ProfessionFilter();
            ProfessionFilter.Skip = 0;
            ProfessionFilter.Take = 20;
            ProfessionFilter.OrderBy = ProfessionOrder.Id;
            ProfessionFilter.OrderType = OrderType.ASC;
            ProfessionFilter.Selects = ProfessionSelect.ALL;
            ProfessionFilter.Id = Customer_ProfessionFilterDTO.Id;
            ProfessionFilter.Code = Customer_ProfessionFilterDTO.Code;
            ProfessionFilter.Name = Customer_ProfessionFilterDTO.Name;
            ProfessionFilter.StatusId = Customer_ProfessionFilterDTO.StatusId;
            ProfessionFilter.RowId = Customer_ProfessionFilterDTO.RowId;

            List<Profession> Professions = await ProfessionService.List(ProfessionFilter);
            List<Customer_ProfessionDTO> Customer_ProfessionDTOs = Professions
                .Select(x => new Customer_ProfessionDTO(x)).ToList();
            return Customer_ProfessionDTOs;
        }
        [Route(CustomerRoute.FilterListProvince), HttpPost]
        public async Task<ActionResult<List<Customer_ProvinceDTO>>> FilterListProvince([FromBody] Customer_ProvinceFilterDTO Customer_ProvinceFilterDTO)
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
            ProvinceFilter.Id = Customer_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = Customer_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = Customer_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = Customer_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = Customer_ProvinceFilterDTO.StatusId;

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<Customer_ProvinceDTO> Customer_ProvinceDTOs = Provinces
                .Select(x => new Customer_ProvinceDTO(x)).ToList();
            return Customer_ProvinceDTOs;
        }
        [Route(CustomerRoute.FilterListSex), HttpPost]
        public async Task<ActionResult<List<Customer_SexDTO>>> FilterListSex([FromBody] Customer_SexFilterDTO Customer_SexFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SexFilter SexFilter = new SexFilter();
            SexFilter.Skip = 0;
            SexFilter.Take = int.MaxValue;
            SexFilter.Take = 20;
            SexFilter.OrderBy = SexOrder.Id;
            SexFilter.OrderType = OrderType.ASC;
            SexFilter.Selects = SexSelect.ALL;

            List<Sex> Sexes = await SexService.List(SexFilter);
            List<Customer_SexDTO> Customer_SexDTOs = Sexes
                .Select(x => new Customer_SexDTO(x)).ToList();
            return Customer_SexDTOs;
        }
        [Route(CustomerRoute.FilterListStatus), HttpPost]
        public async Task<ActionResult<List<Customer_StatusDTO>>> FilterListStatus([FromBody] Customer_StatusFilterDTO Customer_StatusFilterDTO)
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
            List<Customer_StatusDTO> Customer_StatusDTOs = Statuses
                .Select(x => new Customer_StatusDTO(x)).ToList();
            return Customer_StatusDTOs;
        }
        [Route(CustomerRoute.FilterListWard), HttpPost]
        public async Task<ActionResult<List<Customer_WardDTO>>> FilterListWard([FromBody] Customer_WardFilterDTO Customer_WardFilterDTO)
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
            WardFilter.Id = Customer_WardFilterDTO.Id;
            WardFilter.Code = Customer_WardFilterDTO.Code;
            WardFilter.Name = Customer_WardFilterDTO.Name;
            WardFilter.Priority = Customer_WardFilterDTO.Priority;
            WardFilter.DistrictId = Customer_WardFilterDTO.DistrictId;
            WardFilter.StatusId = Customer_WardFilterDTO.StatusId;

            List<Ward> Wards = await WardService.List(WardFilter);
            List<Customer_WardDTO> Customer_WardDTOs = Wards
                .Select(x => new Customer_WardDTO(x)).ToList();
            return Customer_WardDTOs;
        }
        [Route(CustomerRoute.FilterListEmailType), HttpPost]
        public async Task<ActionResult<List<Customer_EmailTypeDTO>>> FilterListEmailType([FromBody] Customer_EmailTypeFilterDTO Customer_EmailTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            EmailTypeFilter EmailTypeFilter = new EmailTypeFilter();
            EmailTypeFilter.Skip = 0;
            EmailTypeFilter.Take = int.MaxValue;
            EmailTypeFilter.Take = 20;
            EmailTypeFilter.OrderBy = EmailTypeOrder.Id;
            EmailTypeFilter.OrderType = OrderType.ASC;
            EmailTypeFilter.Selects = EmailTypeSelect.ALL;

            List<EmailType> EmailTypes = await EmailTypeService.List(EmailTypeFilter);
            List<Customer_EmailTypeDTO> Customer_EmailTypeDTOs = EmailTypes
                .Select(x => new Customer_EmailTypeDTO(x)).ToList();
            return Customer_EmailTypeDTOs;
        }
        [Route(CustomerRoute.FilterListCustomerFeedback), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerFeedbackDTO>>> FilterListCustomerFeedback([FromBody] Customer_CustomerFeedbackFilterDTO Customer_CustomerFeedbackFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFeedbackFilter CustomerFeedbackFilter = new CustomerFeedbackFilter();
            CustomerFeedbackFilter.Skip = 0;
            CustomerFeedbackFilter.Take = 20;
            CustomerFeedbackFilter.OrderBy = CustomerFeedbackOrder.Id;
            CustomerFeedbackFilter.OrderType = OrderType.ASC;
            CustomerFeedbackFilter.Selects = CustomerFeedbackSelect.ALL;
            CustomerFeedbackFilter.Id = Customer_CustomerFeedbackFilterDTO.Id;
            CustomerFeedbackFilter.CustomerId = Customer_CustomerFeedbackFilterDTO.CustomerId;
            CustomerFeedbackFilter.FullName = Customer_CustomerFeedbackFilterDTO.FullName;
            CustomerFeedbackFilter.Email = Customer_CustomerFeedbackFilterDTO.Email;
            CustomerFeedbackFilter.PhoneNumber = Customer_CustomerFeedbackFilterDTO.PhoneNumber;
            CustomerFeedbackFilter.CustomerFeedbackTypeId = Customer_CustomerFeedbackFilterDTO.CustomerFeedbackTypeId;
            CustomerFeedbackFilter.Title = Customer_CustomerFeedbackFilterDTO.Title;
            CustomerFeedbackFilter.SendDate = Customer_CustomerFeedbackFilterDTO.SendDate;
            CustomerFeedbackFilter.Content = Customer_CustomerFeedbackFilterDTO.Content;
            CustomerFeedbackFilter.StatusId = Customer_CustomerFeedbackFilterDTO.StatusId;

            List<CustomerFeedback> CustomerFeedbacks = await CustomerFeedbackService.List(CustomerFeedbackFilter);
            List<Customer_CustomerFeedbackDTO> Customer_CustomerFeedbackDTOs = CustomerFeedbacks
                .Select(x => new Customer_CustomerFeedbackDTO(x)).ToList();
            return Customer_CustomerFeedbackDTOs;
        }
        [Route(CustomerRoute.FilterListCustomerFeedbackType), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerFeedbackTypeDTO>>> FilterListCustomerFeedbackType([FromBody] Customer_CustomerFeedbackTypeFilterDTO Customer_CustomerFeedbackTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFeedbackTypeFilter CustomerFeedbackTypeFilter = new CustomerFeedbackTypeFilter();
            CustomerFeedbackTypeFilter.Skip = 0;
            CustomerFeedbackTypeFilter.Take = int.MaxValue;
            CustomerFeedbackTypeFilter.Take = 20;
            CustomerFeedbackTypeFilter.OrderBy = CustomerFeedbackTypeOrder.Id;
            CustomerFeedbackTypeFilter.OrderType = OrderType.ASC;
            CustomerFeedbackTypeFilter.Selects = CustomerFeedbackTypeSelect.ALL;

            List<CustomerFeedbackType> CustomerFeedbackTypes = await CustomerFeedbackTypeService.List(CustomerFeedbackTypeFilter);
            List<Customer_CustomerFeedbackTypeDTO> Customer_CustomerFeedbackTypeDTOs = CustomerFeedbackTypes
                .Select(x => new Customer_CustomerFeedbackTypeDTO(x)).ToList();
            return Customer_CustomerFeedbackTypeDTOs;
        }
        [Route(CustomerRoute.FilterListPhoneType), HttpPost]
        public async Task<ActionResult<List<Customer_PhoneTypeDTO>>> FilterListPhoneType([FromBody] Customer_PhoneTypeFilterDTO Customer_PhoneTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PhoneTypeFilter PhoneTypeFilter = new PhoneTypeFilter();
            PhoneTypeFilter.Skip = 0;
            PhoneTypeFilter.Take = 20;
            PhoneTypeFilter.OrderBy = PhoneTypeOrder.Id;
            PhoneTypeFilter.OrderType = OrderType.ASC;
            PhoneTypeFilter.Selects = PhoneTypeSelect.ALL;
            PhoneTypeFilter.Id = Customer_PhoneTypeFilterDTO.Id;
            PhoneTypeFilter.Code = Customer_PhoneTypeFilterDTO.Code;
            PhoneTypeFilter.Name = Customer_PhoneTypeFilterDTO.Name;
            PhoneTypeFilter.StatusId = Customer_PhoneTypeFilterDTO.StatusId;
            PhoneTypeFilter.RowId = Customer_PhoneTypeFilterDTO.RowId;

            List<PhoneType> PhoneTypes = await PhoneTypeService.List(PhoneTypeFilter);
            List<Customer_PhoneTypeDTO> Customer_PhoneTypeDTOs = PhoneTypes
                .Select(x => new Customer_PhoneTypeDTO(x)).ToList();
            return Customer_PhoneTypeDTOs;
        }
        [Route(CustomerRoute.FilterListEmailStatus), HttpPost]
        public async Task<ActionResult<List<Customer_EmailStatusDTO>>> FilterListEmailStatus([FromBody] Customer_EmailStatusFilterDTO Customer_EmailStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            EmailStatusFilter EmailStatusFilter = new EmailStatusFilter();
            EmailStatusFilter.Skip = 0;
            EmailStatusFilter.Take = 20;
            EmailStatusFilter.OrderBy = EmailStatusOrder.Id;
            EmailStatusFilter.OrderType = OrderType.ASC;
            EmailStatusFilter.Selects = EmailStatusSelect.ALL;
            EmailStatusFilter.Id = Customer_EmailStatusFilterDTO.Id;
            EmailStatusFilter.Code = Customer_EmailStatusFilterDTO.Code;
            EmailStatusFilter.Name = Customer_EmailStatusFilterDTO.Name;

            List<EmailStatus> Statuses = await EmailStatusService.List(EmailStatusFilter);
            List<Customer_EmailStatusDTO> Customer_EmailStatusDTOs = Statuses
                .Select(x => new Customer_EmailStatusDTO(x)).ToList();
            return Customer_EmailStatusDTOs;
        }
        [Route(CustomerRoute.FilterListContractStatus), HttpPost]
        public async Task<ActionResult<List<Customer_ContractStatusDTO>>> FilterListContractStatus([FromBody] Customer_ContractStatusFilterDTO Customer_ContractStatusFilterDTO)
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
            ContractStatusFilter.Id = Customer_ContractStatusFilterDTO.Id;
            ContractStatusFilter.Name = Customer_ContractStatusFilterDTO.Name;
            ContractStatusFilter.Code = Customer_ContractStatusFilterDTO.Code;

            List<ContractStatus> ContractStatuses = await ContractStatusService.List(ContractStatusFilter);
            List<Customer_ContractStatusDTO> Customer_ContractStatusDTOs = ContractStatuses
                .Select(x => new Customer_ContractStatusDTO(x)).ToList();
            return Customer_ContractStatusDTOs;
        }
        [Route(CustomerRoute.FilterListContractType), HttpPost]
        public async Task<ActionResult<List<Customer_ContractTypeDTO>>> FilterListContractType([FromBody] Customer_ContractTypeFilterDTO Customer_ContractTypeFilterDTO)
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
            ContractTypeFilter.Id = Customer_ContractTypeFilterDTO.Id;
            ContractTypeFilter.Name = Customer_ContractTypeFilterDTO.Name;
            ContractTypeFilter.Code = Customer_ContractTypeFilterDTO.Code;

            List<ContractType> ContractTypes = await ContractTypeService.List(ContractTypeFilter);
            List<Customer_ContractTypeDTO> Customer_ContractTypeDTOs = ContractTypes
                .Select(x => new Customer_ContractTypeDTO(x)).ToList();
            return Customer_ContractTypeDTOs;
        }
        [Route(CustomerRoute.FilterListCurrency), HttpPost]
        public async Task<ActionResult<List<Customer_CurrencyDTO>>> FilterListCurrency([FromBody] Customer_CurrencyFilterDTO Customer_CurrencyFilterDTO)
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
            CurrencyFilter.Id = Customer_CurrencyFilterDTO.Id;
            CurrencyFilter.Code = Customer_CurrencyFilterDTO.Code;
            CurrencyFilter.Name = Customer_CurrencyFilterDTO.Name;

            List<Currency> Currencies = await CurrencyService.List(CurrencyFilter);
            List<Customer_CurrencyDTO> Customer_CurrencyDTOs = Currencies
                .Select(x => new Customer_CurrencyDTO(x)).ToList();
            return Customer_CurrencyDTOs;
        }
        [Route(CustomerRoute.FilterListPaymentStatus), HttpPost]
        public async Task<ActionResult<List<Customer_PaymentStatusDTO>>> FilterListPaymentStatus([FromBody] Customer_PaymentStatusFilterDTO Customer_PaymentStatusFilterDTO)
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
            PaymentStatusFilter.Id = Customer_PaymentStatusFilterDTO.Id;
            PaymentStatusFilter.Name = Customer_PaymentStatusFilterDTO.Name;
            PaymentStatusFilter.Code = Customer_PaymentStatusFilterDTO.Code;

            List<PaymentStatus> PaymentStatuses = await PaymentStatusService.List(PaymentStatusFilter);
            List<Customer_PaymentStatusDTO> Customer_PaymentStatusDTOs = PaymentStatuses
                .Select(x => new Customer_PaymentStatusDTO(x)).ToList();
            return Customer_PaymentStatusDTOs;
        }

        [Route(CustomerRoute.FilterListOrderCategory), HttpPost]
        public async Task<ActionResult<List<Customer_OrderCategoryDTO>>> FilterListOrderCategory([FromBody] Customer_OrderCategoryFilterDTO Customer_OrderCategoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderCategoryFilter OrderCategoryFilter = new OrderCategoryFilter();
            OrderCategoryFilter.Skip = 0;
            OrderCategoryFilter.Take = int.MaxValue;
            OrderCategoryFilter.Take = 20;
            OrderCategoryFilter.OrderBy = OrderCategoryOrder.Id;
            OrderCategoryFilter.OrderType = OrderType.ASC;
            OrderCategoryFilter.Selects = OrderCategorySelect.ALL;

            List<OrderCategory> OrderCategorys = await OrderCategoryService.List(OrderCategoryFilter);
            List<Customer_OrderCategoryDTO> Customer_OrderCategoryDTOs = OrderCategorys
                .Select(x => new Customer_OrderCategoryDTO(x)).ToList();
            return Customer_OrderCategoryDTOs;
        }

        [Route(CustomerRoute.FilterListRepairStatus), HttpPost]
        public async Task<ActionResult<List<Customer_RepairStatusDTO>>> FilterListRepairStatus([FromBody] Customer_RepairStatusFilterDTO Customer_RepairStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairStatusFilter RepairStatusFilter = new RepairStatusFilter();
            RepairStatusFilter.Skip = 0;
            RepairStatusFilter.Take = int.MaxValue;
            RepairStatusFilter.Take = 20;
            RepairStatusFilter.OrderBy = RepairStatusOrder.Id;
            RepairStatusFilter.OrderType = OrderType.ASC;
            RepairStatusFilter.Selects = RepairStatusSelect.ALL;

            List<RepairStatus> RepairStatuses = await RepairStatusService.List(RepairStatusFilter);
            List<Customer_RepairStatusDTO> Customer_RepairStatusDTOs = RepairStatuses
                .Select(x => new Customer_RepairStatusDTO(x)).ToList();
            return Customer_RepairStatusDTOs;
        }

        [Route(CustomerRoute.FilterListCustomerGrouping), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerGroupingDTO>>> FilterListCustomerGrouping([FromBody] Customer_CustomerGroupingFilterDTO Customer_CustomerGroupingFilterDTO)
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
            CustomerGroupingFilter.Id = Customer_CustomerGroupingFilterDTO.Id;
            CustomerGroupingFilter.Name = Customer_CustomerGroupingFilterDTO.Name;
            CustomerGroupingFilter.Code = Customer_CustomerGroupingFilterDTO.Code;
            CustomerGroupingFilter.Path = Customer_CustomerGroupingFilterDTO.Path;
            CustomerGroupingFilter.CustomerTypeId = Customer_CustomerGroupingFilterDTO.CustomerTypeId;
            CustomerGroupingFilter.Level = Customer_CustomerGroupingFilterDTO.Level;
            CustomerGroupingFilter.StatusId = Customer_CustomerGroupingFilterDTO.StatusId;
            CustomerGroupingFilter.Description = Customer_CustomerGroupingFilterDTO.Description;
            CustomerGroupingFilter.ParentId = Customer_CustomerGroupingFilterDTO.ParentId;

            List<CustomerGrouping> CustomerGroupings = await CustomerGroupingService.List(CustomerGroupingFilter);
            List<Customer_CustomerGroupingDTO> Customer_CustomerGroupingDTOs = CustomerGroupings
                .Select(x => new Customer_CustomerGroupingDTO(x)).ToList();
            return Customer_CustomerGroupingDTOs;
        }

        [Route(CustomerRoute.FilterListRequestState), HttpPost]
        public async Task<ActionResult<List<Customer_RequestStateDTO>>> FilterListRequestState([FromBody] Customer_RequestStateFilterDTO Customer_RequestStateFilterDTO)
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
            RequestStateFilter.Id = Customer_RequestStateFilterDTO.Id;
            RequestStateFilter.Code = Customer_RequestStateFilterDTO.Code;
            RequestStateFilter.Name = Customer_RequestStateFilterDTO.Name;

            List<RequestState> RequestStates = await RequestStateService.List(RequestStateFilter);
            List<Customer_RequestStateDTO> Customer_RequestStateDTOs = RequestStates
                .Select(x => new Customer_RequestStateDTO(x)).ToList();
            return Customer_RequestStateDTOs;
        }
        [Route(CustomerRoute.FilterListContract), HttpPost]
        public async Task<ActionResult<List<Customer_ContractDTO>>> FilterListContract([FromBody] Customer_ContractFilterDTO Customer_ContractFilterDTO)
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
            ContractFilter.Id = Customer_ContractFilterDTO.Id;
            ContractFilter.Code = Customer_ContractFilterDTO.Code;
            ContractFilter.Name = Customer_ContractFilterDTO.Name;
            ContractFilter.TotalValue = Customer_ContractFilterDTO.TotalValue;
            ContractFilter.ValidityDate = Customer_ContractFilterDTO.ValidityDate;
            ContractFilter.ExpirationDate = Customer_ContractFilterDTO.ExpirationDate;
            ContractFilter.DeliveryUnit = Customer_ContractFilterDTO.DeliveryUnit;
            ContractFilter.InvoiceAddress = Customer_ContractFilterDTO.InvoiceAddress;
            ContractFilter.InvoiceZipCode = Customer_ContractFilterDTO.InvoiceZipCode;
            ContractFilter.ReceiveAddress = Customer_ContractFilterDTO.ReceiveAddress;
            ContractFilter.ReceiveZipCode = Customer_ContractFilterDTO.ReceiveZipCode;
            ContractFilter.TermAndCondition = Customer_ContractFilterDTO.TermAndCondition;
            ContractFilter.InvoiceNationId = Customer_ContractFilterDTO.InvoiceNationId;
            ContractFilter.InvoiceProvinceId = Customer_ContractFilterDTO.InvoiceProvinceId;
            ContractFilter.InvoiceDistrictId = Customer_ContractFilterDTO.InvoiceDistrictId;
            ContractFilter.ReceiveNationId = Customer_ContractFilterDTO.ReceiveNationId;
            ContractFilter.ReceiveProvinceId = Customer_ContractFilterDTO.ReceiveProvinceId;
            ContractFilter.ReceiveDistrictId = Customer_ContractFilterDTO.ReceiveDistrictId;
            ContractFilter.ContractTypeId = Customer_ContractFilterDTO.ContractTypeId;
            ContractFilter.CompanyId = Customer_ContractFilterDTO.CompanyId;
            ContractFilter.OpportunityId = Customer_ContractFilterDTO.OpportunityId;
            ContractFilter.OrganizationId = Customer_ContractFilterDTO.OrganizationId;
            ContractFilter.AppUserId = Customer_ContractFilterDTO.AppUserId;
            ContractFilter.ContractStatusId = Customer_ContractFilterDTO.ContractStatusId;
            ContractFilter.CreatorId = Customer_ContractFilterDTO.CreatorId;
            ContractFilter.CustomerId = Customer_ContractFilterDTO.CustomerId;
            ContractFilter.CurrencyId = Customer_ContractFilterDTO.CurrencyId;
            ContractFilter.PaymentStatusId = Customer_ContractFilterDTO.PaymentStatusId;

            List<Contract> Contracts = await ContractService.List(ContractFilter);
            List<Customer_ContractDTO> Customer_ContractDTOs = Contracts
                .Select(x => new Customer_ContractDTO(x)).ToList();
            return Customer_ContractDTOs;
        }
        [Route(CustomerRoute.FilterListOpportunity), HttpPost]
        public async Task<ActionResult<List<Customer_OpportunityDTO>>> FilterListOpportunity([FromBody] Customer_OpportunityFilterDTO Customer_OpportunityFilterDTO)
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
            OpportunityFilter.Id = Customer_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = Customer_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = Customer_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.CustomerLeadId = Customer_OpportunityFilterDTO.CustomerLeadId;
            OpportunityFilter.ClosingDate = Customer_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = Customer_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = Customer_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = Customer_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = Customer_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.AppUserId = Customer_OpportunityFilterDTO.AppUserId;
            OpportunityFilter.CurrencyId = Customer_OpportunityFilterDTO.CurrencyId;
            OpportunityFilter.Amount = Customer_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = Customer_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = Customer_OpportunityFilterDTO.Description;
            OpportunityFilter.OpportunityResultTypeId = Customer_OpportunityFilterDTO.OpportunityResultTypeId;
            OpportunityFilter.CreatorId = Customer_OpportunityFilterDTO.CreatorId;

            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<Customer_OpportunityDTO> Customer_OpportunityDTOs = Opportunities
                .Select(x => new Customer_OpportunityDTO(x)).ToList();
            return Customer_OpportunityDTOs;
        }
        [Route(CustomerRoute.FilterListStore), HttpPost]
        public async Task<ActionResult<List<Customer_StoreDTO>>> FilterListStore([FromBody] Customer_StoreFilterDTO Customer_StoreFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreFilter StoreFilter = new StoreFilter();
            StoreFilter.Skip = 0;
            StoreFilter.Take = 20;
            StoreFilter.OrderBy = StoreOrder.Id;
            StoreFilter.OrderType = OrderType.ASC;
            StoreFilter.Selects = StoreSelect.ALL;
            StoreFilter.Id = Customer_StoreFilterDTO.Id;
            StoreFilter.Code = Customer_StoreFilterDTO.Code;
            StoreFilter.CodeDraft = Customer_StoreFilterDTO.CodeDraft;
            StoreFilter.Name = Customer_StoreFilterDTO.Name;
            StoreFilter.ParentStoreId = Customer_StoreFilterDTO.ParentStoreId;
            StoreFilter.OrganizationId = Customer_StoreFilterDTO.OrganizationId;
            StoreFilter.StoreTypeId = Customer_StoreFilterDTO.StoreTypeId;
            StoreFilter.StoreGroupingId = Customer_StoreFilterDTO.StoreGroupingId;
            StoreFilter.Telephone = Customer_StoreFilterDTO.Telephone;
            StoreFilter.ProvinceId = Customer_StoreFilterDTO.ProvinceId;
            StoreFilter.DistrictId = Customer_StoreFilterDTO.DistrictId;
            StoreFilter.WardId = Customer_StoreFilterDTO.WardId;
            StoreFilter.Address = Customer_StoreFilterDTO.Address;
            StoreFilter.DeliveryAddress = Customer_StoreFilterDTO.DeliveryAddress;
            StoreFilter.Latitude = Customer_StoreFilterDTO.Latitude;
            StoreFilter.Longitude = Customer_StoreFilterDTO.Longitude;
            StoreFilter.DeliveryLatitude = Customer_StoreFilterDTO.DeliveryLatitude;
            StoreFilter.DeliveryLongitude = Customer_StoreFilterDTO.DeliveryLongitude;
            StoreFilter.OwnerName = Customer_StoreFilterDTO.OwnerName;
            StoreFilter.OwnerPhone = Customer_StoreFilterDTO.OwnerPhone;
            StoreFilter.OwnerEmail = Customer_StoreFilterDTO.OwnerEmail;
            StoreFilter.StatusId = Customer_StoreFilterDTO.StatusId;

            if (StoreFilter.Id == null) StoreFilter.Id = new IdFilter();
            StoreFilter.Id.In = await FilterStore(StoreService, OrganizationService, CurrentContext);

            List<Store> Stores = await StoreService.List(StoreFilter);
            List<Customer_StoreDTO> Customer_StoreDTOs = Stores
                .Select(x => new Customer_StoreDTO(x)).ToList();
            return Customer_StoreDTOs;
        }

        [Route(CustomerRoute.SingleListBusinessType), HttpPost]
        public async Task<ActionResult<List<Customer_BusinessTypeDTO>>> SingleListBusinessType([FromBody] Customer_BusinessTypeFilterDTO Customer_BusinessTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            BusinessTypeFilter BusinessTypeFilter = new BusinessTypeFilter();
            BusinessTypeFilter.Skip = 0;
            BusinessTypeFilter.Take = int.MaxValue;
            BusinessTypeFilter.Take = 20;
            BusinessTypeFilter.OrderBy = BusinessTypeOrder.Id;
            BusinessTypeFilter.OrderType = OrderType.ASC;
            BusinessTypeFilter.Selects = BusinessTypeSelect.ALL;

            List<BusinessType> BusinessTypes = await BusinessTypeService.List(BusinessTypeFilter);
            List<Customer_BusinessTypeDTO> Customer_BusinessTypeDTOs = BusinessTypes
                .Select(x => new Customer_BusinessTypeDTO(x)).ToList();
            return Customer_BusinessTypeDTOs;
        }
        [Route(CustomerRoute.SingleListCallType), HttpPost]
        public async Task<ActionResult<List<Customer_CallTypeDTO>>> SingleListCallType([FromBody] Customer_CallTypeFilterDTO Customer_CallTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallTypeFilter CallTypeFilter = new CallTypeFilter();
            CallTypeFilter.Skip = 0;
            CallTypeFilter.Take = 20;
            CallTypeFilter.OrderBy = CallTypeOrder.Id;
            CallTypeFilter.OrderType = OrderType.ASC;
            CallTypeFilter.Selects = CallTypeSelect.ALL;
            CallTypeFilter.Id = Customer_CallTypeFilterDTO.Id;
            CallTypeFilter.Code = Customer_CallTypeFilterDTO.Code;
            CallTypeFilter.Name = Customer_CallTypeFilterDTO.Name;
            CallTypeFilter.ColorCode = Customer_CallTypeFilterDTO.ColorCode;
            CallTypeFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<CallType> CallTypes = await CallTypeService.List(CallTypeFilter);
            List<Customer_CallTypeDTO> Customer_CallTypeDTOs = CallTypes
                .Select(x => new Customer_CallTypeDTO(x)).ToList();
            return Customer_CallTypeDTOs;
        }
        [Route(CustomerRoute.SingleListCallCategory), HttpPost]
        public async Task<ActionResult<List<Customer_CallCategoryDTO>>> SingleListCallCategory([FromBody] Customer_CallCategoryFilterDTO Customer_CallCategoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallCategoryFilter CallCategoryFilter = new CallCategoryFilter();
            CallCategoryFilter.Skip = 0;
            CallCategoryFilter.Take = int.MaxValue;
            CallCategoryFilter.Take = 20;
            CallCategoryFilter.OrderBy = CallCategoryOrder.Id;
            CallCategoryFilter.OrderType = OrderType.ASC;
            CallCategoryFilter.Selects = CallCategorySelect.ALL;

            List<CallCategory> CallCategories = await CallCategoryService.List(CallCategoryFilter);
            List<Customer_CallCategoryDTO> Customer_CallCategoryDTOs = CallCategories
                .Select(x => new Customer_CallCategoryDTO(x)).ToList();
            return Customer_CallCategoryDTOs;
        }
        [Route(CustomerRoute.SingleListCallStatus), HttpPost]
        public async Task<ActionResult<List<Customer_CallStatusDTO>>> SingleListCallStatus([FromBody] Customer_CallStatusFilterDTO Customer_CallStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallStatusFilter CallStatusFilter = new CallStatusFilter();
            CallStatusFilter.Skip = 0;
            CallStatusFilter.Take = int.MaxValue;
            CallStatusFilter.Take = 20;
            CallStatusFilter.OrderBy = CallStatusOrder.Id;
            CallStatusFilter.OrderType = OrderType.ASC;
            CallStatusFilter.Selects = CallStatusSelect.ALL;

            List<CallStatus> CallStatuses = await CallStatusService.List(CallStatusFilter);
            List<Customer_CallStatusDTO> Customer_CallStatusDTOs = CallStatuses
                .Select(x => new Customer_CallStatusDTO(x)).ToList();
            return Customer_CallStatusDTOs;
        }
        [Route(CustomerRoute.SingleListCompany), HttpPost]
        public async Task<ActionResult<List<Customer_CompanyDTO>>> SingleListCompany([FromBody] Customer_CompanyFilterDTO Customer_CompanyFilterDTO)
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
            CompanyFilter.Id = Customer_CompanyFilterDTO.Id;
            CompanyFilter.Name = Customer_CompanyFilterDTO.Name;
            CompanyFilter.Phone = Customer_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = Customer_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = Customer_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = Customer_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = Customer_CompanyFilterDTO.EmailOther;
            CompanyFilter.ZIPCode = Customer_CompanyFilterDTO.ZIPCode;
            CompanyFilter.Revenue = Customer_CompanyFilterDTO.Revenue;
            CompanyFilter.Website = Customer_CompanyFilterDTO.Website;
            CompanyFilter.Address = Customer_CompanyFilterDTO.Address;
            CompanyFilter.NationId = Customer_CompanyFilterDTO.NationId;
            CompanyFilter.ProvinceId = Customer_CompanyFilterDTO.ProvinceId;
            CompanyFilter.DistrictId = Customer_CompanyFilterDTO.DistrictId;
            CompanyFilter.NumberOfEmployee = Customer_CompanyFilterDTO.NumberOfEmployee;
            CompanyFilter.CustomerLeadId = Customer_CompanyFilterDTO.CustomerLeadId;
            CompanyFilter.ParentId = Customer_CompanyFilterDTO.ParentId;
            CompanyFilter.Path = Customer_CompanyFilterDTO.Path;
            CompanyFilter.Level = Customer_CompanyFilterDTO.Level;
            CompanyFilter.ProfessionId = Customer_CompanyFilterDTO.ProfessionId;
            CompanyFilter.AppUserId = Customer_CompanyFilterDTO.AppUserId;
            CompanyFilter.CreatorId = Customer_CompanyFilterDTO.CreatorId;
            CompanyFilter.CurrencyId = Customer_CompanyFilterDTO.CurrencyId;
            CompanyFilter.CompanyStatusId = Customer_CompanyFilterDTO.CompanyStatusId;
            CompanyFilter.Description = Customer_CompanyFilterDTO.Description;
            CompanyFilter.RowId = Customer_CompanyFilterDTO.RowId;

            List<Company> Companies = await CompanyService.List(CompanyFilter);
            List<Customer_CompanyDTO> Customer_CompanyDTOs = Companies
                .Select(x => new Customer_CompanyDTO(x)).ToList();
            return Customer_CompanyDTOs;
        }
        [Route(CustomerRoute.SingleListAppUser), HttpPost]
        public async Task<ActionResult<List<Customer_AppUserDTO>>> SingleListAppUser([FromBody] Customer_AppUserFilterDTO Customer_AppUserFilterDTO)
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
            AppUserFilter.Id = Customer_AppUserFilterDTO.Id;
            AppUserFilter.Username = Customer_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Customer_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = Customer_AppUserFilterDTO.Address;
            AppUserFilter.Email = Customer_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Customer_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = Customer_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = Customer_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = Customer_AppUserFilterDTO.Avatar;
            AppUserFilter.Department = Customer_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = Customer_AppUserFilterDTO.OrganizationId;
            AppUserFilter.Longitude = Customer_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = Customer_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Customer_AppUserDTO> Customer_AppUserDTOs = AppUsers
                .Select(x => new Customer_AppUserDTO(x)).ToList();
            return Customer_AppUserDTOs;
        }
        [Route(CustomerRoute.SingleListCustomerResource), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerResourceDTO>>> SingleListCustomerResource([FromBody] Customer_CustomerResourceFilterDTO Customer_CustomerResourceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerResourceFilter CustomerResourceFilter = new CustomerResourceFilter();
            CustomerResourceFilter.Skip = 0;
            CustomerResourceFilter.Take = 20;
            CustomerResourceFilter.OrderBy = CustomerResourceOrder.Id;
            CustomerResourceFilter.OrderType = OrderType.ASC;
            CustomerResourceFilter.Selects = CustomerResourceSelect.ALL;
            CustomerResourceFilter.Id = Customer_CustomerResourceFilterDTO.Id;
            CustomerResourceFilter.Code = Customer_CustomerResourceFilterDTO.Code;
            CustomerResourceFilter.Name = Customer_CustomerResourceFilterDTO.Name;
            CustomerResourceFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            CustomerResourceFilter.Description = Customer_CustomerResourceFilterDTO.Description;
            CustomerResourceFilter.RowId = Customer_CustomerResourceFilterDTO.RowId;

            List<CustomerResource> CustomerResources = await CustomerResourceService.List(CustomerResourceFilter);
            List<Customer_CustomerResourceDTO> Customer_CustomerResourceDTOs = CustomerResources
                .Select(x => new Customer_CustomerResourceDTO(x)).ToList();
            return Customer_CustomerResourceDTOs;
        }
        [Route(CustomerRoute.SingleListCustomerType), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerTypeDTO>>> SingleListCustomerType([FromBody] Customer_CustomerTypeFilterDTO Customer_CustomerTypeFilterDTO)
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
            CustomerTypeFilter.Id = Customer_CustomerTypeFilterDTO.Id;
            CustomerTypeFilter.Code = Customer_CustomerTypeFilterDTO.Code;
            CustomerTypeFilter.Name = Customer_CustomerTypeFilterDTO.Name;

            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            List<Customer_CustomerTypeDTO> Customer_CustomerTypeDTOs = CustomerTypes
                .Select(x => new Customer_CustomerTypeDTO(x)).ToList();
            return Customer_CustomerTypeDTOs;
        }
        [Route(CustomerRoute.SingleListDistrict), HttpPost]
        public async Task<ActionResult<List<Customer_DistrictDTO>>> SingleListDistrict([FromBody] Customer_DistrictFilterDTO Customer_DistrictFilterDTO)
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
            DistrictFilter.Id = Customer_DistrictFilterDTO.Id;
            DistrictFilter.Code = Customer_DistrictFilterDTO.Code;
            DistrictFilter.Name = Customer_DistrictFilterDTO.Name;
            DistrictFilter.Priority = Customer_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = Customer_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<Customer_DistrictDTO> Customer_DistrictDTOs = Districts
                .Select(x => new Customer_DistrictDTO(x)).ToList();
            return Customer_DistrictDTOs;
        }
        [Route(CustomerRoute.SingleListNation), HttpPost]
        public async Task<ActionResult<List<Customer_NationDTO>>> SingleListNation([FromBody] Customer_NationFilterDTO Customer_NationFilterDTO)
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
            NationFilter.Id = Customer_NationFilterDTO.Id;
            NationFilter.Code = Customer_NationFilterDTO.Code;
            NationFilter.Name = Customer_NationFilterDTO.Name;
            NationFilter.Priority = Customer_NationFilterDTO.Priority;
            NationFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Nation> Nations = await NationService.List(NationFilter);
            List<Customer_NationDTO> Customer_NationDTOs = Nations
                .Select(x => new Customer_NationDTO(x)).ToList();
            return Customer_NationDTOs;
        }
        [Route(CustomerRoute.SingleListProfession), HttpPost]
        public async Task<ActionResult<List<Customer_ProfessionDTO>>> SingleListProfession([FromBody] Customer_ProfessionFilterDTO Customer_ProfessionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProfessionFilter ProfessionFilter = new ProfessionFilter();
            ProfessionFilter.Skip = 0;
            ProfessionFilter.Take = 20;
            ProfessionFilter.OrderBy = ProfessionOrder.Id;
            ProfessionFilter.OrderType = OrderType.ASC;
            ProfessionFilter.Selects = ProfessionSelect.ALL;
            ProfessionFilter.Id = Customer_ProfessionFilterDTO.Id;
            ProfessionFilter.Code = Customer_ProfessionFilterDTO.Code;
            ProfessionFilter.Name = Customer_ProfessionFilterDTO.Name;
            ProfessionFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            ProfessionFilter.RowId = Customer_ProfessionFilterDTO.RowId;

            List<Profession> Professions = await ProfessionService.List(ProfessionFilter);
            List<Customer_ProfessionDTO> Customer_ProfessionDTOs = Professions
                .Select(x => new Customer_ProfessionDTO(x)).ToList();
            return Customer_ProfessionDTOs;
        }
        [Route(CustomerRoute.SingleListProvince), HttpPost]
        public async Task<ActionResult<List<Customer_ProvinceDTO>>> SingleListProvince([FromBody] Customer_ProvinceFilterDTO Customer_ProvinceFilterDTO)
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
            ProvinceFilter.Id = Customer_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = Customer_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = Customer_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = Customer_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<Customer_ProvinceDTO> Customer_ProvinceDTOs = Provinces
                .Select(x => new Customer_ProvinceDTO(x)).ToList();
            return Customer_ProvinceDTOs;
        }
        [Route(CustomerRoute.SingleListSex), HttpPost]
        public async Task<ActionResult<List<Customer_SexDTO>>> SingleListSex([FromBody] Customer_SexFilterDTO Customer_SexFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SexFilter SexFilter = new SexFilter();
            SexFilter.Skip = 0;
            SexFilter.Take = int.MaxValue;
            SexFilter.Take = 20;
            SexFilter.OrderBy = SexOrder.Id;
            SexFilter.OrderType = OrderType.ASC;
            SexFilter.Selects = SexSelect.ALL;

            List<Sex> Sexes = await SexService.List(SexFilter);
            List<Customer_SexDTO> Customer_SexDTOs = Sexes
                .Select(x => new Customer_SexDTO(x)).ToList();
            return Customer_SexDTOs;
        }
        [Route(CustomerRoute.SingleListStatus), HttpPost]
        public async Task<ActionResult<List<Customer_StatusDTO>>> SingleListStatus([FromBody] Customer_StatusFilterDTO Customer_StatusFilterDTO)
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
            List<Customer_StatusDTO> Customer_StatusDTOs = Statuses
                .Select(x => new Customer_StatusDTO(x)).ToList();
            return Customer_StatusDTOs;
        }
        [Route(CustomerRoute.SingleListWard), HttpPost]
        public async Task<ActionResult<List<Customer_WardDTO>>> SingleListWard([FromBody] Customer_WardFilterDTO Customer_WardFilterDTO)
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
            WardFilter.Id = Customer_WardFilterDTO.Id;
            WardFilter.Code = Customer_WardFilterDTO.Code;
            WardFilter.Name = Customer_WardFilterDTO.Name;
            WardFilter.Priority = Customer_WardFilterDTO.Priority;
            WardFilter.DistrictId = Customer_WardFilterDTO.DistrictId;
            WardFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Ward> Wards = await WardService.List(WardFilter);
            List<Customer_WardDTO> Customer_WardDTOs = Wards
                .Select(x => new Customer_WardDTO(x)).ToList();
            return Customer_WardDTOs;
        }
        [Route(CustomerRoute.SingleListEmailType), HttpPost]
        public async Task<ActionResult<List<Customer_EmailTypeDTO>>> SingleListEmailType([FromBody] Customer_EmailTypeFilterDTO Customer_EmailTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            EmailTypeFilter EmailTypeFilter = new EmailTypeFilter();
            EmailTypeFilter.Skip = 0;
            EmailTypeFilter.Take = int.MaxValue;
            EmailTypeFilter.Take = 20;
            EmailTypeFilter.OrderBy = EmailTypeOrder.Id;
            EmailTypeFilter.OrderType = OrderType.ASC;
            EmailTypeFilter.Selects = EmailTypeSelect.ALL;

            List<EmailType> EmailTypes = await EmailTypeService.List(EmailTypeFilter);
            List<Customer_EmailTypeDTO> Customer_EmailTypeDTOs = EmailTypes
                .Select(x => new Customer_EmailTypeDTO(x)).ToList();
            return Customer_EmailTypeDTOs;
        }
        [Route(CustomerRoute.SingleListCustomerFeedback), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerFeedbackDTO>>> SingleListCustomerFeedback([FromBody] Customer_CustomerFeedbackFilterDTO Customer_CustomerFeedbackFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFeedbackFilter CustomerFeedbackFilter = new CustomerFeedbackFilter();
            CustomerFeedbackFilter.Skip = 0;
            CustomerFeedbackFilter.Take = 20;
            CustomerFeedbackFilter.OrderBy = CustomerFeedbackOrder.Id;
            CustomerFeedbackFilter.OrderType = OrderType.ASC;
            CustomerFeedbackFilter.Selects = CustomerFeedbackSelect.ALL;
            CustomerFeedbackFilter.Id = Customer_CustomerFeedbackFilterDTO.Id;
            CustomerFeedbackFilter.CustomerId = Customer_CustomerFeedbackFilterDTO.CustomerId;
            CustomerFeedbackFilter.FullName = Customer_CustomerFeedbackFilterDTO.FullName;
            CustomerFeedbackFilter.Email = Customer_CustomerFeedbackFilterDTO.Email;
            CustomerFeedbackFilter.PhoneNumber = Customer_CustomerFeedbackFilterDTO.PhoneNumber;
            CustomerFeedbackFilter.CustomerFeedbackTypeId = Customer_CustomerFeedbackFilterDTO.CustomerFeedbackTypeId;
            CustomerFeedbackFilter.Title = Customer_CustomerFeedbackFilterDTO.Title;
            CustomerFeedbackFilter.SendDate = Customer_CustomerFeedbackFilterDTO.SendDate;
            CustomerFeedbackFilter.Content = Customer_CustomerFeedbackFilterDTO.Content;
            CustomerFeedbackFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<CustomerFeedback> CustomerFeedbacks = await CustomerFeedbackService.List(CustomerFeedbackFilter);
            List<Customer_CustomerFeedbackDTO> Customer_CustomerFeedbackDTOs = CustomerFeedbacks
                .Select(x => new Customer_CustomerFeedbackDTO(x)).ToList();
            return Customer_CustomerFeedbackDTOs;
        }
        [Route(CustomerRoute.SingleListCustomerFeedbackType), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerFeedbackTypeDTO>>> SingleListCustomerFeedbackType([FromBody] Customer_CustomerFeedbackTypeFilterDTO Customer_CustomerFeedbackTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFeedbackTypeFilter CustomerFeedbackTypeFilter = new CustomerFeedbackTypeFilter();
            CustomerFeedbackTypeFilter.Skip = 0;
            CustomerFeedbackTypeFilter.Take = int.MaxValue;
            CustomerFeedbackTypeFilter.Take = 20;
            CustomerFeedbackTypeFilter.OrderBy = CustomerFeedbackTypeOrder.Id;
            CustomerFeedbackTypeFilter.OrderType = OrderType.ASC;
            CustomerFeedbackTypeFilter.Selects = CustomerFeedbackTypeSelect.ALL;

            List<CustomerFeedbackType> CustomerFeedbackTypes = await CustomerFeedbackTypeService.List(CustomerFeedbackTypeFilter);
            List<Customer_CustomerFeedbackTypeDTO> Customer_CustomerFeedbackTypeDTOs = CustomerFeedbackTypes
                .Select(x => new Customer_CustomerFeedbackTypeDTO(x)).ToList();
            return Customer_CustomerFeedbackTypeDTOs;
        }
        [Route(CustomerRoute.SingleListPhoneType), HttpPost]
        public async Task<ActionResult<List<Customer_PhoneTypeDTO>>> SingleListPhoneType([FromBody] Customer_PhoneTypeFilterDTO Customer_PhoneTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PhoneTypeFilter PhoneTypeFilter = new PhoneTypeFilter();
            PhoneTypeFilter.Skip = 0;
            PhoneTypeFilter.Take = 20;
            PhoneTypeFilter.OrderBy = PhoneTypeOrder.Id;
            PhoneTypeFilter.OrderType = OrderType.ASC;
            PhoneTypeFilter.Selects = PhoneTypeSelect.ALL;
            PhoneTypeFilter.Id = Customer_PhoneTypeFilterDTO.Id;
            PhoneTypeFilter.Code = Customer_PhoneTypeFilterDTO.Code;
            PhoneTypeFilter.Name = Customer_PhoneTypeFilterDTO.Name;
            PhoneTypeFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            PhoneTypeFilter.RowId = Customer_PhoneTypeFilterDTO.RowId;

            List<PhoneType> PhoneTypes = await PhoneTypeService.List(PhoneTypeFilter);
            List<Customer_PhoneTypeDTO> Customer_PhoneTypeDTOs = PhoneTypes
                .Select(x => new Customer_PhoneTypeDTO(x)).ToList();
            return Customer_PhoneTypeDTOs;
        }
        [Route(CustomerRoute.SingleListEmailStatus), HttpPost]
        public async Task<ActionResult<List<Customer_EmailStatusDTO>>> SingleListEmailStatus([FromBody] Customer_EmailStatusFilterDTO Customer_EmailStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            EmailStatusFilter EmailStatusFilter = new EmailStatusFilter();
            EmailStatusFilter.Skip = 0;
            EmailStatusFilter.Take = 20;
            EmailStatusFilter.OrderBy = EmailStatusOrder.Id;
            EmailStatusFilter.OrderType = OrderType.ASC;
            EmailStatusFilter.Selects = EmailStatusSelect.ALL;
            EmailStatusFilter.Id = Customer_EmailStatusFilterDTO.Id;
            EmailStatusFilter.Code = Customer_EmailStatusFilterDTO.Code;
            EmailStatusFilter.Name = Customer_EmailStatusFilterDTO.Name;

            List<EmailStatus> Statuses = await EmailStatusService.List(EmailStatusFilter);
            List<Customer_EmailStatusDTO> Customer_EmailStatusDTOs = Statuses
                .Select(x => new Customer_EmailStatusDTO(x)).ToList();
            return Customer_EmailStatusDTOs;
        }
        [Route(CustomerRoute.SingleListContractStatus), HttpPost]
        public async Task<ActionResult<List<Customer_ContractStatusDTO>>> SingleListContractStatus([FromBody] Customer_ContractStatusFilterDTO Customer_ContractStatusFilterDTO)
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
            ContractStatusFilter.Id = Customer_ContractStatusFilterDTO.Id;
            ContractStatusFilter.Name = Customer_ContractStatusFilterDTO.Name;
            ContractStatusFilter.Code = Customer_ContractStatusFilterDTO.Code;

            List<ContractStatus> ContractStatuses = await ContractStatusService.List(ContractStatusFilter);
            List<Customer_ContractStatusDTO> Customer_ContractStatusDTOs = ContractStatuses
                .Select(x => new Customer_ContractStatusDTO(x)).ToList();
            return Customer_ContractStatusDTOs;
        }
        [Route(CustomerRoute.SingleListContractType), HttpPost]
        public async Task<ActionResult<List<Customer_ContractTypeDTO>>> SingleListContractType([FromBody] Customer_ContractTypeFilterDTO Customer_ContractTypeFilterDTO)
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
            ContractTypeFilter.Id = Customer_ContractTypeFilterDTO.Id;
            ContractTypeFilter.Name = Customer_ContractTypeFilterDTO.Name;
            ContractTypeFilter.Code = Customer_ContractTypeFilterDTO.Code;

            List<ContractType> ContractTypes = await ContractTypeService.List(ContractTypeFilter);
            List<Customer_ContractTypeDTO> Customer_ContractTypeDTOs = ContractTypes
                .Select(x => new Customer_ContractTypeDTO(x)).ToList();
            return Customer_ContractTypeDTOs;
        }
        [Route(CustomerRoute.SingleListCurrency), HttpPost]
        public async Task<ActionResult<List<Customer_CurrencyDTO>>> SingleListCurrency([FromBody] Customer_CurrencyFilterDTO Customer_CurrencyFilterDTO)
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
            CurrencyFilter.Id = Customer_CurrencyFilterDTO.Id;
            CurrencyFilter.Code = Customer_CurrencyFilterDTO.Code;
            CurrencyFilter.Name = Customer_CurrencyFilterDTO.Name;

            List<Currency> Currencies = await CurrencyService.List(CurrencyFilter);
            List<Customer_CurrencyDTO> Customer_CurrencyDTOs = Currencies
                .Select(x => new Customer_CurrencyDTO(x)).ToList();
            return Customer_CurrencyDTOs;
        }
        [Route(CustomerRoute.SingleListPaymentStatus), HttpPost]
        public async Task<ActionResult<List<Customer_PaymentStatusDTO>>> SingleListPaymentStatus([FromBody] Customer_PaymentStatusFilterDTO Customer_PaymentStatusFilterDTO)
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
            PaymentStatusFilter.Id = Customer_PaymentStatusFilterDTO.Id;
            PaymentStatusFilter.Name = Customer_PaymentStatusFilterDTO.Name;
            PaymentStatusFilter.Code = Customer_PaymentStatusFilterDTO.Code;

            List<PaymentStatus> PaymentStatuses = await PaymentStatusService.List(PaymentStatusFilter);
            List<Customer_PaymentStatusDTO> Customer_PaymentStatusDTOs = PaymentStatuses
                .Select(x => new Customer_PaymentStatusDTO(x)).ToList();
            return Customer_PaymentStatusDTOs;
        }

        [Route(CustomerRoute.SingleListOrderCategory), HttpPost]
        public async Task<ActionResult<List<Customer_OrderCategoryDTO>>> SingleListOrderCategory([FromBody] Customer_OrderCategoryFilterDTO Customer_OrderCategoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderCategoryFilter OrderCategoryFilter = new OrderCategoryFilter();
            OrderCategoryFilter.Skip = 0;
            OrderCategoryFilter.Take = int.MaxValue;
            OrderCategoryFilter.Take = 20;
            OrderCategoryFilter.OrderBy = OrderCategoryOrder.Id;
            OrderCategoryFilter.OrderType = OrderType.ASC;
            OrderCategoryFilter.Selects = OrderCategorySelect.ALL;

            List<OrderCategory> OrderCategorys = await OrderCategoryService.List(OrderCategoryFilter);
            List<Customer_OrderCategoryDTO> Customer_OrderCategoryDTOs = OrderCategorys
                .Select(x => new Customer_OrderCategoryDTO(x)).ToList();
            return Customer_OrderCategoryDTOs;
        }

        [Route(CustomerRoute.SingleListRepairStatus), HttpPost]
        public async Task<ActionResult<List<Customer_RepairStatusDTO>>> SingleListRepairStatus([FromBody] Customer_RepairStatusFilterDTO Customer_RepairStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid(); if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairStatusFilter RepairStatusFilter = new RepairStatusFilter();
            RepairStatusFilter.Skip = 0;
            RepairStatusFilter.Take = int.MaxValue;
            RepairStatusFilter.Take = 20;
            RepairStatusFilter.OrderBy = RepairStatusOrder.Id;
            RepairStatusFilter.OrderType = OrderType.ASC;
            RepairStatusFilter.Selects = RepairStatusSelect.ALL;

            List<RepairStatus> RepairStatuses = await RepairStatusService.List(RepairStatusFilter);
            List<Customer_RepairStatusDTO> Customer_RepairStatusDTOs = RepairStatuses
                .Select(x => new Customer_RepairStatusDTO(x)).ToList();
            return Customer_RepairStatusDTOs;
        }

        [Route(CustomerRoute.SingleListCustomerGrouping), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerGroupingDTO>>> SingleListCustomerGrouping([FromBody] Customer_CustomerGroupingFilterDTO Customer_CustomerGroupingFilterDTO)
        {
            if (UnAuthorization) return Forbid(); if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerGroupingFilter CustomerGroupingFilter = new CustomerGroupingFilter();
            CustomerGroupingFilter.Skip = 0;
            CustomerGroupingFilter.Take = int.MaxValue;
            CustomerGroupingFilter.OrderBy = CustomerGroupingOrder.Id;
            CustomerGroupingFilter.OrderType = OrderType.ASC;
            CustomerGroupingFilter.Selects = CustomerGroupingSelect.ALL;
            CustomerGroupingFilter.Id = Customer_CustomerGroupingFilterDTO.Id;
            CustomerGroupingFilter.Name = Customer_CustomerGroupingFilterDTO.Name;
            CustomerGroupingFilter.Code = Customer_CustomerGroupingFilterDTO.Code;
            CustomerGroupingFilter.Path = Customer_CustomerGroupingFilterDTO.Path;
            CustomerGroupingFilter.CustomerTypeId = Customer_CustomerGroupingFilterDTO.CustomerTypeId;
            CustomerGroupingFilter.Level = Customer_CustomerGroupingFilterDTO.Level;
            CustomerGroupingFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            CustomerGroupingFilter.Description = Customer_CustomerGroupingFilterDTO.Description;
            CustomerGroupingFilter.ParentId = Customer_CustomerGroupingFilterDTO.ParentId;

            List<CustomerGrouping> CustomerGroupings = await CustomerGroupingService.List(CustomerGroupingFilter);
            List<Customer_CustomerGroupingDTO> Customer_CustomerGroupingDTOs = CustomerGroupings
                .Select(x => new Customer_CustomerGroupingDTO(x)).ToList();
            return Customer_CustomerGroupingDTOs;
        }

        [Route(CustomerRoute.SingleListTicketIssueLevel), HttpPost]
        public async Task<ActionResult<List<Customer_TicketIssueLevelDTO>>> SingleListTicketIssueLevel([FromBody] Customer_TicketIssueLevelFilterDTO Customer_TicketIssueLevelFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketIssueLevelFilter TicketIssueLevelFilter = new TicketIssueLevelFilter();
            TicketIssueLevelFilter.Skip = 0;
            TicketIssueLevelFilter.Take = 20;
            TicketIssueLevelFilter.OrderBy = TicketIssueLevelOrder.Id;
            TicketIssueLevelFilter.OrderType = OrderType.ASC;
            TicketIssueLevelFilter.Selects = TicketIssueLevelSelect.ALL;
            TicketIssueLevelFilter.Id = Customer_TicketIssueLevelFilterDTO.Id;
            TicketIssueLevelFilter.Name = Customer_TicketIssueLevelFilterDTO.Name;
            TicketIssueLevelFilter.OrderNumber = Customer_TicketIssueLevelFilterDTO.OrderNumber;
            TicketIssueLevelFilter.TicketGroupId = Customer_TicketIssueLevelFilterDTO.TicketGroupId;
            TicketIssueLevelFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            TicketIssueLevelFilter.SLA = Customer_TicketIssueLevelFilterDTO.SLA;

            List<TicketIssueLevel> TicketIssueLevels = await TicketIssueLevelService.List(TicketIssueLevelFilter);
            List<Customer_TicketIssueLevelDTO> Customer_TicketIssueLevelDTOs = TicketIssueLevels
                .Select(x => new Customer_TicketIssueLevelDTO(x)).ToList();
            return Customer_TicketIssueLevelDTOs;
        }
        [Route(CustomerRoute.SingleListTicketPriority), HttpPost]
        public async Task<ActionResult<List<Customer_TicketPriorityDTO>>> SingleListTicketPriority([FromBody] Customer_TicketPriorityFilterDTO Customer_TicketPriorityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketPriorityFilter TicketPriorityFilter = new TicketPriorityFilter();
            TicketPriorityFilter.Skip = 0;
            TicketPriorityFilter.Take = 20;
            TicketPriorityFilter.OrderBy = TicketPriorityOrder.Id;
            TicketPriorityFilter.OrderType = OrderType.ASC;
            TicketPriorityFilter.Selects = TicketPrioritySelect.ALL;
            TicketPriorityFilter.Id = Customer_TicketPriorityFilterDTO.Id;
            TicketPriorityFilter.Name = Customer_TicketPriorityFilterDTO.Name;
            TicketPriorityFilter.OrderNumber = Customer_TicketPriorityFilterDTO.OrderNumber;
            TicketPriorityFilter.ColorCode = Customer_TicketPriorityFilterDTO.ColorCode;
            TicketPriorityFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<TicketPriority> TicketPriorities = await TicketPriorityService.List(TicketPriorityFilter);
            List<Customer_TicketPriorityDTO> Customer_TicketPriorityDTOs = TicketPriorities
                .Select(x => new Customer_TicketPriorityDTO(x)).ToList();
            return Customer_TicketPriorityDTOs;
        }
        [Route(CustomerRoute.SingleListTicketSource), HttpPost]
        public async Task<ActionResult<List<Customer_TicketSourceDTO>>> SingleListTicketSource([FromBody] Customer_TicketSourceFilterDTO Customer_TicketSourceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketSourceFilter TicketSourceFilter = new TicketSourceFilter();
            TicketSourceFilter.Skip = 0;
            TicketSourceFilter.Take = 20;
            TicketSourceFilter.OrderBy = TicketSourceOrder.Id;
            TicketSourceFilter.OrderType = OrderType.ASC;
            TicketSourceFilter.Selects = TicketSourceSelect.ALL;
            TicketSourceFilter.Id = Customer_TicketSourceFilterDTO.Id;
            TicketSourceFilter.Name = Customer_TicketSourceFilterDTO.Name;
            TicketSourceFilter.OrderNumber = Customer_TicketSourceFilterDTO.OrderNumber;
            TicketSourceFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<TicketSource> TicketSources = await TicketSourceService.List(TicketSourceFilter);
            List<Customer_TicketSourceDTO> Customer_TicketSourceDTOs = TicketSources
                .Select(x => new Customer_TicketSourceDTO(x)).ToList();
            return Customer_TicketSourceDTOs;
        }
        [Route(CustomerRoute.SingleListTicketStatus), HttpPost]
        public async Task<ActionResult<List<Customer_TicketStatusDTO>>> SingleListTicketStatus([FromBody] Customer_TicketStatusFilterDTO Customer_TicketStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketStatusFilter TicketStatusFilter = new TicketStatusFilter();
            TicketStatusFilter.Skip = 0;
            TicketStatusFilter.Take = 20;
            TicketStatusFilter.OrderBy = TicketStatusOrder.Id;
            TicketStatusFilter.OrderType = OrderType.ASC;
            TicketStatusFilter.Selects = TicketStatusSelect.ALL;
            TicketStatusFilter.Id = Customer_TicketStatusFilterDTO.Id;
            TicketStatusFilter.Name = Customer_TicketStatusFilterDTO.Name;
            TicketStatusFilter.OrderNumber = Customer_TicketStatusFilterDTO.OrderNumber;
            TicketStatusFilter.ColorCode = Customer_TicketStatusFilterDTO.ColorCode;
            TicketStatusFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<TicketStatus> TicketStatuses = await TicketStatusService.List(TicketStatusFilter);
            List<Customer_TicketStatusDTO> Customer_TicketStatusDTOs = TicketStatuses
                .Select(x => new Customer_TicketStatusDTO(x)).ToList();
            return Customer_TicketStatusDTOs;
        }

        [Route(CustomerRoute.SingleListTicketGroup), HttpPost]
        public async Task<ActionResult<List<Customer_TicketGroupDTO>>> SingleListTicketGroup([FromBody] Customer_TicketGroupFilterDTO Customer_TicketGroupFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketGroupFilter TicketGroupFilter = new TicketGroupFilter();
            TicketGroupFilter.Skip = 0;
            TicketGroupFilter.Take = 20;
            TicketGroupFilter.OrderBy = TicketGroupOrder.Id;
            TicketGroupFilter.OrderType = OrderType.ASC;
            TicketGroupFilter.Selects = TicketGroupSelect.ALL;
            TicketGroupFilter.Id = Customer_TicketGroupFilterDTO.Id;
            TicketGroupFilter.Name = Customer_TicketGroupFilterDTO.Name;
            TicketGroupFilter.OrderNumber = Customer_TicketGroupFilterDTO.OrderNumber;
            TicketGroupFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            TicketGroupFilter.TicketTypeId = Customer_TicketGroupFilterDTO.TicketTypeId;

            List<TicketGroup> TicketGroups = await TicketGroupService.List(TicketGroupFilter);
            List<Customer_TicketGroupDTO> TicketIssueLevel_TicketGroupDTOs = TicketGroups
                .Select(x => new Customer_TicketGroupDTO(x)).ToList();
            return TicketIssueLevel_TicketGroupDTOs;
        }
        [Route(CustomerRoute.SingleListTicketResolveType), HttpPost]
        public async Task<ActionResult<List<Customer_TicketResolveTypeDTO>>> SingleListTicketResolveType([FromBody] Customer_TicketResolveTypeFilterDTO Customer_TicketResolveTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketResolveTypeFilter TicketResolveTypeFilter = new TicketResolveTypeFilter();
            TicketResolveTypeFilter.Skip = 0;
            TicketResolveTypeFilter.Take = 20;
            TicketResolveTypeFilter.OrderBy = TicketResolveTypeOrder.Id;
            TicketResolveTypeFilter.OrderType = OrderType.ASC;
            TicketResolveTypeFilter.Selects = TicketResolveTypeSelect.ALL;
            TicketResolveTypeFilter.Id = Customer_TicketResolveTypeFilterDTO.Id;
            TicketResolveTypeFilter.Code = Customer_TicketResolveTypeFilterDTO.Code;
            TicketResolveTypeFilter.Name = Customer_TicketResolveTypeFilterDTO.Name;

            List<TicketResolveType> TicketResolveTypes = await TicketResolveTypeService.List(TicketResolveTypeFilter);
            List<Customer_TicketResolveTypeDTO> Customer_TicketResolveTypeDTOs = TicketResolveTypes
                .Select(x => new Customer_TicketResolveTypeDTO(x)).ToList();
            return Customer_TicketResolveTypeDTOs;
        }
        [Route(CustomerRoute.SingleListTicketType), HttpPost]
        public async Task<ActionResult<List<Customer_TicketTypeDTO>>> SingleListTicketType([FromBody] Customer_TicketTypeFilterDTO Customer_TicketTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter();
            TicketTypeFilter.Skip = 0;
            TicketTypeFilter.Take = 20;
            TicketTypeFilter.OrderBy = TicketTypeOrder.Id;
            TicketTypeFilter.OrderType = OrderType.ASC;
            TicketTypeFilter.Selects = TicketTypeSelect.ALL;
            TicketTypeFilter.Id = Customer_TicketTypeFilterDTO.Id;
            TicketTypeFilter.Code = Customer_TicketTypeFilterDTO.Code;
            TicketTypeFilter.Name = Customer_TicketTypeFilterDTO.Name;
            TicketTypeFilter.ColorCode = Customer_TicketTypeFilterDTO.ColorCode;
            TicketTypeFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);
            List<Customer_TicketTypeDTO> Customer_TicketTypeDTOs = TicketTypes
                .Select(x => new Customer_TicketTypeDTO(x)).ToList();
            return Customer_TicketTypeDTOs;
        }
        [Route(CustomerRoute.SingleListMailTemplate), HttpPost]
        public async Task<ActionResult<List<Customer_MailTemplateDTO>>> SingleListMailTemplate([FromBody] Customer_MailTemplateFilterDTO Customer_MailTemplateFilterDTO)
        {
            if (UnAuthorization) return Forbid(); if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MailTemplateFilter MailTemplateFilter = new MailTemplateFilter();
            MailTemplateFilter.Skip = 0;
            MailTemplateFilter.Take = 20;
            MailTemplateFilter.OrderBy = MailTemplateOrder.Id;
            MailTemplateFilter.OrderType = OrderType.ASC;
            MailTemplateFilter.Selects = MailTemplateSelect.ALL;
            MailTemplateFilter.Id = Customer_MailTemplateFilterDTO.Id;
            MailTemplateFilter.Code = Customer_MailTemplateFilterDTO.Code;
            MailTemplateFilter.Name = Customer_MailTemplateFilterDTO.Name;
            MailTemplateFilter.Content = Customer_MailTemplateFilterDTO.Content;

            List<MailTemplate> MailTemplates = await MailTemplateService.List(MailTemplateFilter);
            List<Customer_MailTemplateDTO> CustomerLead_MailTemplateDTOs = MailTemplates
                .Select(x => new Customer_MailTemplateDTO(x)).ToList();
            return CustomerLead_MailTemplateDTOs;
        }

        [Route(CustomerRoute.SingleListCustomer), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerDTO>>> SingleListCustomer([FromBody] Customer_CustomerFilterDTO Customer_CustomerFilterDTO)
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
            CustomerFilter.Id = Customer_CustomerFilterDTO.Id;
            CustomerFilter.Code = Customer_CustomerFilterDTO.Code;
            CustomerFilter.Name = Customer_CustomerFilterDTO.Name;
            CustomerFilter.Phone = Customer_CustomerFilterDTO.Phone;
            CustomerFilter.Address = Customer_CustomerFilterDTO.Address;
            CustomerFilter.NationId = Customer_CustomerFilterDTO.NationId;
            CustomerFilter.ProvinceId = Customer_CustomerFilterDTO.ProvinceId;
            CustomerFilter.DistrictId = Customer_CustomerFilterDTO.DistrictId;
            CustomerFilter.WardId = Customer_CustomerFilterDTO.WardId;
            CustomerFilter.CustomerTypeId = Customer_CustomerFilterDTO.CustomerTypeId;
            CustomerFilter.Birthday = Customer_CustomerFilterDTO.Birthday;
            CustomerFilter.Email = Customer_CustomerFilterDTO.Email;
            CustomerFilter.ProfessionId = Customer_CustomerFilterDTO.ProfessionId;
            CustomerFilter.CustomerResourceId = Customer_CustomerFilterDTO.CustomerResourceId;
            CustomerFilter.SexId = Customer_CustomerFilterDTO.SexId;
            CustomerFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            CustomerFilter.CompanyId = Customer_CustomerFilterDTO.CompanyId;
            CustomerFilter.ParentCompanyId = Customer_CustomerFilterDTO.ParentCompanyId;
            CustomerFilter.TaxCode = Customer_CustomerFilterDTO.TaxCode;
            CustomerFilter.Fax = Customer_CustomerFilterDTO.Fax;
            CustomerFilter.Website = Customer_CustomerFilterDTO.Website;
            CustomerFilter.NumberOfEmployee = Customer_CustomerFilterDTO.NumberOfEmployee;
            CustomerFilter.BusinessTypeId = Customer_CustomerFilterDTO.BusinessTypeId;
            CustomerFilter.Investment = Customer_CustomerFilterDTO.Investment;
            CustomerFilter.RevenueAnnual = Customer_CustomerFilterDTO.RevenueAnnual;
            CustomerFilter.Descreption = Customer_CustomerFilterDTO.Descreption;

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<Customer_CustomerDTO> Customer_CustomerDTOs = Customers
                .Select(x => new Customer_CustomerDTO(x)).ToList();
            return Customer_CustomerDTOs;
        }
        [Route(CustomerRoute.SingleListEditedPriceStatus), HttpPost]
        public async Task<ActionResult<List<Customer_EditedPriceStatusDTO>>> SingleListEditedPriceStatus([FromBody] Customer_EditedPriceStatusFilterDTO Customer_EditedPriceStatusFilterDTO)
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
            List<Customer_EditedPriceStatusDTO> Customer_EditedPriceStatusDTOs = EditedPriceStatuses
                .Select(x => new Customer_EditedPriceStatusDTO(x)).ToList();
            return Customer_EditedPriceStatusDTOs;
        }
    }
}

