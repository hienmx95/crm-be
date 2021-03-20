using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using Microsoft.AspNetCore.Mvc;
using CRM.Entities;
using CRM.Enums;

namespace CRM.Rpc.contact
{
    public partial class ContactController : RpcController
    {
        [Route(ContactRoute.FilterListCustomerLeadSource), HttpPost]
        public async Task<ActionResult<List<Contact_CustomerLeadSourceDTO>>> FilterListCustomerLeadSource([FromBody] Contact_CustomerLeadSourceFilterDTO Contact_CustomerLeadSourceFilterDTO)
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
            CustomerLeadSourceFilter.Id = Contact_CustomerLeadSourceFilterDTO.Id;
            CustomerLeadSourceFilter.Code = Contact_CustomerLeadSourceFilterDTO.Code;
            CustomerLeadSourceFilter.Name = Contact_CustomerLeadSourceFilterDTO.Name;

            List<CustomerLeadSource> CustomerLeadSources = await CustomerLeadSourceService.List(CustomerLeadSourceFilter);
            List<Contact_CustomerLeadSourceDTO> Contact_CustomerLeadSourceDTOs = CustomerLeadSources
                .Select(x => new Contact_CustomerLeadSourceDTO(x)).ToList();
            return Contact_CustomerLeadSourceDTOs;
        }
        [Route(ContactRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<Contact_AppUserDTO>>> FilterListAppUser([FromBody] Contact_AppUserFilterDTO Contact_AppUserFilterDTO)
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
            AppUserFilter.Id = Contact_AppUserFilterDTO.Id;
            AppUserFilter.Username = Contact_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Contact_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = Contact_AppUserFilterDTO.Address;
            AppUserFilter.Email = Contact_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Contact_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = Contact_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = Contact_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = Contact_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = Contact_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = Contact_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = Contact_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = Contact_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = Contact_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = Contact_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = Contact_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Contact_AppUserDTO> Contact_AppUserDTOs = AppUsers
                .Select(x => new Contact_AppUserDTO(x)).ToList();
            return Contact_AppUserDTOs;
        }
        [Route(ContactRoute.FilterListContactStatus), HttpPost]
        public async Task<ActionResult<List<Contact_ContactStatusDTO>>> FilterListContactStatus([FromBody] Contact_ContactStatusFilterDTO Contact_ContactStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactStatusFilter ContactStatusFilter = new ContactStatusFilter();
            ContactStatusFilter.Skip = 0;
            ContactStatusFilter.Take = 20;
            ContactStatusFilter.OrderBy = ContactStatusOrder.Id;
            ContactStatusFilter.OrderType = OrderType.ASC;
            ContactStatusFilter.Selects = ContactStatusSelect.ALL;
            ContactStatusFilter.Id = Contact_ContactStatusFilterDTO.Id;
            ContactStatusFilter.Code = Contact_ContactStatusFilterDTO.Code;
            ContactStatusFilter.Name = Contact_ContactStatusFilterDTO.Name;

            List<ContactStatus> ContactStatuses = await ContactStatusService.List(ContactStatusFilter);
            List<Contact_ContactStatusDTO> Contact_ContactStatusDTOs = ContactStatuses
                .Select(x => new Contact_ContactStatusDTO(x)).ToList();
            return Contact_ContactStatusDTOs;
        }
        [Route(ContactRoute.FilterListCompany), HttpPost]
        public async Task<ActionResult<List<Contact_CompanyDTO>>> FilterListCompany([FromBody] Contact_CompanyFilterDTO Contact_CompanyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyFilter CompanyFilter = new CompanyFilter();
            CompanyFilter.Skip = 0;
            CompanyFilter.Take = 20;
            CompanyFilter.OrderBy = CompanyOrder.Id;
            CompanyFilter.OrderType = OrderType.ASC;
            CompanyFilter.Selects = CompanySelect.ALL;
            CompanyFilter.Id = Contact_CompanyFilterDTO.Id;
            CompanyFilter.Name = Contact_CompanyFilterDTO.Name;
            CompanyFilter.Phone = Contact_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = Contact_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = Contact_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = Contact_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = Contact_CompanyFilterDTO.EmailOther;
            CompanyFilter.Description = Contact_CompanyFilterDTO.Description;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<Contact_CompanyDTO> Contact_CompanyDTOs = Companys
                .Select(x => new Contact_CompanyDTO(x)).ToList();
            return Contact_CompanyDTOs;
        }
        [Route(ContactRoute.FilterListActivityStatus), HttpPost]
        public async Task<ActionResult<List<Contact_ActivityStatusDTO>>> FilterListActivityStatus([FromBody] Contact_ActivityStatusFilterDTO Contact_ActivityStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ActivityStatusFilter ActivityStatusFilter = new ActivityStatusFilter();
            ActivityStatusFilter.Skip = 0;
            ActivityStatusFilter.Take = 20;
            ActivityStatusFilter.OrderBy = ActivityStatusOrder.Id;
            ActivityStatusFilter.OrderType = OrderType.ASC;
            ActivityStatusFilter.Selects = ActivityStatusSelect.ALL;
            ActivityStatusFilter.Id = Contact_ActivityStatusFilterDTO.Id;
            ActivityStatusFilter.Code = Contact_ActivityStatusFilterDTO.Code;
            ActivityStatusFilter.Name = Contact_ActivityStatusFilterDTO.Name;

            List<ActivityStatus> ActivityStatuses = await ActivityStatusService.List(ActivityStatusFilter);
            List<Contact_ActivityStatusDTO> Contact_ActivityStatusDTOs = ActivityStatuses
                .Select(x => new Contact_ActivityStatusDTO(x)).ToList();
            return Contact_ActivityStatusDTOs;
        }
        [Route(ContactRoute.FilterListActivityType), HttpPost]
        public async Task<ActionResult<List<Contact_ActivityTypeDTO>>> FilterListActivityType([FromBody] Contact_ActivityTypeFilterDTO Contact_ActivityTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid(); if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ActivityTypeFilter ActivityTypeFilter = new ActivityTypeFilter();
            ActivityTypeFilter.Skip = 0;
            ActivityTypeFilter.Take = 20;
            ActivityTypeFilter.OrderBy = ActivityTypeOrder.Id;
            ActivityTypeFilter.OrderType = OrderType.ASC;
            ActivityTypeFilter.Selects = ActivityTypeSelect.ALL;
            ActivityTypeFilter.Id = Contact_ActivityTypeFilterDTO.Id;
            ActivityTypeFilter.Code = Contact_ActivityTypeFilterDTO.Code;
            ActivityTypeFilter.Name = Contact_ActivityTypeFilterDTO.Name;

            List<ActivityType> ActivityTypes = await ActivityTypeService.List(ActivityTypeFilter);
            List<Contact_ActivityTypeDTO> Contact_ActivityTypeDTOs = ActivityTypes
                .Select(x => new Contact_ActivityTypeDTO(x)).ToList();
            return Contact_ActivityTypeDTOs;
        }
        [Route(ContactRoute.FilterListOrderQuoteStatus), HttpPost]
        public async Task<ActionResult<List<Contact_OrderQuoteStatusDTO>>> FilterListOrderQuoteStatus([FromBody] Contact_OrderQuoteStatusFilterDTO Contact_OrderQuoteStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteStatusFilter OrderQuoteStatusFilter = new OrderQuoteStatusFilter();
            OrderQuoteStatusFilter.Skip = 0;
            OrderQuoteStatusFilter.Take = 20;
            OrderQuoteStatusFilter.OrderBy = OrderQuoteStatusOrder.Id;
            OrderQuoteStatusFilter.OrderType = OrderType.ASC;
            OrderQuoteStatusFilter.Selects = OrderQuoteStatusSelect.ALL;
            OrderQuoteStatusFilter.Id = Contact_OrderQuoteStatusFilterDTO.Id;
            OrderQuoteStatusFilter.Code = Contact_OrderQuoteStatusFilterDTO.Code;
            OrderQuoteStatusFilter.Name = Contact_OrderQuoteStatusFilterDTO.Name;

            List<OrderQuoteStatus> OrderQuoteStatuses = await OrderQuoteStatusService.List(OrderQuoteStatusFilter);
            List<Contact_OrderQuoteStatusDTO> Contact_OrderQuoteStatusDTOs = OrderQuoteStatuses
                .Select(x => new Contact_OrderQuoteStatusDTO(x)).ToList();
            return Contact_OrderQuoteStatusDTOs;
        }
        [Route(ContactRoute.FilterListEmailStatus), HttpPost]
        public async Task<ActionResult<List<Contact_EmailStatusDTO>>> FilterListEmailStatus([FromBody] Contact_EmailStatusFilterDTO Contact_EmailStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            EmailStatusFilter EmailStatusFilter = new EmailStatusFilter();
            EmailStatusFilter.Skip = 0;
            EmailStatusFilter.Take = 20;
            EmailStatusFilter.OrderBy = EmailStatusOrder.Id;
            EmailStatusFilter.OrderType = OrderType.ASC;
            EmailStatusFilter.Selects = EmailStatusSelect.ALL;
            EmailStatusFilter.Id = Contact_EmailStatusFilterDTO.Id;
            EmailStatusFilter.Code = Contact_EmailStatusFilterDTO.Code;
            EmailStatusFilter.Name = Contact_EmailStatusFilterDTO.Name;

            List<EmailStatus> Statuses = await EmailStatusService.List(EmailStatusFilter);
            List<Contact_EmailStatusDTO> Contact_EmailStatusDTOs = Statuses
                .Select(x => new Contact_EmailStatusDTO(x)).ToList();
            return Contact_EmailStatusDTOs;
        }
        [Route(ContactRoute.FilterListSaleStage), HttpPost]
        public async Task<ActionResult<List<Contact_SaleStageDTO>>> FilterListSaleStage([FromBody] Contact_SaleStageFilterDTO Contact_SaleStageFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SaleStageFilter SaleStageFilter = new SaleStageFilter();
            SaleStageFilter.Skip = 0;
            SaleStageFilter.Take = 20;
            SaleStageFilter.OrderBy = SaleStageOrder.Id;
            SaleStageFilter.OrderType = OrderType.ASC;
            SaleStageFilter.Selects = SaleStageSelect.ALL;
            SaleStageFilter.Id = Contact_SaleStageFilterDTO.Id;
            SaleStageFilter.Code = Contact_SaleStageFilterDTO.Code;
            SaleStageFilter.Name = Contact_SaleStageFilterDTO.Name;

            List<SaleStage> SaleStages = await SaleStageService.List(SaleStageFilter);
            List<Contact_SaleStageDTO> Contact_SaleStageDTOs = SaleStages
                .Select(x => new Contact_SaleStageDTO(x)).ToList();
            return Contact_SaleStageDTOs;
        }
        [Route(ContactRoute.FilterListContact), HttpPost]
        public async Task<ActionResult<List<Contact_ContactDTO>>> FilterListContact([FromBody] Contact_ContactFilterDTO Contact_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid(); if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = new ContactFilter();
            ContactFilter.Skip = 0;
            ContactFilter.Take = 20;
            ContactFilter.OrderBy = ContactOrder.Id;
            ContactFilter.OrderType = OrderType.ASC;
            ContactFilter.Selects = ContactSelect.ALL;
            ContactFilter.Id = Contact_ContactFilterDTO.Id;
            ContactFilter.Name = Contact_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = Contact_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = Contact_ContactFilterDTO.CompanyId;

            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<Contact_ContactDTO> Contact_ContactDTOs = Contacts
                .Select(x => new Contact_ContactDTO(x)).ToList();
            return Contact_ContactDTOs;
        }
        [Route(ContactRoute.FilterListOrderPaymentStatus), HttpPost]
        public async Task<ActionResult<List<Contact_OrderPaymentStatusDTO>>> FilterListOrderPaymentStatus([FromBody] Contact_OrderPaymentStatusFilterDTO Contact_OrderPaymentStatusFilterDTO)
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
            List<Contact_OrderPaymentStatusDTO> Contact_OrderPaymentStatusDTOs = OrderPaymentStatuses
                .Select(x => new Contact_OrderPaymentStatusDTO(x)).ToList();
            return Contact_OrderPaymentStatusDTOs;
        }
        [Route(ContactRoute.FilterListRequestState), HttpPost]
        public async Task<ActionResult<List<Contact_RequestStateDTO>>> FilterListRequestState([FromBody] Contact_RequestStateFilterDTO Contact_RequestStateFilterDTO)
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
            RequestStateFilter.Id = Contact_RequestStateFilterDTO.Id;
            RequestStateFilter.Code = Contact_RequestStateFilterDTO.Code;
            RequestStateFilter.Name = Contact_RequestStateFilterDTO.Name;

