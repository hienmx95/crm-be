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
using CRM.Services.MCustomerLead;
using CRM.Services.MCustomerLeadLevel;
using CRM.Services.MCustomerLeadSource;
using CRM.Services.MCustomerLeadStatus;
using CRM.Services.MDistrict;
using CRM.Services.MProfession;
using CRM.Services.MProvince;
using CRM.Services.MAppUser;
using CRM.Enums;

namespace CRM.Rpc.opportunity
{
    public partial class OpportunityController : RpcController
    {
        [Route(OpportunityRoute.FilterListActivityStatus), HttpPost]
        public async Task<ActionResult<List<Opportunity_ActivityStatusDTO>>> FilterListActivityStatus([FromBody] Opportunity_ActivityStatusFilterDTO Opportunity_ActivityStatusFilterDTO)
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
            ActivityStatusFilter.Id = Opportunity_ActivityStatusFilterDTO.Id;
            ActivityStatusFilter.Code = Opportunity_ActivityStatusFilterDTO.Code;
            ActivityStatusFilter.Name = Opportunity_ActivityStatusFilterDTO.Name;

            List<ActivityStatus> ActivityStatuses = await ActivityStatusService.List(ActivityStatusFilter);
            List<Opportunity_ActivityStatusDTO> Opportunity_ActivityStatusDTOs = ActivityStatuses
                .Select(x => new Opportunity_ActivityStatusDTO(x)).ToList();
            return Opportunity_ActivityStatusDTOs;
        }
        [Route(OpportunityRoute.FilterListActivityType), HttpPost]
        public async Task<ActionResult<List<Opportunity_ActivityTypeDTO>>> FilterListActivityType([FromBody] Opportunity_ActivityTypeFilterDTO Opportunity_ActivityTypeFilterDTO)
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
            ActivityTypeFilter.Id = Opportunity_ActivityTypeFilterDTO.Id;
            ActivityTypeFilter.Code = Opportunity_ActivityTypeFilterDTO.Code;
            ActivityTypeFilter.Name = Opportunity_ActivityTypeFilterDTO.Name;

