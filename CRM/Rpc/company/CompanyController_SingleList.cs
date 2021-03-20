using CRM.Common;
using CRM.Entities;
using CRM.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.company
{
    public partial class CompanyController : RpcController
    {
        [Route(CompanyRoute.FilterListContact), HttpPost]
        public async Task<ActionResult<List<Company_ContactDTO>>> FilterListContact([FromBody] Company_ContactFilterDTO Company_ContactFilterDTO)
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
            ContactFilter.Id = Company_ContactFilterDTO.Id;
            ContactFilter.Name = Company_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = Company_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = Company_ContactFilterDTO.CompanyId;

            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<Company_ContactDTO> Company_ContactDTOs = Contacts
                .Select(x => new Company_ContactDTO(x)).ToList();
            return Company_ContactDTOs;
        }
        [Route(CompanyRoute.FilterListProfession), HttpPost]
        public async Task<ActionResult<List<Company_ProfessionDTO>>> FilterListProfession([FromBody] Company_ProfessionFilterDTO Company_ProfessionFilterDTO)
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
            ProfessionFilter.Id = Company_ProfessionFilterDTO.Id;
            ProfessionFilter.Code = Company_ProfessionFilterDTO.Code;
            ProfessionFilter.Name = Company_ProfessionFilterDTO.Name;

            List<Profession> Professions = await ProfessionService.List(ProfessionFilter);
            List<Company_ProfessionDTO> Company_ProfessionDTOs = Professions
                .Select(x => new Company_ProfessionDTO(x)).ToList();
            return Company_ProfessionDTOs;
        }
        [Route(CompanyRoute.FilterListCompany), HttpPost]
        public async Task<ActionResult<List<Company_CompanyDTO>>> FilterListCompany([FromBody] Company_CompanyFilterDTO Company_CompanyFilterDTO)
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
            CompanyFilter.Id = Company_CompanyFilterDTO.Id;
            CompanyFilter.Name = Company_CompanyFilterDTO.Name;
            CompanyFilter.Phone = Company_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = Company_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = Company_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = Company_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = Company_CompanyFilterDTO.EmailOther;
            CompanyFilter.Description = Company_CompanyFilterDTO.Description;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<Company_CompanyDTO> Company_CompanyDTOs = Companys
                .Select(x => new Company_CompanyDTO(x)).ToList();
            return Company_CompanyDTOs;
        }
        [Route(CompanyRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<Company_AppUserDTO>>> FilterListAppUser([FromBody] Company_AppUserFilterDTO Company_AppUserFilterDTO)
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
            AppUserFilter.Id = Company_AppUserFilterDTO.Id;
            AppUserFilter.Username = Company_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Company_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = Company_AppUserFilterDTO.Address;
            AppUserFilter.Email = Company_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Company_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = Company_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = Company_AppUserFilterDTO.Birthday;
            AppUserFilter.Department = Company_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = Company_AppUserFilterDTO.OrganizationId;
            AppUserFilter.StatusId = Company_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Company_AppUserDTO> Company_AppUserDTOs = AppUsers
                .Select(x => new Company_AppUserDTO(x)).ToList();
            return Company_AppUserDTOs;
        }
        [Route(CompanyRoute.FilterListCompanyStatus), HttpPost]
        public async Task<ActionResult<List<Company_CompanyStatusDTO>>> FilterListCompanyStatus([FromBody] Company_CompanyStatusFilterDTO Company_CompanyStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyStatusFilter CompanyStatusFilter = new CompanyStatusFilter();
            CompanyStatusFilter.Skip = 0;
            CompanyStatusFilter.Take = 20;
            CompanyStatusFilter.OrderBy = Company_CompanyStatusFilterDTO.OrderBy;
            CompanyStatusFilter.OrderType = Company_CompanyStatusFilterDTO.OrderType;
            CompanyStatusFilter.Selects = CompanyStatusSelect.ALL;
            CompanyStatusFilter.Id = Company_CompanyStatusFilterDTO.Id;
            CompanyStatusFilter.Code = Company_CompanyStatusFilterDTO.Code;
            CompanyStatusFilter.Name = Company_CompanyStatusFilterDTO.Name;

            List<CompanyStatus> CompanyStatuses = await CompanyStatusService.List(CompanyStatusFilter);
            List<Company_CompanyStatusDTO> Company_CompanyStatusDTOs = CompanyStatuses
                .Select(x => new Company_CompanyStatusDTO(x)).ToList();
            return Company_CompanyStatusDTOs;
        }
        [Route(CompanyRoute.FilterListActivityStatus), HttpPost]
        public async Task<ActionResult<List<Company_ActivityStatusDTO>>> FilterListActivityStatus([FromBody] Company_ActivityStatusFilterDTO Company_ActivityStatusFilterDTO)
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
            ActivityStatusFilter.Id = Company_ActivityStatusFilterDTO.Id;
            ActivityStatusFilter.Code = Company_ActivityStatusFilterDTO.Code;
            ActivityStatusFilter.Name = Company_ActivityStatusFilterDTO.Name;

            List<ActivityStatus> ActivityStatuses = await ActivityStatusService.List(ActivityStatusFilter);
            List<Company_ActivityStatusDTO> Company_ActivityStatusDTOs = ActivityStatuses
                .Select(x => new Company_ActivityStatusDTO(x)).ToList();
            return Company_ActivityStatusDTOs;
        }
        [Route(CompanyRoute.FilterListActivityType), HttpPost]
        public async Task<ActionResult<List<Company_ActivityTypeDTO>>> FilterListActivityType([FromBody] Company_ActivityTypeFilterDTO Company_ActivityTypeFilterDTO)
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
            ActivityTypeFilter.Id = Company_ActivityTypeFilterDTO.Id;
            ActivityTypeFilter.Code = Company_ActivityTypeFilterDTO.Code;
            ActivityTypeFilter.Name = Company_ActivityTypeFilterDTO.Name;

            List<ActivityType> ActivityTypes = await ActivityTypeService.List(ActivityTypeFilter);
            List<Company_ActivityTypeDTO> Company_ActivityTypeDTOs = ActivityTypes
                .Select(x => new Company_ActivityTypeDTO(x)).ToList();
            return Company_ActivityTypeDTOs;
        }
        [Route(CompanyRoute.FilterListPosition), HttpPost]
        public async Task<ActionResult<List<Company_PositionDTO>>> FilterListPosition([FromBody] Company_PositionFilterDTO Company_PositionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            PositionFilter PositionFilter = new PositionFilter();
            PositionFilter.Skip = 0;
            PositionFilter.Take = 99999;
            PositionFilter.OrderBy = PositionOrder.Id;
            PositionFilter.OrderType = OrderType.ASC;
            PositionFilter.Selects = PositionSelect.ALL;
            PositionFilter.Id = Company_PositionFilterDTO.Id;
            PositionFilter.Code = Company_PositionFilterDTO.Code;
            PositionFilter.Name = Company_PositionFilterDTO.Name;
            PositionFilter.StatusId = Company_PositionFilterDTO.StatusId;

            List<Position> Positions = await PositionService.List(PositionFilter);
            List<Company_PositionDTO> Company_PositionDTOs = Positions
                .Select(x => new Company_PositionDTO(x)).ToList();
            return Company_PositionDTOs;
        }
        [Route(CompanyRoute.FilterListSaleStage), HttpPost]
        public async Task<ActionResult<List<Company_SaleStageDTO>>> FilterListSaleStage([FromBody] Company_SaleStageFilterDTO Company_SaleStageFilterDTO)
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
            SaleStageFilter.Id = Company_SaleStageFilterDTO.Id;
            SaleStageFilter.Code = Company_SaleStageFilterDTO.Code;
            SaleStageFilter.Name = Company_SaleStageFilterDTO.Name;

            List<SaleStage> SaleStages = await SaleStageService.List(SaleStageFilter);
            List<Company_SaleStageDTO> Company_SaleStageDTOs = SaleStages
                .Select(x => new Company_SaleStageDTO(x)).ToList();
            return Company_SaleStageDTOs;
        }
        [Route(CompanyRoute.FilterListOrderQuoteStatus), HttpPost]
        public async Task<ActionResult<List<Company_OrderQuoteStatusDTO>>> FilterListOrderQuoteStatus([FromBody] Company_OrderQuoteStatusFilterDTO Company_OrderQuoteStatusFilterDTO)
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
            OrderQuoteStatusFilter.Id = Company_OrderQuoteStatusFilterDTO.Id;
            OrderQuoteStatusFilter.Code = Company_OrderQuoteStatusFilterDTO.Code;
            OrderQuoteStatusFilter.Name = Company_OrderQuoteStatusFilterDTO.Name;

            List<OrderQuoteStatus> OrderQuoteStatuses = await OrderQuoteStatusService.List(OrderQuoteStatusFilter);
            List<Company_OrderQuoteStatusDTO> Company_OrderQuoteStatusDTOs = OrderQuoteStatuses
                .Select(x => new Company_OrderQuoteStatusDTO(x)).ToList();
            return Company_OrderQuoteStatusDTOs;
        }
        [Route(CompanyRoute.FilterListOrganization), HttpPost]
        public async Task<ActionResult<List<Company_OrganizationDTO>>> FilterListOrganization([FromBody] Company_OrganizationFilterDTO Company_OrganizationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = 99999;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = Company_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = Company_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = Company_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = Company_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = Company_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = Company_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = Company_OrganizationFilterDTO.StatusId;
            OrganizationFilter.Phone = Company_OrganizationFilterDTO.Phone;
            OrganizationFilter.Address = Company_OrganizationFilterDTO.Address;
            OrganizationFilter.Email = Company_OrganizationFilterDTO.Email;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<Company_OrganizationDTO> Company_OrganizationDTOs = Organizations
                .Select(x => new Company_OrganizationDTO(x)).ToList();
            return Company_OrganizationDTOs;
        }
        [Route(CompanyRoute.FilterListEmailStatus), HttpPost]
        public async Task<ActionResult<List<Company_EmailStatusDTO>>> FilterListEmailStatus([FromBody] Company_EmailStatusFilterDTO Company_EmailStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            EmailStatusFilter EmailStatusFilter = new EmailStatusFilter();
            EmailStatusFilter.Skip = 0;
            EmailStatusFilter.Take = 20;
            EmailStatusFilter.OrderBy = EmailStatusOrder.Id;
            EmailStatusFilter.OrderType = OrderType.ASC;
            EmailStatusFilter.Selects = EmailStatusSelect.ALL;
            EmailStatusFilter.Id = Company_EmailStatusFilterDTO.Id;
            EmailStatusFilter.Code = Company_EmailStatusFilterDTO.Code;
            EmailStatusFilter.Name = Company_EmailStatusFilterDTO.Name;

            List<EmailStatus> Statuses = await EmailStatusService.List(EmailStatusFilter);
            List<Company_EmailStatusDTO> Company_EmailStatusDTOs = Statuses
                .Select(x => new Company_EmailStatusDTO(x)).ToList();
            return Company_EmailStatusDTOs;
        }
        [Route(CompanyRoute.FilterListOrderPaymentStatus), HttpPost]
        public async Task<ActionResult<List<Company_OrderPaymentStatusDTO>>> FilterListOrderPaymentStatus([FromBody] Company_OrderPaymentStatusFilterDTO Company_OrderPaymentStatusFilterDTO)
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
            List<Company_OrderPaymentStatusDTO> Company_OrderPaymentStatusDTOs = OrderPaymentStatuses
                .Select(x => new Company_OrderPaymentStatusDTO(x)).ToList();
            return Company_OrderPaymentStatusDTOs;
        }
        [Route(CompanyRoute.FilterListRequestState), HttpPost]
        public async Task<ActionResult<List<Company_RequestStateDTO>>> FilterListRequestState([FromBody] Company_RequestStateFilterDTO Company_RequestStateFilterDTO)
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
            RequestStateFilter.Id = Company_RequestStateFilterDTO.Id;
            RequestStateFilter.Code = Company_RequestStateFilterDTO.Code;
            RequestStateFilter.Name = Company_RequestStateFilterDTO.Name;

            List<RequestState> RequestStates = await RequestStateService.List(RequestStateFilter);
            List<Company_RequestStateDTO> Company_RequestStateDTOs = RequestStates
                .Select(x => new Company_RequestStateDTO(x)).ToList();
            return Company_RequestStateDTOs;
        }
        [Route(CompanyRoute.FilterListStore), HttpPost]
        public async Task<ActionResult<List<Company_StoreDTO>>> FilterListStore([FromBody] Company_StoreFilterDTO Company_StoreFilterDTO)
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
            StoreFilter.Id = Company_StoreFilterDTO.Id;
            StoreFilter.Code = Company_StoreFilterDTO.Code;
            StoreFilter.CodeDraft = Company_StoreFilterDTO.CodeDraft;
            StoreFilter.Name = Company_StoreFilterDTO.Name;
            StoreFilter.ParentStoreId = Company_StoreFilterDTO.ParentStoreId;
            StoreFilter.OrganizationId = Company_StoreFilterDTO.OrganizationId;
            StoreFilter.StoreTypeId = Company_StoreFilterDTO.StoreTypeId;
            StoreFilter.StoreGroupingId = Company_StoreFilterDTO.StoreGroupingId;
            StoreFilter.Telephone = Company_StoreFilterDTO.Telephone;
            StoreFilter.ProvinceId = Company_StoreFilterDTO.ProvinceId;
            StoreFilter.DistrictId = Company_StoreFilterDTO.DistrictId;
            StoreFilter.WardId = Company_StoreFilterDTO.WardId;
            StoreFilter.Address = Company_StoreFilterDTO.Address;
            StoreFilter.DeliveryAddress = Company_StoreFilterDTO.DeliveryAddress;
            StoreFilter.Latitude = Company_StoreFilterDTO.Latitude;
            StoreFilter.Longitude = Company_StoreFilterDTO.Longitude;
            StoreFilter.DeliveryLatitude = Company_StoreFilterDTO.DeliveryLatitude;
            StoreFilter.DeliveryLongitude = Company_StoreFilterDTO.DeliveryLongitude;
            StoreFilter.OwnerName = Company_StoreFilterDTO.OwnerName;
            StoreFilter.OwnerPhone = Company_StoreFilterDTO.OwnerPhone;
            StoreFilter.OwnerEmail = Company_StoreFilterDTO.OwnerEmail;
            StoreFilter.StatusId = Company_StoreFilterDTO.StatusId;

            if (StoreFilter.Id == null) StoreFilter.Id = new IdFilter();
            StoreFilter.Id.In = await FilterStore(StoreService, OrganizationService, CurrentContext);

            List<Store> Stores = await StoreService.List(StoreFilter);
            List<Company_StoreDTO> Company_StoreDTOs = Stores
                .Select(x => new Company_StoreDTO(x)).ToList();
            return Company_StoreDTOs;
        }
        [Route(CompanyRoute.FilterListEditedPriceStatus), HttpPost]
        public async Task<ActionResult<List<Company_EditedPriceStatusDTO>>> FilterListEditedPriceStatus([FromBody] Company_EditedPriceStatusFilterDTO Company_EditedPriceStatusFilterDTO)
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
            List<Company_EditedPriceStatusDTO> Company_EditedPriceStatusDTOs = EditedPriceStatuses
                .Select(x => new Company_EditedPriceStatusDTO(x)).ToList();
            return Company_EditedPriceStatusDTOs;
        }
        [Route(CompanyRoute.FilterListContractStatus), HttpPost]
        public async Task<ActionResult<List<Company_ContractStatusDTO>>> FilterListContractStatus([FromBody] Company_ContractStatusFilterDTO Company_ContractStatusFilterDTO)
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
            ContractStatusFilter.Id = Company_ContractStatusFilterDTO.Id;
            ContractStatusFilter.Name = Company_ContractStatusFilterDTO.Name;
            ContractStatusFilter.Code = Company_ContractStatusFilterDTO.Code;

            List<ContractStatus> ContractStatuses = await ContractStatusService.List(ContractStatusFilter);
            List<Company_ContractStatusDTO> Company_ContractStatusDTOs = ContractStatuses
                .Select(x => new Company_ContractStatusDTO(x)).ToList();
            return Company_ContractStatusDTOs;
        }
        [Route(CompanyRoute.FilterListContractType), HttpPost]
        public async Task<ActionResult<List<Company_ContractTypeDTO>>> FilterListContractType([FromBody] Company_ContractTypeFilterDTO Company_ContractTypeFilterDTO)
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
            ContractTypeFilter.Id = Company_ContractTypeFilterDTO.Id;
            ContractTypeFilter.Name = Company_ContractTypeFilterDTO.Name;
            ContractTypeFilter.Code = Company_ContractTypeFilterDTO.Code;

            List<ContractType> ContractTypes = await ContractTypeService.List(ContractTypeFilter);
            List<Company_ContractTypeDTO> Company_ContractTypeDTOs = ContractTypes
                .Select(x => new Company_ContractTypeDTO(x)).ToList();
            return Company_ContractTypeDTOs;
        }
        [Route(CompanyRoute.FilterListCurrency), HttpPost]
        public async Task<ActionResult<List<Company_CurrencyDTO>>> FilterListCurrency([FromBody] Company_CurrencyFilterDTO Company_CurrencyFilterDTO)
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
            CurrencyFilter.Id = Company_CurrencyFilterDTO.Id;
            CurrencyFilter.Code = Company_CurrencyFilterDTO.Code;
            CurrencyFilter.Name = Company_CurrencyFilterDTO.Name;

            List<Currency> Currencies = await CurrencyService.List(CurrencyFilter);
            List<Company_CurrencyDTO> Company_CurrencyDTOs = Currencies
                .Select(x => new Company_CurrencyDTO(x)).ToList();
            return Company_CurrencyDTOs;
        }
        [Route(CompanyRoute.FilterListPaymentStatus), HttpPost]
        public async Task<ActionResult<List<Company_PaymentStatusDTO>>> FilterListPaymentStatus([FromBody] Company_PaymentStatusFilterDTO Company_PaymentStatusFilterDTO)
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
            PaymentStatusFilter.Id = Company_PaymentStatusFilterDTO.Id;
            PaymentStatusFilter.Name = Company_PaymentStatusFilterDTO.Name;
            PaymentStatusFilter.Code = Company_PaymentStatusFilterDTO.Code;

            List<PaymentStatus> PaymentStatuses = await PaymentStatusService.List(PaymentStatusFilter);
            List<Company_PaymentStatusDTO> Company_PaymentStatusDTOs = PaymentStatuses
                .Select(x => new Company_PaymentStatusDTO(x)).ToList();
            return Company_PaymentStatusDTOs;
        }

        [Route(CompanyRoute.SingleListCurrency), HttpPost]
        public async Task<ActionResult<List<Company_CurrencyDTO>>> SingleListCurrency([FromBody] Company_CurrencyFilterDTO Company_CurrencyFilterDTO)
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
            CurrencyFilter.Id = Company_CurrencyFilterDTO.Id;
            CurrencyFilter.Code = Company_CurrencyFilterDTO.Code;
            CurrencyFilter.Name = Company_CurrencyFilterDTO.Name;

            List<Currency> Currencies = await CurrencyService.List(CurrencyFilter);
            List<Company_CurrencyDTO> Company_CurrencyDTOs = Currencies
                .Select(x => new Company_CurrencyDTO(x)).ToList();
            return Company_CurrencyDTOs;
        }
        [Route(CompanyRoute.SingleListDistrict), HttpPost]
        public async Task<ActionResult<List<Company_DistrictDTO>>> SingleListDistrict([FromBody] Company_DistrictFilterDTO Company_DistrictFilterDTO)
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
            DistrictFilter.Id = Company_DistrictFilterDTO.Id;
            DistrictFilter.Code = Company_DistrictFilterDTO.Code;
            DistrictFilter.Name = Company_DistrictFilterDTO.Name;
            DistrictFilter.Priority = Company_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = Company_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<Company_DistrictDTO> Company_DistrictDTOs = Districts
                .Select(x => new Company_DistrictDTO(x)).ToList();
            return Company_DistrictDTOs;
        }
        [Route(CompanyRoute.SingleListNation), HttpPost]
        public async Task<ActionResult<List<Company_NationDTO>>> SingleListNation([FromBody] Company_NationFilterDTO Company_NationFilterDTO)
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
            NationFilter.Id = Company_NationFilterDTO.Id;
            NationFilter.Name = Company_NationFilterDTO.Name;
            NationFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Nation> Nations = await NationService.List(NationFilter);
            List<Company_NationDTO> Company_NationDTOs = Nations
                .Select(x => new Company_NationDTO(x)).ToList();
            return Company_NationDTOs;
        }
        [Route(CompanyRoute.SingleListProvince), HttpPost]
        public async Task<ActionResult<List<Company_ProvinceDTO>>> SingleListProvince([FromBody] Company_ProvinceFilterDTO Company_ProvinceFilterDTO)
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
            ProvinceFilter.Id = Company_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = Company_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = Company_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = Company_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<Company_ProvinceDTO> Company_ProvinceDTOs = Provinces
                .Select(x => new Company_ProvinceDTO(x)).ToList();
            return Company_ProvinceDTOs;
        }
        [Route(CompanyRoute.SingleListProfession), HttpPost]
        public async Task<ActionResult<List<Company_ProfessionDTO>>> SingleListProfession([FromBody] Company_ProfessionFilterDTO Company_ProfessionFilterDTO)
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
            ProfessionFilter.Id = Company_ProfessionFilterDTO.Id;
            ProfessionFilter.Code = Company_ProfessionFilterDTO.Code;
            ProfessionFilter.Name = Company_ProfessionFilterDTO.Name;
            ProfessionFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Profession> Professions = await ProfessionService.List(ProfessionFilter);
            List<Company_ProfessionDTO> Company_ProfessionDTOs = Professions
                .Select(x => new Company_ProfessionDTO(x)).ToList();
            return Company_ProfessionDTOs;
        }
        [Route(CompanyRoute.SingleListCustomerLeadSource), HttpPost]
        public async Task<ActionResult<List<Company_CustomerLeadSourceDTO>>> SingleListCustomerLeadSource([FromBody] Company_CustomerLeadSourceFilterDTO Company_CustomerLeadSourceFilterDTO)
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
            CustomerLeadSourceFilter.Id = Company_CustomerLeadSourceFilterDTO.Id;
            CustomerLeadSourceFilter.Code = Company_CustomerLeadSourceFilterDTO.Code;
            CustomerLeadSourceFilter.Name = Company_CustomerLeadSourceFilterDTO.Name;

            List<CustomerLeadSource> CustomerLeadSources = await CustomerLeadSourceService.List(CustomerLeadSourceFilter);
            List<Company_CustomerLeadSourceDTO> Company_CustomerLeadSourceDTOs = CustomerLeadSources
                .Select(x => new Company_CustomerLeadSourceDTO(x)).ToList();
            return Company_CustomerLeadSourceDTOs;
        }
        [Route(CompanyRoute.SingleListCompany), HttpPost]
        public async Task<ActionResult<List<Company_CompanyDTO>>> SingleListCompany([FromBody] Company_CompanyFilterDTO Company_CompanyFilterDTO)
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
            CompanyFilter.Id = Company_CompanyFilterDTO.Id;
            CompanyFilter.Name = Company_CompanyFilterDTO.Name;
            CompanyFilter.Phone = Company_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = Company_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = Company_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = Company_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = Company_CompanyFilterDTO.EmailOther;
            CompanyFilter.Description = Company_CompanyFilterDTO.Description;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<Company_CompanyDTO> Company_CompanyDTOs = Companys
                .Select(x => new Company_CompanyDTO(x)).ToList();
            return Company_CompanyDTOs;
        }
        [Route(CompanyRoute.SingleListRatingStatus), HttpPost]
        public async Task<ActionResult<List<Company_RatingStatusDTO>>> SingleListRatingStatus([FromBody] Company_RatingStatusFilterDTO Company_RatingStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RatingStatusFilter RatingStatusFilter = new RatingStatusFilter();
            RatingStatusFilter.Skip = 0;
            RatingStatusFilter.Take = 20;
            RatingStatusFilter.OrderBy = RatingStatusOrder.Id;
            RatingStatusFilter.OrderType = OrderType.ASC;
            RatingStatusFilter.Selects = RatingStatusSelect.ALL;
            RatingStatusFilter.Id = Company_RatingStatusFilterDTO.Id;
            RatingStatusFilter.Name = Company_RatingStatusFilterDTO.Name;
            RatingStatusFilter.Code = Company_RatingStatusFilterDTO.Code;

            List<RatingStatus> RatingStatuses = await RatingStatusService.List(RatingStatusFilter);
            List<Company_RatingStatusDTO> Company_RatingStatusDTOs = RatingStatuses
                .Select(x => new Company_RatingStatusDTO(x)).ToList();
            return Company_RatingStatusDTOs;
        }
        [Route(CompanyRoute.SingleListAppUser), HttpPost]
        public async Task<ActionResult<List<Company_AppUserDTO>>> SingleListAppUser([FromBody] Company_AppUserFilterDTO Company_AppUserFilterDTO)
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
            AppUserFilter.Id = Company_AppUserFilterDTO.Id;
            AppUserFilter.Username = Company_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Company_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = Company_AppUserFilterDTO.Address;
            AppUserFilter.Email = Company_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Company_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = Company_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = Company_AppUserFilterDTO.Birthday;
            AppUserFilter.Department = Company_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = Company_AppUserFilterDTO.OrganizationId;
            AppUserFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Company_AppUserDTO> Company_AppUserDTOs = AppUsers
                .Select(x => new Company_AppUserDTO(x)).ToList();
            return Company_AppUserDTOs;
        }
        [Route(CompanyRoute.SingleListCompanyStatus), HttpPost]
        public async Task<ActionResult<List<Company_CompanyStatusDTO>>> SingleListCompanyStatus([FromBody] Company_CompanyStatusFilterDTO Company_CompanyStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyStatusFilter CompanyStatusFilter = new CompanyStatusFilter();
            CompanyStatusFilter.Skip = 0;
            CompanyStatusFilter.Take = 20;
            CompanyStatusFilter.OrderType = OrderType.ASC;
            CompanyStatusFilter.Selects = CompanyStatusSelect.ALL;
            CompanyStatusFilter.Id = Company_CompanyStatusFilterDTO.Id;
            CompanyStatusFilter.Code = Company_CompanyStatusFilterDTO.Code;
            CompanyStatusFilter.Name = Company_CompanyStatusFilterDTO.Name; 

            List<CompanyStatus> CompanyStatuses = await CompanyStatusService.List(CompanyStatusFilter);
            List<Company_CompanyStatusDTO> Company_CompanyStatusDTOs = CompanyStatuses
                .Select(x => new Company_CompanyStatusDTO(x)).ToList();
            return Company_CompanyStatusDTOs;
        }
        [Route(CompanyRoute.SingleListSex), HttpPost]
        public async Task<ActionResult<List<Company_SexDTO>>> SingleListSex([FromBody] Company_SexFilterDTO Company_SexFilterDTO)
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
            SexFilter.Id = Company_SexFilterDTO.Id;
            SexFilter.Code = Company_SexFilterDTO.Code;
            SexFilter.Name = Company_SexFilterDTO.Name;

            List<Sex> Sexes = await SexService.List(SexFilter);
            List<Company_SexDTO> Company_SexDTOs = Sexes
                .Select(x => new Company_SexDTO(x)).ToList();
            return Company_SexDTOs;
        }
        [Route(CompanyRoute.SingleListContact), HttpPost]
        public async Task<ActionResult<List<Company_ContactDTO>>> SingleListContact([FromBody] Company_ContactFilterDTO Company_ContactFilterDTO)
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
            ContactFilter.Id = Company_ContactFilterDTO.Id;
            ContactFilter.Name = Company_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = Company_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = Company_ContactFilterDTO.CompanyId;

            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<Company_ContactDTO> Company_ContactDTOs = Contacts
                .Select(x => new Company_ContactDTO(x)).ToList();
            return Company_ContactDTOs;
        }
        [Route(CompanyRoute.SingleListOpportunity), HttpPost]
        public async Task<ActionResult<List<Company_OpportunityDTO>>> SingleListOpportunity([FromBody] Company_OpportunityFilterDTO Company_OpportunityFilterDTO)
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
            OpportunityFilter.Id = Company_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = Company_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = Company_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.CustomerLeadId = Company_OpportunityFilterDTO.CustomerLeadId;
            OpportunityFilter.ClosingDate = Company_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = Company_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = Company_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = Company_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = Company_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.Amount = Company_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = Company_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = Company_OpportunityFilterDTO.Description;
            OpportunityFilter.CustomerLeadId = Company_OpportunityFilterDTO.CustomerLeadId;

            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<Company_OpportunityDTO> Company_OpportunityDTOs = Opportunities
                .Select(x => new Company_OpportunityDTO(x)).ToList();
            return Company_OpportunityDTOs;
        }
        [Route(CompanyRoute.SingleListUnitOfMeasure), HttpPost]
        public async Task<ActionResult<List<Company_UnitOfMeasureDTO>>> SingleListUnitOfMeasure([FromBody] Company_UnitOfMeasureFilterDTO Company_UnitOfMeasureFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            //TODO cần optimize lại phần này, sử dụng itemId thay vì productId

            List<Product> Products = await ProductService.List(new ProductFilter
            {
                Id = Company_UnitOfMeasureFilterDTO.ProductId,
                Selects = ProductSelect.Id,
            });
            long ProductId = Products.Select(p => p.Id).FirstOrDefault();
            Product Product = await ProductService.Get(ProductId);

            List<Company_UnitOfMeasureDTO> Company_UnitOfMeasureDTOs = new List<Company_UnitOfMeasureDTO>();
            if (Product.UnitOfMeasureGrouping != null)
            {
                Company_UnitOfMeasureDTOs = Product.UnitOfMeasureGrouping.UnitOfMeasureGroupingContents.Select(x => new Company_UnitOfMeasureDTO(x)).ToList();
            }
            Company_UnitOfMeasureDTO Company_UnitOfMeasureDTO = new Company_UnitOfMeasureDTO
            {
                Id = Product.UnitOfMeasure.Id,
                Code = Product.UnitOfMeasure.Code,
                Name = Product.UnitOfMeasure.Name,
                Description = Product.UnitOfMeasure.Description,
                StatusId = StatusEnum.ACTIVE.Id,
                Factor = 1,
            };
            Company_UnitOfMeasureDTOs.Add(Company_UnitOfMeasureDTO);
            Company_UnitOfMeasureDTOs = Company_UnitOfMeasureDTOs.Distinct().ToList();
            return Company_UnitOfMeasureDTOs;
        }
        [Route(CompanyRoute.SingleListItem), HttpPost]
        public async Task<ActionResult<List<Company_ItemDTO>>> SingleListItem([FromBody] Company_ItemFilterDTO Company_ItemFilterDTO)
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
            ItemFilter.Id = Company_ItemFilterDTO.Id;
            ItemFilter.ProductId = Company_ItemFilterDTO.ProductId;
            ItemFilter.Code = Company_ItemFilterDTO.Code;
            ItemFilter.Name = Company_ItemFilterDTO.Name;
            ItemFilter.ScanCode = Company_ItemFilterDTO.ScanCode;
            ItemFilter.SalePrice = Company_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = Company_ItemFilterDTO.RetailPrice;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<Company_ItemDTO> Company_ItemDTOs = Items
                .Select(x => new Company_ItemDTO(x)).ToList();
            return Company_ItemDTOs;
        }
        [Route(CompanyRoute.SingleListProbability), HttpPost]
        public async Task<ActionResult<List<Company_ProbabilityDTO>>> SingleListProbability([FromBody] Company_ProbabilityFilterDTO Company_ProbabilityFilterDTO)
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
            ProbabilityFilter.Id = Company_ProbabilityFilterDTO.Id;
            ProbabilityFilter.Code = Company_ProbabilityFilterDTO.Code;
            ProbabilityFilter.Name = Company_ProbabilityFilterDTO.Name;

            List<Probability> Probabilities = await ProbabilityService.List(ProbabilityFilter);
            List<Company_ProbabilityDTO> Company_ProbabilityDTOs = Probabilities
                .Select(x => new Company_ProbabilityDTO(x)).ToList();
            return Company_ProbabilityDTOs;
        }
        [Route(CompanyRoute.SingleListFileType), HttpPost]
        public async Task<ActionResult<List<Company_FileTypeDTO>>> SingleListFileType([FromBody] Company_FileTypeFilterDTO Company_FileTypeFilterDTO)
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
            FileTypeFilter.Id = Company_FileTypeFilterDTO.Id;
            FileTypeFilter.Code = Company_FileTypeFilterDTO.Code;
            FileTypeFilter.Name = Company_FileTypeFilterDTO.Name;

            List<FileType> FileTypes = await FileTypeService.List(FileTypeFilter);
            List<Company_FileTypeDTO> Company_FileTypeDTOs = FileTypes
                .Select(x => new Company_FileTypeDTO(x)).ToList();
            return Company_FileTypeDTOs;
        }
        [Route(CompanyRoute.SingleListPotentialResult), HttpPost]
        public async Task<ActionResult<List<Company_PotentialResultDTO>>> SingleListPotentialResult([FromBody] Company_PotentialResultFilterDTO Company_PotentialResultFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PotentialResultFilter PotentialResultFilter = new PotentialResultFilter();
            PotentialResultFilter.Skip = 0;
            PotentialResultFilter.Take = 20;
            PotentialResultFilter.OrderBy = PotentialResultOrder.Id;
            PotentialResultFilter.OrderType = OrderType.ASC;
            PotentialResultFilter.Selects = PotentialResultSelect.ALL;
            PotentialResultFilter.Id = Company_PotentialResultFilterDTO.Id;
            PotentialResultFilter.Code = Company_PotentialResultFilterDTO.Code;
            PotentialResultFilter.Name = Company_PotentialResultFilterDTO.Name;

            List<PotentialResult> PotentialResults = await PotentialResultService.List(PotentialResultFilter);
            List<Company_PotentialResultDTO> Company_PotentialResultDTOs = PotentialResults
                .Select(x => new Company_PotentialResultDTO(x)).ToList();
            return Company_PotentialResultDTOs;
        }
        [Route(CompanyRoute.SingleListSaleStage), HttpPost]
        public async Task<ActionResult<List<Company_SaleStageDTO>>> SingleListSaleStage([FromBody] Company_SaleStageFilterDTO Company_SaleStageFilterDTO)
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
            SaleStageFilter.Id = Company_SaleStageFilterDTO.Id;
            SaleStageFilter.Code = Company_SaleStageFilterDTO.Code;
            SaleStageFilter.Name = Company_SaleStageFilterDTO.Name;

            List<SaleStage> SaleStages = await SaleStageService.List(SaleStageFilter);
            List<Company_SaleStageDTO> Company_SaleStageDTOs = SaleStages
                .Select(x => new Company_SaleStageDTO(x)).ToList();
            return Company_SaleStageDTOs;
        }
        [Route(CompanyRoute.SingleListEditedPriceStatus), HttpPost]
        public async Task<ActionResult<List<Company_EditedPriceStatusDTO>>> SingleListEditedPriceStatus([FromBody] Company_EditedPriceStatusFilterDTO Company_EditedPriceStatusFilterDTO)
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
            EditedPriceStatusFilter.Id = Company_EditedPriceStatusFilterDTO.Id;
            EditedPriceStatusFilter.Code = Company_EditedPriceStatusFilterDTO.Code;
            EditedPriceStatusFilter.Name = Company_EditedPriceStatusFilterDTO.Name;

            List<EditedPriceStatus> EditedPriceStatuses = await EditedPriceStatusService.List(EditedPriceStatusFilter);
            List<Company_EditedPriceStatusDTO> Company_EditedPriceStatusDTOs = EditedPriceStatuses
                .Select(x => new Company_EditedPriceStatusDTO(x)).ToList();
            return Company_EditedPriceStatusDTOs;
        }
        [Route(CompanyRoute.SingleListOrderQuoteStatus), HttpPost]
        public async Task<ActionResult<List<Company_OrderQuoteStatusDTO>>> SingleListOrderQuoteStatus([FromBody] Company_OrderQuoteStatusFilterDTO Company_OrderQuoteStatusFilterDTO)
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
            OrderQuoteStatusFilter.Id = Company_OrderQuoteStatusFilterDTO.Id;
            OrderQuoteStatusFilter.Code = Company_OrderQuoteStatusFilterDTO.Code;
            OrderQuoteStatusFilter.Name = Company_OrderQuoteStatusFilterDTO.Name;

            List<OrderQuoteStatus> OrderQuoteStatuses = await OrderQuoteStatusService.List(OrderQuoteStatusFilter);
            List<Company_OrderQuoteStatusDTO> Company_OrderQuoteStatusDTOs = OrderQuoteStatuses
                .Select(x => new Company_OrderQuoteStatusDTO(x)).ToList();
            return Company_OrderQuoteStatusDTOs;
        }
        [Route(CompanyRoute.SingleListTaxType), HttpPost]
        public async Task<ActionResult<List<Company_TaxTypeDTO>>> SingleListTaxType([FromBody] Company_TaxTypeFilterDTO InCompany_TaxTypeFilterDTO)
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
            TaxTypeFilter.Id = InCompany_TaxTypeFilterDTO.Id;
            TaxTypeFilter.Code = InCompany_TaxTypeFilterDTO.Code;
            TaxTypeFilter.Name = InCompany_TaxTypeFilterDTO.Name;
            TaxTypeFilter.StatusId = new IdFilter { Equal = Enums.StatusEnum.ACTIVE.Id };

            List<TaxType> TaxTypes = await TaxTypeService.List(TaxTypeFilter);
            List<Company_TaxTypeDTO> Company_TaxTypeDTOs = TaxTypes
                .Select(x => new Company_TaxTypeDTO(x)).ToList();
            return Company_TaxTypeDTOs;
        }
        [Route(CompanyRoute.SingleListCustomerLeadLevel), HttpPost]
        public async Task<ActionResult<List<Company_CustomerLeadLevelDTO>>> SingleListCustomerLeadLevel([FromBody] Company_CustomerLeadLevelFilterDTO Company_CustomerLeadLevelFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadLevelFilter CustomerLeadLevelFilter = new CustomerLeadLevelFilter();
            CustomerLeadLevelFilter.Skip = 0;
            CustomerLeadLevelFilter.Take = 20;
            CustomerLeadLevelFilter.OrderBy = CustomerLeadLevelOrder.Id;
            CustomerLeadLevelFilter.OrderType = OrderType.ASC;
            CustomerLeadLevelFilter.Selects = CustomerLeadLevelSelect.ALL;
            CustomerLeadLevelFilter.Id = Company_CustomerLeadLevelFilterDTO.Id;
            CustomerLeadLevelFilter.Code = Company_CustomerLeadLevelFilterDTO.Code;
            CustomerLeadLevelFilter.Name = Company_CustomerLeadLevelFilterDTO.Name;

            List<CustomerLeadLevel> CustomerLeadLevels = await CustomerLeadLevelService.List(CustomerLeadLevelFilter);
            List<Company_CustomerLeadLevelDTO> Company_CustomerLeadLevelDTOs = CustomerLeadLevels
                .Select(x => new Company_CustomerLeadLevelDTO(x)).ToList();
            return Company_CustomerLeadLevelDTOs;
        }
        [Route(CompanyRoute.SingleListCustomerLeadStatus), HttpPost]
        public async Task<ActionResult<List<Company_CustomerLeadStatusDTO>>> SingleListCustomerLeadStatus([FromBody] Company_CustomerLeadStatusFilterDTO Company_CustomerLeadStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadStatusFilter CustomerLeadStatusFilter = new CustomerLeadStatusFilter();
            CustomerLeadStatusFilter.Skip = 0;
            CustomerLeadStatusFilter.Take = 20;
            CustomerLeadStatusFilter.OrderType = OrderType.ASC;
            CustomerLeadStatusFilter.Selects = CustomerLeadStatusSelect.ALL;
            CustomerLeadStatusFilter.Id = Company_CustomerLeadStatusFilterDTO.Id;
            CustomerLeadStatusFilter.Code = Company_CustomerLeadStatusFilterDTO.Code;
            CustomerLeadStatusFilter.Name = Company_CustomerLeadStatusFilterDTO.Name;

            List<CustomerLeadStatus> CustomerLeadStatuses = await CustomerLeadStatusService.List(CustomerLeadStatusFilter);
            List<Company_CustomerLeadStatusDTO> Company_CustomerLeadStatusDTOs = CustomerLeadStatuses
                .Select(x => new Company_CustomerLeadStatusDTO(x)).ToList();
            return Company_CustomerLeadStatusDTOs;
        }
        [Route(CompanyRoute.SingleListActivityStatus), HttpPost]
        public async Task<ActionResult<List<Company_ActivityStatusDTO>>> SingleListActivityStatus([FromBody] Company_ActivityStatusFilterDTO Company_ActivityStatusFilterDTO)
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
            ActivityStatusFilter.Id = Company_ActivityStatusFilterDTO.Id;
            ActivityStatusFilter.Code = Company_ActivityStatusFilterDTO.Code;
            ActivityStatusFilter.Name = Company_ActivityStatusFilterDTO.Name;

            List<ActivityStatus> ActivityStatuses = await ActivityStatusService.List(ActivityStatusFilter);
            List<Company_ActivityStatusDTO> Company_ActivityStatusDTOs = ActivityStatuses
                .Select(x => new Company_ActivityStatusDTO(x)).ToList();
            return Company_ActivityStatusDTOs;
        }
        [Route(CompanyRoute.SingleListActivityType), HttpPost]
        public async Task<ActionResult<List<Company_ActivityTypeDTO>>> SingleListActivityType([FromBody] Company_ActivityTypeFilterDTO Company_ActivityTypeFilterDTO)
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
            ActivityTypeFilter.Id = Company_ActivityTypeFilterDTO.Id;
            ActivityTypeFilter.Code = Company_ActivityTypeFilterDTO.Code;
            ActivityTypeFilter.Name = Company_ActivityTypeFilterDTO.Name;

            List<ActivityType> ActivityTypes = await ActivityTypeService.List(ActivityTypeFilter);
            List<Company_ActivityTypeDTO> Company_ActivityTypeDTOs = ActivityTypes
                .Select(x => new Company_ActivityTypeDTO(x)).ToList();
            return Company_ActivityTypeDTOs;
        }
        [Route(CompanyRoute.SingleListActivityPriority), HttpPost]
        public async Task<ActionResult<List<Company_ActivityPriorityDTO>>> SingleListActivityPriority([FromBody] Company_ActivityPriorityFilterDTO Company_ActivityPriorityFilterDTO)
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
            ActivityPriorityFilter.Id = Company_ActivityPriorityFilterDTO.Id;
            ActivityPriorityFilter.Code = Company_ActivityPriorityFilterDTO.Code;
            ActivityPriorityFilter.Name = Company_ActivityPriorityFilterDTO.Name;

            List<ActivityPriority> ActivityPriorities = await ActivityPriorityService.List(ActivityPriorityFilter);
            List<Company_ActivityPriorityDTO> Company_ActivityPriorityDTOs = ActivityPriorities
                .Select(x => new Company_ActivityPriorityDTO(x)).ToList();
            return Company_ActivityPriorityDTOs;
        }
        [Route(CompanyRoute.SingleListProduct), HttpPost]
        public async Task<ActionResult<List<Company_ProductDTO>>> SingleListProduct([FromBody] Company_ProductFilterDTO Company_ProductFilterDTO)
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
            ProductFilter.Id = Company_ProductFilterDTO.Id;
            ProductFilter.Name = Company_ProductFilterDTO.Name;
            ProductFilter.Code = Company_ProductFilterDTO.Code;
            ProductFilter.SupplierId = Company_ProductFilterDTO.SupplierId;
            ProductFilter.ProductTypeId = Company_ProductFilterDTO.ProductTypeId;
            ProductFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Product> Products = await ProductService.List(ProductFilter);
            List<Company_ProductDTO> Company_ProductDTOs = Products
                .Select(x => new Company_ProductDTO(x)).ToList();
            return Company_ProductDTOs;
        }
        [Route(CompanyRoute.SingleListProductGrouping), HttpPost]
        public async Task<ActionResult<List<Company_ProductGroupingDTO>>> SingleListProductGrouping([FromBody] Company_ProductGroupingFilterDTO Company_ProductGroupingFilterDTO)
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
            List<Company_ProductGroupingDTO> Company_ProductGroupingDTOs = ProductGroupings
                .Select(x => new Company_ProductGroupingDTO(x)).ToList();
            return Company_ProductGroupingDTOs;
        }
        [Route(CompanyRoute.SingleListProductType), HttpPost]
        public async Task<ActionResult<List<Company_ProductTypeDTO>>> SingleListProductType([FromBody] Company_ProductTypeFilterDTO Company_ProductTypeFilterDTO)
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
            ProductTypeFilter.Id = Company_ProductTypeFilterDTO.Id;
            ProductTypeFilter.Code = Company_ProductTypeFilterDTO.Code;
            ProductTypeFilter.Name = Company_ProductTypeFilterDTO.Name;
            ProductTypeFilter.Description = Company_ProductTypeFilterDTO.Description;
            ProductTypeFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            if (ProductTypeFilter.Id == null) ProductTypeFilter.Id = new IdFilter();
            ProductTypeFilter.Id.In = await FilterProductType(ProductTypeService, CurrentContext);

            List<ProductType> ProductTypes = await ProductTypeService.List(ProductTypeFilter);
            List<Company_ProductTypeDTO> Company_ProductTypeDTOs = ProductTypes
                .Select(x => new Company_ProductTypeDTO(x)).ToList();
            return Company_ProductTypeDTOs;
        }
        [Route(CompanyRoute.SingleListSupplier), HttpPost]
        public async Task<ActionResult<List<Company_SupplierDTO>>> SingleListSupplier([FromBody] Company_SupplierFilterDTO Company_SupplierFilterDTO)
        {
            if (UnAuthorization) return Forbid(); if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SupplierFilter SupplierFilter = new SupplierFilter();
            SupplierFilter.Skip = 0;
            SupplierFilter.Take = 20;
            SupplierFilter.OrderBy = SupplierOrder.Id;
            SupplierFilter.OrderType = OrderType.ASC;
            SupplierFilter.Selects = SupplierSelect.ALL;
            SupplierFilter.Id = Company_SupplierFilterDTO.Id;
            SupplierFilter.Code = Company_SupplierFilterDTO.Code;
            SupplierFilter.Name = Company_SupplierFilterDTO.Name;
            SupplierFilter.TaxCode = Company_SupplierFilterDTO.TaxCode;
            SupplierFilter.Phone = Company_SupplierFilterDTO.Phone;
            SupplierFilter.Email = Company_SupplierFilterDTO.Email;
            SupplierFilter.Address = Company_SupplierFilterDTO.Address;
            SupplierFilter.ProvinceId = Company_SupplierFilterDTO.ProvinceId;
            SupplierFilter.DistrictId = Company_SupplierFilterDTO.DistrictId;
            SupplierFilter.WardId = Company_SupplierFilterDTO.WardId;
            SupplierFilter.OwnerName = Company_SupplierFilterDTO.OwnerName;
            SupplierFilter.PersonInChargeId = Company_SupplierFilterDTO.PersonInChargeId;
            SupplierFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            SupplierFilter.Description = Company_SupplierFilterDTO.Description;

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<Company_SupplierDTO> Company_SupplierDTOs = Suppliers
                .Select(x => new Company_SupplierDTO(x)).ToList();
            return Company_SupplierDTOs;
        }
        [Route(CompanyRoute.SingleListEmailTemplate), HttpPost]
        public async Task<ActionResult<List<Company_MailTemplateDTO>>> SingleListMailTemplate([FromBody] Company_MailTemplateFilterDTO Company_MailTemplateFilterDTO)
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
            MailTemplateFilter.Id = Company_MailTemplateFilterDTO.Id;
            MailTemplateFilter.Code = Company_MailTemplateFilterDTO.Code;
            MailTemplateFilter.Name = Company_MailTemplateFilterDTO.Name;
            MailTemplateFilter.Content = Company_MailTemplateFilterDTO.Content;

            List<MailTemplate> MailTemplates = await MailTemplateService.List(MailTemplateFilter);
            List<Company_MailTemplateDTO> Company_MailTemplateDTOs = MailTemplates
                .Select(x => new Company_MailTemplateDTO(x)).ToList();
            return Company_MailTemplateDTOs;
        }
        [Route(CompanyRoute.SingleListOrderPaymentStatus), HttpPost]
        public async Task<ActionResult<List<Company_OrderPaymentStatusDTO>>> SingleListOrderPaymentStatus([FromBody] Company_OrderPaymentStatusFilterDTO Company_OrderPaymentStatusFilterDTO)
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
            List<Company_OrderPaymentStatusDTO> Company_OrderPaymentStatusDTOs = OrderPaymentStatuses
                .Select(x => new Company_OrderPaymentStatusDTO(x)).ToList();
            return Company_OrderPaymentStatusDTOs;
        }
        [Route(CompanyRoute.SingleListRequestState), HttpPost]
        public async Task<ActionResult<List<Company_RequestStateDTO>>> SingleListRequestState([FromBody] Company_RequestStateFilterDTO Company_RequestStateFilterDTO)
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
            RequestStateFilter.Id = Company_RequestStateFilterDTO.Id;
            RequestStateFilter.Code = Company_RequestStateFilterDTO.Code;
            RequestStateFilter.Name = Company_RequestStateFilterDTO.Name;

            List<RequestState> RequestStates = await RequestStateService.List(RequestStateFilter);
            List<Company_RequestStateDTO> Company_RequestStateDTOs = RequestStates
                .Select(x => new Company_RequestStateDTO(x)).ToList();
            return Company_RequestStateDTOs;
        }

    }
}