            List<RequestState> RequestStates = await RequestStateService.List(RequestStateFilter);
            List<Contact_RequestStateDTO> Contact_RequestStateDTOs = RequestStates
                .Select(x => new Contact_RequestStateDTO(x)).ToList();
            return Contact_RequestStateDTOs;
        }

        [Route(ContactRoute.SingleListDistrict), HttpPost]
        public async Task<ActionResult<List<Contact_DistrictDTO>>> SingleListDistrict([FromBody] Contact_DistrictFilterDTO Contact_DistrictFilterDTO)
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
            DistrictFilter.Id = Contact_DistrictFilterDTO.Id;
            DistrictFilter.Code = Contact_DistrictFilterDTO.Code;
            DistrictFilter.Name = Contact_DistrictFilterDTO.Name;
            DistrictFilter.Priority = Contact_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = Contact_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<Contact_DistrictDTO> Contact_DistrictDTOs = Districts
                .Select(x => new Contact_DistrictDTO(x)).ToList();
            return Contact_DistrictDTOs;
        }
        [Route(ContactRoute.SingleListProfession), HttpPost]
        public async Task<ActionResult<List<Contact_ProfessionDTO>>> SingleListProfession([FromBody] Contact_ProfessionFilterDTO Contact_ProfessionFilterDTO)
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
            ProfessionFilter.Id = Contact_ProfessionFilterDTO.Id;
            ProfessionFilter.Code = Contact_ProfessionFilterDTO.Code;
            ProfessionFilter.Name = Contact_ProfessionFilterDTO.Name;

            List<Profession> Professions = await ProfessionService.List(ProfessionFilter);
            List<Contact_ProfessionDTO> Contact_ProfessionDTOs = Professions
                .Select(x => new Contact_ProfessionDTO(x)).ToList();
            return Contact_ProfessionDTOs;
        }
        [Route(ContactRoute.SingleListCustomerLeadSource), HttpPost]
        public async Task<ActionResult<List<Contact_CustomerLeadSourceDTO>>> SingleListCustomerLeadSource([FromBody] Contact_CustomerLeadSourceFilterDTO Contact_CustomerLeadSourceFilterDTO)
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
            CustomerLeadSourceFilter.Id = Contact_CustomerLeadSourceFilterDTO.Id;
            CustomerLeadSourceFilter.Code = Contact_CustomerLeadSourceFilterDTO.Code;
            CustomerLeadSourceFilter.Name = Contact_CustomerLeadSourceFilterDTO.Name;

            List<CustomerLeadSource> CustomerLeadSources = await CustomerLeadSourceService.List(CustomerLeadSourceFilter);
            List<Contact_CustomerLeadSourceDTO> Contact_CustomerLeadSourceDTOs = CustomerLeadSources
                .Select(x => new Contact_CustomerLeadSourceDTO(x)).ToList();
            return Contact_CustomerLeadSourceDTOs;
        }
        [Route(ContactRoute.SingleListNation), HttpPost]
        public async Task<ActionResult<List<Contact_NationDTO>>> SingleListNation([FromBody] Contact_NationFilterDTO Contact_NationFilterDTO)
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
            NationFilter.Id = Contact_NationFilterDTO.Id;
            NationFilter.Name = Contact_NationFilterDTO.Name;
            NationFilter.Priority = Contact_NationFilterDTO.DisplayOrder;
            NationFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Nation> Nations = await NationService.List(NationFilter);
            List<Contact_NationDTO> Contact_NationDTOs = Nations
                .Select(x => new Contact_NationDTO(x)).ToList();
            return Contact_NationDTOs;
        }
        [Route(ContactRoute.SingleListProvince), HttpPost]
        public async Task<ActionResult<List<Contact_ProvinceDTO>>> SingleListProvince([FromBody] Contact_ProvinceFilterDTO Contact_ProvinceFilterDTO)
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
            ProvinceFilter.Id = Contact_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = Contact_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = Contact_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = Contact_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<Contact_ProvinceDTO> Contact_ProvinceDTOs = Provinces
                .Select(x => new Contact_ProvinceDTO(x)).ToList();
            return Contact_ProvinceDTOs;
        }
        [Route(ContactRoute.SingleListAppUser), HttpPost]
        public async Task<ActionResult<List<Contact_AppUserDTO>>> SingleListAppUser([FromBody] Contact_AppUserFilterDTO Contact_AppUserFilterDTO)
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
            AppUserFilter.Id = Contact_AppUserFilterDTO.Id;
            AppUserFilter.Username = Contact_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Contact_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = Contact_AppUserFilterDTO.Address;
            AppUserFilter.Email = Contact_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Contact_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = Contact_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = Contact_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = Contact_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = Contact_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = Contact_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = Contact_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = Contact_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = Contact_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = Contact_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Contact_AppUserDTO> Contact_AppUserDTOs = AppUsers
                .Select(x => new Contact_AppUserDTO(x)).ToList();
            return Contact_AppUserDTOs;
        }
        [Route(ContactRoute.SingleListContactStatus), HttpPost]
        public async Task<ActionResult<List<Contact_ContactStatusDTO>>> SingleListContactStatus([FromBody] Contact_ContactStatusFilterDTO Contact_ContactStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactStatusFilter ContactStatusFilter = new ContactStatusFilter();
            ContactStatusFilter.Skip = 0;
            ContactStatusFilter.Take = 20;
            ContactStatusFilter.OrderBy = ContactStatusOrder.Id;
            ContactStatusFilter.OrderType = OrderType.ASC;
            ContactStatusFilter.Selects = ContactStatusSelect.ALL;
            ContactStatusFilter.Id = Contact_ContactStatusFilterDTO.Id;
            ContactStatusFilter.Code = Contact_ContactStatusFilterDTO.Code;
            ContactStatusFilter.Name = Contact_ContactStatusFilterDTO.Name;

            List<ContactStatus> ContactStatuses = await ContactStatusService.List(ContactStatusFilter);
            List<Contact_ContactStatusDTO> Contact_ContactStatusDTOs = ContactStatuses
                .Select(x => new Contact_ContactStatusDTO(x)).ToList();
            return Contact_ContactStatusDTOs;
        }
        [Route(ContactRoute.SingleListCompany), HttpPost]
        public async Task<ActionResult<List<Contact_CompanyDTO>>> SingleListCompany([FromBody] Contact_CompanyFilterDTO Contact_CompanyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyFilter CompanyFilter = new CompanyFilter();
            CompanyFilter.Skip = 0;
            CompanyFilter.Take = 20;
            CompanyFilter.OrderBy = CompanyOrder.Id;
            CompanyFilter.OrderType = OrderType.ASC;
            CompanyFilter.Selects = CompanySelect.ALL;
            CompanyFilter.Id = Contact_CompanyFilterDTO.Id;
            CompanyFilter.Name = Contact_CompanyFilterDTO.Name;
            CompanyFilter.Phone = Contact_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = Contact_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = Contact_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = Contact_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = Contact_CompanyFilterDTO.EmailOther;
            CompanyFilter.Description = Contact_CompanyFilterDTO.Description;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<Contact_CompanyDTO> Contact_CompanyDTOs = Companys
                .Select(x => new Contact_CompanyDTO(x)).ToList();
            return Contact_CompanyDTOs;
        }
        [Route(ContactRoute.SingleListSex), HttpPost]
        public async Task<ActionResult<List<Contact_SexDTO>>> SingleListSex([FromBody] Contact_SexFilterDTO Contact_SexFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SexFilter SexFilter = new SexFilter();
            SexFilter.Skip = 0;
            SexFilter.Take = 20;
            SexFilter.OrderBy = SexOrder.Id;
            SexFilter.OrderType = OrderType.ASC;
            SexFilter.Selects = SexSelect.ALL;
            SexFilter.Id = Contact_SexFilterDTO.Id;
            SexFilter.Code = Contact_SexFilterDTO.Code;
            SexFilter.Name = Contact_SexFilterDTO.Name;

            List<Sex> Sexes = await SexService.List(SexFilter);
            List<Contact_SexDTO> Contact_SexDTOs = Sexes
                .Select(x => new Contact_SexDTO(x)).ToList();
            return Contact_SexDTOs;
        }
        [Route(ContactRoute.SingleListOrganization), HttpPost]
        public async Task<ActionResult<List<Contact_OrganizationDTO>>> SingleListOrganization([FromBody] Contact_OrganizationFilterDTO Contact_OrganizationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = 20;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = Contact_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = Contact_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = Contact_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = Contact_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = Contact_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = Contact_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            OrganizationFilter.Phone = Contact_OrganizationFilterDTO.Phone;
            OrganizationFilter.Email = Contact_OrganizationFilterDTO.Email;
            OrganizationFilter.Address = Contact_OrganizationFilterDTO.Address;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<Contact_OrganizationDTO> Contact_OrganizationDTOs = Organizations
                .Select(x => new Contact_OrganizationDTO(x)).ToList();
            return Contact_OrganizationDTOs;
        }
        [Route(ContactRoute.SingleListPosition), HttpPost]
        public async Task<ActionResult<List<Contact_PositionDTO>>> SingleListPosition([FromBody] Contact_PositionFilterDTO Contact_PositionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PositionFilter PositionFilter = new PositionFilter();
            PositionFilter.Skip = 0;
            PositionFilter.Take = 20;
            PositionFilter.OrderBy = PositionOrder.Id;
            PositionFilter.OrderType = OrderType.ASC;
            PositionFilter.Selects = PositionSelect.ALL;
            PositionFilter.Id = Contact_PositionFilterDTO.Id;
            PositionFilter.Code = Contact_PositionFilterDTO.Code;
            PositionFilter.Name = Contact_PositionFilterDTO.Name;
            PositionFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Position> Positions = await PositionService.List(PositionFilter);
            List<Contact_PositionDTO> Contact_PositionDTOs = Positions
                .Select(x => new Contact_PositionDTO(x)).ToList();
            return Contact_PositionDTOs;
        }
        [Route(ContactRoute.SingleListSmsTemplate), HttpPost]
        public async Task<ActionResult<List<Contact_SmsTemplateDTO>>> SingleListSmsTemplate([FromBody] Contact_SmsTemplateFilterDTO Contact_SmsTemplateFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SmsTemplateFilter SmsTemplateFilter = new SmsTemplateFilter();
            SmsTemplateFilter.Skip = 0;
            SmsTemplateFilter.Take = 20;
            SmsTemplateFilter.OrderBy = SmsTemplateOrder.Id;
            SmsTemplateFilter.OrderType = OrderType.ASC;
            SmsTemplateFilter.Selects = SmsTemplateSelect.ALL;
            SmsTemplateFilter.Id = Contact_SmsTemplateFilterDTO.Id;
            SmsTemplateFilter.Code = Contact_SmsTemplateFilterDTO.Code;
            SmsTemplateFilter.Name = Contact_SmsTemplateFilterDTO.Name;
            SmsTemplateFilter.Content = Contact_SmsTemplateFilterDTO.Content;

            List<SmsTemplate> SmsTemplates = await SmsTemplateService.List(SmsTemplateFilter);
            List<Contact_SmsTemplateDTO> Contact_SmsTemplateDTOs = SmsTemplates
                .Select(x => new Contact_SmsTemplateDTO(x)).ToList();
            return Contact_SmsTemplateDTOs;
        }
        [Route(ContactRoute.SingleListEmailTemplate), HttpPost]
        public async Task<ActionResult<List<Contact_MailTemplateDTO>>> SingleListMailTemplate([FromBody] Contact_MailTemplateFilterDTO Contact_MailTemplateFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MailTemplateFilter MailTemplateFilter = new MailTemplateFilter();
            MailTemplateFilter.Skip = 0;
            MailTemplateFilter.Take = 20;
            MailTemplateFilter.OrderBy = MailTemplateOrder.Id;
            MailTemplateFilter.OrderType = OrderType.ASC;
            MailTemplateFilter.Selects = MailTemplateSelect.ALL;
            MailTemplateFilter.Id = Contact_MailTemplateFilterDTO.Id;
            MailTemplateFilter.Code = Contact_MailTemplateFilterDTO.Code;
            MailTemplateFilter.Name = Contact_MailTemplateFilterDTO.Name;
            MailTemplateFilter.Content = Contact_MailTemplateFilterDTO.Content;

            List<MailTemplate> MailTemplates = await MailTemplateService.List(MailTemplateFilter);
            List<Contact_MailTemplateDTO> CustomerLead_MailTemplateDTOs = MailTemplates
                .Select(x => new Contact_MailTemplateDTO(x)).ToList();
            return CustomerLead_MailTemplateDTOs;
        }
        [Route(ContactRoute.SingleListOpportunity), HttpPost]
        public async Task<ActionResult<List<Contact_OpportunityDTO>>> SingleListOpportunity([FromBody] Contact_OpportunityFilterDTO Contact_OpportunityFilterDTO)
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
            OpportunityFilter.Id = Contact_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = Contact_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = Contact_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.ClosingDate = Contact_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = Contact_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = Contact_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = Contact_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = Contact_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.Amount = Contact_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = Contact_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = Contact_OpportunityFilterDTO.Description;

            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<Contact_OpportunityDTO> Contact_OpportunityDTOs = Opportunities
                .Select(x => new Contact_OpportunityDTO(x)).ToList();
            return Contact_OpportunityDTOs;
        }
        [Route(ContactRoute.SingleListUnitOfMeasure), HttpPost]
        public async Task<ActionResult<List<Contact_UnitOfMeasureDTO>>> SingleListUnitOfMeasure([FromBody] Contact_UnitOfMeasureFilterDTO Contact_UnitOfMeasureFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            List<Product> Products = await ProductService.List(new ProductFilter
            {
                Id = Contact_UnitOfMeasureFilterDTO.ProductId,
                Selects = ProductSelect.Id,
            });
            long ProductId = Products.Select(p => p.Id).FirstOrDefault();
            Product Product = await ProductService.Get(ProductId);

            List<Contact_UnitOfMeasureDTO> Contact_UnitOfMeasureDTOs = new List<Contact_UnitOfMeasureDTO>();
            if (Product.UnitOfMeasureGrouping != null)
            {
                Contact_UnitOfMeasureDTOs = Product.UnitOfMeasureGrouping.UnitOfMeasureGroupingContents.Select(x => new Contact_UnitOfMeasureDTO(x)).ToList();
            }
            Contact_UnitOfMeasureDTO Contact_UnitOfMeasureDTO = new Contact_UnitOfMeasureDTO
            {
                Id = Product.UnitOfMeasure.Id,
                Code = Product.UnitOfMeasure.Code,
                Name = Product.UnitOfMeasure.Name,
                Description = Product.UnitOfMeasure.Description,
                StatusId = StatusEnum.ACTIVE.Id,
                Factor = 1,
            };
            Contact_UnitOfMeasureDTOs.Add(Contact_UnitOfMeasureDTO);
            Contact_UnitOfMeasureDTOs = Contact_UnitOfMeasureDTOs.Distinct().ToList();
            return Contact_UnitOfMeasureDTOs;
        }
        [Route(ContactRoute.SingleListTaxType), HttpPost]
        public async Task<ActionResult<List<Contact_TaxTypeDTO>>> SingleListTaxType([FromBody] Contact_TaxTypeFilterDTO Contact_TaxTypeFilterDTO)
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
            TaxTypeFilter.Id = Contact_TaxTypeFilterDTO.Id;
            TaxTypeFilter.Code = Contact_TaxTypeFilterDTO.Code;
            TaxTypeFilter.Name = Contact_TaxTypeFilterDTO.Name;
            TaxTypeFilter.StatusId = new IdFilter { Equal = Enums.StatusEnum.ACTIVE.Id };

            List<TaxType> TaxTypes = await TaxTypeService.List(TaxTypeFilter);
            List<Contact_TaxTypeDTO> Contact_TaxTypeDTOs = TaxTypes
                .Select(x => new Contact_TaxTypeDTO(x)).ToList();
            return Contact_TaxTypeDTOs;
        }
        [Route(ContactRoute.SingleListSupplier), HttpPost]
        public async Task<ActionResult<List<Contact_SupplierDTO>>> SingleListSupplier([FromBody] Contact_SupplierFilterDTO Contact_SupplierFilterDTO)
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
            SupplierFilter.Id = Contact_SupplierFilterDTO.Id;
            SupplierFilter.Code = Contact_SupplierFilterDTO.Code;
            SupplierFilter.Name = Contact_SupplierFilterDTO.Name;
            SupplierFilter.TaxCode = Contact_SupplierFilterDTO.TaxCode;
            SupplierFilter.Phone = Contact_SupplierFilterDTO.Phone;
            SupplierFilter.Email = Contact_SupplierFilterDTO.Email;
            SupplierFilter.Address = Contact_SupplierFilterDTO.Address;
            SupplierFilter.ProvinceId = Contact_SupplierFilterDTO.ProvinceId;
            SupplierFilter.DistrictId = Contact_SupplierFilterDTO.DistrictId;
            SupplierFilter.WardId = Contact_SupplierFilterDTO.WardId;
            SupplierFilter.OwnerName = Contact_SupplierFilterDTO.OwnerName;
            SupplierFilter.PersonInChargeId = Contact_SupplierFilterDTO.PersonInChargeId;
            SupplierFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            SupplierFilter.Description = Contact_SupplierFilterDTO.Description;

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<Contact_SupplierDTO> Contact_SupplierDTOs = Suppliers
                .Select(x => new Contact_SupplierDTO(x)).ToList();
            return Contact_SupplierDTOs;
        }
        [Route(ContactRoute.SingleListProductType), HttpPost]
        public async Task<ActionResult<List<Contact_ProductTypeDTO>>> SingleListProductType([FromBody] Contact_ProductTypeFilterDTO Contact_ProductTypeFilterDTO)
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
            ProductTypeFilter.Id = Contact_ProductTypeFilterDTO.Id;
            ProductTypeFilter.Code = Contact_ProductTypeFilterDTO.Code;
            ProductTypeFilter.Name = Contact_ProductTypeFilterDTO.Name;
            ProductTypeFilter.Description = Contact_ProductTypeFilterDTO.Description;
            ProductTypeFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            if (ProductTypeFilter.Id == null) ProductTypeFilter.Id = new IdFilter();
            ProductTypeFilter.Id.In = await FilterProductType(ProductTypeService, CurrentContext);

            List<ProductType> ProductTypes = await ProductTypeService.List(ProductTypeFilter);
            List<Contact_ProductTypeDTO> Contact_ProductTypeDTOs = ProductTypes
                .Select(x => new Contact_ProductTypeDTO(x)).ToList();
            return Contact_ProductTypeDTOs;
        }
        [Route(ContactRoute.SingleListProductGrouping), HttpPost]
        public async Task<ActionResult<List<Contact_ProductGroupingDTO>>> SingleListProductGrouping([FromBody] Contact_ProductGroupingFilterDTO Contact_ProductGroupingFilterDTO)
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
            List<Contact_ProductGroupingDTO> Contact_ProductGroupingDTOs = ProductGroupings
                .Select(x => new Contact_ProductGroupingDTO(x)).ToList();
            return Contact_ProductGroupingDTOs;
        }
        [Route(ContactRoute.SingleListOrderQuoteStatus), HttpPost]
        public async Task<ActionResult<List<Contact_OrderQuoteStatusDTO>>> SingleListOrderQuoteStatus([FromBody] Contact_OrderQuoteStatusFilterDTO Contact_OrderQuoteStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteStatusFilter OrderQuoteStatusFilter = new OrderQuoteStatusFilter();
            OrderQuoteStatusFilter.Skip = 0;
            OrderQuoteStatusFilter.Take = 20;
            OrderQuoteStatusFilter.OrderBy = OrderQuoteStatusOrder.Id;
            OrderQuoteStatusFilter.OrderType = OrderType.ASC;
            OrderQuoteStatusFilter.Selects = OrderQuoteStatusSelect.ALL;
            OrderQuoteStatusFilter.Id = Contact_OrderQuoteStatusFilterDTO.Id;
            OrderQuoteStatusFilter.Code = Contact_OrderQuoteStatusFilterDTO.Code;
            OrderQuoteStatusFilter.Name = Contact_OrderQuoteStatusFilterDTO.Name;

            List<OrderQuoteStatus> OrderQuoteStatuses = await OrderQuoteStatusService.List(OrderQuoteStatusFilter);
            List<Contact_OrderQuoteStatusDTO> Company_OrderQuoteStatusDTOs = OrderQuoteStatuses
                .Select(x => new Contact_OrderQuoteStatusDTO(x)).ToList();
            return Company_OrderQuoteStatusDTOs;
        }
        [Route(ContactRoute.SingleListActivityStatus), HttpPost]
        public async Task<ActionResult<List<Contact_ActivityStatusDTO>>> SingleListActivityStatus([FromBody] Contact_ActivityStatusFilterDTO Contact_ActivityStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ActivityStatusFilter ActivityStatusFilter = new ActivityStatusFilter();
            ActivityStatusFilter.Skip = 0;
            ActivityStatusFilter.Take = 20;
            ActivityStatusFilter.OrderBy = ActivityStatusOrder.Id;
            ActivityStatusFilter.OrderType = OrderType.ASC;
            ActivityStatusFilter.Selects = ActivityStatusSelect.ALL;
            ActivityStatusFilter.Id = Contact_ActivityStatusFilterDTO.Id;
            ActivityStatusFilter.Code = Contact_ActivityStatusFilterDTO.Code;
            ActivityStatusFilter.Name = Contact_ActivityStatusFilterDTO.Name;

            List<ActivityStatus> ActivityStatuses = await ActivityStatusService.List(ActivityStatusFilter);
            List<Contact_ActivityStatusDTO> Contact_ActivityStatusDTOs = ActivityStatuses
                .Select(x => new Contact_ActivityStatusDTO(x)).ToList();
            return Contact_ActivityStatusDTOs;
        }
        [Route(ContactRoute.SingleListActivityType), HttpPost]
        public async Task<ActionResult<List<Contact_ActivityTypeDTO>>> SingleListActivityType([FromBody] Contact_ActivityTypeFilterDTO Contact_ActivityTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ActivityTypeFilter ActivityTypeFilter = new ActivityTypeFilter();
            ActivityTypeFilter.Skip = 0;
            ActivityTypeFilter.Take = 20;
            ActivityTypeFilter.OrderBy = ActivityTypeOrder.Id;
            ActivityTypeFilter.OrderType = OrderType.ASC;
            ActivityTypeFilter.Selects = ActivityTypeSelect.ALL;
            ActivityTypeFilter.Id = Contact_ActivityTypeFilterDTO.Id;
            ActivityTypeFilter.Code = Contact_ActivityTypeFilterDTO.Code;
            ActivityTypeFilter.Name = Contact_ActivityTypeFilterDTO.Name;

            List<ActivityType> ActivityTypes = await ActivityTypeService.List(ActivityTypeFilter);
            List<Contact_ActivityTypeDTO> Contact_ActivityTypeDTOs = ActivityTypes
                .Select(x => new Contact_ActivityTypeDTO(x)).ToList();
            return Contact_ActivityTypeDTOs;
        }
        [Route(ContactRoute.SingleListActivityPriority), HttpPost]
        public async Task<ActionResult<List<Contact_ActivityPriorityDTO>>> SingleListActivityPriority([FromBody] Contact_ActivityPriorityFilterDTO Contact_ActivityPriorityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ActivityPriorityFilter ActivityPriorityFilter = new ActivityPriorityFilter();
            ActivityPriorityFilter.Skip = 0;
            ActivityPriorityFilter.Take = 20;
            ActivityPriorityFilter.OrderBy = ActivityPriorityOrder.Id;
            ActivityPriorityFilter.OrderType = OrderType.ASC;
            ActivityPriorityFilter.Selects = ActivityPrioritySelect.ALL;
            ActivityPriorityFilter.Id = Contact_ActivityPriorityFilterDTO.Id;
            ActivityPriorityFilter.Code = Contact_ActivityPriorityFilterDTO.Code;
            ActivityPriorityFilter.Name = Contact_ActivityPriorityFilterDTO.Name;

            List<ActivityPriority> ActivityPriorities = await ActivityPriorityService.List(ActivityPriorityFilter);
            List<Contact_ActivityPriorityDTO> Contact_ActivityPriorityDTOs = ActivityPriorities
                .Select(x => new Contact_ActivityPriorityDTO(x)).ToList();
            return Contact_ActivityPriorityDTOs;
        }
        [Route(ContactRoute.SingleListFileType), HttpPost]
        public async Task<ActionResult<List<Contact_FileTypeDTO>>> SingleListFileType([FromBody] Contact_FileTypeFilterDTO Contact_FileTypeFilterDTO)
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
            FileTypeFilter.Id = Contact_FileTypeFilterDTO.Id;
            FileTypeFilter.Code = Contact_FileTypeFilterDTO.Code;
            FileTypeFilter.Name = Contact_FileTypeFilterDTO.Name;

            List<FileType> FileTypes = await FileTypeService.List(FileTypeFilter);
            List<Contact_FileTypeDTO> Contact_FileTypeDTOs = FileTypes
                .Select(x => new Contact_FileTypeDTO(x)).ToList();
            return Contact_FileTypeDTOs;
        }
        [Route(ContactRoute.SingleListContact), HttpPost]
        public async Task<ActionResult<List<Contact_ContactDTO>>> SingleListContact([FromBody] Contact_ContactFilterDTO Contact_ContactFilterDTO)
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
            ContactFilter.Id = Contact_ContactFilterDTO.Id;
            ContactFilter.Name = Contact_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = Contact_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = Contact_ContactFilterDTO.CompanyId;

            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<Contact_ContactDTO> Contact_ContactDTOs = Contacts
                .Select(x => new Contact_ContactDTO(x)).ToList();
            return Contact_ContactDTOs;
        }
        [Route(ContactRoute.SingleListCompanyStatus), HttpPost]
        public async Task<ActionResult<List<Contact_CompanyStatusDTO>>> SingleListCompanyStatus([FromBody] Contact_CompanyStatusFilterDTO Contact_CompanyStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyStatusFilter CompanyStatusFilter = new CompanyStatusFilter();
            CompanyStatusFilter.Skip = 0;
            CompanyStatusFilter.Take = 20;
            CompanyStatusFilter.OrderType = OrderType.ASC;
            CompanyStatusFilter.Selects = CompanyStatusSelect.ALL;
            CompanyStatusFilter.Id = Contact_CompanyStatusFilterDTO.Id;
            CompanyStatusFilter.Code = Contact_CompanyStatusFilterDTO.Code;
            CompanyStatusFilter.Name = Contact_CompanyStatusFilterDTO.Name;

            List<CompanyStatus> CompanyStatuses = await CompanyStatusService.List(CompanyStatusFilter);
            List<Contact_CompanyStatusDTO> Contact_CompanyStatusDTOs = CompanyStatuses
                .Select(x => new Contact_CompanyStatusDTO(x)).ToList();
            return Contact_CompanyStatusDTOs;
        }
        [Route(ContactRoute.SingleListEditedPriceStatus), HttpPost]
        public async Task<ActionResult<List<Contact_EditedPriceStatusDTO>>> SingleListEditedPriceStatus([FromBody] Contact_EditedPriceStatusFilterDTO Contact_EditedPriceStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            EditedPriceStatusFilter EditedPriceStatusFilter = new EditedPriceStatusFilter();
            EditedPriceStatusFilter.Skip = 0;
            EditedPriceStatusFilter.Take = 20;
            EditedPriceStatusFilter.OrderBy = EditedPriceStatusOrder.Id;
            EditedPriceStatusFilter.OrderType = OrderType.ASC;
            EditedPriceStatusFilter.Selects = EditedPriceStatusSelect.ALL;
            EditedPriceStatusFilter.Id = Contact_EditedPriceStatusFilterDTO.Id;
            EditedPriceStatusFilter.Code = Contact_EditedPriceStatusFilterDTO.Code;
            EditedPriceStatusFilter.Name = Contact_EditedPriceStatusFilterDTO.Name;

            List<EditedPriceStatus> EditedPriceStatuses = await EditedPriceStatusService.List(EditedPriceStatusFilter);
            List<Contact_EditedPriceStatusDTO> Contact_EditedPriceStatusDTOs = EditedPriceStatuses
                .Select(x => new Contact_EditedPriceStatusDTO(x)).ToList();
            return Contact_EditedPriceStatusDTOs;
        }
        [Route(ContactRoute.SingleListProbability), HttpPost]
        public async Task<ActionResult<List<Contact_ProbabilityDTO>>> SingleListProbability([FromBody] Contact_ProbabilityFilterDTO Contact_ProbabilityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProbabilityFilter ProbabilityFilter = new ProbabilityFilter();
            ProbabilityFilter.Skip = 0;
            ProbabilityFilter.Take = 20;
            ProbabilityFilter.OrderBy = ProbabilityOrder.Id;
            ProbabilityFilter.OrderType = OrderType.ASC;
            ProbabilityFilter.Selects = ProbabilitySelect.ALL;
            ProbabilityFilter.Id = Contact_ProbabilityFilterDTO.Id;
            ProbabilityFilter.Code = Contact_ProbabilityFilterDTO.Code;
            ProbabilityFilter.Name = Contact_ProbabilityFilterDTO.Name;

            List<Probability> Probabilities = await ProbabilityService.List(ProbabilityFilter);
            List<Contact_ProbabilityDTO> Contact_ProbabilityDTOs = Probabilities
                .Select(x => new Contact_ProbabilityDTO(x)).ToList();
            return Contact_ProbabilityDTOs;
        }
        [Route(ContactRoute.SingleListSaleStage), HttpPost]
        public async Task<ActionResult<List<Contact_SaleStageDTO>>> SingleListSaleStage([FromBody] Contact_SaleStageFilterDTO Contact_SaleStageFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SaleStageFilter SaleStageFilter = new SaleStageFilter();
            SaleStageFilter.Skip = 0;
            SaleStageFilter.Take = 20;
            SaleStageFilter.OrderBy = SaleStageOrder.Id;
            SaleStageFilter.OrderType = OrderType.ASC;
            SaleStageFilter.Selects = SaleStageSelect.ALL;
            SaleStageFilter.Id = Contact_SaleStageFilterDTO.Id;
            SaleStageFilter.Code = Contact_SaleStageFilterDTO.Code;
            SaleStageFilter.Name = Contact_SaleStageFilterDTO.Name;

            List<SaleStage> SaleStages = await SaleStageService.List(SaleStageFilter);
            List<Contact_SaleStageDTO> Contact_SaleStageDTOs = SaleStages
                .Select(x => new Contact_SaleStageDTO(x)).ToList();
            return Contact_SaleStageDTOs;
        }
    }
}