            List<ActivityType> ActivityTypes = await ActivityTypeService.List(ActivityTypeFilter);
            List<Opportunity_ActivityTypeDTO> Opportunity_ActivityTypeDTOs = ActivityTypes
                .Select(x => new Opportunity_ActivityTypeDTO(x)).ToList();
            return Opportunity_ActivityTypeDTOs;
        }
        [Route(OpportunityRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<Opportunity_AppUserDTO>>> FilterListAppUser([FromBody] Opportunity_AppUserFilterDTO Opportunity_AppUserFilterDTO)
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
            AppUserFilter.Id = Opportunity_AppUserFilterDTO.Id;
            AppUserFilter.Username = Opportunity_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Opportunity_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = Opportunity_AppUserFilterDTO.Address;
            AppUserFilter.Email = Opportunity_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Opportunity_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = Opportunity_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = Opportunity_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = Opportunity_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = Opportunity_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = Opportunity_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = Opportunity_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = Opportunity_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = Opportunity_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = Opportunity_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = Opportunity_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Opportunity_AppUserDTO> Opportunity_AppUserDTOs = AppUsers
                .Select(x => new Opportunity_AppUserDTO(x)).ToList();
            return Opportunity_AppUserDTOs;
        }
        [Route(OpportunityRoute.FilterListProductGrouping), HttpPost]
        public async Task<ActionResult<List<Opportunity_ProductGroupingDTO>>> FilterListProductGrouping([FromBody] Opportunity_ProductGroupingFilterDTO Opportunity_ProductGroupingFilterDTO)
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
            List<Opportunity_ProductGroupingDTO> Opportunity_ProductGroupingDTOs = ProductGroupings
                .Select(x => new Opportunity_ProductGroupingDTO(x)).ToList();
            return Opportunity_ProductGroupingDTOs;
        }
        [Route(OpportunityRoute.FilterListProductType), HttpPost]
        public async Task<ActionResult<List<Opportunity_ProductTypeDTO>>> FilterListProductType([FromBody] Opportunity_ProductTypeFilterDTO Opportunity_ProductTypeFilterDTO)
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
            ProductTypeFilter.Id = Opportunity_ProductTypeFilterDTO.Id;
            ProductTypeFilter.Code = Opportunity_ProductTypeFilterDTO.Code;
            ProductTypeFilter.Name = Opportunity_ProductTypeFilterDTO.Name;
            ProductTypeFilter.Description = Opportunity_ProductTypeFilterDTO.Description;
            ProductTypeFilter.StatusId = Opportunity_ProductTypeFilterDTO.StatusId;

            if (ProductTypeFilter.Id == null) ProductTypeFilter.Id = new IdFilter();
            ProductTypeFilter.Id.In = await FilterProductType(ProductTypeService, CurrentContext);

            List<ProductType> ProductTypes = await ProductTypeService.List(ProductTypeFilter);
            List<Opportunity_ProductTypeDTO> Opportunity_ProductTypeDTOs = ProductTypes
                .Select(x => new Opportunity_ProductTypeDTO(x)).ToList();
            return Opportunity_ProductTypeDTOs;
        }
        [Route(OpportunityRoute.FilterListSupplier), HttpPost]
        public async Task<ActionResult<List<Opportunity_SupplierDTO>>> FilterListSupplier([FromBody] Opportunity_SupplierFilterDTO Opportunity_SupplierFilterDTO)
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
            SupplierFilter.Id = Opportunity_SupplierFilterDTO.Id;
            SupplierFilter.Code = Opportunity_SupplierFilterDTO.Code;
            SupplierFilter.Name = Opportunity_SupplierFilterDTO.Name;
            SupplierFilter.TaxCode = Opportunity_SupplierFilterDTO.TaxCode;
            SupplierFilter.Phone = Opportunity_SupplierFilterDTO.Phone;
            SupplierFilter.Email = Opportunity_SupplierFilterDTO.Email;
            SupplierFilter.Address = Opportunity_SupplierFilterDTO.Address;
            SupplierFilter.ProvinceId = Opportunity_SupplierFilterDTO.ProvinceId;
            SupplierFilter.DistrictId = Opportunity_SupplierFilterDTO.DistrictId;
            SupplierFilter.WardId = Opportunity_SupplierFilterDTO.WardId;
            SupplierFilter.OwnerName = Opportunity_SupplierFilterDTO.OwnerName;
            SupplierFilter.PersonInChargeId = Opportunity_SupplierFilterDTO.PersonInChargeId;
            SupplierFilter.StatusId = Opportunity_SupplierFilterDTO.StatusId;
            SupplierFilter.Description = Opportunity_SupplierFilterDTO.Description;

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<Opportunity_SupplierDTO> Opportunity_SupplierDTOs = Suppliers
                .Select(x => new Opportunity_SupplierDTO(x)).ToList();
            return Opportunity_SupplierDTOs;
        }
        [Route(OpportunityRoute.FilterListItem), HttpPost]
        public async Task<ActionResult<List<Opportunity_ItemDTO>>> FilterListItem([FromBody] Opportunity_ItemFilterDTO Opportunity_ItemFilterDTO)
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
            ItemFilter.Id = Opportunity_ItemFilterDTO.Id;
            ItemFilter.ProductId = Opportunity_ItemFilterDTO.ProductId;
            ItemFilter.Code = Opportunity_ItemFilterDTO.Code;
            ItemFilter.Name = Opportunity_ItemFilterDTO.Name;
            ItemFilter.ScanCode = Opportunity_ItemFilterDTO.ScanCode;
            ItemFilter.SalePrice = Opportunity_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = Opportunity_ItemFilterDTO.RetailPrice;
            ItemFilter.StatusId = Opportunity_ItemFilterDTO.StatusId;

            List<Item> Items = await ItemService.List(ItemFilter);
            List<Opportunity_ItemDTO> Opportunity_ItemDTOs = Items
                .Select(x => new Opportunity_ItemDTO(x)).ToList();
            return Opportunity_ItemDTOs;
        }
        [Route(OpportunityRoute.FilterListCompany), HttpPost]
        public async Task<ActionResult<List<Opportunity_CompanyDTO>>> FilterListCompany([FromBody] Opportunity_CompanyFilterDTO Opportunity_CompanyFilterDTO)
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
            CompanyFilter.Id = Opportunity_CompanyFilterDTO.Id;
            CompanyFilter.Name = Opportunity_CompanyFilterDTO.Name;
            CompanyFilter.Phone = Opportunity_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = Opportunity_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = Opportunity_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = Opportunity_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = Opportunity_CompanyFilterDTO.EmailOther;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<Opportunity_CompanyDTO> Opportunity_CompanyDTOs = Companys
                .Select(x => new Opportunity_CompanyDTO(x)).ToList();
            return Opportunity_CompanyDTOs;
        }
        [Route(OpportunityRoute.FilterListContact), HttpPost]
        public async Task<ActionResult<List<Opportunity_ContactDTO>>> FilterListContact([FromBody] Opportunity_ContactFilterDTO Opportunity_ContactFilterDTO)
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
            ContactFilter.Id = Opportunity_ContactFilterDTO.Id;
            ContactFilter.Name = Opportunity_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = Opportunity_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = Opportunity_ContactFilterDTO.CompanyId;
            ContactFilter.ImageId = Opportunity_ContactFilterDTO.ImageId;
            ContactFilter.Description = Opportunity_ContactFilterDTO.Description;
            ContactFilter.Address = Opportunity_ContactFilterDTO.Address;
            ContactFilter.EmailOther = Opportunity_ContactFilterDTO.EmailOther;
            ContactFilter.DateOfBirth = Opportunity_ContactFilterDTO.DateOfBirth;
            ContactFilter.Phone = Opportunity_ContactFilterDTO.Phone;
            ContactFilter.PhoneHome = Opportunity_ContactFilterDTO.PhoneHome;
            ContactFilter.FAX = Opportunity_ContactFilterDTO.FAX;
            ContactFilter.Email = Opportunity_ContactFilterDTO.Email;

            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<Opportunity_ContactDTO> Opportunity_ContactDTOs = Contacts
                .Select(x => new Opportunity_ContactDTO(x)).ToList();
            return Opportunity_ContactDTOs;
        }
        [Route(OpportunityRoute.FilterListSaleStage), HttpPost]
        public async Task<ActionResult<List<Opportunity_SaleStageDTO>>> FilterListSaleStage([FromBody] Opportunity_SaleStageFilterDTO Opportunity_SaleStageFilterDTO)
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
            SaleStageFilter.Id = Opportunity_SaleStageFilterDTO.Id;
            SaleStageFilter.Code = Opportunity_SaleStageFilterDTO.Code;
            SaleStageFilter.Name = Opportunity_SaleStageFilterDTO.Name;

            List<SaleStage> SaleStages = await SaleStageService.List(SaleStageFilter);
            List<Opportunity_SaleStageDTO> Opportunity_SaleStageDTOs = SaleStages
                .Select(x => new Opportunity_SaleStageDTO(x)).ToList();
            return Opportunity_SaleStageDTOs;
        }
        [Route(OpportunityRoute.FilterListPosition), HttpPost]
        public async Task<ActionResult<List<Opportunity_PositionDTO>>> FilterListPosition([FromBody] Opportunity_PositionFilterDTO Opportunity_PositionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            PositionFilter PositionFilter = new PositionFilter();
            PositionFilter.Skip = 0;
            PositionFilter.Take = 99999;
            PositionFilter.OrderBy = PositionOrder.Id;
            PositionFilter.OrderType = OrderType.ASC;
            PositionFilter.Selects = PositionSelect.ALL;
            PositionFilter.Id = Opportunity_PositionFilterDTO.Id;
            PositionFilter.Code = Opportunity_PositionFilterDTO.Code;
            PositionFilter.Name = Opportunity_PositionFilterDTO.Name;
            PositionFilter.StatusId = Opportunity_PositionFilterDTO.StatusId;

            List<Position> Positions = await PositionService.List(PositionFilter);
            List<Opportunity_PositionDTO> Opportunity_PositionDTOs = Positions
                .Select(x => new Opportunity_PositionDTO(x)).ToList();
            return Opportunity_PositionDTOs;
        }
        [Route(OpportunityRoute.FilterListOrderQuoteStatus), HttpPost]
        public async Task<ActionResult<List<Opportunity_OrderQuoteStatusDTO>>> FilterListOrderQuoteStatus([FromBody] Opportunity_OrderQuoteStatusFilterDTO Opportunity_OrderQuoteStatusFilterDTO)
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
            OrderQuoteStatusFilter.Id = Opportunity_OrderQuoteStatusFilterDTO.Id;
            OrderQuoteStatusFilter.Code = Opportunity_OrderQuoteStatusFilterDTO.Code;
            OrderQuoteStatusFilter.Name = Opportunity_OrderQuoteStatusFilterDTO.Name;

            List<OrderQuoteStatus> OrderQuoteStatuses = await OrderQuoteStatusService.List(OrderQuoteStatusFilter);
            List<Opportunity_OrderQuoteStatusDTO> Opportunity_OrderQuoteStatusDTOs = OrderQuoteStatuses
                .Select(x => new Opportunity_OrderQuoteStatusDTO(x)).ToList();
            return Opportunity_OrderQuoteStatusDTOs;
        }
        [Route(OpportunityRoute.FilterListEmailStatus), HttpPost]
        public async Task<ActionResult<List<Opportunity_EmailStatusDTO>>> FilterListEmailStatus([FromBody] Opportunity_EmailStatusFilterDTO Opportunity_EmailStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            EmailStatusFilter EmailStatusFilter = new EmailStatusFilter();
            EmailStatusFilter.Skip = 0;
            EmailStatusFilter.Take = 20;
            EmailStatusFilter.OrderBy = EmailStatusOrder.Id;
            EmailStatusFilter.OrderType = OrderType.ASC;
            EmailStatusFilter.Selects = EmailStatusSelect.ALL;
            EmailStatusFilter.Id = Opportunity_EmailStatusFilterDTO.Id;
            EmailStatusFilter.Code = Opportunity_EmailStatusFilterDTO.Code;
            EmailStatusFilter.Name = Opportunity_EmailStatusFilterDTO.Name;

            List<EmailStatus> Statuses = await EmailStatusService.List(EmailStatusFilter);
            List<Opportunity_EmailStatusDTO> Opportunity_EmailStatusDTOs = Statuses
                .Select(x => new Opportunity_EmailStatusDTO(x)).ToList();
            return Opportunity_EmailStatusDTOs;
        }

        [Route(OpportunityRoute.SingleListCustomerLeadLevel), HttpPost]
        public async Task<ActionResult<List<Opportunity_CustomerLeadLevelDTO>>> SingleListCustomerLeadLevel([FromBody] Opportunity_CustomerLeadLevelFilterDTO Opportunity_CustomerLeadLevelFilterDTO)
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
            CustomerLeadLevelFilter.Id = Opportunity_CustomerLeadLevelFilterDTO.Id;
            CustomerLeadLevelFilter.Code = Opportunity_CustomerLeadLevelFilterDTO.Code;
            CustomerLeadLevelFilter.Name = Opportunity_CustomerLeadLevelFilterDTO.Name;

            List<CustomerLeadLevel> CustomerLeadLevels = await CustomerLeadLevelService.List(CustomerLeadLevelFilter);
            List<Opportunity_CustomerLeadLevelDTO> Opportunity_CustomerLeadLevelDTOs = CustomerLeadLevels
                .Select(x => new Opportunity_CustomerLeadLevelDTO(x)).ToList();
            return Opportunity_CustomerLeadLevelDTOs;
        }
        [Route(OpportunityRoute.SingleListCustomerLeadSource), HttpPost]
        public async Task<ActionResult<List<Opportunity_CustomerLeadSourceDTO>>> SingleListCustomerLeadSource([FromBody] Opportunity_CustomerLeadSourceFilterDTO Opportunity_CustomerLeadSourceFilterDTO)
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
            CustomerLeadSourceFilter.Id = Opportunity_CustomerLeadSourceFilterDTO.Id;
            CustomerLeadSourceFilter.Code = Opportunity_CustomerLeadSourceFilterDTO.Code;
            CustomerLeadSourceFilter.Name = Opportunity_CustomerLeadSourceFilterDTO.Name;

            List<CustomerLeadSource> CustomerLeadSources = await CustomerLeadSourceService.List(CustomerLeadSourceFilter);
            List<Opportunity_CustomerLeadSourceDTO> Opportunity_CustomerLeadSourceDTOs = CustomerLeadSources
                .Select(x => new Opportunity_CustomerLeadSourceDTO(x)).ToList();
            return Opportunity_CustomerLeadSourceDTOs;
        }
        [Route(OpportunityRoute.SingleListCustomerLeadStatus), HttpPost]
        public async Task<ActionResult<List<Opportunity_CustomerLeadStatusDTO>>> SingleListCustomerLeadStatus([FromBody] Opportunity_CustomerLeadStatusFilterDTO Opportunity_CustomerLeadStatusFilterDTO)
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
            CustomerLeadStatusFilter.Id = Opportunity_CustomerLeadStatusFilterDTO.Id;
            CustomerLeadStatusFilter.Code = Opportunity_CustomerLeadStatusFilterDTO.Code;
            CustomerLeadStatusFilter.Name = Opportunity_CustomerLeadStatusFilterDTO.Name;

            List<CustomerLeadStatus> CustomerLeadStatuses = await CustomerLeadStatusService.List(CustomerLeadStatusFilter);
            List<Opportunity_CustomerLeadStatusDTO> Opportunity_CustomerLeadStatusDTOs = CustomerLeadStatuses
                .Select(x => new Opportunity_CustomerLeadStatusDTO(x)).ToList();
            return Opportunity_CustomerLeadStatusDTOs;
        }
        [Route(OpportunityRoute.SingleListActivityStatus), HttpPost]
        public async Task<ActionResult<List<Opportunity_ActivityStatusDTO>>> SingleListActivityStatus([FromBody] Opportunity_ActivityStatusFilterDTO Opportunity_ActivityStatusFilterDTO)
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
            ActivityStatusFilter.Id = Opportunity_ActivityStatusFilterDTO.Id;
            ActivityStatusFilter.Code = Opportunity_ActivityStatusFilterDTO.Code;
            ActivityStatusFilter.Name = Opportunity_ActivityStatusFilterDTO.Name;

            List<ActivityStatus> ActivityStatuses = await ActivityStatusService.List(ActivityStatusFilter);
            List<Opportunity_ActivityStatusDTO> Opportunity_ActivityStatusDTOs = ActivityStatuses
                .Select(x => new Opportunity_ActivityStatusDTO(x)).ToList();
            return Opportunity_ActivityStatusDTOs;
        }
        [Route(OpportunityRoute.SingleListActivityType), HttpPost]
        public async Task<ActionResult<List<Opportunity_ActivityTypeDTO>>> SingleListActivityType([FromBody] Opportunity_ActivityTypeFilterDTO Opportunity_ActivityTypeFilterDTO)
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
            ActivityTypeFilter.Id = Opportunity_ActivityTypeFilterDTO.Id;
            ActivityTypeFilter.Code = Opportunity_ActivityTypeFilterDTO.Code;
            ActivityTypeFilter.Name = Opportunity_ActivityTypeFilterDTO.Name;

            List<ActivityType> ActivityTypes = await ActivityTypeService.List(ActivityTypeFilter);
            List<Opportunity_ActivityTypeDTO> Opportunity_ActivityTypeDTOs = ActivityTypes
                .Select(x => new Opportunity_ActivityTypeDTO(x)).ToList();
            return Opportunity_ActivityTypeDTOs;
        }
        [Route(OpportunityRoute.SingleListActivityPriority), HttpPost]
        public async Task<ActionResult<List<Opportunity_ActivityPriorityDTO>>> SingleListActivityPriority([FromBody] Opportunity_ActivityPriorityFilterDTO Opportunity_ActivityPriorityFilterDTO)
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
            ActivityPriorityFilter.Id = Opportunity_ActivityPriorityFilterDTO.Id;
            ActivityPriorityFilter.Code = Opportunity_ActivityPriorityFilterDTO.Code;
            ActivityPriorityFilter.Name = Opportunity_ActivityPriorityFilterDTO.Name;

            List<ActivityPriority> ActivityPriorities = await ActivityPriorityService.List(ActivityPriorityFilter);
            List<Opportunity_ActivityPriorityDTO> Opportunity_ActivityPriorityDTOs = ActivityPriorities
                .Select(x => new Opportunity_ActivityPriorityDTO(x)).ToList();
            return Opportunity_ActivityPriorityDTOs;
        }
        [Route(OpportunityRoute.SingleListDistrict), HttpPost]
        public async Task<ActionResult<List<Opportunity_DistrictDTO>>> SingleListDistrict([FromBody] Opportunity_DistrictFilterDTO Opportunity_DistrictFilterDTO)
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
            DistrictFilter.Id = Opportunity_DistrictFilterDTO.Id;
            DistrictFilter.Code = Opportunity_DistrictFilterDTO.Code;
            DistrictFilter.Name = Opportunity_DistrictFilterDTO.Name;
            DistrictFilter.Priority = Opportunity_DistrictFilterDTO.Priority;
            DistrictFilter.ProvinceId = Opportunity_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<Opportunity_DistrictDTO> Opportunity_DistrictDTOs = Districts
                .Select(x => new Opportunity_DistrictDTO(x)).ToList();
            return Opportunity_DistrictDTOs;
        }
        [Route(OpportunityRoute.SingleListProfession), HttpPost]
        public async Task<ActionResult<List<Opportunity_ProfessionDTO>>> SingleListProfession([FromBody] Opportunity_ProfessionFilterDTO Opportunity_ProfessionFilterDTO)
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
            ProfessionFilter.Id = Opportunity_ProfessionFilterDTO.Id;
            ProfessionFilter.Code = Opportunity_ProfessionFilterDTO.Code;
            ProfessionFilter.Name = Opportunity_ProfessionFilterDTO.Name;

            List<Profession> Professions = await ProfessionService.List(ProfessionFilter);
            List<Opportunity_ProfessionDTO> Opportunity_ProfessionDTOs = Professions
                .Select(x => new Opportunity_ProfessionDTO(x)).ToList();
            return Opportunity_ProfessionDTOs;
        }
        [Route(OpportunityRoute.SingleListProvince), HttpPost]
        public async Task<ActionResult<List<Opportunity_ProvinceDTO>>> SingleListProvince([FromBody] Opportunity_ProvinceFilterDTO Opportunity_ProvinceFilterDTO)
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
            ProvinceFilter.Id = Opportunity_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = Opportunity_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = Opportunity_ProvinceFilterDTO.Name;
            ProvinceFilter.Priority = Opportunity_ProvinceFilterDTO.Priority;
            ProvinceFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<Opportunity_ProvinceDTO> Opportunity_ProvinceDTOs = Provinces
                .Select(x => new Opportunity_ProvinceDTO(x)).ToList();
            return Opportunity_ProvinceDTOs;
        }
        [Route(OpportunityRoute.SingleListAppUser), HttpPost]
        public async Task<ActionResult<List<Opportunity_AppUserDTO>>> SingleListAppUser([FromBody] Opportunity_AppUserFilterDTO Opportunity_AppUserFilterDTO)
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
            AppUserFilter.Id = Opportunity_AppUserFilterDTO.Id;
            AppUserFilter.Username = Opportunity_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = Opportunity_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = Opportunity_AppUserFilterDTO.Address;
            AppUserFilter.Email = Opportunity_AppUserFilterDTO.Email;
            AppUserFilter.Phone = Opportunity_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = Opportunity_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = Opportunity_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = Opportunity_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = Opportunity_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = Opportunity_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = Opportunity_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = Opportunity_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = Opportunity_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = Opportunity_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<Opportunity_AppUserDTO> Opportunity_AppUserDTOs = AppUsers
                .Select(x => new Opportunity_AppUserDTO(x)).ToList();
            return Opportunity_AppUserDTOs;
        }
        [Route(OpportunityRoute.SingleListProduct), HttpPost]
        public async Task<ActionResult<List<Opportunity_ProductDTO>>> SingleListProduct([FromBody] Opportunity_ProductFilterDTO Opportunity_ProductFilterDTO)
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
            ProductFilter.Id = Opportunity_ProductFilterDTO.Id;
            ProductFilter.Name = Opportunity_ProductFilterDTO.Name;
            ProductFilter.Code = Opportunity_ProductFilterDTO.Code;
            ProductFilter.SupplierId = Opportunity_ProductFilterDTO.SupplierId;
            ProductFilter.ProductTypeId = Opportunity_ProductFilterDTO.ProductTypeId;

            List<Product> Products = await ProductService.List(ProductFilter);
            List<Opportunity_ProductDTO> Opportunity_ProductDTOs = Products
                .Select(x => new Opportunity_ProductDTO(x)).ToList();
            return Opportunity_ProductDTOs;
        }
        [Route(OpportunityRoute.SingleListProductGrouping), HttpPost]
        public async Task<ActionResult<List<Opportunity_ProductGroupingDTO>>> SingleListProductGrouping([FromBody] Opportunity_ProductGroupingFilterDTO Opportunity_ProductGroupingFilterDTO)
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
            List<Opportunity_ProductGroupingDTO> Opportunity_ProductGroupingDTOs = ProductGroupings
                .Select(x => new Opportunity_ProductGroupingDTO(x)).ToList();
            return Opportunity_ProductGroupingDTOs;
        }
        [Route(OpportunityRoute.SingleListProductType), HttpPost]
        public async Task<ActionResult<List<Opportunity_ProductTypeDTO>>> SingleListProductType([FromBody] Opportunity_ProductTypeFilterDTO Opportunity_ProductTypeFilterDTO)
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
            ProductTypeFilter.Id = Opportunity_ProductTypeFilterDTO.Id;
            ProductTypeFilter.Code = Opportunity_ProductTypeFilterDTO.Code;
            ProductTypeFilter.Name = Opportunity_ProductTypeFilterDTO.Name;
            ProductTypeFilter.Description = Opportunity_ProductTypeFilterDTO.Description;
            ProductTypeFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            if (ProductTypeFilter.Id == null) ProductTypeFilter.Id = new IdFilter();
            ProductTypeFilter.Id.In = await FilterProductType(ProductTypeService, CurrentContext);

            List<ProductType> ProductTypes = await ProductTypeService.List(ProductTypeFilter);
            List<Opportunity_ProductTypeDTO> Opportunity_ProductTypeDTOs = ProductTypes
                .Select(x => new Opportunity_ProductTypeDTO(x)).ToList();
            return Opportunity_ProductTypeDTOs;
        }
        [Route(OpportunityRoute.SingleListSupplier), HttpPost]
        public async Task<ActionResult<List<Opportunity_SupplierDTO>>> SingleListSupplier([FromBody] Opportunity_SupplierFilterDTO Opportunity_SupplierFilterDTO)
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
            SupplierFilter.Id = Opportunity_SupplierFilterDTO.Id;
            SupplierFilter.Code = Opportunity_SupplierFilterDTO.Code;
            SupplierFilter.Name = Opportunity_SupplierFilterDTO.Name;
            SupplierFilter.TaxCode = Opportunity_SupplierFilterDTO.TaxCode;
            SupplierFilter.Phone = Opportunity_SupplierFilterDTO.Phone;
            SupplierFilter.Email = Opportunity_SupplierFilterDTO.Email;
            SupplierFilter.Address = Opportunity_SupplierFilterDTO.Address;
            SupplierFilter.ProvinceId = Opportunity_SupplierFilterDTO.ProvinceId;
            SupplierFilter.DistrictId = Opportunity_SupplierFilterDTO.DistrictId;
            SupplierFilter.WardId = Opportunity_SupplierFilterDTO.WardId;
            SupplierFilter.OwnerName = Opportunity_SupplierFilterDTO.OwnerName;
            SupplierFilter.PersonInChargeId = Opportunity_SupplierFilterDTO.PersonInChargeId;
            SupplierFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            SupplierFilter.Description = Opportunity_SupplierFilterDTO.Description;

            List<Supplier> Suppliers = await SupplierService.List(SupplierFilter);
            List<Opportunity_SupplierDTO> Opportunity_SupplierDTOs = Suppliers
                .Select(x => new Opportunity_SupplierDTO(x)).ToList();
            return Opportunity_SupplierDTOs;
        }
        [Route(OpportunityRoute.SingleListCompanyStatus), HttpPost]
        public async Task<ActionResult<List<Opportunity_CompanyStatusDTO>>> SingleListCompanyStatus([FromBody] Opportunity_CompanyStatusFilterDTO Opportunity_CompanyStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyStatusFilter CompanyStatusFilter = new CompanyStatusFilter();
            CompanyStatusFilter.Skip = 0;
            CompanyStatusFilter.Take = 20;
            CompanyStatusFilter.OrderBy = Opportunity_CompanyStatusFilterDTO.OrderBy;
            CompanyStatusFilter.OrderType = Opportunity_CompanyStatusFilterDTO.OrderType;
            CompanyStatusFilter.Selects = CompanyStatusSelect.ALL;
            CompanyStatusFilter.Id = Opportunity_CompanyStatusFilterDTO.Id;
            CompanyStatusFilter.Code = Opportunity_CompanyStatusFilterDTO.Code;
            CompanyStatusFilter.Name = Opportunity_CompanyStatusFilterDTO.Name;

            List<CompanyStatus> CompanyStatuses = await CompanyStatusService.List(CompanyStatusFilter);
            List<Opportunity_CompanyStatusDTO> Opportunity_CompanyStatusDTOs = CompanyStatuses
                .Select(x => new Opportunity_CompanyStatusDTO(x)).ToList();
            return Opportunity_CompanyStatusDTOs;
        }
        [Route(OpportunityRoute.SingleListSex), HttpPost]
        public async Task<ActionResult<List<Opportunity_SexDTO>>> SingleListSex([FromBody] Opportunity_SexFilterDTO Opportunity_SexFilterDTO)
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
            SexFilter.Id = Opportunity_SexFilterDTO.Id;
            SexFilter.Code = Opportunity_SexFilterDTO.Code;
            SexFilter.Name = Opportunity_SexFilterDTO.Name;

            List<Sex> Sexes = await SexService.List(SexFilter);
            List<Opportunity_SexDTO> Opportunity_SexDTOs = Sexes
                .Select(x => new Opportunity_SexDTO(x)).ToList();
            return Opportunity_SexDTOs;
        }
        [Route(OpportunityRoute.SingleListNation), HttpPost]
        public async Task<ActionResult<List<Opportunity_NationDTO>>> SingleListNation([FromBody] Opportunity_NationFilterDTO Opportunity_NationFilterDTO)
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
            NationFilter.Id = Opportunity_NationFilterDTO.Id;
            NationFilter.Code = Opportunity_NationFilterDTO.Code;
            NationFilter.Name = Opportunity_NationFilterDTO.Name;
            NationFilter.Priority = Opportunity_NationFilterDTO.DisplayOrder;
            NationFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Nation> Nations = await NationService.List(NationFilter);
            List<Opportunity_NationDTO> Opportunity_NationDTOs = Nations
                .Select(x => new Opportunity_NationDTO(x)).ToList();
            return Opportunity_NationDTOs;
        }
        [Route(OpportunityRoute.SingleListContact), HttpPost]
        public async Task<ActionResult<List<Opportunity_ContactDTO>>> SingleListContact([FromBody] Opportunity_ContactFilterDTO Opportunity_ContactFilterDTO)
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
            ContactFilter.Id = Opportunity_ContactFilterDTO.Id;
            ContactFilter.Name = Opportunity_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = Opportunity_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = Opportunity_ContactFilterDTO.CompanyId;
            ContactFilter.ImageId = Opportunity_ContactFilterDTO.ImageId;
            ContactFilter.Description = Opportunity_ContactFilterDTO.Description;
            ContactFilter.Address = Opportunity_ContactFilterDTO.Address;
            ContactFilter.EmailOther = Opportunity_ContactFilterDTO.EmailOther;
            ContactFilter.DateOfBirth = Opportunity_ContactFilterDTO.DateOfBirth;
            ContactFilter.Phone = Opportunity_ContactFilterDTO.Phone;
            ContactFilter.PhoneHome = Opportunity_ContactFilterDTO.PhoneHome;
            ContactFilter.FAX = Opportunity_ContactFilterDTO.FAX;
            ContactFilter.Email = Opportunity_ContactFilterDTO.Email;

            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<Opportunity_ContactDTO> Opportunity_ContactDTOs = Contacts
                .Select(x => new Opportunity_ContactDTO(x)).ToList();
            return Opportunity_ContactDTOs;
        }
        [Route(OpportunityRoute.SingleListCompany), HttpPost]
        public async Task<ActionResult<List<Opportunity_CompanyDTO>>> SingleListCompany([FromBody] Opportunity_CompanyFilterDTO Opportunity_CompanyFilterDTO)
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
            CompanyFilter.Id = Opportunity_CompanyFilterDTO.Id;
            CompanyFilter.Name = Opportunity_CompanyFilterDTO.Name;
            CompanyFilter.Phone = Opportunity_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = Opportunity_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = Opportunity_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = Opportunity_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = Opportunity_CompanyFilterDTO.EmailOther;

            List<Company> Companys = await CompanyService.List(CompanyFilter);
            List<Opportunity_CompanyDTO> Opportunity_CompanyDTOs = Companys
                .Select(x => new Opportunity_CompanyDTO(x)).ToList();
            return Opportunity_CompanyDTOs;
        }
        [Route(OpportunityRoute.SingleListOpportunity), HttpPost]
        public async Task<ActionResult<List<Opportunity_OpportunityDTO>>> SingleListOpportunity([FromBody] Opportunity_OpportunityFilterDTO Opportunity_OpportunityFilterDTO)
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
            OpportunityFilter.Id = Opportunity_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = Opportunity_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = Opportunity_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.CustomerLeadId = Opportunity_OpportunityFilterDTO.CustomerLeadId;
            OpportunityFilter.ClosingDate = Opportunity_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = Opportunity_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = Opportunity_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = Opportunity_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = Opportunity_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.AppUserId = Opportunity_OpportunityFilterDTO.AppUserId;
            OpportunityFilter.Amount = Opportunity_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = Opportunity_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = Opportunity_OpportunityFilterDTO.Description;
            OpportunityFilter.CreatorId = Opportunity_OpportunityFilterDTO.CreatorId;
            OpportunityFilter.CustomerLeadId = Opportunity_OpportunityFilterDTO.CustomerLeadId;

            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<Opportunity_OpportunityDTO> Opportunity_OpportunityDTOs = Opportunities
                .Select(x => new Opportunity_OpportunityDTO(x)).ToList();
            return Opportunity_OpportunityDTOs;
        }
        [Route(OpportunityRoute.SingleListUnitOfMeasure), HttpPost]
        public async Task<ActionResult<List<Opportunity_UnitOfMeasureDTO>>> SingleListUnitOfMeasure([FromBody] Opportunity_UnitOfMeasureFilterDTO Opportunity_UnitOfMeasureFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            //TODO cần optimize lại phần này, sử dụng itemId thay vì productId

            List<Product> Products = await ProductService.List(new ProductFilter
            {
                Id = Opportunity_UnitOfMeasureFilterDTO.ProductId,
                Selects = ProductSelect.Id,
            });
            long ProductId = Products.Select(p => p.Id).FirstOrDefault();
            Product Product = await ProductService.Get(ProductId);

            List<Opportunity_UnitOfMeasureDTO> Opportunity_UnitOfMeasureDTOs = new List<Opportunity_UnitOfMeasureDTO>();
            if (Product.UnitOfMeasureGrouping != null)
            {
                Opportunity_UnitOfMeasureDTOs = Product.UnitOfMeasureGrouping.UnitOfMeasureGroupingContents.Select(x => new Opportunity_UnitOfMeasureDTO(x)).ToList();
            }
            Opportunity_UnitOfMeasureDTO Opportunity_UnitOfMeasureDTO = new Opportunity_UnitOfMeasureDTO
            {
                Id = Product.UnitOfMeasure.Id,
                Code = Product.UnitOfMeasure.Code,
                Name = Product.UnitOfMeasure.Name,
                Description = Product.UnitOfMeasure.Description,
                StatusId = StatusEnum.ACTIVE.Id,
                Factor = 1,
            };
            Opportunity_UnitOfMeasureDTOs.Add(Opportunity_UnitOfMeasureDTO);
            Opportunity_UnitOfMeasureDTOs = Opportunity_UnitOfMeasureDTOs.Distinct().ToList();
            return Opportunity_UnitOfMeasureDTOs;
        }
        [Route(OpportunityRoute.SingleListItem), HttpPost]
        public async Task<ActionResult<List<Opportunity_ItemDTO>>> SingleListItem([FromBody] Opportunity_ItemFilterDTO Opportunity_ItemFilterDTO)
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
            ItemFilter.Id = Opportunity_ItemFilterDTO.Id;
            ItemFilter.ProductId = Opportunity_ItemFilterDTO.ProductId;
            ItemFilter.Code = Opportunity_ItemFilterDTO.Code;
            ItemFilter.Name = Opportunity_ItemFilterDTO.Name;
            ItemFilter.ScanCode = Opportunity_ItemFilterDTO.ScanCode;
            ItemFilter.SalePrice = Opportunity_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = Opportunity_ItemFilterDTO.RetailPrice;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<Opportunity_ItemDTO> Opportunity_ItemDTOs = Items
                .Select(x => new Opportunity_ItemDTO(x)).ToList();
            return Opportunity_ItemDTOs;
        }
        [Route(OpportunityRoute.SingleListProbability), HttpPost]
        public async Task<ActionResult<List<Opportunity_ProbabilityDTO>>> SingleListProbability([FromBody] Opportunity_ProbabilityFilterDTO Opportunity_ProbabilityFilterDTO)
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
            ProbabilityFilter.Id = Opportunity_ProbabilityFilterDTO.Id;
            ProbabilityFilter.Code = Opportunity_ProbabilityFilterDTO.Code;
            ProbabilityFilter.Name = Opportunity_ProbabilityFilterDTO.Name;

            List<Probability> Probabilities = await ProbabilityService.List(ProbabilityFilter);
            List<Opportunity_ProbabilityDTO> Opportunity_ProbabilityDTOs = Probabilities
                .Select(x => new Opportunity_ProbabilityDTO(x)).ToList();
            return Opportunity_ProbabilityDTOs;
        }
        [Route(OpportunityRoute.SingleListFileType), HttpPost]
        public async Task<ActionResult<List<Opportunity_FileTypeDTO>>> SingleListFileType([FromBody] Opportunity_FileTypeFilterDTO Opportunity_FileTypeFilterDTO)
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
            FileTypeFilter.Id = Opportunity_FileTypeFilterDTO.Id;
            FileTypeFilter.Code = Opportunity_FileTypeFilterDTO.Code;
            FileTypeFilter.Name = Opportunity_FileTypeFilterDTO.Name;

            List<FileType> FileTypes = await FileTypeService.List(FileTypeFilter);
            List<Opportunity_FileTypeDTO> Opportunity_FileTypeDTOs = FileTypes
                .Select(x => new Opportunity_FileTypeDTO(x)).ToList();
            return Opportunity_FileTypeDTOs;
        }
        [Route(OpportunityRoute.SingleListPotentialResult), HttpPost]
        public async Task<ActionResult<List<Opportunity_PotentialResultDTO>>> SingleListPotentialResult([FromBody] Opportunity_PotentialResultFilterDTO Opportunity_PotentialResultFilterDTO)
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
            PotentialResultFilter.Id = Opportunity_PotentialResultFilterDTO.Id;
            PotentialResultFilter.Code = Opportunity_PotentialResultFilterDTO.Code;
            PotentialResultFilter.Name = Opportunity_PotentialResultFilterDTO.Name;

            List<PotentialResult> PotentialResults = await PotentialResultService.List(PotentialResultFilter);
            List<Opportunity_PotentialResultDTO> Opportunity_PotentialResultDTOs = PotentialResults
                .Select(x => new Opportunity_PotentialResultDTO(x)).ToList();
            return Opportunity_PotentialResultDTOs;
        }
        [Route(OpportunityRoute.SingleListSaleStage), HttpPost]
        public async Task<ActionResult<List<Opportunity_SaleStageDTO>>> SingleListSaleStage([FromBody] Opportunity_SaleStageFilterDTO Opportunity_SaleStageFilterDTO)
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
            SaleStageFilter.Id = Opportunity_SaleStageFilterDTO.Id;
            SaleStageFilter.Code = Opportunity_SaleStageFilterDTO.Code;
            SaleStageFilter.Name = Opportunity_SaleStageFilterDTO.Name;

            List<SaleStage> SaleStages = await SaleStageService.List(SaleStageFilter);
            List<Opportunity_SaleStageDTO> Opportunity_SaleStageDTOs = SaleStages
                .Select(x => new Opportunity_SaleStageDTO(x)).ToList();
            return Opportunity_SaleStageDTOs;
        }
        [Route(OpportunityRoute.SingleListCurrency), HttpPost]
        public async Task<ActionResult<List<Opportunity_CurrencyDTO>>> SingleListCurrency([FromBody] Opportunity_CurrencyFilterDTO Opportunity_CurrencyFilterDTO)
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
            CurrencyFilter.Id = Opportunity_CurrencyFilterDTO.Id;
            CurrencyFilter.Code = Opportunity_CurrencyFilterDTO.Code;
            CurrencyFilter.Name = Opportunity_CurrencyFilterDTO.Name;

            List<Currency> Currencies = await CurrencyService.List(CurrencyFilter);
            List<Opportunity_CurrencyDTO> Opportunity_CurrencyDTOs = Currencies
                .Select(x => new Opportunity_CurrencyDTO(x)).ToList();
            return Opportunity_CurrencyDTOs;
        }
        [Route(OpportunityRoute.SingleListEditedPriceStatus), HttpPost]
        public async Task<ActionResult<List<Opportunity_EditedPriceStatusDTO>>> SingleListEditedPriceStatus([FromBody] Opportunity_EditedPriceStatusFilterDTO Opportunity_EditedPriceStatusFilterDTO)
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
            EditedPriceStatusFilter.Id = Opportunity_EditedPriceStatusFilterDTO.Id;
            EditedPriceStatusFilter.Code = Opportunity_EditedPriceStatusFilterDTO.Code;
            EditedPriceStatusFilter.Name = Opportunity_EditedPriceStatusFilterDTO.Name;

            List<EditedPriceStatus> EditedPriceStatuses = await EditedPriceStatusService.List(EditedPriceStatusFilter);
            List<Opportunity_EditedPriceStatusDTO> Opportunity_EditedPriceStatusDTOs = EditedPriceStatuses
                .Select(x => new Opportunity_EditedPriceStatusDTO(x)).ToList();
            return Opportunity_EditedPriceStatusDTOs;
        }
        [Route(OpportunityRoute.SingleListOrderQuoteStatus), HttpPost]
        public async Task<ActionResult<List<Opportunity_OrderQuoteStatusDTO>>> SingleListOrderQuoteStatus([FromBody] Opportunity_OrderQuoteStatusFilterDTO Opportunity_OrderQuoteStatusFilterDTO)
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
            OrderQuoteStatusFilter.Id = Opportunity_OrderQuoteStatusFilterDTO.Id;
            OrderQuoteStatusFilter.Code = Opportunity_OrderQuoteStatusFilterDTO.Code;
            OrderQuoteStatusFilter.Name = Opportunity_OrderQuoteStatusFilterDTO.Name;

            List<OrderQuoteStatus> OrderQuoteStatuses = await OrderQuoteStatusService.List(OrderQuoteStatusFilter);
            List<Opportunity_OrderQuoteStatusDTO> Opportunity_OrderQuoteStatusDTOs = OrderQuoteStatuses
                .Select(x => new Opportunity_OrderQuoteStatusDTO(x)).ToList();
            return Opportunity_OrderQuoteStatusDTOs;
        }
        [Route(OpportunityRoute.SingleListTaxType), HttpPost]
        public async Task<ActionResult<List<Opportunity_TaxTypeDTO>>> SingleListTaxType([FromBody] Opportunity_TaxTypeFilterDTO IndirectSalesOrder_TaxTypeFilterDTO)
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
            TaxTypeFilter.Id = IndirectSalesOrder_TaxTypeFilterDTO.Id;
            TaxTypeFilter.Code = IndirectSalesOrder_TaxTypeFilterDTO.Code;
            TaxTypeFilter.Name = IndirectSalesOrder_TaxTypeFilterDTO.Name;
            TaxTypeFilter.StatusId = new IdFilter { Equal = Enums.StatusEnum.ACTIVE.Id };

            List<TaxType> TaxTypes = await TaxTypeService.List(TaxTypeFilter);
            List<Opportunity_TaxTypeDTO> Opportunity_TaxTypeDTOs = TaxTypes
                .Select(x => new Opportunity_TaxTypeDTO(x)).ToList();
            return Opportunity_TaxTypeDTOs;
        }
        [Route(OpportunityRoute.SingleListPosition), HttpPost]
        public async Task<ActionResult<List<Opportunity_PositionDTO>>> SingleListPosition([FromBody] Opportunity_PositionFilterDTO Opportunity_PositionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            PositionFilter PositionFilter = new PositionFilter();
            PositionFilter.Skip = 0;
            PositionFilter.Take = 99999;
            PositionFilter.OrderBy = PositionOrder.Id;
            PositionFilter.OrderType = OrderType.ASC;
            PositionFilter.Selects = PositionSelect.ALL;
            PositionFilter.Id = Opportunity_PositionFilterDTO.Id;
            PositionFilter.Code = Opportunity_PositionFilterDTO.Code;
            PositionFilter.Name = Opportunity_PositionFilterDTO.Name;
            PositionFilter.StatusId = new IdFilter { Equal = Enums.StatusEnum.ACTIVE.Id };

            List<Position> Positions = await PositionService.List(PositionFilter);
            List<Opportunity_PositionDTO> Opportunity_PositionDTOs = Positions
                .Select(x => new Opportunity_PositionDTO(x)).ToList();
            return Opportunity_PositionDTOs;
        }
        [Route(OpportunityRoute.SingleListOpportunityResultType), HttpPost]
        public async Task<ActionResult<List<Opportunity_OpportunityResultTypeDTO>>> SingleListOpportunityResultType([FromBody] Opportunity_OpportunityResultTypeFilterDTO Opportunity_OpportunityResultTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityResultTypeFilter OpportunityResultTypeFilter = new OpportunityResultTypeFilter();
            OpportunityResultTypeFilter.Skip = 0;
            OpportunityResultTypeFilter.Take = 20;
            OpportunityResultTypeFilter.OrderBy = OpportunityResultTypeOrder.Id;
            OpportunityResultTypeFilter.OrderType = OrderType.ASC;
            OpportunityResultTypeFilter.Selects = OpportunityResultTypeSelect.ALL;
            OpportunityResultTypeFilter.Id = Opportunity_OpportunityResultTypeFilterDTO.Id;
            OpportunityResultTypeFilter.Code = Opportunity_OpportunityResultTypeFilterDTO.Code;
            OpportunityResultTypeFilter.Name = Opportunity_OpportunityResultTypeFilterDTO.Name;

            List<OpportunityResultType> OpportunityResultTypes = await OpportunityResultTypeService.List(OpportunityResultTypeFilter);
            List<Opportunity_OpportunityResultTypeDTO> Opportunity_OpportunityResultTypeDTOs = OpportunityResultTypes
                .Select(x => new Opportunity_OpportunityResultTypeDTO(x)).ToList();
            return Opportunity_OpportunityResultTypeDTOs;
        }
        [Route(OpportunityRoute.SingleListEmailTemplate), HttpPost]
        public async Task<ActionResult<List<Opportunity_MailTemplateDTO>>> SingleListMailTemplate([FromBody] Opportunity_MailTemplateFilterDTO Opportunity_MailTemplateFilterDTO)
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
            MailTemplateFilter.Id = Opportunity_MailTemplateFilterDTO.Id;
            MailTemplateFilter.Code = Opportunity_MailTemplateFilterDTO.Code;
            MailTemplateFilter.Name = Opportunity_MailTemplateFilterDTO.Name;
            MailTemplateFilter.Content = Opportunity_MailTemplateFilterDTO.Content;

            List<MailTemplate> MailTemplates = await MailTemplateService.List(MailTemplateFilter);
            List<Opportunity_MailTemplateDTO> Opportunity_MailTemplateDTOs = MailTemplates
                .Select(x => new Opportunity_MailTemplateDTO(x)).ToList();
            return Opportunity_MailTemplateDTOs;
        }
    }
}


